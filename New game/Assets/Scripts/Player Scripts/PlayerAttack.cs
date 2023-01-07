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
    public RaycastHit hit;

    //Attack delay
    public float attackDelay = 1f;
    private float attackTimer = 0f;

    //Health
    private EnemyHealth enemyHealth;

    //Camera Shake
    private ShockWaveUnityEvent shockWave;

    //Weapon equip
    private WeaponPickAndDrop pickAndDrop;

    private void Awake()
    {
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();
        pickAndDrop = GetComponent<WeaponPickAndDrop>();

        shockWave = GetComponent<ShockWaveUnityEvent>();
    }

    void Update()
    {
        //Shoots a ray from the position of attackPoint
        enemyDetected = Physics.Raycast(attackPoint.transform.position, attackPoint.transform.forward, out hit, attackRange, enemyLayer);

        //Draw a ray to see it in the scene view
        Debug.DrawRay(attackPoint.transform.position, attackPoint.transform.forward, Color.red);

        attackTimer -= Time.deltaTime;

        Attack();
    }

    private void Attack()
    {
        if (pickAndDrop.isEquiped)
        {
            if (Input.GetMouseButtonDown(0) && enemyDetected && attackTimer <= 0/*&& Time.time > nextAttack*/)
            {
                //animator.SetBool("IsAttacking", true);
                animator.SetBool("Attack", true);
                knifeSwingAudio.Play();
                knifeHit.Play();
                enemyHealth.TakeDamage(30f);
                shockWave.ShockWaveEvent();
                attackTimer = attackDelay;
            }
            else if (Input.GetMouseButtonDown(0) && !enemyDetected && attackTimer <= 0/*&& Time.time > nextAttack*/)
            {
                //animator.SetBool("IsAttacking", true);
                animator.SetBool("Attack", true);

                knifeSwingAudio.Play();
                shockWave.ShockWaveEvent();
                attackTimer = attackDelay;
                Debug.Log("Attack");
            }

            if (Input.GetMouseButtonUp(0))
            {
                //animator.SetBool("IsAttacking", false);
                animator.SetBool("Attack", false);
            }
        }
    }
}
