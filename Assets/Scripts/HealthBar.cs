using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Slider slider;
    public GameManager manager;
    public Animator playerAnimator;

    public TMP_Text m_LivesValue;

    public GameObject player;
    public bool canBeDamage = true;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de HealthBar dans la scène");
            return;
        }

        instance = this;// Le script HealthBar.cs est stocké dans la variable 'instance'
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void SwitchHealthbarDisplayMode()
    {
        if (manager.currentHealth > 3)
        {
            slider.value = 1;
            m_LivesValue.gameObject.SetActive(true);

        }
        else m_LivesValue.gameObject.SetActive(false);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        m_LivesValue.text = health.ToString();


        SwitchHealthbarDisplayMode();
    }

    public void HealPlayer(int life)
    {
        manager.currentHealth += life;

        SetHealth(manager.currentHealth); 
    }

    public void RestorePlayerHealthBar()
    {
        manager.currentHealth = manager.maxHealth;
        SetHealth(manager.currentHealth);
    }

    public void GetLPBonus()
    {
        if (manager.currentHealth >= manager.maxHealth) HealPlayer(1);       // If healthbar is alreay full, add 1 LP
        else RestorePlayerHealthBar();                                      // Else restore healthbar to the max
    }

    public void TakeDamage(int damage)
    {
        if (canBeDamage)
        {
            canBeDamage = false;
            StartCoroutine(Damage(damage));
        }
    }

    IEnumerator Damage(int damage)
    {
        manager.currentHealth -= damage;
        SetHealth(manager.currentHealth);

        if (manager.currentHealth > 0)
        {
            CharacterController playerController = player.gameObject.GetComponent<CharacterController>();

            playerAnimator.SetTrigger("Fall");

            yield return new WaitForSeconds(0.6f);

            playerController.enabled = false;

            player.transform.position -= new Vector3(0f, -3f, 7f);// Le player réapparait en arrière

            playerController.enabled = true;
            canBeDamage = true;
            Debug.LogError("Player has been hit -1!" + manager.currentHealth);
        }
        else
        {
            playerAnimator.SetTrigger("Die");

            yield return new WaitForSeconds(1); // Wait for animation to finish
            canBeDamage = true;

            manager.Over();
        }
    }
}

