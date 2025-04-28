using NUnit.Framework;
using UnityEngine;

public class CarTests
{
    [Test]
    public void Car_Initialization_CorrectValues()
    {
        // Создание игрового объекта и добавление компонента Car
        GameObject carObject = new GameObject();
        Car car = carObject.AddComponent<Car>();

        // Инициализация параметров автомобиля
        car.speed = 10f;
        car.armor = 5;
        car.name = "TestCar";

        // Проверка значений параметров после инициализации
        Assert.AreEqual(10f, car.speed);
        Assert.AreEqual(5, car.armor);
        Assert.AreEqual("TestCar", car.name);

        // Удаление игрового объекта после теста
        Object.DestroyImmediate(carObject);
    }
}
