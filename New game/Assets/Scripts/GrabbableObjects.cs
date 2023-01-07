using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjects : MonoBehaviour
{
    private Rigidbody objectRigidBody;

    private Transform objectGrabPointTransform;

    private Transform cam;

    public Collider coll;

    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 40f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            objectRigidBody.MovePosition(newPosition);
        }
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        //this.transform.SetParent(objectGrabPointTransform);
        objectRigidBody.useGravity = false;
        objectRigidBody.drag = 20f;
        coll.isTrigger = true;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);//gunContainer.localRotation;
        //transform.localScale = Vector3.one;
        //objectRigidBody.velocity = new Vector3(0f, 0f, 0f);
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        //this.transform.SetParent(null);
        objectRigidBody.useGravity = true;
        objectRigidBody.drag = 0f;
        objectRigidBody.AddForce(cam.forward * 4f, ForceMode.Impulse);
        coll.isTrigger = false;
    }
}
