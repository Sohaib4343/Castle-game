using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    //Header("Attack")]
    /*public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public bool enemyDetected;*/

    [Header("Audio")]
    public AudioSource knifeSwingAudio;
    public AudioSource knifeHit;

    private ShockWaveUnityEvent shockWave;

    private EnemyHealth enemyHealth;

    private void Awake()
    {
        shockWave = GameObject.FindGameObjectWithTag("Player").GetComponent<ShockWaveUnityEvent>();
        enemyHealth = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyHealth>();

    }

    void Update()
    {
        //enemyDetected = Physics.CheckSphere(attackPoint.position, attackRange, enemyLayer);
    }

    public void AttackIfTheEnemyIsDetected()
    {
        //if (Input.GetMouseButtonDown(0) && enemyDetected && attackTimer <= 0/*&& Time.time > nextAttack*/)
        //{
            
            //animator.SetBool("IsAttacking", true);
            knifeSwingAudio.Play();
            knifeHit.Play();
            enemyHealth.TakeDamage(30f);
            shockWave.ShockWaveEvent();
        Debug.Log("Enemy is in range");
            //attackTimer = attackDelay;
        //}
        //else if (Input.GetMouseButtonDown(0) && !enemyDetected && attackTimer <= 0/*&& Time.time > nextAttack*/)
        //{
            //animator.SetBool("IsAttacking", true);
            //knifeSwingAudio.Play();
            //shockWave.ShockWaveEvent();
            //attackTimer = attackDelay;
            //Debug.Log("Attack");
            //}
        //}

        /*if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);
        }*/
    }

    public void AttackIfThereIsNoEnemyInRange()
    {
        knifeSwingAudio.Play();
        shockWave.ShockWaveEvent();
        Debug.Log("Enemy is not in range");
    }
}
