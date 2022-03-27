using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireSound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        play();
    }
    private void play(){
        if(!sound.isPlaying){
            sound.Play();
        }
    }
}
