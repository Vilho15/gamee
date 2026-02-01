using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Textsystem : MonoBehaviour
{
    [SerializeField] string[] burgers;
    [SerializeField] TextMeshProUGUI burgeruitext;
    [SerializeField] GameObject button;

    void OnMouseDown()
    {
        int index = Random.Range(0, burgers.Length);
        burgeruitext.text =   burgers[index];
        button.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        burgeruitext.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
