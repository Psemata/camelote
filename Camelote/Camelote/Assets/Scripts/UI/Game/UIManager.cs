using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : VisualElement
{

    VisualElement menuClosedInterface;
    VisualElement menuOpenInterface;
    VisualElement resourcesInterface;
    VisualElement interactionInterface;

    // Public class used to get the UIManager UXML
    public new class UxmlFactory : UxmlFactory<UIManager, UxmlTraits> { }

    public UIManager()
    {
        InteractionPOI.OnClick += OpenInteractionInterface;
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
        menuClosedInterface = this.Q("UIClosed");
        menuOpenInterface = this.Q("UIOpen");
        resourcesInterface = this.Q("UIResources");
        interactionInterface = this.Q("UIInteraction");

        // Button on the closed menu interface
        menuClosedInterface?.Q("ui-button-holder")?.Q("ui-opener")?.RegisterCallback<ClickEvent>(ev => OpenMenu());

        // Button on the open menu interface
        menuOpenInterface?.Q("ui-open")?.Q("ui-closer")?.RegisterCallback<ClickEvent>(ev => CloseMenu());
        menuOpenInterface?.Q("ui-open")?.Q("buttons-holder")?.Q("ui-resources").RegisterCallback<ClickEvent>(ev => ResourcesMenu());
        menuOpenInterface?.Q("ui-open")?.Q("buttons-holder")?.Q("ui-castle").RegisterCallback<ClickEvent>(ev => CastleMenu());

        // Back button
        resourcesInterface?.Q<Button>("back-button")?.RegisterCallback<ClickEvent>(ev => ResourcesBack());
        interactionInterface.Q<Button>("back-button").RegisterCallback<ClickEvent>(ev => InteractionBack());

        // Unregistrer from the event, so it can trigger only once (at the beginning)
        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Function used to open a point of interest interface
    /// </summary>
    /// <param name="name"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public void OpenInteractionInterface(string name, float latitude, float longitude)
    {
        // Set the data
        interactionInterface.Q<Label>("pof-name-label").text = name;
        interactionInterface.Q<Label>("pof-pos-label").text = "Lat. : " + latitude + " " + " Lon. : " + longitude;

        // Hide the drop informations
        interactionInterface.Q<Label>("info-resources-label").style.display = DisplayStyle.None;

        // Open the menu
        menuClosedInterface.style.display = DisplayStyle.None;
        interactionInterface.style.display = DisplayStyle.Flex;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsInMenu = true;
        }
    }

    /// <summary>
    /// Close the interaction menu
    /// </summary>
    void InteractionBack()
    {
        interactionInterface.style.display = DisplayStyle.None;
        menuClosedInterface.style.display = DisplayStyle.Flex;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsInMenu = false;
        }
    }

    /// <summary>
    /// Open the main menu
    /// </summary>
    void OpenMenu()
    {
        menuOpenInterface.style.display = DisplayStyle.Flex;
        menuClosedInterface.style.display = DisplayStyle.None;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsInMenu = true;
        }
    }

    /// <summary>
    /// Close the main menu
    /// </summary>
    void CloseMenu()
    {
        menuClosedInterface.style.display = DisplayStyle.Flex;
        menuOpenInterface.style.display = DisplayStyle.None;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IsInMenu = false;
        }
    }

    /// <summary>
    /// Open the resources menu
    /// </summary>
    void ResourcesMenu()
    {
        resourcesInterface.style.display = DisplayStyle.Flex;
        menuOpenInterface.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Close the resources menu
    /// </summary>
    void ResourcesBack()
    {
        menuOpenInterface.style.display = DisplayStyle.Flex;
        resourcesInterface.style.display = DisplayStyle.None;
    }

    /// <summary>
    /// Open the castle menu
    /// </summary>
    void CastleMenu()
    {
        GameManager.Instance.UpdateGameState(GameState.Castle);
    }
}
