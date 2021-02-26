using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadBoatMove : MonoBehaviour
{
    public Rigidbody _RB_Boat;
    public int speedChange;
    public float speed;
    public float highSpeed;
    public float lowSpeed;
    public float normalSpeed;

    public int rotationChance;
    public float howmuch;
    public Quaternion smoothRotateAmount = Quaternion.Euler(0, 1, 0);
    public float newRotationAngle;

    public float currentRotation;
    public bool canStopRotaion;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("HUHU", 3, 3);
        InvokeRepeating("CheckIfRotateFinished", 0.1f, 0.1f);
        Debug.Log(gameObject.transform.localRotation.y);
        newRotationAngle = gameObject.transform.localRotation.y;
        currentRotation = gameObject.transform.localRotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedChange == 0)
            speed = lowSpeed;
        else if (speedChange == 1)
            speed = normalSpeed;
        else if (speedChange == 2)
            speed = highSpeed;

        

        _RB_Boat.AddRelativeForce(Vector3.forward * speed * 5);
    }

    public void HUHU()
    {
        speedChange = Random.Range(0, 3);
    }
    public void JIJI()
    {
        rotationChance = Random.Range(0, 3);

        if (rotationChance == 0)
        {
            //do nothing
            howmuch = 0;
        }
        else if (rotationChance == 1)
        {
            howmuch = Random.Range(0, 90);
            ApplyRotation();
        }
        else if (rotationChance == 2)
        {
            howmuch = Random.Range(0, 90) * -1;
            ApplyRotation();
        }
    }
    public void ApplyRotation()
    {
        //newRotationAngle = gameObject.transform.localRotation.y + howmuch;
        InvokeRepeating("LPLP", 0.5f, 0.5f);
        
    }
    public void LPLP()
    {
        /*
        if(howmuch >= 0)
            gameObject.transform.Rotate(0, 1f, 0);
        else if (howmuch <= 0)
            gameObject.transform.Rotate(0, -1f, 0);
        */
        currentRotation += 0.5f;
        transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
        if (currentRotation >= howmuch)
        {
            canStopRotaion = true;
        }
    }

    public void CheckIfRotateFinished()
    {
        /*
        if(gameObject.transform.eulerAngles.y > 180)
        {
            float tempooo = gameObject.transform.eulerAngles.y - 180;
            Debug.Log(Mathf.Floor(gameObject.transform.eulerAngles.y));
        }
        else
        {
            Debug.Log(Mathf.Floor(gameObject.transform.eulerAngles.y));
        }
        */
        Debug.Log(gameObject.transform.rotation.y);

        

        if (canStopRotaion == true)
        {
            CancelInvoke("LPLP");
            JIJI();
            canStopRotaion = false;
        }
    }
}
