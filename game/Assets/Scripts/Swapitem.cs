using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swapitem : MonoBehaviour
{
    //ainesosien kuvat
    [SerializeField] GameObject objectpictueres;
    //aineosien button
    [SerializeField] GameObject button;
   
      public void ogame()
    {
        //ainesosien kuvat samaan positioniin ainesosien buttonien kanssa
        objectpictueres.transform.position = button.transform.position;
        //ainesosian kuvat p‰‰lle 
        objectpictueres.SetActive(true);
    }

   
}
