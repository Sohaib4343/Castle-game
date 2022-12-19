using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public AudioSource knifeSwingAudio;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsAttacking", true);
            knifeSwingAudio.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAttacking", false);

        }
    }
}
