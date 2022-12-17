using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRotation : MonoBehaviour
{
    private Transform cam;

    public Transform playerHands;

    private CinemachineHardLockToTarget cinemachineCamera;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);

        playerHands.rotation = Quaternion.Euler(cam.eulerAngles.x, cam.eulerAngles.y, 0f);
    }

    private void Rotation()
    {
        //float yAngle = CinemachineHardLockToTarget.
    }
}
