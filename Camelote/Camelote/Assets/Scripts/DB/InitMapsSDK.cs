using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps;

public class InitMapsSDK : MonoBehaviour
{
    /// <summary>
    /// At the init of the MapsSDK element, link itself to the DBManager
    /// </summary>
    void Awake()
    {
        DBManager.Instance.SetMapsSDK(gameObject.GetComponent<MapsService>());
    }
}
