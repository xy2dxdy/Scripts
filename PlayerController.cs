using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Car currentCar;
    private bool isInvincible = false;
    private bool isInvisible = false;
    private bool hasActiveBonus = false;
    public Text armorLevelText;
    private Renderer[] renderers;

    private float currentRotation = 0f;

    public float speedBoostDuration = 5f;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            return;
        }

        currentCar = GetComponent<Car>();
        if (currentCar == null)
        {
            Debug.LogError("Car component not found on the car prefab");
            return;
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on the car prefab");
            return;
        }

        renderers = GetComponentsInChildren<Renderer>();

        if (armorLevelText != null)
        {
            UpdateArmorLevelText();
        }
        else
        {
            Debug.LogError("armorLevelText is not assigned in PlayerController Start");
        }
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            return;
        }

        if (rb != null)
        {
            rb.velocity = Quaternion.Euler(0, -90, 0) * transform.forward * currentCar.speed;

            rb.MoveRotation(Quaternion.Euler(0, 90 + currentRotation, 0));
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            return;
        }

        if (rb != null)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                float halfScreenWidth = Screen.width / 2;
                if (touch.position.x < halfScreenWidth)
                {
                    currentRotation -= currentCar.turnSpeed * Time.deltaTime;
                    transform.Rotate(0, -currentCar.turnSpeed * Time.deltaTime, 0);
                }
                else if (touch.position.x > halfScreenWidth)
                {
                    currentRotation += currentCar.turnSpeed * Time.deltaTime;
                    transform.Rotate(0, currentCar.turnSpeed * Time.deltaTime, 0);
                }
            }
            else 
            {
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    currentRotation -= currentCar.turnSpeed * Time.deltaTime;
                    transform.Rotate(0, -currentCar.turnSpeed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    currentRotation += currentCar.turnSpeed * Time.deltaTime;
                    transform.Rotate(0, currentCar.turnSpeed * Time.deltaTime, 0);
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            return;
        }

        if (!isInvincible)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
            {
                TakeDamage();
                Destroy(collision.gameObject);

                if (currentCar.armor <= 0)
                {
                    CoinCollector coinCollector = GetComponent<CoinCollector>();
                    if (coinCollector != null)
                    {
                        coinCollector.OnLevelComplete();
                    }
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage()
    {
        currentCar.armor -= 1;
        UpdateArmorLevelText();
        Debug.Log("Current Armor: " + currentCar.armor);
    }

    private void UpdateArmorLevelText()
    {
        if (armorLevelText != null)
        {
            armorLevelText.text = "Armor Level: " + currentCar.armor;
        }
        else
        {
            Debug.LogError("armorLevelText is not assigned in UpdateArmorLevelText");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().name != "Gameplay")
        {
            return;
        }

        if (other.CompareTag("Coin"))
        {
            CoinCollector coinCollector = GetComponent<CoinCollector>();
            if (coinCollector != null)
            {
                coinCollector.OnTriggerEnter(other);
            }
        }
        else if (!hasActiveBonus)
        {
            Bonus bonus = other.GetComponent<Bonus>();
            if (bonus != null)
            {
                bonus.ActivateBonus(gameObject);
                bonus.gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    public void SetInvincibility(bool state)
    {
        isInvincible = state;
    }

    public void SetInvisibility(bool state)
    {
        isInvisible = state;
        SetObjectTransparency(state ? 0.3f : 1f);
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }

    public void SetActiveBonus(bool state)
    {
        hasActiveBonus = state;
    }

    public void ActivateSpeedBoost()
    {
        Debug.Log("Activating speed boost.");
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        currentCar.speed *= 2;
        Debug.Log("Speed boost activated! Current speed: " + currentCar.speed);
        yield return new WaitForSeconds(speedBoostDuration);
        Debug.Log("Restoring speed to original.");
        currentCar.speed /= 2;
        Debug.Log("Speed boost ended. Speed restored to: " + currentCar.speed);
    }

    public void SetArmorLevelText(Text armorLevelText)
    {
        this.armorLevelText = armorLevelText;
        if (this.armorLevelText != null)
        {
            Debug.Log("ArmorLevelText assigned successfully in SetArmorLevelText.");
            UpdateArmorLevelText();
        }
        else
        {
            Debug.LogError("ArmorLevelText is not assigned in SetArmorLevelText!");
        }
    }

    public float GetCurrentSpeed()
    {
        return currentCar.speed;
    }

    private void SetObjectTransparency(float alpha)
    {
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                if (material.HasProperty("_Color"))
                {
                    Color color = material.color;
                    color.a = alpha;
                    material.color = color;
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                }
            }
        }
    }

    public bool HasActiveBonus() { return hasActiveBonus; }
}
