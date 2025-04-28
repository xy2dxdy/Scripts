using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float baseSpeed = 5f; 
    public float acceleration = 1f;
    public float reactionDelay = 1f;
    public float rotationSpeed = 100f;
    public float randomPathOffset = 2f;
    public float enemySpeedMultiplier = 1.5f;

    private Transform target;
    private float moveSpeed;
    private float currentSpeed;
    private Vector3 delayedTargetPosition;
    private float delayTimer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (target != null)
        {
            delayedTargetPosition = target.position;
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                moveSpeed = Mathf.Max(baseSpeed, playerController.GetCurrentSpeed()) * enemySpeedMultiplier;
            }
            else
            {
                moveSpeed = baseSpeed;
            }
            currentSpeed = 0f;
        }
        else
        {
            Debug.LogError("Player не найден! Убедитесь, что у объекта игрока есть тег 'Player'.");
            moveSpeed = baseSpeed;
            currentSpeed = baseSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 90, 0);
        transform.localScale = new Vector3(400, 400, 400);
    }

    void Update()
    {
        if (target == null)
            return;

        PlayerController playerController = target.GetComponent<PlayerController>();

        if (playerController != null && playerController.HasActiveBonus())
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
            transform.position += transform.forward * currentSpeed * Time.deltaTime;
            return;
        }

        delayTimer += Time.deltaTime;
        if (delayTimer >= reactionDelay)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-randomPathOffset, randomPathOffset),
                                               0,
                                               Random.Range(-randomPathOffset, randomPathOffset));
            delayedTargetPosition = target.position + randomOffset;

            if (playerController != null)
            {
                moveSpeed = Mathf.Max(baseSpeed, playerController.GetCurrentSpeed()) * enemySpeedMultiplier;
            }
            else
            {
                moveSpeed = baseSpeed;
            }
            delayTimer = 0f;
        }

        Vector3 direction = delayedTargetPosition - transform.position;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        currentSpeed = Mathf.MoveTowards(currentSpeed, moveSpeed, acceleration * Time.deltaTime);
        transform.position += transform.forward * currentSpeed * Time.deltaTime;
    }
}
