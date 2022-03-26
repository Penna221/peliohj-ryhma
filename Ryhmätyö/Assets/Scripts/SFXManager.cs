using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    public AudioSource player_walking_sound;
    public AudioSource player_running_sound;
    public AudioSource wolf1_sound;
    public AudioSource wolf2_sound;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void stopWalk(){
        player_walking_sound.Stop();
    }
    public void stopRun(){
        player_running_sound.Stop();
    }
    public void p_walk(){
        if(!player_walking_sound.isPlaying){
            player_walking_sound.Play();
        }

    }
    public void p_run(){
        if(!player_running_sound.isPlaying){
            player_running_sound.Play();
        }
    }
    public void wolf1(){
       if(!wolf1_sound.isPlaying){
           wolf1_sound.Play();
       }
    }
    public void wolf2(){
        if(!wolf2_sound.isPlaying){
            wolf2_sound.Play();
        }
    }
}
