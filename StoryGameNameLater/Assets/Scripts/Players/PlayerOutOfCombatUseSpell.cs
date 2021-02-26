using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutOfCombatUseSpell : MonoBehaviour
{
    public string attack1;
    public string attack2;
    public string attack3;

    public GameObject inCombatState;
    public GameObject _CM;
    public Combat _CM_CS;

    public Vector3 velocity;
    public float jumpHeight = 2000f;
    public float gravity = -10f;
    public CharacterController controller;
    public MovementScript _MS;

    public GameObject fireballIN;
    public GameObject fireballOB;

    public float aCheckLeg;
    public float bCheckLeg;
    public float distanceToPlayerSquared;
    public float distanceToPlayer;
    public float[] pauseableDistance;

    // Start is called before the first frame update
    void Start()
    {
        inCombatState = GameObject.Find("BigPlayer");
        _CM = GameObject.Find("CombatManager");
        _CM_CS = _CM.GetComponent<Combat>();
        GetSpells();
        _MS = gameObject.GetComponent<MovementScript>();
    }
    public void GetSpells()
    {
        attack1 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack1;
        attack2 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack2;
        attack3 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack3;
    }
    public void NoPls()
    {
        GameObject[] testa = GameObject.FindGameObjectsWithTag("PauseWhenInCombat");
        pauseableDistance = new float[testa.Length];
        int i = 0;
        foreach(GameObject teste in testa)
        {
            aCheckLeg = teste.gameObject.transform.position.x - gameObject.transform.position.x;
            bCheckLeg = teste.gameObject.transform.position.z - gameObject.transform.position.z;
            distanceToPlayerSquared = Mathf.Pow(aCheckLeg, 2) + Mathf.Pow(bCheckLeg, 2);
            distanceToPlayer = Mathf.Sqrt(distanceToPlayerSquared);
            pauseableDistance[i] = distanceToPlayer;
            i++;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(_CM_CS.isinCombat != true)
        {
            if (attack3 == "Fly")
            {
                if (Input.GetKey("3"))
                {
                    velocity.y = jumpHeight;
                    controller.Move(velocity);
                }
            }
            else if (attack3 == "Lav")
            {
                if (Input.GetKeyDown("3") && _MS.isGrounded == true)
                {
                    controller.enabled = false;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 3, gameObject.transform.position.z);
                    controller.enabled = true;
                }
                else if (Input.GetKey("3") && _MS.isGrounded == false)
                {
                    controller.enabled = false;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    controller.enabled = true;
                }
            }
            else if (attack3 == "Fireball")
            {
                if (Input.GetKeyDown("3"))
                {
                    fireballOB = Instantiate(fireballIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
                    fireballOB.transform.localPosition = new Vector3(0, 1, 1);
                    fireballOB.transform.rotation = gameObject.transform.rotation;
                    fireballOB.transform.parent = gameObject.transform.parent;
                    fireballOB.GetComponent<Fireball>().MoveFireball();
                    fireballOB.GetComponent<Fireball>().InvokeRepeating("DestroyFireball", 1f, 1);
                }
            }
            else if (attack3 == "NoPls")
            {
                if (Input.GetKey("3"))
                {
                    NoPls();
                    GameObject[] testa = GameObject.FindGameObjectsWithTag("PauseWhenInCombat");
                    int i = 0;
                    foreach (GameObject teste in testa)
                    {
                        
                        if(pauseableDistance[i] <= 8)
                        {
                            Debug.Log(pauseableDistance[i]);
                            testa[i].GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 100);
                        }
                        i++;
                    }
                }
            }
        }
    }
}
