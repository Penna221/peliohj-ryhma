using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndSceneSetter : MonoBehaviour
{
    [SerializeField]
    private Button mainMenubtn, restartBtn;
    [SerializeField]
    Text middleText, titleText;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        if (GameVariables.win)
        {
            titleText.text = "You win!";
            middleText.text = "Time: " + GameVariables.time;
        }
        else
        {
            titleText.text = "You lost!";
            middleText.text = "Time: " + GameVariables.time + "\nItems: " + GameVariables.items + "/" + GameVariables.intemsToWin;
        }

        Button playButton = mainMenubtn.GetComponent<Button>();
        playButton.onClick.AddListener(goToMainMenu);

        Button settingsButton = restartBtn.GetComponent<Button>();
        settingsButton.onClick.AddListener(restartGame);

        
    }
    
    public void restartGame()
    {
        titleText.text = "You winasdasdadas!";
        SceneManager.LoadScene("GameScene");
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
