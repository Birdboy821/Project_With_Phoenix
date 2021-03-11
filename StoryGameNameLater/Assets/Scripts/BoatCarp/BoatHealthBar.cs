using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoatHealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public MoveBoat _PSAScript;
    public void Start()
    {
        player = GameObject.Find("BoatTempoi");
        _PSAScript = player.GetComponent<MoveBoat>();
        SetMaxHealth(_PSAScript.health);
    }
    public void LateUpdate()
    {
        SetHealth(_PSAScript.health);
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
