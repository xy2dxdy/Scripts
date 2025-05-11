using NUnit.Framework;
using UnityEngine;

public class CarTests
{
    [Test]
    public void Car_Initialization_CorrectValues()
    {
        GameObject carObject = new GameObject();
        Car car = carObject.AddComponent<Car>();

        car.speed = 10f;
        car.armor = 5;
        car.name = "TestCar";

        Assert.AreEqual(10f, car.speed);
        Assert.AreEqual(5, car.armor);
        Assert.AreEqual("TestCar", car.name);

        Object.DestroyImmediate(carObject);
    }
}
