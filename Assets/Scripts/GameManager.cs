using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float winLossDelay;

    PlayerController playerController;
    OpponentController opponentController;
    Coroutine loseRoutine;
    Coroutine winRoutine;

    private void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        opponentController = FindFirstObjectByType<OpponentController>();
    }

    private void Update()
    {
        CheckReferences();

        if (playerController != null && playerController.health <= 0 && loseRoutine == null)
        {
            loseRoutine = StartCoroutine(Lose());
        }

        if (opponentController != null && opponentController.health <= 0 && winRoutine == null)
        {
            loseRoutine = StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        Debug.Log("Win");

        yield return new WaitForSeconds(winLossDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Lose()
    {
        Debug.Log("Loss");

        yield return new WaitForSeconds(winLossDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CheckReferences()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }

        if (opponentController == null)
        {
            opponentController = FindFirstObjectByType<OpponentController>();
        }
    }
}
