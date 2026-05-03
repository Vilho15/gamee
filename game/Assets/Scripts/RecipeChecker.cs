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

    // ?? TÄMÄ on se geneerinen tarkistus
    public bool IsIngredientValid(IngredientType ingredient)
    {
        return currentRecipe.ingredients.Contains(ingredient);
    }

    public void TryAddIngredient()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject;

        if (clicked == null) return;

        IngredientButton btn = clicked.GetComponent<IngredientButton>();

        if (btn == null) return;

        IngredientType type = btn.type;

        // ? forbidden check
        if (forbiddenIngredients.Contains(type))
        {
            wrongClicks++;
            Debug.Log("Kielletty: " + type);
            return;
        }

        // ? ei kuulu reseptiin
        if (!IsIngredientValid(type))
        {
            wrongClicks++;
            Debug.Log("Ei kuulu reseptiin: " + type);
            return;
        }

        // ? duplikaatti
        if (addedIngredients.Contains(type))
        {
            Debug.Log("Jo lisätty: " + type);
            return;
        }

        // ? lisätään
        addedIngredients.Add(type);

        SpawnIngredient(btn.prefab);
        Debug.Log("lisätty: " + type);

        // ? valmis?
        if (addedIngredients.Count >= currentRecipe.ingredients.Count)
        {
            Debug.Log("Resepti valmis!");
            LogPerformance();
        }
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
    void SpawnIngredient(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, burgerStack);

        RectTransform rect = newItem.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(0, currentStackIndex * stackHeight);

        currentStackIndex++;
    }

    void LogPerformance()
    {
        string msg = wrongClicks switch
        {
            0 => "Täydellinen suoritus!",
            1 => "Yksi virhe",
            2 => "Kaksi virhettä",
            3 => "Kolme virhettä",
            _ => "Paljon virheitä"
        };

        ShowMessage(msg);
    }

    void ShowMessage(string message)
    {
        if (feedbackBox != null)
            feedbackBox.SetActive(true);

        if (feedbackText != null)
            feedbackText.text = message;
    }
}