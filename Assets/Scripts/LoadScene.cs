using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string sceneToLoad;

    public GameObject player;
    public GameObject player1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
            if (MusicLoopsManager.Instance)
                MusicLoopsManager.Instance.PlayMusic(Constants.LEVELBONUS_MUSIC);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Vortex());  
        }
    }

    IEnumerator Vortex()
    {
        player.SetActive(false);
        player1.SetActive(true);

        yield return new WaitForSeconds(1.2f);

        player1.SetActive(false);

        if (MusicLoopsManager.Instance)
            MusicLoopsManager.Instance.PlayMusic(Constants.LEVEL2_MUSIC);

        SceneManager.LoadScene(sceneToLoad);

        /*if (SceneManager.GetActiveScene().name == "level3")
            EventManager.Instance.Raise(new NextLevelEvent() { });*/
        
    }

    
}
