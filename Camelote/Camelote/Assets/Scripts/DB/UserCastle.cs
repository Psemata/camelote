using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserCastle : MonoBehaviour
{
    // Object containing the user's castle
    Castle userCastle;

    public void CallCastle()
    {
        StartCoroutine(GetCastle());
    }

    /// <summary>
    /// Send a request to get the user's castle
    /// </summary>
    /// <returns></returns>
    IEnumerator GetCastle()
    {
        // Form used to send the user id to the php script
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.Instance.Id);

        // Create and send the post request to the php script
        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/UserCastle.php", form);
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
                this.userCastle = JsonHelper.FromJson<Castle>(jsonString)[0];
                // Get the user's castle
                GameManager.Instance.Castle = this.userCastle;
            }
            else
            {
                Debug.Log("No resources found");
            }
        }
    }
}
