using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserRegister : MonoBehaviour
{

    // Error event
    public delegate void ErrorAction();
    public static event ErrorAction OnError;

    // Registered event
    public delegate void RegisteredAction();
    public static event RegisteredAction OnRegister;

    public void CallRegister(string pseudo, string surname, string name, string email, string password, string checkPassword)
    {
        StartCoroutine(UserRegistration(pseudo, surname, name, email, password, checkPassword));
    }

    /// <summary>
    /// Send a request of user creation on the webserver
    /// </summary>
    /// <returns>IEnumerator</returns>
    IEnumerator UserRegistration(string pseudo, string surname, string name, string email, string password, string checkPassword)
    {
        if (pseudo != "" && surname != "" && name != "" && email != "" && password != "" && checkPassword != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("pseudo", pseudo);
            form.AddField("surname", surname);
            form.AddField("name", name);
            form.AddField("email", email);
            form.AddField("password", password);

            UnityWebRequest www = UnityWebRequest.Post("http://157.26.64.176/camelote/Register.php", form);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Get the data
                string data = www.downloadHandler.text;
                Debug.Log(data);
                // If data sends an error
                if (data == "-1" || password != checkPassword)
                {
                    if (OnError != null)
                        OnError();
                }
                else
                {
                    // If the registering passed
                    if (OnRegister != null)
                        OnRegister();
                }
            }
        }
        else
        {
            // If the fields were empty
            if (OnError != null)
                OnError();
        }
    }
}
