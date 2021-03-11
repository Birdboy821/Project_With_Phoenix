using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Rigidbody fireball_RB;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveFireball()
    {
        fireball_RB.AddRelativeForce(Vector3.left * 5000 * -1);
    }
    public void MoveFireballRight()
    {
        fireball_RB.AddRelativeForce(Vector3.right * 5000 * -1);
    }


    public void OnTriggerEnter(Collider collision)
    {
        DestroyFireball();
        if(collision.CompareTag("Boat"))
        {
            if(collision.name == "BoatTempoi")
            {
                collision.GetComponent<MoveBoat>().health -= damage;
            }
            else
            {
                collision.GetComponent<BadBoatMove>().boatHealth -= damage;
            }
        }
        Debug.Log(collision.name);
    }
    public void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
