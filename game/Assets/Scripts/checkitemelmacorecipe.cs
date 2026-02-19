using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class checkitemelmacorecipe: MonoBehaviour
{
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI whatportionuitext;
    [SerializeField] GameObject elmaco;
    [SerializeField] GameObject endscreenuipanel;
    [SerializeField] GameObject textbox;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private int wrongIngredientClicks = 0;
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
        
        if (whatportionuitext == null || !whatportionuitext.text.ToLower().Contains("el maco"))
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

      
        foreach (Transform forbidden in forbiddenItems)
        {
            if (forbidden == clickedTransform)
            {
                wrongIngredientClicks++;
                Debug.Log($"? Et voi lisätä tätä él macoon: {ingredientName}");
               
                return;
            }
        }

   
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"?? {ingredientName} on jo lisätty el macoon");
            return;
        }

    
        addedIngredients.Add(ingredientName);
        Debug.Log($"? Lisätty el macoon: {ingredientName}");

        
        if (addedIngredients.Count >= elmacoRecipe.Count)
        {
            Debug.Log("?? KAIKKI ainesosat lisätty – el maco on valmis!");
            LogPerformance();
            elmaco.SetActive(true);
            endscreenuipanel.SetActive(true);
        }
    }
    

    void ShowMessage(string message)
    {
        textbox.SetActive(true);

        if (feedbackText != null)
        {
            textbox.SetActive(true);
            feedbackText.text = message;
        }
        textbox.SetActive(true);

      
    }
    void LogPerformance()
    {
        if (wrongIngredientClicks == 0)
        {
            ShowMessage("?? Täydellinen suoritus – ei virheitä!");
        }
        else if (wrongIngredientClicks == 1)
        {
            ShowMessage("?? Teit yhden virheen");
        }
        else if (wrongIngredientClicks == 2)
        {
            ShowMessage("?? Teit kaksi virhettä");
        }
        else if (wrongIngredientClicks == 3)
        {
            ShowMessage("?? Teit kolme virhettä");
        }
        else
        {
            ShowMessage("?? Teit neljä tai enemmän virheitä");
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
