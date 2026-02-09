using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject Ingredients;
    [SerializeField] GameObject Button;
    [SerializeField] GameObject Ingredientsbigmac;
    [SerializeField] GameObject Ingredientsqpwc;
    [SerializeField] TextMeshProUGUI uitext;
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
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
        if (uitext != null &&
            uitext.text.ToLower().Contains("el maco"))
        {
            Debug.Log("el macon resepti");
            Ingredients.SetActive(true);
            Button.SetActive(false);
        }
        if (uitext != null &&
           uitext.text.ToLower().Contains("big mac"))
        {
            Debug.Log("big macin resepti");
            Ingredientsbigmac.SetActive(true);
            Button.SetActive(false);
        }
        if (uitext != null &&
           uitext.text.ToLower().Contains("quarter pounder with cheese"))
        {
            Debug.Log("qpwc resepti");
            Ingredientsqpwc.SetActive(true);
            Button.SetActive(false);

        }
     
    }
}
