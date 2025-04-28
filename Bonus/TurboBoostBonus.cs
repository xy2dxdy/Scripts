using UnityEngine;
using System.Collections;

public class TurboBoostBonus : Bonus
{
    public float boostForce = 20f;

    public override void Activate(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (rb != null && playerController != null)
        {
            rb.AddForce(player.transform.forward * boostForce, ForceMode.Impulse);
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
