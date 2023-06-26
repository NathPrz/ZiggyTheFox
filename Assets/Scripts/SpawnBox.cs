using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public GameObject[] boxes;

    private int currentBoxIndex = 0;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            // Vérifie si on a atteint la fin du tableau
            if (currentBoxIndex >= boxes.Length)
            {
                Debug.Log("Toutes les boîtes ont été utilisées.");
                return;
            }

            boxes[currentBoxIndex].SetActive(true);
            Destroy(collision.gameObject);

            // Incrémente l'index pour la prochaine boîte
            currentBoxIndex++;
           
        }
    }
}
