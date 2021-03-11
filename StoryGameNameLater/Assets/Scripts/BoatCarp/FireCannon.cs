using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCannon : MonoBehaviour
{
    public GameObject CannonBallIN;
    public GameObject CannonBallOB;

    public GameObject player;
    public float aCheckLeg;
    public float bCheckLeg;
    public float distanceToPlayerSquared;
    public float distanceToPlayer;
    public float cannonCoolDown = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CoolDown", 0, 1);
    }
    public void CoolDown()
    {
        cannonCoolDown++;
    }
    // Update is called once per frame
    void Update()
    {
        aCheckLeg = player.gameObject.transform.position.x - gameObject.transform.position.x;
        bCheckLeg = player.gameObject.transform.position.z - gameObject.transform.position.z;
        distanceToPlayerSquared = Mathf.Pow(aCheckLeg, 2) + Mathf.Pow(bCheckLeg, 2);
        distanceToPlayer = Mathf.Sqrt(distanceToPlayerSquared);

        if (distanceToPlayer <= 3 && Input.GetKey("x") && cannonCoolDown >= 4)
        {
            cannonCoolDown = 0;
            CannonBallOB = Instantiate(CannonBallIN, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform);
            CannonBallOB.transform.localPosition = new Vector3(2.5f, 1, 0);
            CannonBallOB.transform.rotation = gameObject.transform.rotation;
            CannonBallOB.transform.parent = gameObject.transform.parent;
            CannonBallOB.GetComponent<CannonBall>().MoveFireball();
            CannonBallOB.GetComponent<CannonBall>().InvokeRepeating("DestroyFireball", 5f, 1);
        }
    }

}
