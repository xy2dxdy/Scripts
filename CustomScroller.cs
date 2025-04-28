using UnityEngine;
using UnityEngine.EventSystems;

public class CustomScroller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform content;
    private Vector2 lastMousePosition;
    private Vector2 velocity;
    private bool isDragging;

    void Update()
    {
        if (!isDragging)
        {
            content.anchoredPosition += velocity * Time.deltaTime;
            velocity *= 0.95f; // Снижает скорость для плавного замедления
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        lastMousePosition = eventData.position;
        velocity = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;
        content.anchoredPosition += delta;
        lastMousePosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        velocity = (eventData.position - lastMousePosition) / Time.deltaTime;
    }
}
