using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- John Esslemont

public class rCade_GPS : MonoBehaviour {

    #region Public Variables

    public bool CanCheckGPS;

    #endregion

    #region Private Variables

    public GPSPlugin gpsPlugin;
    private long updateInterval = 200; // millisecs
    private long minimumMeterChangeForUpdate = 0;
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    private void Start()
    {
        gpsPlugin = GPSPlugin.GetInstance();
        gpsPlugin.Init(updateInterval, minimumMeterChangeForUpdate);
        gpsPlugin.onLocationChange += OnLocationChange;
        gpsPlugin.onEnableGPS += OnEnableGPS;
        gpsPlugin.onGetLocationComplete += OnGetLocationComplete;
        gpsPlugin.onGetLocationFail += OnGetLocationFail;
    }
    #endregion

    #region Private Functions
    private void OnLocationChange(double latitude, double longitude)
    {
        Debug.Log("[GPSDemo] OnLocationChange latitude: " + latitude + " longitude: " + longitude);
        SA_StatusBar.text += "Location Changed";
    }

    private void OnEnableGPS(string status)
    {
        //do something here
        SA_StatusBar.text += "Enabling GPS";
    }

    private void OnGetLocationComplete(double latitude, double longitude)
    {
        SA_StatusBar.text += "Getting location info";
        SA_StatusBar.text += "[GPSDemo] OnGetLocationComplete latitude: " + latitude + " longitude: " + longitude;
        
    }

    private void OnGetLocationFail()
    {
        Debug.Log("[GPSDemo] OnGetLocationFail");
    }
    #endregion

    #region Main Functions
    public void IsGPSAvailabble()
    {
        gpsPlugin.CheckGPS();
    }

    public void AskUserForPermission()
    {
        gpsPlugin.ShowGPSAlert("Allow GPS", "This opens other features man!", "YES", "No");
    }

    public void GetLocation()
    {
        SA_StatusBar.text += "Trying to get location" + " -->::<-- " + gpsPlugin.GetLatitude().ToString() ;
        gpsPlugin.GetLocation();
    }


    #endregion

    #region Utility Functions
    #endregion
}
