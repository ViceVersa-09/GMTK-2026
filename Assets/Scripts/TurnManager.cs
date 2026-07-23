using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    [Header("Quick Time Events")]
    [SerializeField] float quickTime;

    public enum Turn
    {
        Player,
        Opponent,
        Inbetween,
    }

    [HideInInspector] public bool quickTimeEvent;

    public Turn currentTurn;
    Turn previousTurn;
    Turn nextTurn;
    ComboManager comboManager;
    InputAction dodgeAction;
    InputAction blockAction;

    private void Start()
    {
        comboManager = FindFirstObjectByType<ComboManager>();
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        blockAction = InputSystem.actions.FindAction("Block");

        currentTurn = Turn.Player;
    }

    private void Update()
    {
        SolveTurn();
        Debug.Log(quickTimeEvent);
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
                    nextTurn = Turn.Player;
                    comboManager.AssignGood();
                }
                else
                {
                    nextTurn = Turn.Opponent;
                    comboManager.goodNextAttack = (ComboManager.AttackStates[])Enum.GetValues
                        (typeof(ComboManager.AttackStates));
                }
            }
            else if (previousTurn == Turn.Opponent)
            {
                if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
                {
                    nextTurn = Turn.Opponent;
                    comboManager.AssignGood();
                }
                else
                {
                    nextTurn = Turn.Player;
                    comboManager.goodNextAttack = (ComboManager.AttackStates[])Enum.GetValues
                        (typeof(ComboManager.AttackStates));
                }
            }
        }

        if (!quickTimeEvent)
        {
            currentTurn = nextTurn;
        }
    }

    public IEnumerator StartQuickTimeEvent()
    {
        StartCoroutine(Timer(quickTime));

        yield return new WaitUntil(() => !quickTimeEvent || dodgeAction.WasPressedThisFrame() || 
            blockAction.WasPressedThisFrame());

        if (dodgeAction.WasPressedThisFrame())
        {
            StopAllCoroutines();
            quickTimeEvent = false;
            currentTurn = Turn.Player;
        }
        else if (blockAction.WasPressedThisFrame())
        {
            StopAllCoroutines();
            quickTimeEvent = false;
            currentTurn = Turn.Opponent;
        }
    }

    IEnumerator Timer(float time)
    {
        quickTimeEvent = true;
        yield return new WaitForSeconds(time);
        quickTimeEvent = false;
    }
}
