using UnityEngine;
using System.Collections;

public abstract class Bonus : MonoBehaviour
{
    public float duration = 5f;
    public float warningDuration = 2f;

    public void ActivateBonus(GameObject player)
    {
        transform.SetParent(null);
        StartCoroutine(BonusDuration(player));
    }

    private IEnumerator BonusDuration(GameObject player)
    {
        Activate(player);

        yield return new WaitForSeconds(duration - warningDuration);
        StartCoroutine(BlinkPlayer(player, warningDuration));

        yield return new WaitForSeconds(warningDuration);
        Deactivate(player);
    }

    private IEnumerator BlinkPlayer(GameObject player, float blinkDuration)
    {
        Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
        float blinkInterval = 0.2f;
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            SetRenderersEnabled(renderers, false);
            yield return new WaitForSeconds(blinkInterval / 2);
            elapsed += blinkInterval / 2;

            if (elapsed >= blinkDuration)
                break;

            SetRenderersEnabled(renderers, true);
            yield return new WaitForSeconds(blinkInterval / 2);
            elapsed += blinkInterval / 2;
        }

        SetRenderersEnabled(renderers, true);
    }

    private void SetRenderersEnabled(Renderer[] renderers, bool enabled)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = enabled;
        }
    }

    public abstract void Activate(GameObject player);

    public abstract void Deactivate(GameObject player);
}
