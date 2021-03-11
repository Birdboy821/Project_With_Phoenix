using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public PlayersSpellsAttacks _PSAScript;
    public void Start()
    {
        player = GameObject.Find("BigPlayer");
        _PSAScript = player.GetComponent<PlayersSpellsAttacks>();
        SetMaxHealth(_PSAScript.mana);
    }
    public void LateUpdate()
    {
        SetHealth(_PSAScript.mana);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
