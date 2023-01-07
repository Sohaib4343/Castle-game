using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRotation : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        //Rotate the player on y axis along the camera
        transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
    }
}
