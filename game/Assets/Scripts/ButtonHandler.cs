using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] GameObject Ingredients;
    [SerializeField] GameObject Button;
    [SerializeField] TextMeshProUGUI burgeuitext;
    [SerializeField] GameObject Ingredientsbigmac;
    [SerializeField] GameObject Button2;
    [SerializeField] GameObject Ingredientsqpwc;
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
        if (burgeuitext != null &&
            burgeuitext.text.ToLower().Contains("el maco"))
        {
            Debug.Log("el macon resepti");
            Ingredients.SetActive(true);
            Button.SetActive(false);
        }
        if (burgeuitext != null &&
           burgeuitext.text.ToLower().Contains("big mac"))
        {
            Debug.Log("big macin resepti");
            Ingredientsbigmac.SetActive(true);
            Button.SetActive(false);
        }
        if (burgeuitext != null &&
           burgeuitext.text.ToLower().Contains("quarter pounder with cheese"))
        {
            Debug.Log("qpwc resepti");
            Ingredientsqpwc.SetActive(true);
            Button.SetActive(false);

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
}
