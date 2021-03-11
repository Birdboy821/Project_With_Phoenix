using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayersHealth : MonoBehaviour
{
    public Color Stage1;
    public Color Stage2;
    public Color Stage3;
    public Color Stage4;
    public Color Stage5;
    public Color Stage6;
    public Color Stage7;
    public Color Stage8;
    public Color Stage9;
    public Color Stage10;
    public Color Stage11;
    public Color[] colorRotation = new Color[11];

    public Image[] PlayerHealthSegment;

    public GameObject player;
    public PlayersSpellsAttacks _PSAScript;
    public int d = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("BigPlayer");
        _PSAScript = player.GetComponent<PlayersSpellsAttacks>();

        colorRotation[10] = Stage1;
        colorRotation[9] = Stage2;
        colorRotation[8] = Stage3;
        colorRotation[7] = Stage4;
        colorRotation[6] = Stage5;
        colorRotation[5] = Stage6;
        colorRotation[4] = Stage7;
        colorRotation[3] = Stage8;
        colorRotation[2] = Stage9;
        colorRotation[1] = Stage10;
        colorRotation[0] = Stage11;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_PSAScript.health % 100 != 0)
        {
            d = 0;
            for (int i = 0; i < ((int)(_PSAScript.health % 100)) / 10; i++)
            {
                PlayerHealthSegment[i].color = colorRotation[10];
                d++;
            }
            PlayerHealthSegment[d].color = colorRotation[(int)_PSAScript.health % 10];
        }
        else if (_PSAScript.health % 100 == 0)
        {
            foreach(Image im in PlayerHealthSegment)
            {
                im.color = colorRotation[10];
            }
        }
        if (d < 9 && _PSAScript.health != 100)
        {
            for (int g = 9; g > d; g--)
            {
                PlayerHealthSegment[g].color = colorRotation[0];
            }
        }
    }
}
