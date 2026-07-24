using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] GameObject background;
    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI tipText;

    [Header("Texts")]
    [SerializeField] string[] texts;
    [SerializeField] string[] tips;

    bool continueTutorial;

    TurnManager turnManager;

    private void Start()
    {
        turnManager = FindFirstObjectByType<TurnManager>();

        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        tutorialText.gameObject.SetActive(true);
        background.SetActive(true);
        button.SetActive(true);
        tipText.gameObject.SetActive(false);

        tutorialText.text = texts[0];

        yield return new WaitUntil(() => continueTutorial);

        continueTutorial = false;

        tutorialText.gameObject.SetActive(false);
        background.SetActive(false);
        button.SetActive(false);
        tipText.gameObject.SetActive(true);

        tipText.text = tips[0];

        yield return new WaitUntil(() => turnManager.currentTurn == TurnManager.Turn.Inbetween);

        turnManager.currentTurn = TurnManager.Turn.Player;

        tutorialText.gameObject.SetActive(true);
        background.SetActive(true);
        button.SetActive(true);
        tipText.gameObject.SetActive(false);

        tutorialText.text = texts[1];

        yield return new WaitUntil(() => continueTutorial);

        continueTutorial = false;

        tutorialText.gameObject.SetActive(false);
        background.SetActive(false);
        button.SetActive(false);
        tipText.gameObject.SetActive(true);

        tipText.text = tips[1];
    }

    public void NextButton()
    {
        continueTutorial = true;
    }
}
