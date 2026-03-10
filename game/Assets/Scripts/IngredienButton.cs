using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientButton : MonoBehaviour
{
    public string ingredientName;
    public CheckItembigmacrecipe checkItembigmacrecipe;
    public checkitemelmacorecipe checkitemelmacorecipe;
    public checkitemqpwc checkitemqpwc;

    public void OnClick()
    {
        checkItembigmacrecipe.TryAddIngredient();
        checkitemqpwc.TryAddIngredient();
        checkitemelmacorecipe.TryAddIngredient();
    }
}