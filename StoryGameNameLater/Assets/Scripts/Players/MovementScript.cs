using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Vector3 velocity;
    public bool isGrounded;
    public PlayerOutOfCombatUseSpell _POOCUS;
    public MouseLook _MLS;
    public bool is4pressed = false;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (!Input.GetKey("3") && (isGrounded && velocity.y < 0))
        {
            velocity.y = -2f;
        }
        else if (Input.GetKey("3") && _POOCUS.attack3 == "Fly")
        {
            velocity.y = 1f;
        }
        else if (Input.GetKeyDown("3") && _POOCUS.attack3 == "Lav")
        {
            gravity = 0;
            velocity.y = 0;
        }
        else if (Input.GetKeyUp("3") && _POOCUS.attack3 == "Lav")
        {
            gravity = -10;
        }
        if (gameObject.transform.position.y >= 100)
        {
            controller.enabled = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 100, gameObject.transform.position.z);
            controller.enabled = true;
        }
        if (Input.GetKeyDown("4"))
        {
            if(is4pressed == false)
            {
                _MLS.enabled = false;
                is4pressed = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(is4pressed == true)
            {
                _MLS.enabled = true;
                is4pressed = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }



        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
