using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject Ingredients;
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Ingredientsbigmac;
    [SerializeField] GameObject Ingredientsqpwc;
    [SerializeField] TextMeshProUGUI uitext;
    [SerializeField] TextMeshProUGUI textbigmac;
    [SerializeField] TextMeshProUGUI textqpwc;
    [SerializeField] TextMeshProUGUI textelmaco;
    [SerializeField] GameObject canvas;
    [SerializeField] Boolean ifclick;
    [SerializeField] Boolean ifclick1;
    [SerializeField] Boolean ifclick2;
    public void playGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void gosettings()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void gomenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void goIngredients()
    {
        if (textelmaco != null &&
           textelmaco.text.ToLower().Contains("el maco"))
        {
            gogame();
            ifclick2 = true;
        }
        if (textelmaco != null && textelmaco.text.ToLower().Contains("el maco") && ifclick2)
        {
            Debug.Log("el macon resepti");
            Ingredients.SetActive(true);
            Button.SetActive(false);
        }
        if (textbigmac != null &&
           textbigmac.text.ToLower().Contains("big mac"))
        {
            gogame();
            ifclick = true;
        }
        if (textqpwc != null && textqpwc.text.ToLower().Contains("quarter pounder with cheese") && ifclick1) 
        {
            Debug.Log("qpwc resepti");
            Ingredientsqpwc.SetActive(true);
            Button.SetActive(false);
        }
        if (textqpwc != null &&
           textqpwc.text.ToLower().Contains("quarter pounder with cheese"))
        {
            gogame();
           ifclick1 = true;
        }
        if(textbigmac != null && textbigmac.text.ToLower().Contains("big mac") && ifclick)
        {
            Debug.Log("big macin resepti");
            Ingredientsbigmac.SetActive(true);
            Button.SetActive(false);
        }

     
    }
    public void ShowBigMac()
    {
        HideAll();
        Button.SetActive(true);
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        Transform clickedTransform = clicked.transform;
        string ingredientName = clickedTransform.name.Replace("(Clone)", "").Trim();

        Debug.Log($"Klikattiin nappia: {ingredientName}");
        if (ingredientName == "big mac")
        {
            Ingredientsbigmac.SetActive(true);
            Debug.Log("toimii");
            canvas.SetActive(false);
        }

    }

    public void ShowElMaco()
    {
        HideAll();
        Button.SetActive(true);
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        Transform clickedTransform = clicked.transform;
        string ingredientName = clickedTransform.name.Replace("(Clone)", "").Trim();

        Debug.Log($"Klikattiin nappia: {ingredientName}");
        if (ingredientName == "elmaco")
        {
            Ingredients.SetActive(true);
            Debug.Log("toimii");
            canvas.SetActive(false);
        }
    }

    public void ShowQPWC()
    {
        HideAll();
        Button.SetActive(true);
        GameObject clicked = EventSystem.current.currentSelectedGameObject;
        Transform clickedTransform = clicked.transform;
        string ingredientName = clickedTransform.name.Replace("(Clone)", "").Trim();

        Debug.Log($"Klikattiin nappia: {ingredientName}");
      
            if (ingredientName == "qpwc")
            {
                Ingredientsqpwc.SetActive(true);
                Debug.Log("toimii");
            canvas.SetActive(false);
        }
        

    }

    void HideAll()
    {
        Ingredientsbigmac.SetActive(false);
        Ingredients.SetActive(false);
        Ingredientsqpwc.SetActive(false);
    }
    public void gogame() 
    {
       
        Button.SetActive(true);
        canvas.SetActive(false);
      

    }
 


}
