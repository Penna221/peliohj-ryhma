using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSound : MonoBehaviour
{
    [SerializeField]
    public AudioSource sound1;
    [SerializeField]
    public AudioSource sound2;
    
    float defaultperiod = 15;
    float nextActionTime;

    // Start is called before the first frame update
    void Start()
    {
        nextActionTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuScript.isPaused){
            
            if (Time.time > nextActionTime ) {
                float newTime = Random.Range(-4f,5);
                float totalTime = newTime + defaultperiod;
                nextActionTime = Time.time + totalTime;
                play();
            }
        }
        //JOKU LOGIIKKA TÄHÄN? AJASTIMEN AVULLA VAAN JOSKUS SOITETAAN ÄÄNI ?
    }
    private void play(){
        if(!sound2.isPlaying){
            sound2.Play();
        }
    }
    public void stopSounds(){
        sound2.Stop();
        sound1.Stop();
    }
}
