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
            Debug.Log("PlanetButtonHandler ��������������� ��� ������� � ��������: " + planetIndex);
        }
        else
        {
            Debug.LogWarning("PlanetManager �� ������!");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("������ ������ ������� � ��������: " + planetIndex);

        if (planetManager.GetCurrentPlanetIndex() == planetIndex)
        {
            // ����� ����� ���� ������� �����, ����������� ������, ���� �������� �������� ������������� �����.
            // ��������, ���� � PlanetManager ���������� �����:
            // public void AddFixedAmountToPlanet(int planetIndex)
            // ����� ������:
            planetManager.AddFixedAmountToPlanet(planetIndex);

            // ����, ���� � ��� ����� ��� ��������� ��� ������� �������:
            // planetManager.AddFixedAmountToCurrentPlanet();
        }
        else
        {
            Debug.Log("������ ���������� ������� (planetIndex = " + planetIndex + "). �������� �� �����������.");
        }
    }
}
