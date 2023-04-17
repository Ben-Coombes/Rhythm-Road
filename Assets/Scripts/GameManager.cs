using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    private void Awake()
    {
        currentState = GameState.Menu;
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public MusicSO currentSelectedMusic;
    public GameState currentState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateStates();
    }

    private void UpdateStates()
    {
        if (currentState == GameState.Menu)
        {

        }
        else if (currentState == GameState.Countdown)
        {

        }
        else if (currentState == GameState.Playing)
        {

        }
        else if (currentState == GameState.Paused)
        {

        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeState(GameState state)
    {
        StateEntered(state);
        currentState = state;
    }

    private void StateEntered(GameState state)
    {
        if (state == GameState.Menu)
        {

        }
        else if (state == GameState.Countdown)
        {

        }
        else if (state == GameState.Playing)
        {
            Debug.Log("start level");
            //Conductor.Instance.StartLevel();
        }
        else if (state == GameState.Paused)
        {

        }
    }


}


public enum GameState
{
    Paused,
    Playing,
    Countdown,
    Menu
}