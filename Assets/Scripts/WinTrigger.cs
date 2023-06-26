using SDD.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter()
    {

        if (SceneManager.GetActiveScene().name == "level1") EventManager.Instance.Raise(new NextLevelEvent() { });
        else EventManager.Instance.Raise(new LevelCompletedEvent() { });
    }
}
