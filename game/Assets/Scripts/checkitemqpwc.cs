using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class checkitemqpwc : MonoBehaviour
{
    [Header("Recipe Settings")]
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] private TextMeshProUGUI whatburgertext;

    [Header("UI")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject bigmac;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject text;

    [Header("Stack Settings")]
    [SerializeField] private Transform burgerStack;   
    [SerializeField] private float stackHeight = 0.2f; 

    private int currentStackIndex = 0;
    private int wrongIngredientClicks = 0;
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



    void ShowMessage(string message)
    {
        if (feedbackText != null)
        {
            text.SetActive(true);
            feedbackText.text = message;
        }

        Debug.Log(message);
    }

    public void TryAddIngredient()
    {
        if (whatburgertext == null ||
            !whatburgertext.text.ToLower().Contains("quarter pounder with cheese"))
        {
            Debug.Log("Valittuna ei ole qpwc – ainesosia ei käsitellä");
            return;
        }

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked == null)
        {
            Debug.LogError("Klikattua UI-elementtiä ei löytynyt");
            return;
        }

        Transform clickedTransform = clicked.transform;
        string ingredientName = clickedTransform.name.Replace("(Clone)", "").Trim();

        Debug.Log($"Klikattiin nappia: {ingredientName}");

  
        foreach (Transform forbidden in forbiddenItems)
        {
            if (forbidden.name == ingredientName)
            {
                wrongIngredientClicks++;
                Debug.Log($"Et voi lisätä tätä qpwc: {ingredientName}");
                return;
            }
        }

        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"{ingredientName} on jo lisätty");
            return;
        }

        // Lisätään listaan
        addedIngredients.Add(ingredientName);
        Debug.Log($"Lisätty qpwc: {ingredientName}");

    
        clickedTransform.SetParent(burgerStack);

        clickedTransform.localPosition = new Vector3(
            0,
            currentStackIndex * stackHeight,
            0
        );

        currentStackIndex++;
      
        if (addedIngredients.Count >= QpwcRecipe.Count)
        {
            Debug.Log("KAIKKI ainesosat lisätty – qpwc on valmis!");
            LogPerformance();
            bigmac.SetActive(true);
            ui.SetActive(true);
        }
    }

    void LogPerformance()
    {
        if (wrongIngredientClicks == 0)
        {
            ShowMessage("Täydellinen suoritus – ei virheitä!");
        }
        else if (wrongIngredientClicks == 1)
        {
            ShowMessage("Teit yhden virheen");
        }
        else if (wrongIngredientClicks == 2)
        {
            ShowMessage("Teit kaksi virhettä");
        }
        else if (wrongIngredientClicks == 3)
        {
            ShowMessage("Teit kolme virhettä");
        }
        else
        {
            ShowMessage("Teit neljä tai enemmän virheitä");
        }
    }
}
