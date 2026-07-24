using System;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public enum AttackStates
    {
        rightUppercut,
        rightJab,
        rightHook,
        leftUppercut,
        leftJab,
        leftHook
    }

    [SerializeField] AttackStates[] rightUpGood;
    [SerializeField] AttackStates[] rightJabGood;
    [SerializeField] AttackStates[] rightHookGood;
    [SerializeField] AttackStates[] leftUpGood;
    [SerializeField] AttackStates[] leftJabGood;
    [SerializeField] AttackStates[] leftHookGood;

    [HideInInspector] public AttackStates[] goodNextAttack;
    [HideInInspector] public AttackStates currentAttack;

    private void Start()
    {
        goodNextAttack = (AttackStates[])Enum.GetValues(typeof(AttackStates));
    }

    public void AssignGood()
    {
        if (currentAttack == AttackStates.rightUppercut)
        {
            goodNextAttack = rightUpGood;
        }
        else if (currentAttack == AttackStates.rightJab)
        {
            goodNextAttack = rightJabGood;
        }
        else if (currentAttack == AttackStates.rightHook)
        {
            goodNextAttack = rightHookGood;
        }
        else if (currentAttack == AttackStates.leftUppercut)
        {
            goodNextAttack = leftUpGood;
        }
        else if (currentAttack == AttackStates.leftJab)
        {
            goodNextAttack = leftJabGood;
        }
        else if (currentAttack == AttackStates.leftHook)
        {
            goodNextAttack = leftHookGood;
        }
        else
        {
            goodNextAttack = (AttackStates[])Enum.GetValues(typeof(AttackStates));
        }

        foreach (var attack in goodNextAttack)
        {
            Debug.Log(attack);
        }
    }
}
