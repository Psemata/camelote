using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Logged,
    Game,
    Castle,
    BackFromCastle
}

public class GameManager : MonoBehaviour
{
    // Game State
    public GameState State { get; set; }
    // Singleton Instance
    public static GameManager Instance;

    // User's id
    public int Id { get; set; }
    // User's pseudo
    public string Pseudo { get; set; }
    // User's resources
    public Dictionary<int, Resource> Resources { get; set; }
    // User's castle
    public Castle Castle { get; set; }
    // User's units
    public List<Unit> Units { get; set; }

    // Value which tells if the user is in the menu or not
    public bool IsInMenu { get; set; }

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

        // Line used to not destroy the GameManager when the user changes scene
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// When the game start, the first state is the Menu one
    /// </summary>
    void Start()
    {
        // Initialization of the dictionary
        this.Resources = new Dictionary<int, Resource>();
        for (int i = 1; i <= 6; i++)
        {
            this.Resources.Add(i, null);
        }

        UpdateGameState(GameState.Menu);
    }

    /// <summary>
    /// Set the users info in the GameManager
    /// </summary>
    /// <param name="id"></param>
    /// <param name="pseudo"></param>
    public void SetUserInfo(int id, string pseudo)
    {
        this.Id = id;
        this.Pseudo = pseudo;
    }

    /// <summary>
    /// Spend the resources
    /// </summary>
    public void SpendResources() {
        for(int i = 1; i <= Resources.Count ; i++) {
            Resources[i].quantity -= Castle.level * 100;
        }
    }

    /// <summary>
    /// Update the game state
    /// </summary>
    /// <param name="newState"></param>
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Logged:
                // Go to the main scene once the user is connected
                SceneManager.LoadScene("MainGame");
                // Load the user resources
                DBManager.Instance.UserResources();
                // Load the user castle
                DBManager.Instance.UserCastle();
                break;
            case GameState.Game:
                // The Google Maps service has started, the game can then begin
                DBManager.Instance.LoadPOI();
                break;
            case GameState.Castle:
                // The user decides to go the to Castle scene
                SceneManager.LoadScene("Castle");
                break;
            case GameState.BackFromCastle:
                // Change the scene from the castle to the game scene
                SceneManager.LoadScene("MainGame");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}
