using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    private Button bt1,bt2,bt3;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Button playButton = bt1.GetComponent<Button>();
		playButton.onClick.AddListener(play);

        Button settingsButton = bt2.GetComponent<Button>();
		settingsButton.onClick.AddListener(settings);
        
        Button exitButton = bt3.GetComponent<Button>();
		exitButton.onClick.AddListener(exit);
        
    }
    void settings(){
        StartCoroutine(transitionToScene("SettingsScene"));
    }
    void play(){
        StartCoroutine(transitionToScene("GameScene"));
    }

    IEnumerator transitionToScene(string scene){
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
    void exit(){
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
