using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState MainGameState {get; private set;} = GameState.Playing;
    [SerializeField] private Transform _prefabSlot;
    public Transform PrefabSlot
    {
        get => _prefabSlot;
    }

    private void Awake()
    {
        Instance = this;
    }

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
