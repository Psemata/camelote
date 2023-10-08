using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserLogin : MonoBehaviour
{
    // Error event
    public delegate void ErrorAction();
    public static event ErrorAction OnError;

    public void CallLogin(string email, string password)
    {
        StartCoroutine(UserConnection(email, password));
    }

    /// <summary>
    /// Send a connection request on the webserver
    /// </summary>
    /// <returns></returns>
    IEnumerator UserConnection(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/Login.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string data = www.downloadHandler.text;
            // If the account exist and the user can connect
            if (data != "-1")
            {
                string[] parsedData = data.Split(',');
                GameManager.Instance.SetUserInfo(int.Parse(parsedData[0]), parsedData[1]);
                // Once the user is connected, switch to gamestate to logged so the game can proceed
                GameManager.Instance.UpdateGameState(GameState.Logged);
            }
            else
            {
                // If the info are incorrect and there was an error
                if (OnError != null)
                    OnError();
            }
        }

    }
}
