using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickAndDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;

    public GrabbableObjects grabbableObjects;

    //Variables for shooting the ray
    float pickUpDistance = 5f;
    private bool isRayIsHittingTheGrabbleObject;
    private RaycastHit rayCastHit;

    public bool isGrabbed;

    private WeaponPickAndDrop weaponPickAndDrop;

    private void Awake()
    {
        weaponPickAndDrop = GetComponent<WeaponPickAndDrop>();
    }

    void Update()
    {
        isRayIsHittingTheGrabbleObject = Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out rayCastHit,
                    pickUpDistance, pickUpLayerMask);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbableObjects == null && !weaponPickAndDrop.isEquiped)
            {
                //Not carrying something, grab
                //float pickUpDistance = 10f;
                if (isRayIsHittingTheGrabbleObject)
                {
                    if (rayCastHit.transform.TryGetComponent(out grabbableObjects))
                    {
                        grabbableObjects.Grab(objectGrabPointTransform);

                        Debug.Log("Object grabbed");

                        isGrabbed = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Currently carrying something, drop
            grabbableObjects.Drop();
            grabbableObjects = null;
            isGrabbed = false;
        }
    }
}
