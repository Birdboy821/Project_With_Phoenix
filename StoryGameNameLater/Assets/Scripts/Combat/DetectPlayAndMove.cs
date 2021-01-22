using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayAndMove : MonoBehaviour
{
    public GameObject player;
    public GameObject _CM; //Combat Manager
    public int typeOfEnemy = 0;

    //distants to player math stuff
    public float legA = 0;
    public float legB = 0;
    public float distantSquared = 0;
    public float distantsToPlayer = 0;

    //Movement for enemy
    public float moveChance = 0;
    public Rigidbody _RB;
    public float moveAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        _CM = GameObject.Find("CombatManager");
        InvokeRepeating("MoveMent", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        LookPart();
    }

    private void LookPart()
    {
        legA = gameObject.transform.position.x - player.transform.position.x;
        legB = gameObject.transform.position.z - player.transform.position.z;
        distantSquared = Mathf.Pow(legA, 2) + Mathf.Pow(legB, 2);
        distantsToPlayer = Mathf.Sqrt(distantSquared);
        if (distantsToPlayer <= 10)
            gameObject.transform.LookAt(new Vector3(player.transform.position.x, 1, player.transform.position.z));
    }
    private void MoveMent()
    {
        moveChance = Random.Range(0, 10);
        if (moveChance >= 7)
        {
            moveAmount = Random.Range(1, 21);
            //Debug.Log(moveChance);
            _RB.AddForce(transform.forward * (25 * moveAmount));
        }
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            //Debug.Log("BIRBS!!!!");
            _RB.velocity = new Vector3(0,0,0);
            _CM.GetComponent<Combat>().isinCombat = true;
            _CM.GetComponent<Combat>().inCombatWith = gameObject.name;
            _CM.GetComponent<Combat>().typeThatIsFighting = typeOfEnemy;
        }
    }
    void OnCollisionStay(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            //Debug.Log("BIRBS!!!!");
            _RB.velocity = new Vector3(0, 0, 0);
            _CM.GetComponent<Combat>().isinCombat = true;
            _CM.GetComponent<Combat>().inCombatWith = gameObject.name;
            _CM.GetComponent<Combat>().typeThatIsFighting = typeOfEnemy;
        }
    }
}
