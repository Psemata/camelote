using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UICastle : VisualElement
{

    // Public class used to get the UIManager UXML
    public new class UxmlFactory : UxmlFactory<UICastle, UxmlTraits> { }

    public UICastle()
    {
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    void OnGeometryChange(GeometryChangedEvent evt)
    {
        // Back button
        this.Q<Button>("back-button").RegisterCallback<ClickEvent>(ev => BackToGame());
        // Upgrade button
        this.Q<Button>("upgrade-button").RegisterCallback<ClickEvent>(ev => Upgrade());

        // Refresh the label data concerning the resources
        RefreshLabel();

        // Check the castle level
        if (GameManager.Instance != null && GameManager.Instance.Castle != null && GameManager.Instance.Castle.level >= 3)
        {
            DisableButton();
        }

        // Unregistrer from the event, so it can trigger only once (at the beginning)
        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Change label data according to the user's resources and the castle level
    /// </summary>
    void RefreshLabel()
    {
        if (GameManager.Instance != null && GameManager.Instance.Resources != null && GameManager.Instance.Castle != null)
        {
            // Calculate the resources requirements
            int requirements = GameManager.Instance.Castle.level * 100;

            // Change the labels texts
            this.Q<Label>("wheat-label").text = "Wheat : " + (GameManager.Instance.Resources[1] != null ? GameManager.Instance.Resources[1].quantity : 0) + "/" + requirements;
            this.Q<Label>("iron-label").text = "Iron : " + (GameManager.Instance.Resources[2] != null ? GameManager.Instance.Resources[2].quantity : 0) + "/" + requirements;
            this.Q<Label>("stone-label").text = "Stone : " + (GameManager.Instance.Resources[3] != null ? GameManager.Instance.Resources[3].quantity : 0) + "/" + requirements;
            this.Q<Label>("food-label").text = "Food : " + (GameManager.Instance.Resources[4] != null ? GameManager.Instance.Resources[4].quantity : 0) + "/" + requirements;
            this.Q<Label>("wood-label").text = "Wood : " + (GameManager.Instance.Resources[5] != null ? GameManager.Instance.Resources[5].quantity : 0) + "/" + requirements;
            this.Q<Label>("gold-label").text = "Gold : " + (GameManager.Instance.Resources[6] != null ? GameManager.Instance.Resources[6].quantity : 0) + "/" + requirements;
        }
    }

    /// <summary>
    /// If the castle is at level 3, the upgrade button is disabled
    /// </summary>
    void DisableButton()
    {
        this.Q<Button>("upgrade-button").SetEnabled(false);
    }

    /// <summary>
    /// Return to the main scene
    /// </summary>
    void BackToGame()
    {
        GameManager.Instance.IsInMenu = false;
        GameManager.Instance.UpdateGameState(GameState.BackFromCastle);
    }

    /// <summary>
    /// Update the castle level
    /// </summary>
    void Upgrade()
    {
        // If the user can upgrade he upgrade it
        if (GameObject.Find("Castle").GetComponent<CastleManagement>().UpgradeCastle())
        {
            this.Q<Label>("error-label").style.display = DisplayStyle.None;
            // Refresh the labels texts with the new amount of resources
            RefreshLabel();
            if (GameManager.Instance.Castle.level >= 3)
            {
                DisableButton();
            }
        }
        else
        {
            // Display an error error
            this.Q<Label>("error-label").style.display = DisplayStyle.Flex;
        }
    }
}
