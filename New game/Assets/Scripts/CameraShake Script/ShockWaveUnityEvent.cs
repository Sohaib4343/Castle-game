using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShockWaveUnityEvent : MonoBehaviour
{
    public UnityEvent shock;
    
    public void ShockWaveEvent()
    {
        shock.Invoke();
    }

}
