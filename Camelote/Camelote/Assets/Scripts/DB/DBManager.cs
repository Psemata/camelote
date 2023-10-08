using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Maps;

public class DBManager : MonoBehaviour
{
    // Singleton instance
    public static DBManager Instance;

    // Scripts used to access the database
    public LoadPointOfInterest lpof;
    public UserRegister usrr;
    public UserLogin usrl;
    public UserResources usrre;
    public UserCastle usrc;
    public UpdateResources upr;
    public UpdateCastle upc;
    public InsertResources irs;

    // Awake is called before the start function
    void Awake()
    {
        // If the instance exist then use it
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Line used to not destroy the DBManager when the user changes scene
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Load all the points of interest
    /// </summary>
    public void LoadPOI()
    {
        lpof.CallPOI();
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    public void UserRegister(string pseudo, string surname, string name, string email, string password, string checkPassword)
    {
        usrr.CallRegister(pseudo, surname, name, email, password, checkPassword);
    }

    /// <summary>
    /// Log a user in
    /// </summary>
    public void UserLogin(string email, string password)
    {
        usrl.CallLogin(email, password);
    }

    /// <summary>
    /// Get all the user's resources
    /// </summary>
    public void UserResources()
    {
        usrre.CallResources();
    }

    /// <summary>
    /// Get the user's castle
    /// </summary>
    public void UserCastle()
    {
        usrc.CallCastle();
    }

    /// <summary>
    /// Update the user's resources
    /// </summary>
    public void UpdateResources()
    {
        upr.CallUpdateResources();
    }

    /// <summary>
    /// Update the user's castle level
    /// </summary>
    public void UpdateCastle()
    {
        upc.CallCastleUpdate();
    }

    /// <summary>
    /// Insert new resources
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="amounts"></param>
    public void InsertResources(List<int> ids, List<int> amounts)
    {
        irs.CallResourcesInsert(ids, amounts);
    }

    /// <summary>
    /// Set the MapsService element
    /// </summary>
    /// <param name="elem"></param>
    public void SetMapsSDK(MapsService elem)
    {
        lpof.MapsService = elem;
    }
}
