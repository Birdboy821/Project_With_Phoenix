using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSpellsAttacks : MonoBehaviour
{
    public float health = 100;
    public float armor = 20;
    public float mana = 100;

    public string attack1Type;
    public string attack1AttackType = "Offensive";
    public string attack1;
    public float attack1Damage;

    public string attack2Type;
    public string attack2AttackType = "Defensive";
    public string attack2;
    public float attack2Damage;

    public string attack3Type;
    public string attack3AttackType;
    public string attack3;
    public float attack3Damage;

    public GameObject companionOne;
    public GameObject companionTwo;
    public GameObject companionOneClone;
    public GameObject companionTwoClone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateComplanions()
    {
        GameObject tempest = null;
        Destroy(tempest);
        GameObject tempost = null;
        Destroy(tempost);
        if (companionOne != null)
        {
            companionOneClone = companionOne.transform.GetChild(2).gameObject;
            tempest = Instantiate(companionOneClone, new Vector3(-18f, 4f, 110), Quaternion.Euler(0, 45, 0));
            tempest.transform.rotation = Quaternion.Euler(0, 135, 0);
            companionOneClone = tempest;
        }
        if (companionTwo != null)
        {
            companionTwoClone = companionTwo.transform.GetChild(2).gameObject;
            tempost = Instantiate(companionTwoClone, new Vector3(-18f, 4f, 110), Quaternion.Euler(0, 45, 0));
            tempost.transform.rotation = Quaternion.Euler(0, 135, 0);
            companionTwoClone = tempost;
        }
           
    }
}
