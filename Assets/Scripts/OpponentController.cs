using UnityEngine;
using System;
using System.Linq;

public class OpponentController : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] int chancesForGood;

    TurnManager turnManager;

    private void Awake()
    {
        turnManager = FindFirstObjectByType<TurnManager>();
    }

    private void Update()
    {
        if (turnManager.currentTurn == TurnManager.Turn.Opponent)
        {
            ChooseAttack();
        }
    }

    void ChooseAttack()
    {
        ComboManager comboManager = FindFirstObjectByType<ComboManager>();
        ComboManager.AttackStates[] choices = (ComboManager.AttackStates[])Enum.GetValues
            (typeof(ComboManager.AttackStates));

        for (int i = 0; i < chancesForGood; i++)
        {
            comboManager.currentAttack = choices[UnityEngine.Random.Range(0, choices.Length)];

            if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
            {
                break;
            }
        }

        turnManager.currentTurn = TurnManager.Turn.Inbetween;
    }
}
