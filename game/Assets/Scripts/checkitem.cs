using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class CheckItem : MonoBehaviour
{
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI burgeuitext;
    private bool bigMacInitialized = false;
    [SerializeField] GameObject bigmac;
    private List<string> addedIngredients = new List<string>();
    [SerializeField] GameObject ui;
    public static List<string> bigMacRecipe = new List<string>()
    {
        "Bottom Bun",
        "Lettuce",
        "Cheese",
        "Beef",
        "Sauce",
        "Bun2",
        "Pickeled Cucumber",  
        "Onion",
        "TopBun"


    };

    private void Awake()
    {
        Debug.Log("CheckItem heräsi: " + gameObject.name);
        Debug.Log($"CheckItem heräsi: {gameObject.name} | ID: {GetInstanceID()}");
    }
    

    private static int currentIndex = 0;

    public void TryAddIngredient()
    {
        // ?? 0?? TARKISTUS: Onko valittuna Big Mac
        if (burgeuitext == null || !burgeuitext.text.ToLower().Contains("big mac"))
        {
            Debug.Log("?? Valittuna ei ole Big Mac – ainesosia ei käsitellä");
            return;
        }

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked == null)
        {
            Debug.LogError("? Klikattua UI-elementtiä ei löytynyt");
            return;
        }

        Transform clickedTransform = clicked.transform;
        string ingredientName = clickedTransform.name.Replace("(Clone)", "").Trim();

        Debug.Log($"Klikattiin nappia: {ingredientName}");

        // ?? 1?? KIELLETTY TARKISTUS
        foreach (Transform forbidden in forbiddenItems)
        {
            if (forbidden == clickedTransform)
            {
                Debug.Log($"? Et voi lisätä tätä Big Maciin: {ingredientName}");
                return;
            }
        }

        // ?? 2?? ONKO JO LISÄTTY
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"?? {ingredientName} on jo lisätty Big Maciin");
            return;
        }

        // ?? 3?? LISÄTÄÄN AINESOSA
        addedIngredients.Add(ingredientName);
        Debug.Log($"? Lisätty Big Maciin: {ingredientName}");

        // ?? 4?? ONKO KAIKKI LISÄTTY
        if (addedIngredients.Count >= bigMacRecipe.Count)
        {
            Debug.Log("?? KAIKKI ainesosat lisätty – Big Mac on valmis!");
            bigmac.SetActive(true);
            ui.SetActive(true);
        }
    }


    public void ResetRecipe()
    {
        currentIndex = 0;
        bigMacInitialized = false;
        addedIngredients.Clear();

        Debug.Log("Resepti resetattu");
        Debug.Log("index: " + currentIndex);
    }


}
