using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    // Combat Status
    public bool isinCombat = false;
    public string inCombatWith = "";
    public GameObject[] pauseable;
    public bool hasChange = false;
    public int typeThatIsFighting = 0;
    public GameObject[] fightOrder;
    public float[] fightOrderTeam; // 0 is players, 1 is enemies

    public GameObject[] allEnemys;

    //Cameras
    public GameObject playerCam;
    public GameObject battleCam;
    
    //PlayerButtons
    public Button[] buttons;
    public float lastButtonPressed = 0;
    public float lastButtonPressedToAttack = 0;

    //Attacking and Defending
    public float damageAmount;
    public int target = 0;
    public float damageBeingDone = 0;
    public bool didAction = false;

    //In combat Stats for team
    public float playerTeamHealth = 0;
    public float enemyTeamHealth = 0;

    int d = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerButtons();
    }

    // Update is called once per frame
    void Update()
    {
        if(isinCombat == true && hasChange == false)
        {
            hasChange = true;
            pauseable = GameObject.FindGameObjectsWithTag("PauseWhenInCombat");
            for(int i = 0; i < pauseable.Length; i++)
            {
                pauseable[i].GetComponent<DetectPlayAndMove>().CancelInvoke("MoveMent");
                pauseable[i].GetComponent<DetectPlayAndMove>().enabled = false;
            }
            playerCam.SetActive(false);
            battleCam.SetActive(true);
            GameObject tempo = Instantiate(allEnemys[typeThatIsFighting], new Vector3(15f, 4f, 92.1f), Quaternion.Euler(0, -45, 0));
            tempo.name = inCombatWith;
            fightOrder[1] = tempo;
            SetPlayerButtons();
            SetTheEnemy();
            CombatFight();
            
        }
    }
    private void SetTheEnemy()
    {
        buttons[3].enabled = true;
        buttons[4].enabled = true;
        buttons[5].enabled = true;
        buttons[3].GetComponentInChildren<Text>().text = fightOrder[1].name;
        if (fightOrder[3] != null)
            buttons[4].GetComponentInChildren<Text>().text = fightOrder[3].name;
        if (fightOrder[5] != null)
            buttons[4].GetComponentInChildren<Text>().text = fightOrder[5].name;
    }
    private void SetPlayerButtons()
    {
        buttons[0].GetComponentInChildren<Text>().text = fightOrder[0].GetComponent<PlayersSpellsAttacks>().attack1;
        buttons[1].GetComponentInChildren<Text>().text = fightOrder[0].GetComponent<PlayersSpellsAttacks>().attack2;
        buttons[2].GetComponentInChildren<Text>().text = fightOrder[0].GetComponent<PlayersSpellsAttacks>().attack3;
    }
    private void CombatFight()
    {
        int i = 0;
        foreach(GameObject tempo in fightOrder)
        {
            if(tempo != null)
            {
                if (i % 2 == 0)
                {
                    playerTeamHealth += tempo.GetComponent<PlayersSpellsAttacks>().health;
                }
                else
                {
                    enemyTeamHealth += tempo.GetComponent<EnemyInfo>().health;
                }
            }
            i++;
            Cursor.lockState = CursorLockMode.None;
            InvokeRepeating("combatTurnBase", 0.01f, 0.5f);
        }
        
    }
    public void ChangeButtonClicked1()
    {
        lastButtonPressed = 1;
    }
    public void ChangeButtonClicked2()
    {
        lastButtonPressed = 2;
    }
    public void ChangeButtonClicked3()
    {
        lastButtonPressed = 3;
    }
    public void ChangeButtonClicked4()
    {
        lastButtonPressedToAttack = 1;
    }
    public void ChangeButtonClicked5()
    {
        lastButtonPressedToAttack = 2;
    }
    public void ChangeButtonClicked6()
    {
        lastButtonPressedToAttack = 3;
    }
    private void combatTurnBase()
    {
        if(playerTeamHealth > 0 && enemyTeamHealth > 0)
        {
            
            if(fightOrder[d] != null)
            {
                if (d % 2 == 0)
                {
                    if(lastButtonPressed == 0)
                    {

                    }
                    else if(lastButtonPressed == 1)
                    {
                        damageAmount = fightOrder[d].GetComponent<PlayersSpellsAttacks>().attack1Damage;
                        InvokeRepeating("Target", 0.01f, 0.5f);
                    }
                    else if (lastButtonPressed == 2)
                    {
                        damageAmount = fightOrder[d].GetComponent<PlayersSpellsAttacks>().attack2Damage;
                        InvokeRepeating("Target", 0.01f, 0.5f);
                    }
                    else if (lastButtonPressed == 3)
                    {
                        damageAmount = fightOrder[d].GetComponent<PlayersSpellsAttacks>().attack3Damage;
                        InvokeRepeating("Target", 0.01f, 0.5f);

                    }
                }
                else
                {
                    Debug.Log(damageAmount);
                }
            }
        }
        else
        {
            return;
        }
    }
    private void Target()
    {
        if (lastButtonPressedToAttack == 0)
        {

        }
        else if(lastButtonPressedToAttack == 1 && didAction == false)
        {
            d++;
            target = 1;
            didAction = true;
            DoDamage();
        }
        else if (lastButtonPressedToAttack == 2 && didAction == false)
        {
            d++;
            target = 3;
            didAction = true;
            DoDamage();
        }
        else if (lastButtonPressedToAttack == 3 && didAction == false)
        {
            d++;
            target = 5;
            didAction = true;
            DoDamage();
        }
    }
    private void DoDamage()
    {
        damageBeingDone = damageAmount - fightOrder[target].GetComponent<EnemyInfo>().armor;
        fightOrder[target].GetComponent<EnemyInfo>().health -= damageBeingDone;
        CancelInvoke("Target");
    }
}
