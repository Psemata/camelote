using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPOI : MonoBehaviour
{

    // Click event
    public delegate void ClickAction(string pointName, float latitude, float longitude);
    public static event ClickAction OnClick;

    // Values to be displayed
    private string PointName { get; set; }
    private float Latitude { get; set; }
    private float Longitude { get; set; }

    /// <summary>
    /// When the interest point is clicked on
    /// </summary>
    void OnMouseDown()
    {
        if (!GameManager.Instance.IsInMenu && OnClick != null)
        {
            OnClick(this.PointName, this.Latitude, this.Longitude);
        }
    }

    /// <summary>
    /// Set the important data to be displayed
    /// </summary>
    /// <param name="pointName"></param>
    /// <param name="latitude"></param>
    /// <param name="longitude"></param>
    public void SetPointData(string pointName, float latitude, float longitude)
    {
        this.PointName = pointName;
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}