using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInteraction : VisualElement
{
    // Public class used to get the UIManager UXML
    public new class UxmlFactory : UxmlFactory<UIInteraction, UxmlTraits> { }

    private Dictionary<int, string> ResourcesNames { get; set; }

    public UIInteraction()
    {
        this.ResourcesNames = new Dictionary<int, string>();
        this.ResourcesNames.Add(1, "Blé");
        this.ResourcesNames.Add(2, "Fer");
        this.ResourcesNames.Add(3, "Pierre");
        this.ResourcesNames.Add(4, "Nourriture");
        this.ResourcesNames.Add(5, "Bois");
        this.ResourcesNames.Add(6, "Or");

        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        // Upgrade button
        this.Q<Button>("resources-button").RegisterCallback<ClickEvent>(ev => GetResources());

        // Unregistrer from the event, so it can trigger only once (at the beginning)
        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Generate the two resources got from the point of interest
    /// </summary>
    void GetResources()
    {
        // Lists which will contain the new resources data
        List<int> resourcesIDs = new List<int>();
        List<int> resourcesAmounts = new List<int>();

        string informationText = "";

        // Resource max amount
        int max = GameManager.Instance.Castle.level * 10;

        for (int i = 0; i < 2; i++)
        {
            // Generate the random resource
            int resourceId = Random.Range(1, 7);
            // Generate the random amount
            int resourceAmount = Random.Range(5, max + 1);
            if (GameManager.Instance.Resources[resourceId] == null)
            {
                // Add it to the "to be inserted" list
                resourcesIDs.Add(resourceId);
                resourcesAmounts.Add(resourceAmount);
            }
            else
            {
                GameManager.Instance.Resources[resourceId].quantity += resourceAmount;
            }

            informationText += this.ResourcesNames[resourceId] + " : " + resourceAmount + "\n";
        }

        // Show what the user got
        this.Q<Label>("info-resources-label").text = informationText;
        this.Q<Label>("info-resources-label").style.display = DisplayStyle.Flex;

        // Update database
        DBManager.Instance.UpdateResources();
        if (resourcesIDs.Count > 0)
        {
            DBManager.Instance.InsertResources(resourcesIDs, resourcesAmounts);
        }
    }
}
