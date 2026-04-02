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
    [SerializeField] private GameObject endscreenuipanel;
    [SerializeField] private GameObject qpwc;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GameObject text;

    [Header("Stack Settings")]
    [SerializeField] private Transform burgerStack;   
    [SerializeField] private float stackHeight = 0.2f;

    [SerializeField] private List<GameObject> ingredientPrefabs;
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

        // ? tarkista kielletyt
        foreach (Transform forbidden in forbiddenItems)
        {
            if (forbidden.name == ingredientName)
            {
                wrongIngredientClicks++;
                Debug.Log($"Et voi lisätä tätä qpwc: {ingredientName}");
                return;
            }
        }

        // ? estä duplikaatit
        if (addedIngredients.Contains(ingredientName))
        {
            Debug.Log($"{ingredientName} on jo lisätty");
            return;
        }

        // ? lisätään listaan
        addedIngredients.Add(ingredientName);
        Debug.Log($"Lisätty qpwc: {ingredientName}");

        // ?? LUODAAN KUVA PREFABISTA
        GameObject prefab = ingredientPrefabs.Find(p => p.name == ingredientName);

        if (prefab != null)
        {
            GameObject newItem = Instantiate(prefab, burgerStack);

            RectTransform newRect = newItem.GetComponent<RectTransform>();
            RectTransform clickedRect = clickedTransform.GetComponent<RectTransform>();

            // ?? ilmestyy napin kohdalle
            newRect.position = clickedRect.position;

            // ?? siirtyy stackiin (pinoutuu)
            newRect.anchoredPosition = new Vector2(0, currentStackIndex * stackHeight);

            newRect.SetAsLastSibling();
            currentStackIndex++;
        }
        else
        {
            Debug.LogError("Prefabia ei löytynyt: " + ingredientName);
        }

        // ? tarkista valmistuminen
        if (addedIngredients.Count >= QpwcRecipe.Count)
        {
            Debug.Log("KAIKKI ainesosat lisätty – qpwc on valmis!");
            LogPerformance();
            qpwc.SetActive(true);
            endscreenuipanel.SetActive(true);
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
