using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepManager : MonoBehaviour
{

    public List<AudioClip> footsteps = new List<AudioClip>();

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        if (footsteps == null || footsteps.Count == 0)
            return;

        AudioClip clip = footsteps[Random.Range(0, footsteps.Count)];
        source.PlayOneShot(clip);
    }

}
