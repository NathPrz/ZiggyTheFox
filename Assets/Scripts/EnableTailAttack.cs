using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTailAttack : MonoBehaviour
{
    public Collider tail;

    public void EnableTailCollider(int enable)
    {
        if(tail != null)
        {
            if(enable == 1)
                tail.enabled = true;
            else
                tail.enabled = false;
        }
    }
}
