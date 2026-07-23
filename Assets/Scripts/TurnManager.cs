using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum WhosTurn
    {
        Player,
        Opponent,
        Inbetween,
    }

    WhosTurn currentTurn;

    private void Start()
    {
        currentTurn = WhosTurn.Player;
    }
}
