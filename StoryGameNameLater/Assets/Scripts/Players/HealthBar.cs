using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public PlayersSpellsAttacks _PSAScript;
    public void Start()
    {
        player = GameObject.Find("BigPlayer");
        _PSAScript = player.GetComponent<PlayersSpellsAttacks>();
    }
    public void LateUpdate()
    {
        SetHealth(_PSAScript.health);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
