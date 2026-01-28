using System.Collections.Generic;

using TMPro;
using UnityEngine;


public class CheckItem : MonoBehaviour
{
    [SerializeField] List<Transform> forbiddenItems;
    [SerializeField] TextMeshProUGUI burgeuitext;
 

    public List<string> bigMacRecipe = new List<string>()
    {
        "Beef",
        "Cheese",
        "Lettuce",
        "Sauce",
        "MiddleBun",
        "Beef",
        "Cheese",
        "onion",
        "pickled cucumber"
    };

    private void Awake()
    {
        Debug.Log("CheckItem heräsi: " + gameObject.name);
    }

    private int currentIndex = 0;
    public void TryAddIngredient(Transform clickedTransform, string ingredientName)
    {
        Debug.Log($"[{gameObject.name}] index = {currentIndex}");
        Debug.Log("toimii");
        string cleanText = burgeuitext.text.ToLower().Replace("\n", "Tilaus: ");
        Debug.Log("CLEAN TEXT: '" + cleanText + "'");

        if (cleanText.Contains("big mac"))
        {
            Debug.Log("CLEAN TEXT: '" + cleanText + "'");
            Debug.Log("Klikattiin: " + clickedTransform.name);

            foreach (var item in forbiddenItems)
            {
                Debug.Log("Forbidden listassa: " + item.name);
            }
            if (forbiddenItems.Contains(clickedTransform))
            {
                Debug.Log("?? Et voi lisätä tätä Big Maciin: " + clickedTransform.name);
                return;
            }
        }

        // 3?? Normaali reseptitarkistus
        if (currentIndex >= bigMacRecipe.Count)
        {
            Debug.Log("Big Mac on jo valmis!");
            return;
        }

        if (string.Equals(
     ingredientName,
     bigMacRecipe[currentIndex],
     System.StringComparison.OrdinalIgnoreCase))

        {
            Debug.Log("? Oikea ainesosa: " + ingredientName);
            currentIndex++;
        }
        else
        {
            Debug.Log("? VÄÄRÄ AINESOSA! Klikattiin: "
                + ingredientName
                + " | Odotettiin: "
                + bigMacRecipe[currentIndex]);
        }
    }
    

}
