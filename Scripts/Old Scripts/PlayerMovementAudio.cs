using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public Rigidbody rb2d;
    bool isMoving;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(0.9f,1f);
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

    }
}
