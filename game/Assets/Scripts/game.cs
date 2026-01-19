using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject button;
    [SerializeField] GameObject palyer;
    [SerializeField] GameObject palyer2;
    [SerializeField] bool yes = false;
    
    public void ogame()
    {
        player.transform.position = button.transform.position;
        Debug.Log("player position: "+ player.transform.position);
        Debug.Log("button position: "+button.transform.position);
        player.SetActive(true);
        yes = true;
        ogame2();
       
    }
    public void ogame2()
    {
       
      
        if (yes)
        {
            palyer.transform.position = palyer2.transform.position;
            palyer.SetActive(true);
            Debug.Log(yes);
            yes = false;
        }

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
