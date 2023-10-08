using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIResources : VisualElement
{
    // ListView element
    private ListView resourcesList;
    private List<Resource> resources;

    // Public class used to get the UIManager UXML
    public new class UxmlFactory : UxmlFactory<UIResources, UxmlTraits> { }

    public UIResources()
    {
        UserResources.OnRefresh += RefreshData;
        this.RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// Use of OnGeometryChange and not OnEnabled because this script inherit VisualElement and not MonoBehaviour 
    /// </summary>
    /// <param name="evt"></param>
    void OnGeometryChange(GeometryChangedEvent evt)
    {
        this.resourcesList = this.Q<ListView>("resources-list");

        // Create the list view for the resources
        if (GameManager.Instance != null && GameManager.Instance.Resources != null)
        {
            this.resources = DictToList(GameManager.Instance.Resources);
            this.resourcesList.makeItem = MakeItem;
            this.resourcesList.bindItem = BindItem;
            this.resourcesList.itemsSource = this.resources;
            this.resourcesList.fixedItemHeight = 30;
        }

        // Unregistrer from the event, so it can trigger only once (at the beginning)
        this.UnregisterCallback<GeometryChangedEvent>(OnGeometryChange);
    }

    /// <summary>
    /// MakeItem, create the VisualElement which will contain the list data
    /// </summary>
    /// <returns></returns>
    private VisualElement MakeItem()
    {
        //Here we take the uxml and make a VisualElement
        VisualTreeAsset resourceItem = Resources.Load<VisualTreeAsset>("ResourceElement");
        VisualElement listItem = resourceItem.Instantiate();
        return listItem;
    }

    /// <summary>
    /// BindItem, bind the data to the VisualElement previously created
    /// </summary>
    /// <param name="e"></param>
    /// <param name="index"></param>
    private void BindItem(VisualElement e, int index)
    {
        //We add the resource name to the label of the list item
        e.Q<Label>("name-label").text = this.resources[index].name;
        //We add the resource quantity to the label of the list item
        e.Q<Label>("quantity-label").text = this.resources[index].quantity.ToString();
    }

    /// <summary>
    /// Translate the dictionary to a list
    /// </summary>
    /// <param name="resources"></param>
    /// <returns></returns>
    private List<Resource> DictToList(Dictionary<int, Resource> resources)
    {
        List<Resource> list = new List<Resource>();
        foreach (KeyValuePair<int, Resource> kvp in resources)
        {
            if (kvp.Value != null)
            {
                list.Add(kvp.Value);
            }
        }
        return list;
    }

    /// <summary>
    /// Used to refresh list data
    /// </summary>
    public void RefreshData()
    {
        if (this.resourcesList != null)
        {
            this.resources = DictToList(GameManager.Instance.Resources);
            this.resourcesList.itemsSource = this.resources;
            this.resourcesList.Rebuild();
        }
    }
}
