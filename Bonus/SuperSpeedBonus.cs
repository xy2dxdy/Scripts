using UnityEngine;

public class SuperSpeedBonus : Bonus
{
    public override void Activate(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.ActivateSpeedBoost();
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
