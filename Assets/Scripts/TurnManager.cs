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
    ButtonMethods[] buttonMethods;

    private void Start()
    {
        comboManager = FindFirstObjectByType<ComboManager>();
        dodgeAction = InputSystem.actions.FindAction("Dodge");
        blockAction = InputSystem.actions.FindAction("Block");
        buttonMethods = FindObjectsByType<ButtonMethods>(FindObjectsSortMode.None);

        currentTurn = Turn.Player;
        previousTurn = currentTurn;
    }

    private void Update()
    {
        SolveTurn();
        StartCoroutine(RemoveButtons());
    }

    void SolveTurn()
    {
        if (previousTurn == currentTurn)
        {
            previousTurn = currentTurn;
        }

        if (currentTurn == Turn.Inbetween && !quickTimeEvent)
        {
            if (previousTurn == Turn.Player)
            {
                if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
                {
                    comboManager.hits++;
                    nextTurn = Turn.Player;
                    comboManager.AssignGood();
                }
                else
                {
                    comboManager.hits = 0;
                    nextTurn = Turn.Opponent;
                    comboManager.goodNextAttack = (ComboManager.AttackStates[])Enum.GetValues
                        (typeof(ComboManager.AttackStates));
                }
            }
            else if (previousTurn == Turn.Opponent)
            {
                if (comboManager.goodNextAttack.Contains(comboManager.currentAttack))
                {
                    comboManager.hits++;
                    nextTurn = Turn.Opponent;
                    comboManager.AssignGood();
                }
                else
                {
                    comboManager.hits = 0;
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

        yield return new WaitUntil(() => !quickTimeEvent || dodgeAction.WasPressedThisFrame() || blockAction.WasPressedThisFrame());

        if (dodgeAction.WasPressedThisFrame())
        {
            StopAllCoroutines();
            comboManager.hits = 0;
            quickTimeEvent = false;
            nextTurn = Turn.Player;
            UIManager uIManager = FindFirstObjectByType<UIManager>();
            StartCoroutine(uIManager.Timer(0));
        }
        else if (blockAction.WasPressedThisFrame())
        {
            StopAllCoroutines();
            comboManager.hits = 0;
            quickTimeEvent = false;
            nextTurn = Turn.Inbetween;
            UIManager uIManager = FindFirstObjectByType<UIManager>();
            StartCoroutine(uIManager.Timer(0));
        }
        else
        {
            ButtonMethods[] buttonMethods = FindObjectsByType<ButtonMethods>(FindObjectsSortMode.None);
            float damage = 0;

            foreach (var attack in buttonMethods)
            {
                if (comboManager.currentAttack == attack.attack)
                {
                    damage = attack.attackDamage;
                    break;
                }
            }

            nextTurn = Turn.Inbetween;

            PlayerController playerController = FindFirstObjectByType<PlayerController>();
            playerController.health -= damage;
        }
    }

    IEnumerator Timer(float time)
    {
        quickTimeEvent = true;
        UIManager uIManager = FindFirstObjectByType<UIManager>();
        StartCoroutine(uIManager.Timer(time));
        yield return new WaitForSeconds(time);
        quickTimeEvent = false;
    }

    public void UpdateQuickTime()
    {
        OpponentController opponentController = FindFirstObjectByType<OpponentController>();

        // hard coded, please change or something
        quickTime = opponentController.health / 20;
    }

    IEnumerator RemoveButtons()
    {
        if (currentTurn == Turn.Player)
        {
            yield return new WaitForEndOfFrame();
            
            if (currentTurn == Turn.Player)
            {
                foreach (var button in buttonMethods)
                {
                    button.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach (var button in buttonMethods)
            {
                button.gameObject.SetActive(false);
            }
        }
    }
}
