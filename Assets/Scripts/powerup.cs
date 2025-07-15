using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InvincibilityPowerUpManager : MonoBehaviour
{
    public Button invincibilityButton;
    public float buttonVisibleDuration = 1f;  // 1 segundo visible
    public float invincibilityDuration = 2f;  // 2 segundos de invencibilidad

    private bool buttonActive = false;

    void Start()
    {
        invincibilityButton.gameObject.SetActive(false);
        invincibilityButton.onClick.AddListener(OnButtonPressed);
    }

 
    public void ShowButtonNow()
    {
        if (!buttonActive)
        {
            StartCoroutine(ShowButtonCoroutine());
        }
    }

    private IEnumerator ShowButtonCoroutine()
    {
        buttonActive = true;
        invincibilityButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(buttonVisibleDuration);
        invincibilityButton.gameObject.SetActive(false);
        buttonActive = false;
    }

    public void ResetButton()
    {
        StopAllCoroutines();
        invincibilityButton.gameObject.SetActive(false);
        buttonActive = false;
    }


    private void OnButtonPressed()
    {
        
        PlayerController esquivePlayer = GetCurrentEsquivePlayer();
        if (esquivePlayer != null)
        {
            esquivePlayer.ActivateInvincibility(invincibilityDuration);
        }
        ResetButton();
    }

    private PlayerController GetCurrentEsquivePlayer()
    {
       
        if (GameManager.Instance == null) return null;

        if (GameManager.Instance.player1 != null && !GameManager.Instance.player1.isShooter)
            return GameManager.Instance.player1;

        if (GameManager.Instance.player2 != null && !GameManager.Instance.player2.isShooter)
            return GameManager.Instance.player2;

        return null;
    }
}
