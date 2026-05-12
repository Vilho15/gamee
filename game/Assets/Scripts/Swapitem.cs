using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swapitem : MonoBehaviour
{
    //ainesosien kuvat
    [SerializeField] GameObject objectpictueres;
    //aineosien button
    [SerializeField] GameObject objectbutton;
   
      public void ogame()
    {
        //ainesosien kuvat samaan positioniin ainesosien buttonien kanssa
        objectpictueres.transform.position = objectbutton.transform.position;
        //ainesosian kuvat p‰‰lle 
        objectpictueres.SetActive(true);
    }

   
}
