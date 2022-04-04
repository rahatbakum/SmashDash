using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState MainGameState {get; private set;} = GameState.Playing;
    [SerializeField] private Transform _prefabSlot;
    public Transform PrefabSlot
    {
        get => _prefabSlot;
    }

    [SerializeField] private UnityEvent _won;
    public event UnityAction Won
    {
        add => _won.AddListener(value);
        remove => _won.RemoveListener(value);
    }
    [SerializeField] private UnityEvent _lost;
    public event UnityAction Lost
    {
        add => _lost.AddListener(value);
        remove => _lost.RemoveListener(value);
    }
    
    public void Win()
    {
        MainGameState = GameState.Won;
        _won.Invoke();
    }

    public void Lose()
    {
        MainGameState = GameState.Lost;
        _lost.Invoke();
    }

    private void Awake()
    {
        Instance = this;
    }
}

public enum GameState
{
    Playing,
    Won,
    Lost
}
