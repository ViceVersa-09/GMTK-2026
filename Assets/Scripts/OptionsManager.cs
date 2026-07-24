using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    private void Awake()
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

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void CloseOptions()
    {
        gameObject.SetActive(false);
    }
}
