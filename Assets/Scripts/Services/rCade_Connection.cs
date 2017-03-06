using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//-- John Esslemont

    /// <summary>
    // This class will determine the connection of the player to the internet along with other functionality. 
    ///
    // To check if we have a connection you can use rCade_Connection.HasConnection which returns a bool
    /// 
    // To Disable the connection call DisableConnection; 
    /// 
    // To enable call Enable
    /// 
    // To check which connection type we have you would check currentConnection which is an enum 
    /// </summary>
public class rCade_Connection : MonoBehaviour {

    #region Public Variables
    public Action OnConnectedToInternet;
    public Action OnFailedToConnectToInternet;

    public bool CheckConnectionAtStart = true;
    public bool ContinuousCheck = false;
    public int ContinuousInterval = 5;
    public static bool HasConnection { get; private set; }

    //-------------- WIFI INFO -----------------\\

    public static string IPAddress { get; private set; }
    public string SubnetMask { get; private set; }
    public string MacAddress { get; private set; }
    public string SSID { get; private set; }
    public string BSSID { get; private set; }
    public int LinkSpeed { get; private set; }
    public int NetworkID { get; private set; }

        // Current Connection Type
    public enum CurrentConnection
    {
        WifiConnection,
        DataConnection, 
        NoConnection
    };

    public CurrentConnection currentConnection { get; private set; }
    #endregion

    #region Private Variables
    // This is purley for testing purposes 
    private bool canSearchForConnection = true;
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions

    private void Start()
    {
        OnFailedToConnectToInternet += FailedToConnectToInternet;
        OnConnectedToInternet += ConnectedToInternet;
        // Testing -- This is called all the time 
        if (!ContinuousCheck)
        {
            if (CheckConnectionAtStart)
                CheckConnection();
        }
        else
            StartCoroutine(CR_ContinuousCheck());
    }
    private void OnDestroy()
    {
        OnFailedToConnectToInternet -= FailedToConnectToInternet;
        OnConnectedToInternet -= ConnectedToInternet;
    }

    #endregion

    #region Main 
    // Check he current connetion type
    public CurrentConnection CheckConnection()
    {
        if (!canSearchForConnection)
        {
            OnFailedToConnectToInternet();
            HasConnection = false;
            return CurrentConnection.NoConnection;
        }
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            OnConnectedToInternet();
            HasConnection = true;
            return CurrentConnection.WifiConnection;
        }

        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            OnConnectedToInternet();
            HasConnection = true;
            return CurrentConnection.DataConnection;
        }

        else if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OnFailedToConnectToInternet();
            HasConnection = false;
            return CurrentConnection.NoConnection;
        }
        else
        {
            OnFailedToConnectToInternet();
            HasConnection = false;
            return CurrentConnection.NoConnection;
        }
    }
    // Checks the internet connection every X seconds; - There may b issues with this not using deltatime with old devices etc. 
    private IEnumerator CR_ContinuousCheck()
    {
        CheckConnection();
        yield return new WaitForSeconds(ContinuousInterval);
        StartCoroutine(CR_ContinuousCheck());
    }
    // Callback for when we are connected
    private void ConnectedToInternet()
    {
        SA_StatusBar.text = "We are connected via " + currentConnection;
    }
    // Callback for when we do not have a connection 
    private void FailedToConnectToInternet()
    {
        SA_StatusBar.text = "We do not have a connection to the internet - Load guest account";
    }

    // Call this so that the network info is filled in
    public void GetNetworkInfo()
    {
        AndroidNativeUtility.ActionNetworkInfoLoaded += LoadNetworkInfo;
        AndroidNativeUtility.Instance.LoadNetworkInfo();
    }

    private void LoadNetworkInfo(AN_NetworkInfo networkInfo)
    {
        IPAddress = networkInfo.IpAddress;
        SubnetMask = networkInfo.SubnetMask;
        MacAddress = networkInfo.MacAddress;
        SSID = networkInfo.SSID;
        BSSID = networkInfo.BSSID;
        LinkSpeed = networkInfo.LinkSpeed;
        NetworkID = networkInfo.NetworkId;

        SA_StatusBar.text = "Network info :: " +
            "IPAddress is : " + IPAddress +
            "Subnet Mask is : " + SubnetMask +
            "Mac Address Is : " + MacAddress +
            "SSID is : " + SSID +
            "BSSID is : " + BSSID +
            "Link Speed Is " + LinkSpeed +
            "NetworkID is : " + NetworkID; 
    }


    // For Testing Purposes Only
    public void DisableConnection()
    {
        canSearchForConnection = false;
        StopCoroutine(CR_ContinuousCheck());
    }
    // For Testing Purposes Only
    public void EnableConnection()
    {
        canSearchForConnection = true;
        StartCoroutine(CR_ContinuousCheck());
    }

    #endregion

    #region Utility Functions
    #endregion
}
