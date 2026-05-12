using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RecipeChecker : MonoBehaviour
{
    // -------------------------
    // RESEPTI
    // -------------------------

    [Header("Recipe")]

    // T‰ll‰ hetkell‰ k‰ytˆss‰ oleva resepti
    [SerializeField] private Recipe currentRecipe;


    // -------------------------
    // UI
    // -------------------------

    [Header("UI")]

    // Teksti johon n‰ytet‰‰n palaute
    [SerializeField] private TextMeshProUGUI feedbackText;

    //  laatikko,johon tulee teksti burgerin palautteesta
    [SerializeField] private GameObject feedbackBox;

    // Canvas viittaus
    [SerializeField] private GameObject canvas;


    // -------------------------
    // BURGER STACK
    // -------------------------

    [Header("Stack")]

    // Parent objekti johon ingredientit spawnataan
    [SerializeField] private Transform burgerStack;

    // Stackin korkeus
    [SerializeField] private float stackHeight = 25f;


    // -------------------------
    // OPTIONAL
    // -------------------------

    [Header("Optional")]

    // Lista kielletyist‰ aineksista
    [SerializeField] private List<IngredientType> forbiddenIngredients;


    // -------------------------
    // PELIN MUUTTUJAT
    // -------------------------

    // Lista hyv‰ksytyist‰ aineksista
    private List<IngredientType> addedIngredients = new();

    // Nykyinen stack index
    private int currentStackIndex = 0;

    // V‰‰rien klikkausten m‰‰r‰
    private int wrongClicks = 0;

    // Oikeiden klikkausten m‰‰r‰
    private int correctClicks = 0;

    // Nykyinen Y-position stackissa
    private float currentY = 0f;

    // Lista kaikista visuaalisesti lis‰tyist‰ aineksista
    private List<IngredientType> visualIngredients = new();


    // -------------------------
    // MONEY
    // -------------------------

    [Header("Money")]

    // Raha UI teksti
    public TextMeshProUGUI moneyText;

    // Pelaajan rahat
    public static int money = 0;


    // -------------------------
    // TARKISTAA KUULUUKO INGREDIENT RESEPTIIN
    // -------------------------

    public bool IsIngredientValid(IngredientType ingredient)
    {
        return currentRecipe.ingredients.Contains(ingredient);
    }


    // -------------------------
    // ASETTAA UUDEN RESEPTIN
    // -------------------------

    public void SetRecipe(Recipe newRecipe)
    {
        // Vaihdetaan nykyinen resepti
        currentRecipe = newRecipe;

        // Resetoi kaikki muuttujat
        addedIngredients.Clear();
        currentStackIndex = 0;
        wrongClicks = 0;
        correctClicks = 0;

        // Tulostaa reseptin nimen konsoliin
        Debug.Log("Nykyinen resepti: " + newRecipe.name);
    }


    // -------------------------
    // START
    // -------------------------

    private void Start()
    {
        // P‰ivitt‰‰ rahat UI:hin
        moneyText.text = "$ " + money;
    }


    // -------------------------
    // LISƒƒ RAHAA
    // -------------------------

    void AddMoney(int amount)
    {
        // Lis‰‰ rahaa
        money += amount;

        // P‰ivitt‰‰ tekstin
        if (moneyText != null)
        {
            moneyText.text = "$ " + money;
        }
    }


    // -------------------------
    // YRITTƒƒ LISƒTƒ INGREDIENTIN
    // -------------------------

    public void TryAddIngredientFromDrop(IngredientButton btn)
    {
        // Haetaan ingredientin tyyppi
        IngredientType type = btn.type;

        // Lis‰t‰‰n visuaaliseen listaan
        visualIngredients.Add(type);

        // Debuggaa kaikki ingredientit
        foreach (IngredientType i in visualIngredients)
        {
            Debug.Log(i);
        }

        // Spawnataan ingredient burgeriin
        SpawnIngredient(btn.prefab);


        // -------------------------
        // TARKISTA KUULUUKO RESEPTIIN
        // -------------------------

        if (!IsIngredientValid(type))
        {
            wrongClicks++;

            Debug.Log("Ei kuulu reseptiin: " + type);

            return;
        }


        // -------------------------
        // TARKISTA MƒƒRƒ
        // -------------------------

        int maxAllowed = CountInRecipe(type);

        int alreadyAdded = CountAdded(type);

        // Jos samaa ingredienti‰ liikaa
        if (alreadyAdded >= maxAllowed)
        {
            Debug.Log("Liikaa t‰t‰ ainesosaa: " + type);

            wrongClicks++;

            return;
        }


        // -------------------------
        // TARKISTA JƒRJESTYS
        // -------------------------

        if (!IsCorrectOrder(type))
        {
            wrongClicks++;

            Debug.Log("V‰‰r‰ j‰rjestys! Odotettiin: " +
                currentRecipe.ingredients[addedIngredients.Count]);

            return;
        }


        // -------------------------
        // BUNIEN TARKISTUS
        // -------------------------

        // Tarkistaa ett‰ ensimm‰inen ingredient on BottomBun
        bool hasBottomBun =
            visualIngredients.Count > 0 &&
            visualIngredients[0] == IngredientType.BottomBun;

        // Tarkistaa ett‰ viimeinen ingredient on TopBun
        bool hasTopBun =
            visualIngredients.Count > 0 &&
            visualIngredients[visualIngredients.Count - 1] == IngredientType.TopBun;


        // -------------------------
        // OIKEA INGREDIENT
        // -------------------------

        // Lis‰‰ hyv‰ksyttyihin ingredientteihin
        addedIngredients.Add(type);

        // Lis‰‰ oikeiden klikkausten m‰‰r‰‰
        correctClicks++;

        Debug.Log("Oikea ingredient oikeassa j‰rjestyksess‰: " + type);
    }


    // -------------------------
    // LASKEE ONNISTUMISPROSENTIN
    // -------------------------

    float GetSuccessRate()
    {
        return (float)correctClicks / currentRecipe.ingredients.Count;
    }


    // -------------------------
    // SPAWNAA INGREDIENTIN BURGERIIN
    // -------------------------

    void SpawnIngredient(GameObject prefab)
    {
        // Luo uusi ingredient objekti
        GameObject newItem = Instantiate(prefab, burgerStack);

        // Haetaan RectTransform
        RectTransform rect = newItem.GetComponent<RectTransform>();

        // Satunnainen X offset
        float randomX = Random.Range(-5f, 5f);

        // Asetetaan paikka
        rect.anchoredPosition = new Vector2(randomX, currentY);

        // Nostetaan seuraavan ingredientin paikkaa
        currentY += rect.sizeDelta.y * 0.35f;

        // Asetetaan viimeiseksi childiksi
        newItem.transform.SetAsLastSibling();
    }


    // -------------------------
    // LASKEE KUINKA MONTA KERTAA
    // INGREDIENT ON RESEPTISSƒ
    // -------------------------

    int CountInRecipe(IngredientType type)
    {
        return currentRecipe.ingredients.FindAll(i => i == type).Count;
    }


    // -------------------------
    // LASKEE KUINKA MONTA KERTAA
    // INGREDIENT ON JO LISƒTTY
    // -------------------------

    int CountAdded(IngredientType type)
    {
        return addedIngredients.FindAll(i => i == type).Count;
    }


    // -------------------------
    // LASKEE SUORITUKSEN
    // -------------------------

    public void LogPerformance()
    {
        // Tarkistaa ett‰ burgerissa on jotain
        if (addedIngredients.Count == 0)
        {
            ShowMessage("Hyl‰tty\nBurgeri puuttuu!");

            return;
        }

        // Tarkistaa ett‰ visual ingredienttej‰ on olemassa
        if (visualIngredients.Count == 0)
        {
            ShowMessage("Hyl‰tty\nBurgeri puuttuu!");

            return;
        }


        // -------------------------
        // TARKISTA BUNIT
        // -------------------------

        // Ensimm‰inen ingredient pit‰‰ olla BottomBun
        bool hasBottomBun =
            visualIngredients[0] == IngredientType.BottomBun;

        // Viimeinen ingredient pit‰‰ olla TopBun
        bool hasTopBun =
            visualIngredients[visualIngredients.Count - 1] == IngredientType.TopBun;

        // Jos ei ole oikeita buneja -> automaattinen hylk‰ys
        if (!hasBottomBun || !hasTopBun)
        {
            ShowMessage("Hyl‰tty\nBurgerista puuttuu bunit!");

            Debug.Log("Automaattinen hylk‰ys: bunit v‰‰rin.");

            return;
        }


        // -------------------------
        // ARVOSANAN LASKENTA
        // -------------------------

        float successRate = GetSuccessRate();

        string msg;

        int earnedMoney = 0;

        // T‰ydellinen suoritus
        if (successRate == 1f && wrongClicks == 0)
        {
            msg = "S (T‰ydellinen!)";

            earnedMoney = 100;
        }

        // Hyv‰ suoritus
        else if (successRate >= 0.8f && wrongClicks <= 1)
        {
            msg = "A";

            earnedMoney = 75;
        }

        // Keskitaso
        else if (successRate >= 0.6f)
        {
            msg = "B";

            earnedMoney = 50;
        }

        // Heikompi
        else if (successRate >= 0.4f)
        {
            msg = "C";

            earnedMoney = 25;
        }

        // Huono suoritus
        else if (successRate >= 0.2)
        {
            msg = "D";

            earnedMoney = 5;
        }

        // T‰ysi fail
        else
        {
            msg = "Hyl‰tty";

            earnedMoney = 0;
        }


        // Lis‰‰ rahat
        AddMoney(earnedMoney);

        // N‰yt‰ viesti
        ShowMessage(msg + "\n+" + earnedMoney + "$");

        // Debug tiedot
        Debug.Log($"Oikein: {correctClicks}/{currentRecipe.ingredients.Count}");

        Debug.Log($"V‰‰rin: {wrongClicks}");

        Debug.Log($"Arvosana: {msg}");

        Debug.Log($"Rahaa saatu: {earnedMoney}$");
    }


    // -------------------------
    // TARKISTAA OIKEAN JƒRJESTYKSEN
    // -------------------------

    bool IsCorrectOrder(IngredientType ingredient)
    {
        // Seuraava oikea index
        int currentIndex = addedIngredients.Count;

        // Jos resepti loppui
        if (currentIndex >= currentRecipe.ingredients.Count)
            return false;

        // Tarkistaa vastaako ingredient resepti‰
        return currentRecipe.ingredients[currentIndex] == ingredient;
    }


    // -------------------------
    // NƒYTTƒƒ PALAUTEVIESTIN
    // -------------------------

    void ShowMessage(string message)
    {
        // N‰ytt‰‰ feedback boxin
        if (feedbackBox != null)
            feedbackBox.SetActive(true);

        // Vaihtaa tekstin
        if (feedbackText != null)
            feedbackText.text = message;
    }
}