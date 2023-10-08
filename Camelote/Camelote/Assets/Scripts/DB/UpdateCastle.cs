using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateCastle : MonoBehaviour
{
    public void CallCastleUpdate()
    {
        StartCoroutine(CastleUpdate());
    }

    /// <summary>
    /// Update the castle's level
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator CastleUpdate()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", GameManager.Instance.Id);
        form.AddField("level", GameManager.Instance.Castle.level);

        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/UpdateUserCastle.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
    }
}
