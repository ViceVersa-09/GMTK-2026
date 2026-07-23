using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float health;

    [HideInInspector] public float maxHealth;

    private void Awake()
    {
        maxHealth = health;
    }
}
