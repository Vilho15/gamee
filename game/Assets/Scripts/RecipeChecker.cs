using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeChecker : MonoBehaviour
{
    [Header("Recipe")]
    [SerializeField] private Recipe currentRecipe;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject feedbackBox;

    [Header("Stack")]
    [SerializeField] private Transform burgerStack;
    [SerializeField] private float stackHeight = 100f;

    [Header("Optional")]
    [SerializeField] private List<IngredientType> forbiddenIngredients;

    private List<IngredientType> addedIngredients = new();
    private int currentStackIndex = 0;
    private int wrongClicks = 0;

    // ?? TƒMƒ on se geneerinen tarkistus
    public bool IsIngredientValid(IngredientType ingredient)
    {
        return currentRecipe.ingredients.Contains(ingredient);
    }

 
    public void SetRecipe(Recipe newRecipe)
    {
        currentRecipe = newRecipe;

        // resetoi peli
        addedIngredients.Clear();
        currentStackIndex = 0;
        wrongClicks = 0;

        Debug.Log("Nykyinen resepti: " + newRecipe.name);
    }
    public void TryAddIngredientFromDrop(IngredientButton btn)
    {
        IngredientType type = btn.type;

        if (forbiddenIngredients.Contains(type))
        {
            wrongClicks++;
            Debug.Log("Kielletty: " + type);
            return;
        }

        if (!IsIngredientValid(type))
        {
            wrongClicks++;
            Debug.Log("Ei kuulu reseptiin: " + type);
            return;
        }

        if (addedIngredients.Contains(type))
        {
            Debug.Log("Jo lis‰tty: " + type);
            return;
        }

        addedIngredients.Add(type);

        SpawnIngredient(btn.prefab);
        Debug.Log("Lis‰tty dropista: " + type);

        if (addedIngredients.Count >= currentRecipe.ingredients.Count)
        {
            Debug.Log("Resepti valmis!");
            LogPerformance();
        }
    }
    void SpawnIngredient(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, burgerStack);

        RectTransform rect = newItem.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(0, currentStackIndex * stackHeight);

        currentStackIndex++;
    }

  public  void LogPerformance()
    {
        string msg = wrongClicks switch
        {
            0 => "T‰ydellinen suoritus!",
            1 => "Yksi virhe",
            2 => "Kaksi virhett‰",
            3 => "Kolme virhett‰",
            _ => "Paljon virheit‰"
        };

        ShowMessage(msg);
        Debug.Log(msg);
        Debug.Log("v‰‰r‰t ainesosat klikattu m‰‰r‰: " + wrongClicks);
    }

    void ShowMessage(string message)
    {
        if (feedbackBox != null)
            feedbackBox.SetActive(true);

        if (feedbackText != null)
            feedbackText.text = message;
    }
}