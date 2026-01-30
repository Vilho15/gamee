using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class checkitemcheck : MonoBehaviour
{
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI burgeuitext;
    private bool bigMacInitialized = false;
    [SerializeField] GameObject elmaco;
    [SerializeField] GameObject ui;
    private List<string> addedIngredients = new List<string>();
    public static List<string> elmacoRecipe = new List<string>()
    {
        "Bottom Bun",
        "Beef",
        "Onion",
        "Cheese",
       "Tomatoes",
       "Lettuce",
       "Salsa",
       "Sauce",
       "TopBun"


    };
    public void TryAddIngredient()
    {
        // ?? 0?? TARKISTUS: Onko valittuna Big Mac
        if (burgeuitext == null || !burgeuitext.text.ToLower().Contains("el maco"))
        {
            Debug.Log("?? Valittuna ei ole el maco  – ainesosia ei käsitellä");
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
                Debug.Log($"? Et voi lisätä tätä él macoon: {ingredientName}");
                return;
            }
        }

        // ?? 2?? ONKO JO LISÄTTY
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"?? {ingredientName} on jo lisätty el macoon");
            return;
        }

        // ?? 3?? LISÄTÄÄN AINESOSA
        addedIngredients.Add(ingredientName);
        Debug.Log($"? Lisätty el macoon: {ingredientName}");

        // ?? 4?? ONKO KAIKKI LISÄTTY
        if (addedIngredients.Count >= elmacoRecipe.Count)
        {
            Debug.Log("?? KAIKKI ainesosat lisätty – el maco on valmis!");
            elmaco.SetActive(true);
            ui.SetActive(true);
        }
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
