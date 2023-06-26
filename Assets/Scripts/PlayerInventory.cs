using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    //private Vector3 initialPosition;
    //private Quaternion initialRotation;

    //public Vector3 GetPlayerInitialPosition()
    //{
    //    return initialPosition;
    //}

    //public void ResetPlayerPosition()
    //{
    //    transform.position = initialPosition;
    //    transform.rotation = initialRotation;
    //}

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("PlayerInventory a plus d'une instance dans la scène");
            return;
        }

        instance = this;    // Le script PlayerInventory.cs est stocké dans la variable 'instance'
    }
}

