using System.Collections;
using UnityEngine;

namespace StarterAssets
{

    public class DestroyBox : MonoBehaviour
    {
        public GameObject cherries;
        public GameObject life;
        public ParticleSystem hitParticle;
        private bool isInRange;
        public ThirdPersonController player;

        void HitBox()
        {
            Destroy(gameObject);

            Instantiate(hitParticle, transform.position, transform.rotation);
            // On décale la position de cherries et life par rapport à celle de la boîte : 
            Vector3 cherriesPosition = transform.TransformPoint(new Vector3(0f, 0.8f, 0f));
            Instantiate(cherries, cherriesPosition, transform.rotation);

            Vector3 lifePosition = transform.TransformPoint(new Vector3(0.1f, 1.8f, 0.1f));
            Instantiate(life, lifePosition, transform.rotation);
        }

        private void Update()
        {
            // Si le player attaque la boîte
            if (Input.GetKeyDown(KeyCode.E) && isInRange)
            {
                StartCoroutine(DamageBox());
            }
        }

        IEnumerator DamageBox()
        {
            yield return new WaitForSeconds(0.3f);

            HitBox();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isInRange = true;

                // Si le player saute sur la boîte
                if (!player.Grounded)
                {
                    HitBox();
                }
            }  
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                isInRange = false;
        }
    }
}


