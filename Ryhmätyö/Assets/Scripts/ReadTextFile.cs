using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReadTextFile : MonoBehaviour
{
    public TextAsset textFile;
    public Text destination;
    // Start is called before the first frame update
    void Start()
    {
        string text = textFile.text;

        destination.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
