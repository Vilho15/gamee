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
    [SerializeField] private int wrongIngredientClicks = 0;
    [SerializeField] private int maxWrongClicks = 7;
    [SerializeField] GameObject text;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] GameObject button;
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
        
            Debug.Log("?? Valittuna ei ole el maco  ñ ainesosia ei k‰sitell‰");
            return;
        }

        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked == null)
        {
            Debug.LogError("? Klikattua UI-elementti‰ ei lˆytynyt");
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
                wrongIngredientClicks++;
                Debug.Log($"? Et voi lis‰t‰ t‰t‰ Èl macoon: {ingredientName}");
                CheckWrongClicks();
                return;
            }
        }

        // ?? 2?? ONKO JO LISƒTTY
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"?? {ingredientName} on jo lis‰tty el macoon");
            return;
        }

        // ?? 3?? LISƒTƒƒN AINESOSA
        addedIngredients.Add(ingredientName);
        Debug.Log($"? Lis‰tty el macoon: {ingredientName}");

        // ?? 4?? ONKO KAIKKI LISƒTTY
        if (addedIngredients.Count >= elmacoRecipe.Count)
        {
            Debug.Log("?? KAIKKI ainesosat lis‰tty ñ el maco on valmis!");
            LogPerformance();
            elmaco.SetActive(true);
            ui.SetActive(true);
        }
    }
    void CheckWrongClicks()
    {
        if (wrongIngredientClicks >= maxWrongClicks)
        {
            Debug.Log("?? Liikaa v‰‰ri‰ ainesosia! Ep‰onnistuit.");

            // t‰nne voi laittaa:
            // - game over ruudun
            // - UI-varoituksen
            // - resetin
        }
    }
    void ShowMessage(string message)
    {
        text.SetActive(true);

        if (feedbackText != null)
        {
            text.SetActive(true);
            feedbackText.text = message;
        }
        text.SetActive(true);

        Debug.Log(message); // voit poistaa jos et halua konsoliin
    }
    void LogPerformance()
    {
        if (wrongIngredientClicks == 0)
        {
            ShowMessage("?? T‰ydellinen suoritus ñ ei virheit‰!");
        }
        else if (wrongIngredientClicks == 1)
        {
            ShowMessage("?? Teit yhden virheen");
        }
        else if (wrongIngredientClicks == 2)
        {
            ShowMessage("?? Teit kaksi virhett‰");
        }
        else if (wrongIngredientClicks == 3)
        {
            ShowMessage("?? Teit kolme virhett‰");
        }
        else
        {
            ShowMessage("?? Teit nelj‰ tai enemm‰n virheit‰");
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
