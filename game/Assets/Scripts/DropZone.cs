using UnityEngine;
using UnityEngine.EventSystems;

// Tämä scripti toimii droppialueena UI:ssa
public class DropZone : MonoBehaviour, IDropHandler
{
    // Viittaus RecipeChecker scriptiin
    [SerializeField] RecipeChecker checker;

    // Kutsutaan kun objekti pudotetaan tämän alueen päälle
    public void OnDrop(PointerEventData eventData)
    {
        // Haetaan dragattu objekti
        GameObject dragged = eventData.pointerDrag;

        // Jos mitään ei dragattu -> lopeta
        if (dragged == null) return;

        // Yritetään hakea IngredientButton komponentti
        IngredientButton btn = dragged.GetComponent<IngredientButton>();

        // Jos komponenttia ei löydy
        if (btn == null)
        {
            Debug.Log("Ei IngredientButton!");

            return;
        }

        // Näyttää konsolissa mikä ingredient pudotettiin
        Debug.Log("Dropattu: " + btn.type);

        // Kutsuu RecipeCheckerin funktiota
        // joka tarkistaa ingredientin
        checker.TryAddIngredientFromDrop(btn);
    }
}