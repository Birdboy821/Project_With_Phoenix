using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadBoatHealthBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    public BadBoatMove _PSAScript;
    public void Start()
    {
        _PSAScript = player.GetComponent<BadBoatMove>();
        SetMaxHealth(_PSAScript.boatHealth);
    }
    public void LateUpdate()
    {
        SetHealth(_PSAScript.boatHealth);
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
