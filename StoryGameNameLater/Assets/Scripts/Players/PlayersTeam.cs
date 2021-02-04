using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersTeam : MonoBehaviour
{
    public GameObject companionOne;
    public GameObject companionTwo;
    // Start is called before the first frame update
    void Start()
    {
        companionOne = GameObject.Find("Ze");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
