using UnityEngine;
using System.Collections;

public class JetpackBonus : Bonus
{
    public float jetpackForce = 10f;

    public override void Activate(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (rb != null && playerController != null)
        {
            rb.AddForce(Vector3.up * jetpackForce, ForceMode.Impulse);
            playerController.SetInvincibility(true);
            playerController.SetActiveBonus(true);
        }
    }

    public override void Deactivate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetInvincibility(false);
            playerController.SetActiveBonus(false);
        }
        Destroy(gameObject);
    }
}
