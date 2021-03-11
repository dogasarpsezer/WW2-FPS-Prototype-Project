using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLeanScript : MonoBehaviour
{
    [SerializeField]
    private float xPos;

    [SerializeField]
    private float leaningPositionSpeed;

    //This codes rotation part is in "Mouse Look Rotation" script.

    [SerializeField]
    private Transform playerRoot;

    private float leaningPosition = 0f;

    private float rollAngle = 0f;

    private void Awake()
    {
        playerRoot = transform.Find("Root").GetComponent<Transform>();
    }

    private void Update()
    {
        Leaning();
        playerRoot.localPosition = new Vector3(leaningPosition, playerRoot.localPosition.y, playerRoot.localPosition.z);
    }

    public void Leaning()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            //Changing leaning position which is x over time for leaning left
            leaningPosition -= Time.deltaTime * leaningPositionSpeed;
            leaningPosition = Mathf.Clamp(leaningPosition,-xPos, 0f);

        }
        else if (Input.GetKey(KeyCode.E))
        {
            //Changing leaning position which is x over time for leaning right
            leaningPosition += Time.deltaTime * leaningPositionSpeed;
            leaningPosition = Mathf.Clamp(leaningPosition, 0f, xPos);

        }
        else
        {
            if (leaningPosition < 0)
            {
                //Initialize the position if the player leans left
                leaningPosition += Time.deltaTime * leaningPositionSpeed;
                leaningPosition = Mathf.Clamp(leaningPosition, -xPos, 0f);

            }
            else if (leaningPosition > 0)
            {
                //Initialize the position if the player leans right
                leaningPosition -= Time.deltaTime * leaningPositionSpeed;
                leaningPosition = Mathf.Clamp(leaningPosition, 0f, xPos);

            }
        }
    }
}
