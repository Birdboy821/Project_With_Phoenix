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

    public int currentRotation;
    public bool canStopRotaion;

    public float legA = 0;
    public float legB = 0;
    public float distantSquared = 0;
    public float distantsToPlayer = 0;

    public GameObject player;

    public bool isTracking = false;

    public float fireChance;

    public GameObject CannonBallIN;
    public GameObject CannonBallOB;

    public bool StartedToFireCannons = false;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("HUHU", 3, 3);
        InvokeRepeating("CheckIfRotateFinished", 0.1f, 0.1f);
        Debug.Log(gameObject.transform.localRotation.y);
        newRotationAngle = gameObject.transform.localRotation.y;
        currentRotation = (int)Mathf.Round(gameObject.transform.localRotation.eulerAngles.y);
        player = GameObject.Find("BoatTempoi");

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


        CheckIfPlayerIsInRange();
        _RB_Boat.AddRelativeForce(Vector3.forward * speed * 5);
    }

    public void HUHU()
    {
        speedChange = Random.Range(0, 3);
    }
    public void JIJI()
    {
        rotationChance = Random.Range(1, 3);

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
        if (howmuch > currentRotation)
        {
            if (howmuch >= currentRotation)
                currentRotation += 1;
            else if (howmuch <= currentRotation)
                currentRotation -= 1;
        }
        else if (howmuch < currentRotation)
        {
            if (howmuch <= currentRotation)
                currentRotation -= 1;
            else if (howmuch >= currentRotation)
                currentRotation += 1;
        }

        transform.localRotation = Quaternion.Euler(0f, currentRotation, 0f);
        if (currentRotation == howmuch)
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
        //Debug.Log(Mathf.Round(gameObject.transform.localRotation.eulerAngles.y));



        if (canStopRotaion == true)
        {
            CancelInvoke("LPLP");
            JIJI();
            canStopRotaion = false;
        }
    }

    public void CheckIfPlayerIsInRange()
    {
        legA = gameObject.transform.position.x - player.transform.position.x;
        legB = gameObject.transform.position.z - player.transform.position.z;
        distantSquared = Mathf.Pow(legA, 2) + Mathf.Pow(legB, 2);
        distantsToPlayer = Mathf.Sqrt(distantSquared);

        if (distantsToPlayer <= 20)
        {
            gameObject.transform.LookAt(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z));
            gameObject.transform.Rotate(new Vector3(0, 90, 0));
            CancelInvoke("LPLP");
            isTracking = true;
            StartCannonFire();
        }
        else if (distantsToPlayer > 20 && isTracking == true)
        {
            CancelInvoke("CanFireCannonAtWill");
            StartedToFireCannons = false;
            canStopRotaion = true;
            CheckIfRotateFinished();
            isTracking = false;
        }

    }
    public void StartCannonFire()
    {
        if(StartedToFireCannons == false)
        {
            StartedToFireCannons = true;
            InvokeRepeating("CanFireCannonAtWill", 0.1f, 2f);
        }
    }
    public void CanFireCannonAtWill()
    {
        fireChance = Random.Range(0, 5);

        if(fireChance == 0)
        {
            //Nothing
        }
        else if(fireChance == 1)
        {
            //Fire Left
            CannonBallOB = Instantiate(CannonBallIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            CannonBallOB.transform.localPosition = new Vector3(3.2f, 1, -1);
            CannonBallOB.transform.rotation = gameObject.transform.rotation;
            CannonBallOB.transform.parent = gameObject.transform.parent;
            CannonBallOB.GetComponent<CannonBall>().MoveFireball();
            CannonBallOB.GetComponent<CannonBall>().InvokeRepeating("DestroyFireball", 5f, 1);
        }
        else if (fireChance == 2)
        {
            //Fire Right
            CannonBallOB = Instantiate(CannonBallIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            CannonBallOB.transform.localPosition = new Vector3(-3.2f, 1, -1);
            CannonBallOB.transform.rotation = gameObject.transform.rotation;
            CannonBallOB.transform.parent = gameObject.transform.parent;
            CannonBallOB.GetComponent<CannonBall>().MoveFireballRight();
            CannonBallOB.GetComponent<CannonBall>().InvokeRepeating("DestroyFireball", 5f, 1);
        }
        else if (fireChance == 3)
        {
            //Left Jam
            CannonBallOB = Instantiate(CannonBallIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            CannonBallOB.transform.localPosition = new Vector3(3.2f, 1, 1);
            CannonBallOB.transform.rotation = gameObject.transform.rotation;
            CannonBallOB.transform.parent = gameObject.transform.parent;
            CannonBallOB.GetComponent<CannonBall>().MoveFireball();
            CannonBallOB.GetComponent<CannonBall>().InvokeRepeating("DestroyFireball", 5f, 1);
        }
        else if (fireChance == 4)
        {
            //Right Jam
            CannonBallOB = Instantiate(CannonBallIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            CannonBallOB.transform.localPosition = new Vector3(-3.2f, 1, 1);
            CannonBallOB.transform.rotation = gameObject.transform.rotation;
            CannonBallOB.transform.parent = gameObject.transform.parent;
            CannonBallOB.GetComponent<CannonBall>().MoveFireballRight();
            CannonBallOB.GetComponent<CannonBall>().InvokeRepeating("DestroyFireball", 5f, 1);
        }
    }
}
