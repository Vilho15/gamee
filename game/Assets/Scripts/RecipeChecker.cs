using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RecipeChecker : MonoBehaviour
{
    [Header("Recipe")]
    [SerializeField] private Recipe currentRecipe;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject feedbackBox;
    [SerializeField]private GameObject canvas;

    [Header("Stack")]
    [SerializeField] private Transform burgerStack;
    [SerializeField] private float stackHeight = 100f;

    [Header("Optional")]
    [SerializeField] private List<IngredientType> forbiddenIngredients;

    private List<IngredientType> addedIngredients = new();
    private int currentStackIndex = 0;
    private int wrongClicks = 0;
    private int numberofclicks;
    private int correctClicks = 0;


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
        correctClicks = 0;

        Debug.Log("Nykyinen resepti: " + newRecipe.name);
    }
    public void TryAddIngredientFromDrop(IngredientButton btn)
    {
        IngredientType type = btn.type;



        if(!IsIngredientValid(type))
{
            wrongClicks++;
            Debug.Log("Ei kuulu reseptiin: " + type);
            return;
        }
        int maxAllowed = CountInRecipe(type);
        int alreadyAdded = CountAdded(type);

        if (alreadyAdded >= maxAllowed)
        {
            Debug.Log("Liikaa t‰t‰ ainesosaa: " + type);
            wrongClicks++;
            return;
        }
        // ? OIKEA VALINTA
        addedIngredients.Add(type);
        correctClicks++;

        SpawnIngredient(btn.prefab);
        Debug.Log("Lis‰tty dropista: " + type);
    }
    float GetSuccessRate()
    {
        return (float)correctClicks / currentRecipe.ingredients.Count;
    }
    void SpawnIngredient(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, burgerStack);

        RectTransform rect = newItem.GetComponent<RectTransform>();

        rect.anchoredPosition = new Vector2(0, currentStackIndex * stackHeight);

        currentStackIndex++;
    }
    int CountInRecipe(IngredientType type)
    {
        return currentRecipe.ingredients.FindAll(i => i == type).Count;
    }

    int CountAdded(IngredientType type)
    {
        return addedIngredients.FindAll(i => i == type).Count;
    }
    public void LogPerformance()
    {
        float successRate = GetSuccessRate();

        string msg;

        if (successRate == 1f && wrongClicks == 0)
            msg = "S (T‰ydellinen!)";
        else if (successRate >= 0.8f && wrongClicks <= 1)
            msg = "A";
        else if (successRate >= 0.6f)
            msg = "B";
        else if (successRate >= 0.4f)
            msg = "C";
        else
            msg = "Hyl‰tty";

        ShowMessage(msg);

        Debug.Log($"Oikein: {correctClicks}/{currentRecipe.ingredients.Count}");
        Debug.Log($"V‰‰rin: {wrongClicks}");
        Debug.Log($"Arvosana: {msg}");
       
    }
   

    void ShowMessage(string message)
    {
       
        if (feedbackBox != null)
            feedbackBox.SetActive(true);

        if (feedbackText != null)
            feedbackText.text = message;
      
    }
}