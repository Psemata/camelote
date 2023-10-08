using System.Collections;
using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using Google.Maps.Coord;
using Google.Maps.Examples.Shared;
using Google.Maps;

/// <summary>
/// Example showing how to have the player's real-world GPS location reflected by on-screen
/// movements.
/// </summary>
/// <remarks>
/// Uses <see cref="ErrorHandling"/> component to display any errors encountered by the
/// <see cref="MapsService"/> component when loading geometry.
/// </remarks>
// [RequireComponent(typeof(MapsService), typeof(ErrorHandling))]
public class PlayerMovementsLocations : MonoBehaviour
{
    // Dialog for location services request.
#if PLATFORM_ANDROID
    GameObject dialog = null;
#endif

    /// <summary>
    /// The maps service.
    /// </summary>
    public MapsService MapsService;
    public Vector3 floatingOrigin = new Vector3(0, 0, 0);

    /// <summary>
    /// The player's last recorded real-world location and the the current floating origin.
    /// </summary>
    private LatLng PreviousLocation;

    /// <summary>Start following player's real-world location.</summary>
    private void Start()
    {
        GetPermissions();
        StartCoroutine(Follow());
        floatingOrigin = GetCameraPositionOnGroundPlane();
        // Once the service has started, spawn the game elements
        GameManager.Instance.UpdateGameState(GameState.Game);
    }

    /// <summary>
    /// Follow player's real-world location, as derived from the device's GPS signal.
    /// </summary>
    private IEnumerator Follow()
    {
        // If location is allowed by the user, start the location service and compass, otherwise abort
        // the coroutine.
#if PLATFORM_IOS
            // The location permissions request in IOS does not seem to get invoked until it is called for
            // in the code. It happens at runtime so if the code is not trying to access the location
            // right away, it will not pop up the permissions dialog.
            Input.location.Start();
#endif

        while (!Input.location.isEnabledByUser)
        {
            Debug.Log("Waiting for location services to become enabled..");
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Location services is enabled.");

#if !PLATFORM_IOS
        Input.location.Start();
#endif

        Input.compass.enabled = true;

        // Wait for the location service to start.
        while (true)
        {
            if (Input.location.status == LocationServiceStatus.Initializing)
            {
                // Starting, just wait.
                yield return new WaitForSeconds(1f);
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                // Failed, abort the coroutine.
                Debug.LogError("Location Services failed to start.");

                yield break;
            }
            else if (Input.location.status == LocationServiceStatus.Running)
            {
                // Started, continue the coroutine.
                break;
            }
        }

        // Get the MapsService component and load it at the device location.
        PreviousLocation = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);
        // Error protection
        /*
        if (!MapsService.Projection.IsFloatingOriginSet)
        {
            MapsService.InitFloatingOrigin(PreviousLocation);
        }
        */
        MapsService.InitFloatingOrigin(PreviousLocation);
        MapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);
    }

    /// <summary>
    /// Moves the camera and refreshes the map as the player moves.
    /// </summary>
    private void Update()
    {
        if (MapsService == null)
        {
            return;
        }

        // Get the current map location.
        LatLng currentLocation = new LatLng(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Vector3 currentWorldLocation = MapsService.Projection.FromLatLngToVector3(currentLocation);

        // Move the camera to the current map location.
        Vector3 targetCameraPosition = new Vector3(-currentWorldLocation.x, 0, -currentWorldLocation.z);

        MapsService.transform.position = Vector3.Lerp(MapsService.transform.position, targetCameraPosition, Time.deltaTime * 5);

        Vector3 newOrigin = GetCameraPositionOnGroundPlane();
        if (Vector3.Distance(floatingOrigin, newOrigin) > 100)
        {
            MapsService.MoveFloatingOrigin(newOrigin);

            MapsService.transform.position = new Vector3(0, 0, 0);

            MapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);

            floatingOrigin = newOrigin;
        }
    }

    private Vector3 GetCameraPositionOnGroundPlane()
    {
        Vector3 result = MapsService.transform.position;
        // Ignore the Y value since the floating origin only really makes sense on the ground plane.
        result.y = 0;
        result.x = -result.x;
        result.z = -result.z;
        return result;
    }

    private void GetPermissions()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            dialog = new GameObject();
        }
#endif // Android
    }

    void OnGUI()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            // The user denied permission to use location information.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
        }
#endif
    }
}

