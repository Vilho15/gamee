using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject button;
    public void ogame()
    {
        player.transform.position = button.transform.position;
        Debug.Log("player position: "+ player.transform.position);
        Debug.Log("button position: "+button.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
