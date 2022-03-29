using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    GameManager gm;
    private bool get = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        get = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (get)
        {
            Destroy(gameObject);
            gm.takeItem();
        }
        get = false;
    }
}
