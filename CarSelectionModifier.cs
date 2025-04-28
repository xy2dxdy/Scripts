using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectionModifier : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "CarSelection")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
            }

            transform.localScale *= 40;

        }
    }
}
