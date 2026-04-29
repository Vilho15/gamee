using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swapitem : MonoBehaviour
{
    [SerializeField] GameObject objectpictueres;
    [SerializeField] GameObject button;
    [SerializeField] GameObject area;
   
    
   
    
    public void ogame()
    {
        objectpictueres.transform.position = button.transform.position;
       //Debug.Log("player position: "+ objectpictueres.transform.position);
        //Debug.Log("button position: "+button.transform.position);
        objectpictueres.SetActive(true);
        Debug.Log("area: "+ area.transform.position);
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            Debug.Log("toimiiko");
            if (area.transform.position == objectpictueres.transform.position)
            {
                
                Debug.Log("Toimii");
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("player position: " + objectpictueres.transform.position);
    }
}
