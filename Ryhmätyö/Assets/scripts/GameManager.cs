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
    int ItemCountToEnd = 5;

    public bool haveItem = false;
    private GameObject[] respawns;
    private float startTime;
    private string endTime;
    private int items = 0;
    private int rnd;
    [SerializeField]
    AI_Enemy enemy;

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
        if(items == ItemCountToEnd) loadWinScene();

        haveItem = true;
        itemText.text = "Items: " + items.ToString() + "/" + GameVariables.intemsToWin.ToString();
        spawnItem();
        enemy.upSpeed();

    }

    public void enemyHit() // kun vihu koskee pelajaan
    {
        itemText.text = "Enemy kill you";
        loadLostScene();
    }

    private void updateTimer() // kellon päivitys
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

        Instantiate(mainItem, respawns[rnd].transform.position, respawns[rnd].transform.rotation);
    }

    private void loadWinScene()
    {
        GameVariables.win = true;
        GameVariables.time = endTime;
        SceneManager.LoadScene("EndScene");
    }
    private void loadLostScene()
    {
        GameVariables.win = false;
        GameVariables.time = endTime;
        GameVariables.items = items;
        SceneManager.LoadScene("EndScene");
    }

}
