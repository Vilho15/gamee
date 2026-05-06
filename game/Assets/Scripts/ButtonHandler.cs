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
    [SerializeField] private Recipe bigMacRecipe;
    [SerializeField] private Recipe qpwcRecipe;
    [SerializeField] private Recipe elMacoRecipe;
    [SerializeField] GameObject Ingredientsbigmac;
    [SerializeField] GameObject Ingredientsqpwc;
    [SerializeField] TextMeshProUGUI textbigmac;
    [SerializeField] TextMeshProUGUI textqpwc;
    [SerializeField] TextMeshProUGUI textelmaco;
    [SerializeField] GameObject canvas;
    [SerializeField] RecipeChecker recipe;
    public enum BurgerType
    {
        None,
        BigMac,
        QPWC,
        ElMaco
    }
    public static BurgerType selectedBurger = BurgerType.None;
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

 
    public void SelectBigMac()
    {
        selectedBurger = BurgerType.BigMac;

        recipe.SetRecipe(bigMacRecipe); // ?? TÄRKEIN RIVI

        Ingredientsbigmac.SetActive(true);
        canvas.SetActive(false);
    }
    public void SelectQPWC()
    {
        selectedBurger = BurgerType.QPWC;
        recipe.SetRecipe(qpwcRecipe);
        Ingredientsqpwc.SetActive(true);
        canvas.SetActive(false);
    }

    public void SelectElMaco()
    {
        selectedBurger = BurgerType.ElMaco;
        recipe.SetRecipe (elMacoRecipe);
        Ingredients.SetActive(true);

        
        canvas.SetActive(false);
    }

    
    
    public void burgerready()
    {
        EventSystem.current.enabled = false;
        recipe.LogPerformance();
    }


}
