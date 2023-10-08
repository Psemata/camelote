using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManagement : MonoBehaviour
{

    // All the castles prefab
    public GameObject Castle_1;
    public GameObject Castle_2;
    public GameObject Castle_3;

    /// <summary>
    /// Function which is called at the creation of the object
    /// </summary>
    void Awake()
    {
        UpdateCastle();
    }

    /// <summary>
    /// Upgrade the level of the castle
    /// </summary>
    public bool UpgradeCastle()
    {
        // If there is enough resources
        if (EnoughResources())
        {
            // Spend them
            GameManager.Instance.SpendResources();
        }
        else
        {
            return false;
        }
        // Upgrade the castle level
        GameManager.Instance.Castle.level++;
        // Call the database scripts to upgrade the castle level and update the resources' amounts
        // Update the castle's level in the database
        DBManager.Instance.UpdateCastle();
        // Update the resources in the database
        DBManager.Instance.UpdateResources();
        // Change the castle appearance
        UpdateCastle();
        return true;
    }

    /// <summary>
    /// Update Castle Game Object with its level
    /// </summary>
    public void UpdateCastle()
    {
        if (GameObject.Find("Castle"))
        {
            HideCastle();
            // Show the corresponding castle form
            switch (GameManager.Instance.Castle.level)
            {
                case 1:
                    Castle_1.SetActive(true);
                    break;
                case 2:
                    Castle_2.SetActive(true);
                    break;
                case 3:
                    Castle_3.SetActive(true);
                    break;
            }
        }
    }

    /// <summary>
    /// private function used to hide all the prefac castle object
    /// </summary>
    private void HideCastle()
    {
        Castle_1.SetActive(false);
        Castle_2.SetActive(false);
        Castle_3.SetActive(false);
    }

    /// <summary>
    /// Check the user resources if the amount is enough
    /// </summary>
    private bool EnoughResources()
    {
        foreach (KeyValuePair<int, Resource> entry in GameManager.Instance.Resources)
        {
            if (entry.Value.quantity < GameManager.Instance.Castle.level * 100)
            {
                return false;
            }
        }
        return true;
    }
}
