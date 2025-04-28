using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetButtonHandler : MonoBehaviour, IPointerDownHandler
{
    private PlanetManager planetManager;
    public int planetIndex;

    void Start()
    {
        planetManager = FindObjectOfType<PlanetManager>();
        if (planetManager != null)
        {
            Debug.Log("PlanetButtonHandler инициализирован для планеты с индексом: " + planetIndex);
        }
        else
        {
            Debug.LogWarning("PlanetManager не найден!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Нажата кнопка планеты с индексом: " + planetIndex);

        if (planetManager.GetCurrentPlanetIndex() == planetIndex)
        {
            // Здесь можно либо вызвать метод, принимающий индекс, либо напрямую добавить фиксированную сумму.
            // Например, если в PlanetManager реализован метод:
            // public void AddFixedAmountToPlanet(int planetIndex)
            // Тогда делаем:
            planetManager.AddFixedAmountToPlanet(planetIndex);

            // Либо, если у вас метод без параметра для текущей планеты:
            // planetManager.AddFixedAmountToCurrentPlanet();
        }
        else
        {
            Debug.Log("Нажата неактивная планета (planetIndex = " + planetIndex + "). Действие не выполняется.");
        }
    }
}
