using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject fadeToBlackEffect;
    public GameObject fadeToClearEffect;
    public GameObject gameOverScreen;
    public GameObject gameOverArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Events.onGameOverTrigger.AddListener(startGameOverFade);
    }
    private void OnDisable()
    {
        Events.onGameOverTrigger.RemoveListener(startGameOverFade);
    }

    public void startGameOverFade()
    {
        fadeToBlackEffect.SetActive(true);
        StartCoroutine(fadeToBlackTimer());
    }

    IEnumerator fadeToBlackTimer()
    {
        yield return new WaitForSeconds(3);
        gameOverArea.SetActive(true);
        mainCam.SetActive(false);
        startClearFade();
    }

    public void startClearFade()
    {
        fadeToClearEffect.SetActive(true);
        fadeToBlackEffect.SetActive(false);
        StartCoroutine(fadeToClearTimer());
    }

    IEnumerator fadeToClearTimer()
    {
        enableGameOver();
        yield return new WaitForSeconds(3);
        
    }

    public void enableGameOver()
    {
        gameOverScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
