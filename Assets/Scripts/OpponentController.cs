using UnityEngine;
using System;
using System.Linq;
using System.Collections;

public class OpponentController : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] int chancesForGood;
    [SerializeField] float attackDelay;

    TurnManager turnManager;
    Coroutine attackRoutine;

    private void Awake()
    {
        turnManager = FindFirstObjectByType<TurnManager>();
    }

    private void Update()
    {
        if (turnManager.currentTurn == TurnManager.Turn.Opponent && !turnManager.quickTimeEvent && attackRoutine == null)
        {
            attackRoutine = StartCoroutine(ChooseAttack());
        }
    }

    IEnumerator ChooseAttack()
    {
        // if youre wondering why quicktimevent is not true immediately its because its called at the bottom here,
        // its not a bug, its a feature
        yield return new WaitForSeconds(attackDelay);

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
        StartCoroutine(turnManager.StartQuickTimeEvent());
        attackRoutine = null;
    }
}
