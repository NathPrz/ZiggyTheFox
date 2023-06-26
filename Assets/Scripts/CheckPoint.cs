using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))// On vérifie si c'est le Player qui est entré dans la zone
        {
            respawnPoint.position = transform.position;
        }
    }
}
