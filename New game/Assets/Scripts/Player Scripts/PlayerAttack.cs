using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Animation")]
    public Animator animator;

    [Header("Audio")]
    public AudioSource knifeSwingAudio;
    public AudioSource knifeHit;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public bool enemyDetected;

    public float nextAttack = 0.0f;
    public float attackRate = 0.5f;

    private EnemyHealth enemyHealth;
    //public GameObject[] enemies;

    //Camera Shake
    private ShockWaveUnityEvent shockWave;

    private void Awake()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();

        shockWave = GetComponent<ShockWaveUnityEvent>();
    }

    void Update()
    {
        enemyDetected = Physics.CheckSphere(attackPoint.position, attackRange, enemyLayer);

        if (Input.GetMouseButtonDown(0) && enemyDetected && Time.time > nextAttack)
        {
            animator.SetBool("IsAttacking", true);
            knifeSwingAudio.Play();
            knifeHit.Play();
            enemyHealth.TakeDamage(30f);
            shockWave.ShockWaveEvent();
            nextAttack = Time.time + attackRate;
        }
        else if (Input.GetMouseButtonDown(0) && !enemyDetected && Time.time > nextAttack)
        {
            animator.SetBool("IsAttacking", true);
            knifeSwingAudio.Play();
            shockWave.ShockWaveEvent();
            nextAttack = Time.time + attackRate;
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    //Draw a wire sphere around the attack point
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
