using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.Raise(new PlayerGotHitEvent() { });
        }
    }
}
