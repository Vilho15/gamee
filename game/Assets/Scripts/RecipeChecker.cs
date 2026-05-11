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
    [SerializeField] private float stackHeight = 25f;

    [Header("Optional")]
    [SerializeField] private List<IngredientType> forbiddenIngredients;

    private List<IngredientType> addedIngredients = new();
    private int currentStackIndex = 0;
    private int wrongClicks = 0;
    private int numberofclicks;
    private int correctClicks = 0;
    private float currentY = 0f;
    [Header("Money")]
    public  TextMeshProUGUI moneyText;


    public static int money = 0;

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
    private void Start()
    {
        moneyText.text = "$ " + money;
    }
    void AddMoney(int amount)
    {
        money += amount;

        if (moneyText != null)
        {
            moneyText.text = "$ " + money;
        }
    }
    public void TryAddIngredientFromDrop(IngredientButton btn)
    {
        IngredientType type = btn.type;

        // Spawnataan aina burgeriin
        SpawnIngredient(btn.prefab);

        // V‰‰r‰ ainesosa
        if (!IsIngredientValid(type))
        {
            wrongClicks++;
            Debug.Log("Ei kuulu reseptiin: " + type);
            return;
        }

        int maxAllowed = CountInRecipe(type);
        int alreadyAdded = CountAdded(type);

        // Liikaa samaa
        if (alreadyAdded >= maxAllowed)
        {
            Debug.Log("Liikaa t‰t‰ ainesosaa: " + type);
            wrongClicks++;
            return;
        }

        // Tarkista j‰rjestys
        if (!IsCorrectOrder(type))
        {
            wrongClicks++;

            Debug.Log("V‰‰r‰ j‰rjestys! Odotettiin: " +
                currentRecipe.ingredients[addedIngredients.Count]);

            return;
        }

        // Oikea ingredient oikeaan aikaan
        addedIngredients.Add(type);
        correctClicks++;

        Debug.Log("Oikea ingredient oikeassa j‰rjestyksess‰: " + type);
    }
    float GetSuccessRate()
    {
        return (float)correctClicks / currentRecipe.ingredients.Count;
    }
    void SpawnIngredient(GameObject prefab)
    {
        GameObject newItem = Instantiate(prefab, burgerStack);

        RectTransform rect = newItem.GetComponent<RectTransform>();

        float randomX = Random.Range(-5f, 5f);

        // Asetetaan uusi kerros
        rect.anchoredPosition = new Vector2(randomX, currentY);

        // Lasketaan seuraavan kerroksen paikka
        currentY += rect.sizeDelta.y * 0.35f;

        newItem.transform.SetAsLastSibling();
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
        int earnedMoney = 0;

        if (successRate == 1f && wrongClicks == 0)
        {
            msg = "S (T‰ydellinen!)";
            earnedMoney = 100;
        }
        else if (successRate >= 0.8f && wrongClicks <= 1)
        {
            msg = "A";
            earnedMoney = 75;
        }
        else if (successRate >= 0.6f)
        {
            msg = "B";
            earnedMoney = 50;
        }
        else if (successRate >= 0.4f)
        {
            msg = "C";
            earnedMoney = 25;
        }
        else
        {
            msg = "Hyl‰tty";
            earnedMoney = 0;
        }

        AddMoney(earnedMoney);

        ShowMessage(msg + "\n+" + earnedMoney + "$");

        Debug.Log($"Oikein: {correctClicks}/{currentRecipe.ingredients.Count}");
        Debug.Log($"V‰‰rin: {wrongClicks}");
        Debug.Log($"Arvosana: {msg}");
        Debug.Log($"Rahaa saatu: {earnedMoney}$");
    }

    bool IsCorrectOrder(IngredientType ingredient)
    {
        // Mik‰ indeksi seuraavaksi pit‰isi t‰ytt‰‰
        int currentIndex = addedIngredients.Count;

        // Jos resepti loppui jo
        if (currentIndex >= currentRecipe.ingredients.Count)
            return false;

        // Tarkista oikea ingredient
        return currentRecipe.ingredients[currentIndex] == ingredient;
    }
    void ShowMessage(string message)
    {
       
        if (feedbackBox != null)
            feedbackBox.SetActive(true);

        if (feedbackText != null)
            feedbackText.text = message;
      
    }
}