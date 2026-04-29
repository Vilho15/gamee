using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class CheckItembigmacrecipe : MonoBehaviour
{
   
    [SerializeField] GameObject bigmac;
    [SerializeField] GameObject uipanel;
    [SerializeField] GameObject wrongingredientsclicksrawimage;
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI whatburgertext;
    [Header("Stack Settings")]
    [SerializeField] private Transform burgerStack;
    [SerializeField] private float stackHeight = 0.2f;

    private int currentStackIndex = 0;
    [SerializeField] private TextMeshProUGUI wrongingredientsclickstext;
    [SerializeField] private int wrongIngredientClicks = 0;
    private List<string> addedIngredients = new List<string>();
    [SerializeField] private TMP_InputField inputField;
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Joku tuli areaan: " + other.name);

        string ingredientName = other.name.Replace("(Clone)", "").Trim();

        Debug.Log("Tunnistettu ainesosa: " + ingredientName);

        // Esim: tarkista resepti
        if (bigMacRecipe.Contains(ingredientName))
        {
            Debug.Log("Tämä kuuluu Big Maciin!");
        }
        else
        {
            Debug.Log("Väärä ainesosa!");
        }
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

     
        foreach (Transform forbidden in forbiddenItems)
        {
            if (forbidden == clickedTransform)
            {
                wrongIngredientClicks++;
                Debug.Log($"? Et voi lisätä tätä Big Maciin: {ingredientName}");
                return;
            }
        }

      
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"?? {ingredientName} on jo lisätty Big Maciin");
            return;
        }

       
        addedIngredients.Add(ingredientName);
        Debug.Log($"? Lisätty Big Maciin: {ingredientName}");
        clickedTransform.SetParent(burgerStack);

        clickedTransform.localPosition = new Vector3(
            0,
            currentStackIndex * stackHeight,
            0
        );

        currentStackIndex++;

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

        Debug.Log(message);
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
