using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.ChangeState(GameState.Menu);
    }

    // Start is called before the first frame update

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
