using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemcheck : MonoBehaviour
{
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI burgeuitext;
    private bool bigMacInitialized = false;
    [SerializeField] GameObject ui;
    [SerializeField] GameObject bigmac;
    private List<string> addedIngredients = new List<string>();
    public static List<string> QpwcRecipe = new List<string>()
    {
        "Bottom Bun",
        "Cheese",
        "Beef",
        "Onion",
        "Pickeled Cucumber",
        "ketchup",
        "Senap",
        "TopBun"


    };
    public void TryAddIngredient()
    {
        // ?? 0?? TARKISTUS: Onko valittuna Big Mac
        if (burgeuitext == null || !burgeuitext.text.ToLower().Contains("quarter pounder with cheese"))
        {
            Debug.Log("?? Valittuna ei ole qpwr – ainesosia ei käsitellä");
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
        Debug.Log($"? Lisätty qpwr: {ingredientName}");

        // ?? 4?? ONKO KAIKKI LISÄTTY
        if (addedIngredients.Count >= QpwcRecipe.Count)
        {
            Debug.Log("?? KAIKKI ainesosat lisätty – qpwr on valmis!");
            bigmac.SetActive(true);
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
