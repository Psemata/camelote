using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary> 
/// Tools to convert tab of objects into JSON and reverse
///</summary>
public static class JsonHelper {
    ///<summary> 
    /// Convert from JSON to table
    ///</summary>
    public static T[] FromJson<T>(string json) {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    ///<summary> 
    /// Convert to JSON from table
    ///</summary>
    public static string ToJson<T>(T[] array) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    ///<summary> 
    /// Convert to JSON from table with pretty print
    ///</summary>
    public static string ToJson<T>(T[] array, bool prettyPrint) {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    ///<summary> 
    /// Just add first and last {} for the JSON format
    ///</summary>
    public static string FixJson(string value) {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    ///<summary> 
    /// Wrap table into object
    ///</summary>
    [System.Serializable]
    private class Wrapper<T> {
        public T[] Items;
    }
}