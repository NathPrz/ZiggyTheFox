using SDD.Events;
using StarterAssets;
using UnityEngine;

public class Spider : MonoBehaviour
{
    public ThirdPersonController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!player.Grounded)
                Destroy(gameObject);

            else
                EventManager.Instance.Raise(new PlayerGotHitEvent() { });
        }
    }
}
