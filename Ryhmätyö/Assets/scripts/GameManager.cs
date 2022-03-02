using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject mainItem;
    [SerializeField]
    Text itemText;

    private GameObject mainItemOnMap;
    private GameObject[] respawns;

    void Start()
    {
        if (respawns == null)
            respawns = GameObject.FindGameObjectsWithTag("ItemSpawn");

        int rnd = Random.Range(0, respawns.Length);
        mainItemOnMap = Instantiate(mainItem, respawns[rnd].transform.position, respawns[rnd].transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void takeItem() // Tänne mitä tapahtuu, kun pelaaja ottaa Itemin
    {
        Destroy(mainItemOnMap);
        itemText.text = "You have item: Yes";
    }
    public void enemyHit() // Tänne mitä tapahtuu, kun vihu koskee pelajaan
    {
        itemText.text = "Enemy kill you";
    }
}
