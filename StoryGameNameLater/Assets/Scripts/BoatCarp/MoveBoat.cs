﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoat : MonoBehaviour
{
    public GameObject boatInterface;
    public GameObject player;
    public Rigidbody _BoatRB;

    public bool isinboat = false;
    public MouseLook _MLS;

    public float health = 1000;

    public GameObject boatHealth;
    public GameObject playerHealth;

    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        _BoatRB = gameObject.GetComponent<Rigidbody>();
        _MLS = player.GetComponentInChildren<MouseLook>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w") && isinboat == true && isMoving == false)
        {
            InvokeRepeating("MakeBoatMove", 0.01f, 0.01f);
            isMoving = true;
        }
        else if (Input.GetKeyDown("w") && isinboat == true && isMoving == true)
        {
            CancelInvoke("MakeBoatMove");
            isMoving = false;
        }
        if (Input.GetKey("a") || Input.GetKey("d"))
        {
            float rotation = Input.GetAxis("Horizontal");
            gameObject.transform.Rotate(0, rotation, 0);
        }
    }
    public void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            boatInterface.SetActive(true);
        }
        
    }
    public void IfPlayerSaysYes()
    {
        player.transform.parent = gameObject.transform;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.localPosition = new Vector3(0, 2.5f, -1);
        player.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        player.GetComponent<CharacterController>().enabled = true;
        boatInterface.SetActive(false);
        player.GetComponent<MovementScript>().enabled = false;
        isinboat = true;
        Cursor.lockState = CursorLockMode.Locked;
        _MLS.enabled = true;
        playerHealth.SetActive(false);
        boatHealth.SetActive(true);

        player.GetComponent<CapsuleCollider>().enabled = false;
    }
    public void IfPlayerSaysNo()
    {

    }
    public void MakeBoatMove()
    {
        //_BoatRB.AddRelativeForce(Vector3.forward * 1000000);
        gameObject.transform.position += transform.forward * Time.deltaTime * 1f;
    }
}
