using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Google.Maps.Coord;
using Google.Maps;

///<summary> 
/// Component used to get data from the database and convert this data to c# objects. 
/// Objects are used to instantiate GameObject with info.
///</summary>
public class LoadPointOfInterest : MonoBehaviour
{
    // MapService - Google Maps API element
    public MapsService MapsService;
    // Object used to replicate the point of interest
    public GameObject pofObject;
    // Array containing the database elements
    PointOfInterest[] pointsOfInterest;
    // List containing all the points of interest
    List<GameObject> pointsOfInterestGO = new List<GameObject>();

    public void CallPOI()
    {
        StartCoroutine(GetPointOfInterests());
    }

    ///<summary> 
    /// Get data and convert into objects and call the method who instantiate GameObject.
    ///</summary>
    IEnumerator GetPointOfInterests()
    {
        // Get the request from the URL where the php script for all the points of interests is
        // The script is stored in a server from the HE-Arc
        UnityWebRequest www = UnityWebRequest.Get("http://157.26.64.176/camelote/AllInterestPoints.php");
        yield return www.SendWebRequest();

        // If there is an error, return it
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get the JSON needed and fix some details
            string data = www.downloadHandler.text;
            // If Json has been retrieved
            if (data != "-1")
            {
                string jsonString = JsonHelper.FixJson(data);
                // Create an array from the JSON received
                this.pointsOfInterest = JsonHelper.FromJson<PointOfInterest>(jsonString);
                // Instantiate the GameObjects for all the points of interest
                AddGameObject();
            }
            else
            {
                Debug.Log("No points of interest found");
            }
        }
    }

    ///<summary>    
    /// Instantiate GameObject from objects.
    ///</summary>
    void AddGameObject()
    {
        foreach (PointOfInterest point in this.pointsOfInterest)
        {
            // Create the latitude and longitude position
            LatLng ping = new LatLng(point.latitude, point.longitude);
            // From those, we create a Vector3 position
            Vector3 position = MapsService.Projection.FromLatLngToVector3(ping);
            // Change the y position so they are a bit higher than the ground
            position.y = 40;
            // Basic rotation is needed
            Quaternion rotation = new Quaternion(-1, 0, 0, 1);

            // Instantiate the object and add it to the list
            pointsOfInterestGO.Add(Instantiate(pofObject, position, rotation));
            // Set the properties -> Done after the Instantiate, otherwise, the data would have been set in the void
            pointsOfInterestGO[pointsOfInterestGO.Count - 1].GetComponent<InteractionPOI>().SetPointData(point.name, point.latitude, point.longitude);
            // Set the MapsService as parent so that they move with the map
            pointsOfInterestGO[pointsOfInterestGO.Count - 1].transform.SetParent(MapsService.transform, false);
        }
    }
}