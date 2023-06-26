using SDD.Events;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent; // Enemy
    Animator animator;
    public Transform playerTransform; // Player
    public float lookRadius = 5f;
    [Tooltip("How much dammage this enemy can cause")]
    public int dammage = 1;
    //public float rangeOfAttack = 1.3f;
    [Tooltip("Time required to pass before being able to attack again. Set to 0f to instantly attack again")]
    public float attackTimeout = 0.50f;

    // timeout deltatime
    private float attackTimeoutDelta;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDAttack;
    private int _animIDDie;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AssignAnimationIDs();
        attackTimeoutDelta = attackTimeout;        
    }

    void Update()
    {

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        //Chase the player
        if (distance <= lookRadius)
        {
            agent.SetDestination(playerTransform.position);
            transform.LookAt(playerTransform.position);
        }

        //Attack timeout timer
        if (attackTimeoutDelta >= 0.0f)
        {
            attackTimeoutDelta -= Time.deltaTime;
        }

        animator.SetFloat(_animIDSpeed, agent.velocity.magnitude);
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDAttack = Animator.StringToHash("Attack");
        _animIDDie = Animator.StringToHash("Die");
    }

    private void Attack()
    {
        animator.SetTrigger(_animIDAttack);
        EventManager.Instance.Raise(new PlayerGotHitEvent() { });
        // reset the attack timeout timer
        attackTimeoutDelta = attackTimeout;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && attackTimeoutDelta <= 0.0f)
        {            
            Attack();
        }
    }
    public void TakeDamage()
    {
        animator.SetTrigger(_animIDDie);
        StartCoroutine(destroyAgent());
    }

    IEnumerator destroyAgent()
    {
        agent.GetComponent<Collider>().enabled = false;
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        float wait = animatorInfo.length;
        yield return new WaitForSeconds(wait);
        Destroy(agent.gameObject);
    }
}
