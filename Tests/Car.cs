using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    public int armor;

    void Start()
    {
        speed = PlayerPrefs.GetFloat("CarSpeed" + name, speed);
        armor = PlayerPrefs.GetInt("CarArmor" + name, armor);
    }
}
