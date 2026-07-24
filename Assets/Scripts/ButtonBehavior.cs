using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    public static ButtonBehavior instance;

    [SerializeField] bool pauseMenu;

    GameObject optionsMenu;
    InputAction pauseAction;

    private void Awake()
    {
        if (pauseMenu)
        {
            if (instance == null)
            {
                instance = this;
            }

            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        pauseAction = InputSystem.actions.FindAction("Pause");
        OptionsManager optionsManager = FindFirstObjectByType<OptionsManager>();
        optionsMenu = optionsManager.gameObject;
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame() && SceneManager.GetActiveScene().buildIndex != 0)
        {
            PauseMenu();
        }
    }

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

    public void PauseMenu()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}