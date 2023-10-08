using UnityEngine;
using UnityEngine.UIElements;

public class LoginInterface : VisualElement
{
    public new class UxmlFactory : UxmlFactory<LoginInterface, UxmlTraits> { }

    public LoginInterface()
    {
        // Register to the GeometryChangedEvent
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Use of OnGeometryChange and not OnEnabled because this script inherit VisualElement and not MonoBehaviour 
    /// </summary>
    /// <param name="evt"></param>
    void OnGeometryChange(GeometryChangedEvent evt)
    {
        // Subscribe to the error event on the login UI
        UserLogin.OnError += ShowErrorLogin;
        this.Q("button-holder").Q("login-button").RegisterCallback<ClickEvent>(ev => Login());
        this.Q("back-button").RegisterCallback<ClickEvent>(ev => this.Q("login-info").style.display = DisplayStyle.None);
    }

    /// <summary>
    /// Unsubscribe to the error event
    /// </summary>
    void OnDisable()
    {
        UserLogin.OnError -= ShowErrorLogin;
    }

    // Affiche une erreur à l'écran
    void ShowErrorLogin()
    {
        this.Q("login-info").style.display = DisplayStyle.Flex;
        ((Label)this.Q("login-info")).text = "Login informations incorrect";
    }

    /// <summary>
    /// Function used to login
    /// </summary>
    void Login()
    {
        string email = ((TextField)this.Q("email-input")).value;
        string password = ((TextField)this.Q("password-input")).value;
        DBManager.Instance.UserLogin(email, password);
    }
}
