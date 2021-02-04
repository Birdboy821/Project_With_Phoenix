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
    public float typeThatIsFightingCompOneUwU = 0.5f;
    public string inCombatWithCompOneUwU = "";
    public float typeThatIsFightingCompTwoUwU = 0.5f;
    public string inCombatWithCompTwoUwU = "";
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
    public int numberOnPlayersTeam = 0; 

    public int d = 0;
    public GameObject player;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetPlayerButtons();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isinCombat == true && hasChange == false)
        {
            i = 0;
            hasChange = true;
            pauseable = GameObject.FindGameObjectsWithTag("PauseWhenInCombat");
            for(int i = 0; i < pauseable.Length; i++)
            {
                pauseable[i].GetComponent<DetectPlayAndMove>().CancelInvoke("MoveMent");
                pauseable[i].GetComponent<DetectPlayAndMove>().enabled = false;
            }
            playerCam.SetActive(false);
            battleCam.SetActive(true);
            GameObject tempe;
            GameObject tempa;
            GameObject tempo = Instantiate(allEnemys[typeThatIsFighting], new Vector3(8, 4f, 99.1f), Quaternion.Euler(0, -45, 0));
            tempo.name = inCombatWith;
            fightOrder[1] = tempo;
            if(typeThatIsFightingCompOneUwU != 0.5)
            {
                tempa = Instantiate(allEnemys[(int)typeThatIsFightingCompOneUwU], new Vector3(8, 4f, 89.1f), Quaternion.Euler(0, -45, 0));
                tempa.name = inCombatWithCompOneUwU;
                fightOrder[3] = tempa;
            }
            if (typeThatIsFightingCompTwoUwU != 0.5)
            {
                tempe = Instantiate(allEnemys[(int)typeThatIsFightingCompTwoUwU], new Vector3(18f, 4f, 99.1f), Quaternion.Euler(0, -45, 0));
                tempe.name = inCombatWithCompTwoUwU;
                fightOrder[5] = tempe;
            }
            
            
            
            SetTheEnemy();
            CombatFight();
            fightOrder[0].GetComponent<PlayersSpellsAttacks>().companionOne = player.GetComponent<PlayersTeam>().companionOne;
            fightOrder[0].GetComponent<PlayersSpellsAttacks>().companionTwo = player.GetComponent<PlayersTeam>().companionTwo;
            fightOrder[0].GetComponent<PlayersSpellsAttacks>().CreateComplanions();

            if (player.GetComponent<PlayersTeam>().companionOne != null)
                fightOrder[2] = fightOrder[0].GetComponent<PlayersSpellsAttacks>().companionOneClone;
            if (player.GetComponent<PlayersTeam>().companionTwo != null)
                fightOrder[4] = fightOrder[0].GetComponent<PlayersSpellsAttacks>().companionTwoClone;
            SetPlayerButtons();
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
            buttons[5].GetComponentInChildren<Text>().text = fightOrder[5].name;
    }
    private void SetPlayerButtons()
    {
        if(fightOrder[i] != null)
        {
            buttons[0].GetComponentInChildren<Text>().text = fightOrder[i].GetComponent<PlayersSpellsAttacks>().attack1;
            buttons[1].GetComponentInChildren<Text>().text = fightOrder[i].GetComponent<PlayersSpellsAttacks>().attack2;
            buttons[2].GetComponentInChildren<Text>().text = fightOrder[i].GetComponent<PlayersSpellsAttacks>().attack3;
            
        }
        Debug.Log(fightOrder[i]);
        i += 2;

        if (i < 6 && fightOrder[i] == null)
            i += 2;
        if (i >= 6)
            i = 0;
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
                    numberOnPlayersTeam++;
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
                    float tempe = Random.Range(0, 3) + 1;
                    if(tempe == 2)
                    {

                    }
                    else if(tempe == 1 || tempe == 3)
                    {
                        float tempa = Random.Range(0, numberOnPlayersTeam) + 1;
                        if(tempa == 1)
                        {
                            if(tempe == 1)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack1Damage;
                            }
                            else if (tempe == 3)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack3Damage;
                            }
                            target = 0;
                            EnemyAttackRound();
                        }
                        else if (tempa == 2)
                        {
                            if (tempe == 1)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack1Damage;
                            }
                            else if (tempe == 3)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack3Damage;
                            }
                            target = 2;
                            EnemyAttackRound();
                        }
                        else if (tempa == 3)
                        {
                            if (tempe == 1)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack1Damage;
                            }
                            else if (tempe == 3)
                            {
                                damageAmount = fightOrder[d].GetComponent<EnemyInfo>().attack3Damage;
                            }
                            target = 4;
                            EnemyAttackRound();
                        }
                    }
                }
            }
            else if (fightOrder[d] == null)
            {
                d++;
                
            }
            if (d >= 6)
            {
                d = 0;
            }
        }
        else
        {
            CancelInvoke("combatTurnBase");
            DieDieDieDie();
        }
    }
    private void EnemyAttackRound()
    {
        damageBeingDone = damageAmount / fightOrder[target].GetComponent<PlayersSpellsAttacks>().armor;
        fightOrder[target].GetComponent<PlayersSpellsAttacks>().health -= damageBeingDone;
        playerTeamHealth -= damageBeingDone;
        if(fightOrder[target].GetComponent<PlayersSpellsAttacks>().health <= 0)
        {
            playerTeamHealth -= fightOrder[target].GetComponent<PlayersSpellsAttacks>().health;
        }
        Debug.Log(fightOrder[target].GetComponent<PlayersSpellsAttacks>().health);
        d++;
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
        damageBeingDone = damageAmount / fightOrder[target].GetComponent<EnemyInfo>().armor;
        fightOrder[target].GetComponent<EnemyInfo>().health -= damageBeingDone;
        enemyTeamHealth -= damageBeingDone;
        if (fightOrder[target].GetComponent<EnemyInfo>().health <= 0)
        {
            enemyTeamHealth -= fightOrder[target].GetComponent<EnemyInfo>().health;
        }
        Debug.Log(fightOrder[target].GetComponent<EnemyInfo>().health);
        CancelInvoke("Target");
        lastButtonPressedToAttack = 0;
        lastButtonPressed = 0;
        didAction = false;
        SetPlayerButtons();
    }
    private void DieDieDieDie()
    {
        if(playerTeamHealth == 0 || enemyTeamHealth == 0)
        {
            isinCombat = false;
            for (int i = 0; i < pauseable.Length; i++)
            {
                pauseable[i].GetComponent<DetectPlayAndMove>().enabled = true;
                pauseable[i].GetComponent<DetectPlayAndMove>().Start();
            }
            playerCam.SetActive(true);
            battleCam.SetActive(false);
        }
    }
}
