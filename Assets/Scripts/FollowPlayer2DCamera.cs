using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2DCamera : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 positionOffset;

    private Vector3 velocity;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset, ref velocity, timeOffset);
    }
}
