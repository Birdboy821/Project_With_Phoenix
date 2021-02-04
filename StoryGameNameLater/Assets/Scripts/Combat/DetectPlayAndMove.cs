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

    //Companions For the Enemies
    public GameObject companionOneUwU = null;
    public GameObject companionTwoUwU = null;
    // Start is called before the first frame update
    public void Start()
    {
        player = GameObject.Find("Player");
        _CM = GameObject.Find("CombatManager");
        InvokeRepeating("MoveMent", 1, 1);
        if (companionOneUwU != null || companionTwoUwU != null)
            Invoke("CompanionCrap", 2);
        
            
    }
    private void CompanionCrap()
    {
        if (companionOneUwU != null)
        {
            companionOneUwU.GetComponent<SphereCollider>().isTrigger = false;
            companionOneUwU.GetComponent<SphereCollider>().enabled = false;
            companionOneUwU.GetComponent<DetectPlayAndMove>().CancelInvoke("MoveMent");
        }
        if (companionTwoUwU != null)
        {
            companionTwoUwU.GetComponent<SphereCollider>().isTrigger = false;
            companionTwoUwU.GetComponent<SphereCollider>().enabled = false;
            companionTwoUwU.GetComponent<DetectPlayAndMove>().CancelInvoke("MoveMent");
        }
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
            _RB.AddForce(transform.forward * (25 * moveAmount));
            if(companionOneUwU != null)
                companionOneUwU.GetComponent<Rigidbody>().AddForce(transform.forward * (25 * moveAmount));
            if (companionTwoUwU != null)
                companionTwoUwU.GetComponent<Rigidbody>().AddForce(transform.forward * (25 * moveAmount));
        }
    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            _RB.velocity = new Vector3(0,0,0);
            _CM.GetComponent<Combat>().isinCombat = true;
            _CM.GetComponent<Combat>().inCombatWith = gameObject.name;
            _CM.GetComponent<Combat>().typeThatIsFighting = typeOfEnemy;;
            if(companionOneUwU != null)
            {
                _CM.GetComponent<Combat>().inCombatWithCompOneUwU = companionOneUwU.name;
                _CM.GetComponent<Combat>().typeThatIsFightingCompOneUwU = companionOneUwU.GetComponent<DetectPlayAndMove>().typeOfEnemy;
            }
            if (companionTwoUwU != null)
            {
                _CM.GetComponent<Combat>().inCombatWithCompTwoUwU = companionTwoUwU.name;
                _CM.GetComponent<Combat>().typeThatIsFightingCompTwoUwU = companionTwoUwU.GetComponent<DetectPlayAndMove>().typeOfEnemy;
            }
        }
    }
    void OnCollisionStay(Collision target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            _RB.velocity = new Vector3(0, 0, 0);
            _CM.GetComponent<Combat>().isinCombat = true;
            _CM.GetComponent<Combat>().inCombatWith = gameObject.name;
            _CM.GetComponent<Combat>().typeThatIsFighting = typeOfEnemy;
            if (companionOneUwU != null)
            {
                _CM.GetComponent<Combat>().inCombatWithCompOneUwU = companionOneUwU.name;
                _CM.GetComponent<Combat>().typeThatIsFightingCompOneUwU = companionOneUwU.GetComponent<DetectPlayAndMove>().typeOfEnemy;
            }
            if (companionTwoUwU != null)
            {
                _CM.GetComponent<Combat>().inCombatWithCompTwoUwU = companionTwoUwU.name;
                _CM.GetComponent<Combat>().typeThatIsFightingCompTwoUwU = companionTwoUwU.GetComponent<DetectPlayAndMove>().typeOfEnemy;
            }
        }
    }
}
