using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{

    void Start()
    {
        
    }

    // Start is called before the first frame update
    public void update()
    {
        FindObjectOfType<Sound_Manager>().Play("BadGuy");
    }
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
