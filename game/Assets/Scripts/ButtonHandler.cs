using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // El Maco ingredientit 
    [SerializeField] GameObject Ingredients;

    // Yleinen nappiobjekti
    [SerializeField] GameObject Button;

    // Big Mac ingredientit 
    [SerializeField] GameObject Ingredientsbigmac;

    // Quarter Pounder with Cheese ingredientit 
    [SerializeField] GameObject Ingredientsqpwc;

    // Burgerien tekstit
    [SerializeField] TextMeshProUGUI textbigmac;
    [SerializeField] TextMeshProUGUI textqpwc;
    [SerializeField] TextMeshProUGUI textelmaco;

    // Aloituscanvas
    [SerializeField] GameObject canvas;

    // Viittaus scriptiin RecipeChecker
    [SerializeField] RecipeChecker recipe;

    // Reseptit ScriptableObjecteina
    [SerializeField] private Recipe bigMacRecipe;
    [SerializeField] private Recipe qpwcRecipe;
    [SerializeField] private Recipe elMacoRecipe;

    // Seuraava nappi
    [SerializeField] private GameObject nextbutton;

    // UI:n blokkaus kun burgeri valmis
    [SerializeField] private GameObject inputBlocker;

    //viittaus ingredientbutton scriptiin
    [SerializeField] private IngredientButton ingredientbutton;

    // Enum burgerityypeille
    public enum BurgerType
    {
        None,
        BigMac,
        QPWC,
        ElMaco
    }

    // Tallentaa t‰ll‰ hetkell‰ valitun burgerin
    public static BurgerType selectedBurger = BurgerType.None;

    // K‰ynnist‰‰ peliscenen
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // Menee settings-sceneen
    public void gosettings()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Menee menu-sceneen
    public void gomenu()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // Valitaan Big Mac
    public void SelectBigMac()
    {
        // Tallennetaan valinta
        selectedBurger = BurgerType.BigMac;

        // Asetetaan oikea resepti RecipeCheckeriin
        recipe.SetRecipe(bigMacRecipe);

        // N‰ytet‰‰n Big Mac ingredientit
        Ingredientsbigmac.SetActive(true);

        // Piilotetaan  canvas, ettei se ole tiell‰ peliss‰
        canvas.SetActive(false);
    }

    // Valitaan Quarter Pounder with Cheese
    public void SelectQPWC()
    {
        // Tallennetaan valinta
        selectedBurger = BurgerType.QPWC;

        // Asetetaan resepti
        recipe.SetRecipe(qpwcRecipe);

        // N‰ytet‰‰n quarter  pounder with cheese ingredientit
        Ingredientsqpwc.SetActive(true);

        // Piilotetaan  canvas, ettei se ole tiell‰ peliss‰
        canvas.SetActive(false);
    }

    // Valitaan El Maco
    public void SelectElMaco()
    {
        // Tallennetaan valinta
        selectedBurger = BurgerType.ElMaco;

        // Asetetaan resepti
        recipe.SetRecipe(elMacoRecipe);

        // N‰ytet‰‰n El Maco ingredientit
        Ingredients.SetActive(true);

        // Piilotetaan  canvas, ettei se ole tiell‰ peliss‰
        canvas.SetActive(false);
    }

    // Kutsutaan kun pelaaja painaa burger ready nappia eli h‰nen mielest‰‰n burgeri on valmis
    public void burgerready()
    {
        // Jos RecipeChecker puuttuu -> lopeta
        if (recipe == null)
        {
            return;
        }

        //estet‰‰n ettei tiettyj‰ alueita pysty klikata 
        inputBlocker.SetActive(true);

        // Lasketaan score / arvosana
        recipe.LogPerformance();

        // N‰ytet‰‰n next-nappi
        nextbutton.SetActive(true);
    }

    // Siirtyy seuraavaan kierrokseen
    public void next()
    {
        // Ladataan peli scene uudestaan
        SceneManager.LoadSceneAsync(1);

        // P‰ivitet‰‰n rahat UI:hin
        recipe.moneyText.text = "$ " + recipe.moneyText.text;
    }
}