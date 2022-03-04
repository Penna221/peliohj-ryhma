using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class SettingsButtons : MonoBehaviour
{
    [SerializeField]
    private Button bt1;
    // Start is called before the first frame update
    void Start()
    {
        Button backButton = bt1.GetComponent<Button>();
		backButton.onClick.AddListener(back);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void back(){
        SceneManager.LoadScene("MenuScene");
    }
}
