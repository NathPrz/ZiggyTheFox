using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IDestroyable
{
    //AudioSource audioSource;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //audioSource.Play();
            EventManager.Instance.Raise(new ItemHasBeenHitEvent() { eItem = this.gameObject });
            Kill();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //audioSource.Play();
            EventManager.Instance.Raise(new ItemHasBeenHitEvent() { eItem = this.gameObject });
            Kill();
        }
    }


    public void Kill()
    {
        gameObject.SetActive(false);
    }          
}

