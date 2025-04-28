using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public int sessionCoins = 0;
    private Text coinText;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            sessionCoins++;
            Destroy(other.gameObject);
            UpdateCoinText();
        }
    }

    public void OnLevelComplete()
    {
        CoinManager.Instance.AddCoins(sessionCoins);
        sessionCoins = 0;
    }

    public void SetCoinText(Text coinText)
    {
        this.coinText = coinText;
        if (this.coinText != null)
        {
            UpdateCoinText();
        }
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = sessionCoins.ToString();
        }
    }
}
