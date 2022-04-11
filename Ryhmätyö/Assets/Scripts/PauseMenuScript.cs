using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator animator;
    public static bool isPaused = false;
    [SerializeField]
    private Button bt1,bt2;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        Button resumeButton = bt1.GetComponent<Button>();
		resumeButton.onClick.AddListener(resumeGame);
        Button menuButton = bt2.GetComponent<Button>();
		menuButton.onClick.AddListener(goToMenu);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                resumeGame();
            }else{
                pauseGame();
            }
        }
    }

    public void pauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void goToMenu(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(transitionToScene("MenuScene"));
    }
    IEnumerator transitionToScene(string scene){
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
