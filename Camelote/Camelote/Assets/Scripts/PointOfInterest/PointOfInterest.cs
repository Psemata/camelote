///<summary> 
/// Object that represent a point of interest.
/// The properties are :
///     - Id
///     - Name of the point
///     - Latitude
///     - Longitude
///     - If the point is an arena or not
///     - The path to the image of the point of interest
///</summary>
[System.Serializable]
public class PointOfInterest {
    public int id;
    public string name;
    public float latitude;
    public float longitude;
    public bool is_arena;
    public string image;    
}
