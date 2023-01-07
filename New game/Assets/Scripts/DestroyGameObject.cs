using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectibleObjects"))
        {
            Destroy(other.gameObject);
            Debug.Log("Trigger enter is getting called");
        }
    }
}
