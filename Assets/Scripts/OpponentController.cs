using UnityEngine;

public class OpponentController : MonoBehaviour
{
    TurnManager turnManager;

    private void Awake()
    {
        turnManager = FindFirstObjectByType<TurnManager>();
    }

    void ChooseAttack()
    {
        
    }
}
