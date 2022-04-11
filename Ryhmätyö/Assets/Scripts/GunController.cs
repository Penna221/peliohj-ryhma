using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem muzzleFlash;
    AudioSource muzzleFlashAudioSource;
    public float range = 100f;
    public Camera fpsCam;
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlashAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuScript.isPaused){
            if (Input.GetButtonDown("Fire1"))
            {
                muzzleFlash.Play();
                Shoot();
            }

        }
    }
    private void Shoot()
    {
        muzzleFlashAudioSource.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            AI_Enemy enemy = hit.transform.GetComponent<AI_Enemy>();
            if(enemy != null)
            {
                enemy.takeDamage();
            }
        }
    }
}
