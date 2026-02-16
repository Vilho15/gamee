using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swapitem : MonoBehaviour
{
    [SerializeField] GameObject objectpictueres;
    [SerializeField] GameObject button;
    
   
    
    public void ogame()
    {
        objectpictueres.transform.position = button.transform.position;
        Debug.Log("player position: "+ objectpictueres.transform.position);
        Debug.Log("button position: "+button.transform.position);
        objectpictueres.SetActive(true);
        
       
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
