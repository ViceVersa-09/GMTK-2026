using UnityEngine;

public class ButtonMethods : MonoBehaviour
{
    [Header("Attack button")]
    [SerializeField] public ComboManager.AttackStates attack;
    [SerializeField] public float attackDamage;

    public void ExampleMethod()
    {
        Debug.Log("It works");
    }

    public void PlayerAttack()
    {
        OpponentController opponent = FindFirstObjectByType<OpponentController>();
        TurnManager turnManager = FindFirstObjectByType<TurnManager>();
        ComboManager comboManager = FindFirstObjectByType<ComboManager>();

        opponent.health -= attackDamage;
        comboManager.currentAttack = attack;
        turnManager.currentTurn = TurnManager.Turn.Inbetween;
        turnManager.UpdateQuickTime();
    }
}
