using UnityEngine;
using UnityEngine.UIElements;

public class RegisterInterface : VisualElement
{
    public new class UxmlFactory : UxmlFactory<RegisterInterface, UxmlTraits> { }

    public RegisterInterface()
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
        UserRegister.OnError += ShowErrorRegister;
        UserRegister.OnRegister += ShowRegister;
        this.Q("button-holder").Q("register-button").RegisterCallback<ClickEvent>(ev => Register());
        this.Q("back-button").RegisterCallback<ClickEvent>(ev => { this.Q("register-info").style.display = DisplayStyle.None; this.Q("register-info").style.color = new StyleColor(Color.red); });
    }

    /// <summary>
    /// Unregister the events when the interface is being disabled
    /// </summary>
    void OnDisable()
    {
        UserRegister.OnError -= ShowErrorRegister;
        UserRegister.OnRegister -= ShowRegister;
    }

    /// <summary>
    /// Show that the user has been added
    /// </summary>
    void ShowRegister()
    {
        this.Q("register-info").style.display = DisplayStyle.Flex;
        this.Q("register-info").style.color = new StyleColor(Color.green);
        ((Label)this.Q("register-info")).text = "User correctly added";
    }

    /// <summary>
    /// Show an error if the user has not been added correctly
    /// </summary>
    void ShowErrorRegister()
    {
        this.Q("register-info").style.display = DisplayStyle.Flex;
        ((Label)this.Q("register-info")).text = "Email already used or password check incorrect";
    }

    /// <summary>
    /// Register the user to the database
    /// </summary>
    void Register()
    {
        string pseudo = ((TextField)this.Q("pseudo-input")).value;
        string surname = ((TextField)this.Q("surname-input")).value;
        string name = ((TextField)this.Q("name-input")).value;
        string email = ((TextField)this.Q("email-input")).value;
        string password = ((TextField)this.Q("password-input")).value;
        string checkPassword = ((TextField)this.Q("password-check-input")).value;

        DBManager.Instance.UserRegister(pseudo, surname, name, email, password, checkPassword);
    }
}
