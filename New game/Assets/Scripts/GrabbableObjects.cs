using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjects : MonoBehaviour
{
    private Rigidbody objectRigidBody;

    private Transform objectGrabPointTransform;

    private Transform cam;

    private Collider coll;

    private Vector3 originalScale;
    //public Vector3 offset;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        //this.GetComponent<Collider>();
        cam = Camera.main.transform;
    }

    private void Start()
    {
        originalScale = this.transform.localScale;

    }

    /*private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 500f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            objectRigidBody.MovePosition(newPosition);
        }
    }*/

    public void Grab(Transform objectGrabPointTransform)
    {
        //this.objectGrabPointTransform = objectGrabPointTransform;
        //transform.localScale = Vector3.one;
        this.transform.localScale = Vector3.one;

        this.transform.SetParent(objectGrabPointTransform);

        //this.transform.localScale = originalScale;

        this.transform.localScale = new Vector3(originalScale.x / objectGrabPointTransform.transform.localScale.x,
                                                originalScale.y / objectGrabPointTransform.transform.localScale.y,
                                                originalScale.z / objectGrabPointTransform.transform.localScale.z);
        objectRigidBody.useGravity = false;
        objectRigidBody.drag = 5f;
        objectRigidBody.angularVelocity = Vector3.zero;
        //coll.isTrigger = true;
        this.GetComponent<Collider>().isTrigger = true;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);//gunContainer.localRotation;
        //objectRigidBody.velocity = new Vector3(0f, 0f, 0f);
    }

    public void Drop()
    {
        //this.objectGrabPointTransform = null;
        this.transform.SetParent(null);
        objectRigidBody.useGravity = true;
        objectRigidBody.drag = 0f;
        objectRigidBody.AddForce(cam.forward * 4f, ForceMode.Impulse);
        //coll.isTrigger = false;
        this.GetComponent<Collider>().isTrigger = false;

        this.transform.localScale = originalScale;

    }
}
