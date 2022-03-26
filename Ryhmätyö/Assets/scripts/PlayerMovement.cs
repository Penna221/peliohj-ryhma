using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startPos = transform.position;
        controller = gameObject.AddComponent<CharacterController>();
        FL.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LightSwitch();
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            running = true;
            
        }else{
            running = false;
        }
        Move();
        CameraFollow();   
        CameraRotate();
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
        if (move != Vector3.zero)
        {
            if(running){
                Debug.Log("RUNNING");
                controller.Move(move * Time.deltaTime * runSpeed);
                sm.stopWalk();
                sm.p_run();
            }else{
                controller.Move(move * Time.deltaTime * walkSpeed);
                sm.p_walk();

                sm.stopRun();
            }
            gameObject.transform.forward = move;
            //Soitetaan ääni
            
        }else{
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
    void LightSwitch()
    {
        if (FL.enabled == false)
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
}
