using UnityEngine;

public class MenuController : MonoBehaviour
{

    void Start()
    {
        GameManager.Instance.ChangeState(GameState.Menu);
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
