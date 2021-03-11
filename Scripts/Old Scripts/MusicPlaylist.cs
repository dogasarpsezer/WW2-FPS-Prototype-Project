using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public AudioSource stereo;
    public AudioClip[] songs;

    int index = 0;
    bool isPaused = false;

    void Start()
    {
        stereo.PlayOneShot(songs[index]);

    }
    private void Update()
    {
        if (stereo.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                stereo.Stop();
                isPaused = true;
            }
        }
        if (isPaused)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                 stereo.PlayOneShot(songs[index]);
                 isPaused = false;
            }
        }
 
        if (Input.GetKeyDown(KeyCode.N))
        {
            if(index < songs.Length - 1)
            {

                index++;
            }
               
            stereo.PlayOneShot(songs[index]);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(index != 0)
            {
                index--;
            }
                
            stereo.PlayOneShot(songs[index]);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            stereo.volume -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            stereo.volume += 0.1f;
        }
    }


}
