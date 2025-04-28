using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed; // Скорость автомобиля
    public float turnSpeed; // Скорость поворота
    public int armor; // Броня автомобиля

    void Start()
    {
        speed = PlayerPrefs.GetFloat("CarSpeed" + name, speed);
        armor = PlayerPrefs.GetInt("CarArmor" + name, armor);
    }
}
