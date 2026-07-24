using System.Collections;
using TMPro;
using UnityEditor.Rendering;
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
    TurnManager turnManager;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        opponentController = FindFirstObjectByType<OpponentController>();
        turnManager = FindFirstObjectByType<TurnManager>();

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
        TimerOnOff();
    }

    void UpdateHealthbars()
    {
        playerText.text = "Your health: " + Mathf.Clamp(playerController.health, 0, playerController.maxHealth) + 
            "/" + playerController.maxHealth;
        playerSlider.value = playerController.health;

        opponentText.text = "Your health: " + Mathf.Clamp(opponentController.health, 0, opponentController.maxHealth) + 
            "/" + opponentController.maxHealth;
        opponentSlider.value = opponentController.health;
    }

    public IEnumerator Timer(float seconds)
    {
        timerSlider.maxValue = seconds;
        timerSlider.value = seconds;

        for (float i = seconds; i > 0; i -= Time.deltaTime)
        {
            timerSlider.value = i;
            timerText.text = Mathf.CeilToInt(i).ToString();
            yield return new WaitForEndOfFrame();
        }
    }

    void TimerOnOff()
    {
        if (turnManager.quickTimeEvent)
        {
            timerText.gameObject.SetActive(true);
            timerSlider.gameObject.SetActive(true);
        }
        else
        {
            timerText.gameObject.SetActive(false);
            timerSlider.gameObject.SetActive(false);
        }
    }
}
