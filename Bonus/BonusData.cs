using UnityEngine;

public abstract class BonusData : ScriptableObject
{
    [Header("Общие параметры бонуса")]
    public float duration = 5f;
    public float warningDuration = 2f;
    public Sprite icon;

    public abstract void Activate(GameObject player);
}
