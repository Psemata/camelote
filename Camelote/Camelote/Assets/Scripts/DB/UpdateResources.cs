using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateResources : MonoBehaviour
{
    public void CallUpdateResources()
    {
        StartCoroutine(ResourcesUpdate());
    }

    /// <summary>
    /// Update all the resources amounts
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator ResourcesUpdate()
    {
        WWWForm form = new WWWForm();

        foreach(KeyValuePair<int, Resource> entry in GameManager.Instance.Resources)
        {
            if(entry.Value != null) {
                form.AddField(entry.Value.id.ToString(), entry.Value.quantity.ToString());
            }
        }

        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/UpdateResourcesFromUser.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
    }
}
