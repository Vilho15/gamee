using UnityEngine;
using UnityEngine.EventSystems;

// T‰m‰ scripti mahdollistaa objektin raahaamisen UI:ssa
public class Grabitem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Objektin RectTransform
    private RectTransform rectTransform;

    // Canvas johon objekti kuuluu
    private Canvas canvas;

    // CanvasGroup tarvitaan raycastien hallintaan
    private CanvasGroup canvasGroup;

    // Kutsutaan kun objekti luodaan
    void Awake()
    {
        // Haetaan RectTransform komponentti
        rectTransform = GetComponent<RectTransform>();

        // Haetaan parent canvas
        canvas = GetComponentInParent<Canvas>();

        // Haetaan CanvasGroup komponentti
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Kutsutaan kun raahaus alkaa
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Est‰‰ objektia blokkaamasta raycasteja dragin aikana
        // T‰m‰ mahdollistaa droppauksen muiden objektien p‰‰lle
        canvasGroup.blocksRaycasts = false;
    }

    // Kutsutaan jatkuvasti raahauksen aikana
    public void OnDrag(PointerEventData eventData)
    {
        // Liikuttaa objektia hiiren mukana
        // scaleFactor korjaa canvaksen skaalauksen
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // Kutsutaan kun raahaus loppuu
    public void OnEndDrag(PointerEventData eventData)
    {
        // Raycastit takaisin p‰‰lle
        canvasGroup.blocksRaycasts = true;
    }
}