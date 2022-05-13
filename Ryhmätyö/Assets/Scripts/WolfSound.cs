using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfSound : MonoBehaviour
{
    [SerializeField]
    public AudioSource sound1;
    [SerializeField]
    public AudioSource sound2;
    [SerializeField]
    Transform player;
    [SerializeField]
    float distance;
    
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
        if (!PauseMenuScript.isPaused)
        {
            distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (distance > 180 && distance < 500)
            {
                if (!sound2.isPlaying && !sound1.isPlaying)
                {
                    sound2.Play();
                }
            } else if(distance < 180)
            {
                if (!sound2.isPlaying && !sound1.isPlaying)
                {
                    sound1.Play();
                }
            }
        }
    }
    public void stopSounds(){
        sound2.Stop();
        sound1.Stop();
    }
}
