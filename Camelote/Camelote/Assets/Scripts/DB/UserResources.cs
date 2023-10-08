using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserResources : MonoBehaviour
{
    // Array containing all the resources from the connected user
    Resource[] userResources;

    // When the user get the resources, refresh the resources list
    public delegate void RefreshResources();
    public static event RefreshResources OnRefresh;

    public void CallResources()
    {
        StartCoroutine(UserResourcesData());
    }

    /// <summary>
    /// Send a request to get all the resources from the suer
    /// </summary>
    /// <returns></returns>
    IEnumerator UserResourcesData()
    {
        // Form used to send the user id to the php script
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.Instance.Id);

        // Create and send the post request to the php script
        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/ResourcesFromUser.php", form);
        yield return www.SendWebRequest();

        // If there is an error, return it
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Get the data in JSON form
            string data = www.downloadHandler.text;
            if (data != "-1")
            {
                string jsonString = JsonHelper.FixJson(data);
                this.userResources = JsonHelper.FromJson<Resource>(jsonString);
                // Convert the array of resources to a list
                foreach (Resource r in this.userResources)
                {
                    GameManager.Instance.Resources[r.resources] = r;
                }

                if (OnRefresh != null)
                    OnRefresh();
            }
            else
            {
                Debug.Log("No resources found");
            }
        }
    }
}
