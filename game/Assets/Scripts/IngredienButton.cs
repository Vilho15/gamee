using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientButton : MonoBehaviour
{
    public string ingredientName;
    public CheckItembigmacrecipe checkItem;

    public void OnClick()
    {
        checkItem.TryAddIngredient();
    }
}