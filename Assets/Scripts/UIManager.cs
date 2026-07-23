using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] TextMeshProUGUI playerText;
    [SerializeField] Slider playerSlider;

    [Header("Opponent")]
    [SerializeField] TextMeshProUGUI opponentText;
    [SerializeField] Slider opponentSlider;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Slider timerSlider;

    PlayerController playerController;
    OpponentController opponentController;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        opponentController = FindFirstObjectByType<OpponentController>();

        playerSlider.maxValue = playerController.maxHealth;
        playerSlider.value = playerSlider.maxValue;
        opponentSlider.maxValue = opponentController.maxHealth;
        opponentSlider.value = opponentSlider.maxValue;
        timerText.gameObject.SetActive(false);
        timerSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateHealthbars();
    }

    void UpdateHealthbars()
    {
        playerText.text = "Your health: " + playerController.health + "/" + playerController.maxHealth;
        playerSlider.value = playerController.health;

        opponentText.text = "Your health: " + opponentController.health + "/" + opponentController.maxHealth;
        opponentSlider.value = opponentController.health;
    }

    public IEnumerator Timer(int seconds)
    {
        timerText.gameObject.SetActive(true);
        timerSlider.gameObject.SetActive(true);

        timerSlider.maxValue = seconds;
        timerSlider.value = seconds;

        for (int i = seconds; i > 0; i--)
        {
            timerSlider.value = i;
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        timerText.gameObject.SetActive(false);
        timerSlider.gameObject.SetActive(false);
    }
}
