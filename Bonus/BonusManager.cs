using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BonusManager : MonoBehaviour
{
    [Header("Настройки бонусов")]
    public int maxBonuses = 3;
    public Image[] bonusIcons;

    private float lastTapTime = 0f;
    [SerializeField] private float doubleTapDelay = 0.5f;

    private GameObject player;

    private List<Bonus> bonusStack = new List<Bonus>();


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - lastTapTime < doubleTapDelay)
                {
                    UseBonus();
                }
                lastTapTime = Time.time;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            UseBonus();
        }
    }

    public bool AddBonus(Bonus bonus)
    {
        if (bonusStack.Count < maxBonuses)
        {
            bonusStack.Add(bonus);
            UpdateBonusUI();

            bonus.transform.SetParent(null);

            Collider bonusCollider = bonus.GetComponent<Collider>();
            if (bonusCollider != null)
            {
                bonusCollider.enabled = false;
            }
            Renderer[] renderers = bonus.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            return true;
        }
        else
        {
            Debug.Log("Бонус-стек заполнен!");
            return false;
        }
    }


    public void UseBonus()
    {
        if (player == null)
        {
            Debug.LogError("Player не назначен в BonusManager");
            return;
        }

        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null && playerController.HasActiveBonus())
        {
            Debug.Log("Невозможно использовать бонус, пока длится действие предыдущего");
            return;
        }

        if (bonusStack.Count > 0)
        {
            Bonus bonusToUse = bonusStack[0];
            bonusStack.RemoveAt(0);
            bonusToUse.ActivateBonus(player);
            UpdateBonusUI();
        }
        else
        {
            Debug.Log("Нет бонусов для использования");
        }
    }


    void UpdateBonusUI()
    {
        for (int i = 0; i < bonusIcons.Length; i++)
        {
            if (i < bonusStack.Count)
            {
                bonusIcons[i].sprite = bonusStack[i].icon;
                bonusIcons[i].color = Color.white;
            }
            else
            {
                bonusIcons[i].sprite = null;
                bonusIcons[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
    
    public void setPlayer(GameObject pc) { this.player = pc; }
}
