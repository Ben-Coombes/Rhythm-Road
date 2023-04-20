using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Events.onGameOverTrigger.Invoke();
        Destroy(gameObject);
    }
}
