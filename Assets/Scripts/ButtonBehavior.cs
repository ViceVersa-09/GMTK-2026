using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
    public void Tutorial()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void OptionsMenu(bool active)
    {
        optionsMenu.SetActive(active);
    }
}