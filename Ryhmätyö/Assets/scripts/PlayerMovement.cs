using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    GameObject camera;
    private CharacterController controller;
    private bool groundedPlayer;
    [SerializeField]
    float walkSpeed = 5.0f;
    [SerializeField]
    float runSpeed = 12.0f;
    [SerializeField]
    float jumpSpeed = 10.0f;
    [SerializeField]
    Light FL; // flushlight
    [SerializeField]
    GameManager gm;
    [SerializeField]
    Slider StaminaSlider, FLSlider;
    [SerializeField]
    Image StaminaFill;
    [SerializeField]
    float staminaValue, StaminaStep, flushlightValue, flushlightStep;

    //ÄÄNI - Lisää vaan playerMovementscriptin kohtaan Map kun siellä on SFXManager.
    [SerializeField]
    SFXManager sm;

    Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private bool running;
    private float SpeedH = 2f;
    private float SpeedV = 2f;

    private float yaw = 0f;
    private float pitch = 0f;
    private float minPitch = -80f;
    private float maxPitch = 80f;
    Vector3 startPos;
    private float StaminaMaxValue, FlushliteMaxValue; // voi käyttää jatkossa, jos on jotain itemiä, jotka täyttää staminan/valon
    private bool staminaDelay = false;
    private Color staminaFillDefaultColor, flushlightDefaultColor; 
    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startPos = transform.position;
        controller = gameObject.AddComponent<CharacterController>();
        FL.enabled = false;

        staminaFillDefaultColor = StaminaFill.color;

        StaminaMaxValue = staminaValue;
        StaminaSlider.maxValue = StaminaMaxValue;
        FlushliteMaxValue = flushlightValue;
        FLSlider.maxValue = FlushliteMaxValue;
    }

    // Update is called once per frame
    void Update()
    {
       if(!PauseMenuScript.isPaused){
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlushlighSwitch();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            running = true;
        }
        else{
            running = false;
        }
        Move();
        Flushlight();
        CameraFollow();   
        CameraRotate();
        StaminaSlider.value = staminaValue;
        FLSlider.value = flushlightValue;
       }
    }

    void Move()
    {
        groundedPlayer = controller.isGrounded;
       
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Camera.main.transform.TransformDirection(move);
        move = Vector3.ProjectOnPlane(move, Vector3.up);
        
        Vector3 camDir = camera.transform.forward;
        camDir = Vector3.ProjectOnPlane(camDir, Vector3.up);
        transform.forward = camDir;

        if (staminaValue < 0.2)
        {
            staminaDelay = true;
            Color color = new Color(208F / 255f, 48F / 255f, 48F / 255f);
            StaminaFill.color = color; // väri voi kyl vaihtaa ehkä tummemmaksi
        }
        else if (staminaValue > StaminaMaxValue * 0.2) // 20% staminasta  palautua, ennen ku voi juosta
        {
            staminaDelay = false;
            StaminaFill.color = staminaFillDefaultColor;
        }

        if (move != Vector3.zero)
        {
            if (running && staminaValue > 0)
            {
                if (!staminaDelay)
                {
                    //Debug.Log("RUNNING");
                    controller.Move(move * Time.deltaTime * runSpeed);
                    sm.stopWalk();
                    sm.p_run();
                    DecreaseStamina();
                }
                else {
                    IncreaseStamina(3);
                    controller.Move(move * Time.deltaTime * walkSpeed);
                    sm.p_walk();
                    sm.stopRun();

                }
            }
            else{
                controller.Move(move * Time.deltaTime * walkSpeed);
                sm.p_walk();
                sm.stopRun();
                IncreaseStamina(3);
            }
            gameObject.transform.forward = move;
        
            //Soitetaan ääni
        }else{
            IncreaseStamina(1);
            sm.stopWalk();
            sm.stopRun();
        }
        if (Input.GetButtonDown("Jump")&&groundedPlayer)
        {
            playerVelocity.y = jumpSpeed;
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }
    void CameraRotate()
    {
        yaw += Input.GetAxis("Mouse X") * SpeedH;
        pitch -= Input.GetAxis("Mouse Y") * SpeedV;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        camera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        FL.transform.eulerAngles = new Vector3(pitch, yaw, 0f);

    }
    void CameraFollow()
    {
        //Set cameras position
        Vector3 pPos = transform.position;
        camera.transform.position = pPos;
    }
    private void FlushlighSwitch()
    {
        
        if (FL.enabled == false && flushlightValue > 2)
        {
            FL.enabled = true;
        }
        else
        {
            FL.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            gm.enemyHit();
        }
    }

    private void DecreaseStamina()
    {
        if(staminaValue != 0)
        {
            staminaValue-= StaminaStep * Time.deltaTime;
        }
    }
    private void IncreaseStamina(float IncreaseSpeed) // IncreaseSpeed - staminan täyttönopeus jakaja
    {
        if (staminaValue < StaminaMaxValue)
        {
            staminaValue += StaminaStep / IncreaseSpeed * Time.deltaTime;
        }
    }

    private void Flushlight()
    {
        if (FL.enabled && flushlightValue > 0)
            DecreaseFlushlight();
        else if (FL.enabled && flushlightValue < 0.2)
            FL.enabled = false;
        else if (!FL.enabled)
            IncreaseFlushlight();
    }
    private void DecreaseFlushlight()
    {
        if (flushlightValue != 0)
        {
            flushlightValue -= flushlightStep * Time.deltaTime;
        }
    }
    private void IncreaseFlushlight()
    {
        if (flushlightValue < FlushliteMaxValue)
        {
            flushlightValue += flushlightStep / 3 * Time.deltaTime;
        }
    }
}
