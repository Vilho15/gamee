using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    [SerializeField] RecipeChecker checker;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;

        if (dragged == null) return;

        IngredientButton btn = dragged.GetComponent<IngredientButton>();

        if (btn == null)
        {
            Debug.Log("Ei IngredientButton!");
            return;
        }

        Debug.Log("Dropattu: " + btn.type);

        

        checker.TryAddIngredientFromDrop(btn);
    }
}