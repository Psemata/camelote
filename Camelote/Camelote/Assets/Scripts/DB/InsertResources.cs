using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InsertResources : MonoBehaviour
{
    public void CallResourcesInsert(List<int> ids, List<int> amounts)
    {
        StartCoroutine(ResourcesInsert(ids, amounts));
    }

    /// <summary>
    /// Insert all the new resources
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator ResourcesInsert(List<int> ids, List<int> amounts)
    {
        WWWForm form = new WWWForm();

        form.AddField("id", GameManager.Instance.Id);

        for (int i = 0; i < ids.Count; i++)
        {
            form.AddField(ids[i].ToString(), amounts[i].ToString());
        }

        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/InsertUserResources.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }

        DBManager.Instance.UserResources();
    }
}
