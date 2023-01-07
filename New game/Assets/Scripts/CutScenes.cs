using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenes : MonoBehaviour
{

    public GameObject playerCam;
    public GameObject secondCam;

    public GameObject playerBody;
    public GameObject playerHands;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerBody.SetActive(false);
            playerCam.gameObject.SetActive(false);
            secondCam.SetActive(true);
            playerHands.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerCam.gameObject.SetActive(true);
            secondCam.SetActive(false);
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        playerHands.SetActive(true);
        playerBody.SetActive(true);
    }
}
