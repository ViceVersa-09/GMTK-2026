using System;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Turn
    {
        Player,
        Opponent,
        Inbetween,
    }

    public Turn currentTurn;
    Turn previousTurn;
    ComboManager comboManager;

    private void Start()
    {
        comboManager = FindFirstObjectByType<ComboManager>();

        currentTurn = Turn.Player;
    }

    private void Update()
    {
        SolveTurn();
    }

    void SolveTurn()
    {
        if (previousTurn == currentTurn)
        {
            previousTurn = currentTurn;
        }

        if (currentTurn == Turn.Inbetween)
        {
            if (previousTurn == Turn.Player)
            {
                if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
                {
                    currentTurn = Turn.Player;
                    comboManager.AssignGood();
                }
                else
                {
                    currentTurn = Turn.Opponent;
                    comboManager.goodNextAttack = (ComboManager.AttackStates[])Enum.GetValues
                        (typeof(ComboManager.AttackStates));
                }
            }
            else if (previousTurn == Turn.Opponent)
            {
                if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
                {
                    currentTurn = Turn.Opponent;
                    comboManager.AssignGood();
                }
                else
                {
                    currentTurn = Turn.Player;
                    comboManager.goodNextAttack = (ComboManager.AttackStates[])Enum.GetValues
                        (typeof(ComboManager.AttackStates));
                }
            }
        }
    }
}
