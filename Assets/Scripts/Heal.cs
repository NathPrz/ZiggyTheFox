using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{ 
    public GameObject cube;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.currentHealth < GameManager.Instance.maxHealth)
            {
                StartCoroutine(HealPowerUp());
            }
        }
    }

    IEnumerator HealPowerUp()
    {
        HealthBar.instance.HealPlayer(1);
        audioSource.Play();

        cube.SetActive(false);
        yield return new WaitForSeconds(0.6f);

        Destroy(gameObject);
    }
}
