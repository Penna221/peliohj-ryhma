using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainItem;
    [SerializeField]
    Text itemText;
    [SerializeField]
    Text timerText;
    [SerializeField]
    public int ItemCountToEnd = 5;

    public bool haveItem = false;
    private GameObject[] respawns;
    private float startTime;
    private string endTime;
    private int items = 0;
    private int rnd;
    [SerializeField]
    AI_Enemy enemy;
    [SerializeField]
    Compass compass;

    public Animator animator;
    void Start()
    {
        startTime = Time.time;
        GameVariables.intemsToWin = ItemCountToEnd;
        if (respawns == null) respawns = GameObject.FindGameObjectsWithTag("ItemSpawn");

        spawnItem();
        
        itemText.text = "Items: " + items.ToString() + "/" + GameVariables.intemsToWin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
    }

    public void takeItem() // kun pelaaja ottaa Itemin
    {
        items = items + 1;
        compass.remoweItem();
        if (items == ItemCountToEnd) loadWinScene();

        haveItem = true;
        itemText.text = "Items: " + items.ToString() + "/" + GameVariables.intemsToWin.ToString();
        spawnItem();
        
        enemy.upSpeed();

    }

    public void enemyHit() // kun vihu koskee pelajaan
    {
        itemText.text = "You Died!";
        loadLostScene();
    }

    private void updateTimer() // kellon p�ivitys
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();

        if (t / 60 < 10) minutes = "0" + minutes;

        string seconds = (t % 60).ToString("f1");

        if (t % 60 < 10) seconds = "0" + seconds;

        endTime = minutes + ":" + seconds;
        timerText.text = "Time: " + minutes + ":" + seconds;
    }

    private void spawnItem()
    {
        int lastRnd = rnd;
        while(rnd == lastRnd)
        {
            rnd = Random.Range(0, respawns.Length);
        }

        GameObject itemi = Instantiate(mainItem, respawns[rnd].transform.position, respawns[rnd].transform.rotation);
        compass.newItem(itemi);
    }

    private void loadWinScene()
    {
        Cursor.lockState = CursorLockMode.None;
        GameVariables.win = true;
        GameVariables.time = endTime;

        Cursor.visible = true;
        StartCoroutine(transitionToScene("EndScene"));
    }
    private void loadLostScene()
    {
        Cursor.lockState = CursorLockMode.None;
        GameVariables.win = false;
        GameVariables.time = endTime;
        GameVariables.items = items;
        Cursor.visible = true;
        StartCoroutine(transitionToScene("EndScene"));
    }
    IEnumerator transitionToScene(string scene){
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

}
