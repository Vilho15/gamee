using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class grabitem : MonoBehaviour
{
    [SerializeField] GameObject go;
    [SerializeField] GameObject knife;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            knife.transform.position = go.transform.position;
        }
    
    }
}
