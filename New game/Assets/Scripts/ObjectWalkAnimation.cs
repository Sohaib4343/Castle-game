using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWalkAnimation : MonoBehaviour
{
    private Animator animator;

    //Script Reference
    private ObjectPickAndDrop objectPickAndDropScriptReference;
    private FirstPersonController firstPersonControllerScriptReference;

    void Awake()
    {
        animator = GetComponent<Animator>();

        objectPickAndDropScriptReference = GameObject.Find("FirstPersonController").GetComponent<ObjectPickAndDrop>();
        firstPersonControllerScriptReference = GameObject.Find("FirstPersonController").GetComponent<FirstPersonController>();
    }


    void Update()
    {
        if(objectPickAndDropScriptReference.isGrabbed && firstPersonControllerScriptReference.isMoving)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
