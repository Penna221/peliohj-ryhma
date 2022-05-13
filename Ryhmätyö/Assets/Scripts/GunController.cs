using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem muzzleFlash;
    AudioSource muzzleFlashAudioSource;
    public float range = 100f;
    public Camera fpsCam;
    [SerializeField]
    int currentAmmo = 2;
    int maxAmmo = 2;
    [SerializeField]
    Slider AmmoSlider;
    [SerializeField]
    Text ammoText;
    [SerializeField]
    float sliderValue, sliderMax = 30, sliderStep = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
        sliderValue = 0;
        AmmoSlider.maxValue = sliderMax;
        ammoText.text = currentAmmo + " / " + maxAmmo;
        muzzleFlashAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenuScript.isPaused){
            if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
            {
                muzzleFlash.Play();
                Shoot();
                currentAmmo--;
            }

        }
        IncreaseAmmo();
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    private void IncreaseAmmo()
    {
        if (currentAmmo < maxAmmo) {
            if (sliderValue < sliderMax)
            {
                sliderValue += sliderStep * Time.deltaTime;
            }
            else if (sliderValue >= sliderMax)
            {
                currentAmmo++;
                sliderValue = 0;
            }
            AmmoSlider.value = sliderValue;
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
