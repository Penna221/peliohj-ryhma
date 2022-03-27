using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound1;
    [SerializeField]
    AudioSource sound2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //JOKU LOGIIKKA TÄHÄN? AJASTIMEN AVULLA VAAN JOSKUS SOITETAAN ÄÄNI ?
        play();   
    }
    private void play(){
        if(!sound2.isPlaying){
            sound2.Play();
        }
    }
}
