///<summary> 
/// Object that represent a unit.
/// The properties are :
///     - Id
///     - Name of the unit
///     - Type of the unit
///     - Its strength
///     - Its endurance
///     - Its potential
///     - Its level
///     - The owner of the unit
///</summary>
[System.Serializable]
public class Unit {
    public int id;
    public string name;
    public int type;
    public int strength;
    public int endurance;
    public int potential;
    public int level;
    public int owner;
}
