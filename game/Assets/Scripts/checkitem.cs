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

   

    private int currentIndex = 0;
    public void TryAddIngredient(Transform clickedTransform, string ingredientName)
    {            
        Debug.Log("toimii");
        string cleanText = burgeuitext.text.ToLower().Replace("\n", " ");

        if (cleanText.Contains("big mac"))
        {
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

        if (ingredientName == bigMacRecipe[currentIndex])
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
