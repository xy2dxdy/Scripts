using UnityEngine;

public class Car : MonoBehaviour
{
    public string carID = "DefaultCar";

    public float speed;
    public float turnSpeed;
    public int armor;

    void Start()
    {
        speed = PlayerPrefs.GetFloat("CarSpeed" + carID);
        armor = PlayerPrefs.GetInt("CarArmor" + carID);
    }
}
