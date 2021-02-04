using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionsPathing : MonoBehaviour
{
    public GameObject player;
    public Rigidbody _RB;

    //distants to player math stuff
    public float legA = 0;
    public float legB = 0;
    public float distantSquared = 0;
    public float distantsToPlayer = 0;

    public Vector3 companionPosotion;
    public int qwerty = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        companionPosotion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        legA = gameObject.transform.position.x - player.transform.position.x;
        legB = gameObject.transform.position.z - player.transform.position.z;
        distantSquared = Mathf.Pow(legA, 2) + Mathf.Pow(legB, 2);
        distantsToPlayer = Mathf.Sqrt(distantSquared);
        if (distantsToPlayer > 5)
            _RB.AddForce(transform.forward * (25));
        else if (distantsToPlayer < 2)
            _RB.AddForce(transform.forward * -25);
        if (Math.Floor(companionPosotion.x*1000) == Math.Floor(transform.position.x*1000) && Math.Floor(companionPosotion.y * 1000) == Math.Floor(transform.position.y * 1000) && Math.Floor(companionPosotion.z * 1000) == Math.Floor(transform.position.z * 1000))
            _RB.AddForce(transform.up * 250);
        qwerty++;
        if (qwerty == 50)
        {
            companionPosotion = transform.position;
            qwerty = 0;
        } 
    }
}
