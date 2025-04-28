using UnityEngine;
using System.Collections;

public class InvisibilityBonus : Bonus
{
    public override void Activate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetInvisibility(true);
            playerController.SetInvincibility(true);
            playerController.SetActiveBonus(true);
        }
    }

    public override void Deactivate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetInvisibility(false);
            playerController.SetInvincibility(false);
            playerController.SetActiveBonus(false);
        }
        Destroy(gameObject);
    }
}
