using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuManager : VisualElement
{
    // All the different interfaces which will be switched to
    VisualElement startInterface;
    VisualElement loginInterface;
    VisualElement registerInterface;

    // Public class used to get the MainMenuManager UXML
    public new class UxmlFactory : UxmlFactory<MainMenuManager, UxmlTraits> { }

    public MainMenuManager()
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
        // Set all the interfaces to the children objects
        startInterface = this.Q("StartInterface");
        loginInterface = this.Q("LoginInterface");
        registerInterface = this.Q("RegisterInterface");

        // Set the functions to the main menu interface buttons
        startInterface?.Q("login-button")?.RegisterCallback<ClickEvent>(ev => LoginScreen());
        startInterface?.Q("register-button")?.RegisterCallback<ClickEvent>(ev => RegisterScreen());

        // Set the function to the back to the main menu button
        loginInterface?.Q("back-button")?.RegisterCallback<ClickEvent>(ev => BackToMainMenu());
        registerInterface?.Q("back-button")?.RegisterCallback<ClickEvent>(ev => BackToMainMenu());

        // Unregistrer from the event, so it can trigger only once (at the beginning)
        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Hide the other interfaces and show the main menu interface
    /// </summary>
    void BackToMainMenu()
    {
        startInterface.style.display = DisplayStyle.Flex;
        loginInterface.style.display = DisplayStyle.None;
        registerInterface.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Hide the main menu interface and show the login interface
    /// </summary>
    void LoginScreen()
    {
        startInterface.style.display = DisplayStyle.None;
        loginInterface.style.display = DisplayStyle.Flex;
        registerInterface.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Hide the main menu interface and show the register interface
    /// </summary>
    void RegisterScreen()
    {
        startInterface.style.display = DisplayStyle.None;
        loginInterface.style.display = DisplayStyle.None;
        registerInterface.style.display = DisplayStyle.Flex;
    }
}
