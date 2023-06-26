using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallintoWater : MonoBehaviour
{
    [SerializeField] 
    private Transform respawnPoint;
    public ParticleSystem waterSplash;

    private void OnTriggerEnter(Collider other)
    {
        //S'il entre en collision avec  n'importe quel GameObject ayant un tag "Player" : 
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(PlayerRespawn(other));
            EventManager.Instance.Raise(new PlayerFellIntoWaterEvent() { });
        }
    }

    IEnumerator PlayerRespawn(Collider player)
    {
        CharacterController playerController = player.gameObject.GetComponent<CharacterController>();
        GameObject fox = player.transform.Find("fox").gameObject;

        Instantiate(waterSplash, player.transform.position, waterSplash.transform.rotation);
        fox.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);// On attend que l'animation se termine

        // Désactive le character controller le temps de repositioner le player
        playerController.enabled = false;
        
        // Repositionnement du player
        player.gameObject.transform.position = respawnPoint.transform.position;
        
        // Réactive le character controller
        playerController.enabled = true;

        //waterSplash.SetActive(false);
        fox.SetActive(true);
    }
}


