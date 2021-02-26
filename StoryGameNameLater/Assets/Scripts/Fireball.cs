using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody fireball_RB;
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
        fireball_RB.AddRelativeForce(Vector3.forward * 2000);
    }

    public void OnTriggerEnter(Collider collision)
    {
        DestroyFireball();
    }
    public void DestroyFireball()
    {
        Destroy(gameObject);
    }
}
