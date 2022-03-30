using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState MainGameState {get; private set;} = GameState.Playing;

    public void Win()
    {
        MainGameState = GameState.Won;
        Debug.Log("Win");
    }

    public void Lose()
    {
        MainGameState = GameState.Lost;
        Debug.Log("Lose");
    }

}

public enum GameState
{
    Playing,
    Won,
    Lost
}
