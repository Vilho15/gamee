using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientButton : MonoBehaviour
{
    public string ingredientName;
    public CheckItem checkItem;

    public void OnClick()
    {
        checkItem.TryAddIngredient(transform, ingredientName);
    }
}