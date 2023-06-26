using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public ParticleSystem hitSmoke;
 
    private void Awake()
    {
        player.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayerTransformation());
    }

    IEnumerator PlayerTransformation()
    {
        animator.SetTrigger("Trigger");

        yield return new WaitForSeconds(1.3f);

        Destroy(gameObject);

        Vector3 hitSmokePos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 3f);
        Instantiate(hitSmoke, hitSmokePos, hitSmoke.transform.rotation);
        player.SetActive(true);

    }
}
