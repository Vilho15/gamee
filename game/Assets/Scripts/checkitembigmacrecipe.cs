using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class CheckItembigmacrecipe : MonoBehaviour
{
    [SerializeField] GameObject button;
    [SerializeField] GameObject bigmac;
    [SerializeField] GameObject uipanel;
    [SerializeField] GameObject wrongingredientsclicksrawimage;
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI whatburgertext;
    [SerializeField] private TextMeshProUGUI wrongingredientsclickstext;
    [SerializeField] private int wrongIngredientClicks = 0;
    private List<string> addedIngredients = new List<string>();
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

        if (whatburgertext == null || !whatburgertext.text.ToLower().Contains("big mac"))
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
                wrongIngredientClicks++;
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
            LogPerformance();
            bigmac.SetActive(true);
            uipanel.SetActive(true);
        }
    }
    void ShowMessage(string message)
    {
        wrongingredientsclicksrawimage.SetActive(true);

        if (wrongingredientsclickstext != null)
        {
            wrongingredientsclicksrawimage.SetActive(true);
            wrongingredientsclickstext.text = message;
        }
        wrongingredientsclicksrawimage.SetActive(true);

        Debug.Log(message); // voit poistaa jos et halua konsoliin
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

    public void ResetRecipe()
    {
        currentIndex = 0;
        
        addedIngredients.Clear();

       

    }


}
