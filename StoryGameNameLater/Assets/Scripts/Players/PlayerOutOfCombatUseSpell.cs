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
    public float jumpHeight = 5f;
    public float gravity = -10f;
    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        inCombatState = GameObject.Find("BigPlayer");
        _CM = GameObject.Find("CombatManager");
        _CM_CS = _CM.GetComponent<Combat>();
        GetSpells();
    }
    public void GetSpells()
    {
        attack1 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack1;
        attack2 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack2;
        attack3 = inCombatState.GetComponent<PlayersSpellsAttacks>().attack3;
    }

    // Update is called once per frame
    void Update()
    {
        if(_CM_CS.isinCombat != true)
        {
            if(attack3 == "Fly")
            {
                if (Input.GetKey("3"))
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                    controller.Move(velocity * Time.deltaTime);
                    Debug.Log("HELLO");
                }
            }
        }
    }
}
