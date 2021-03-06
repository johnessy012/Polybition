﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//-- John Esslemont


/// <summary>
// This class will log the player into google, in the process it will create a new account on our servers passing in the google id which is used as a unique key.
// If the player exists it will grab the data, if they do not it will create the player and then pull the data. 
// 
// To use this script all you need to do is call -> Login <-
// I have also created a manager class called RcadeServices which is a singleton. So you can call this from anywhere
/// </summary>
public class rCade_Login : MonoBehaviour
{

    #region Public Variables
    public bool IsGuest { get; private set; }
    // Set by google and used by Rcade servers 
    public static string UserID;
    // User information 
    public string email;
    public string password;
    public string firstName;
    public string lastName;

    #endregion

    #region Private Variables
    // Can get rid of this - Not needed 
    private GUIReferenceManager guiReferences;
    /// <summary>
    /// Reference to the internet connection checker - Woould of made static but decided not too
    /// </summary>
    private rCade_Connection internetConnection;
    [SerializeField]
    ///Should we auto login?
    private bool autoLogin = true;

    /// <summary>
    /// Actions for logging in and out
    /// </summary>
    private Action IsNewUser;
    private Action ReturningUser;
    private Action OnLoggedIntoGoogle;

    #endregion

    #region Built In Functions

    private void Awake()
    {
        /// Can get rid of this when needs be
        if (!guiReferences)
            guiReferences = FindObjectOfType<GUIReferenceManager>();
    }

    private void Start()
    {
        // Reference required to check connection status 
        // Actions for async operations from google play 
        GooglePlayConnection.ActionPlayerConnected += OnLoggedIn;
        GooglePlayConnection.ActionPlayerDisconnected += OnPlayerLoggedOut;
        GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;
        // Action subscriptions for Rcade servers 
        IsNewUser += CreateRcader;
        ReturningUser += LoginToRcade;
        //StartCoroutine(ReturnIsNewUser());
        if (autoLogin)
            AutoLogin();
    }

    #endregion

    #region Main Functions
    /// <summary>
    /// Login with google play - This also logs into rcade or creates a new account on Rcade
    /// </summary>
    public void Login()
    {
        // Check if we arre connected to the net
        RcadeServices.Instance.rcadeConnection.CheckConnection();
        // If we are then carry one
        if (rCade_Connection.HasConnection)
        {
            // If we are already connected then update the profile screen and return
            if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
            {
                SA_StatusBar.text = "We are already connected";
                // We are already connected so no need to connect again 
                guiReferences.playerProfile.UpdateScreen();
                StartCoroutine(ReturnIsNewUser());
                return;

            }
            // if we are not connected then connect - Connect has a call back that will update the profile screen on success
            else if (GooglePlayConnection.State == GPConnectionState.STATE_DISCONNECTED)
            {
                SA_StatusBar.text += "We are trying to connect now ";
                // We are already connected so no need to connect again 
                GooglePlayConnection.Instance.Connect();
                StartCoroutine(ReturnIsNewUser());
                return;
            }
        }
        else
        {
            // Connect to google play
            SA_StatusBar.text += "We Cannot connect as there is no network connection";
            LoadGuestAccount();
        }
    }

    /// <summary>
    /// Load a guest account until they login. 
    /// </summary>
    private void LoadGuestAccount()
    {
        IsGuest = true;
        Debug.Log("We Are in guest mode");
        guiReferences.playerProfile.GuestAccount();
        SA_StatusBar.text = "Guestt Mode";
    }

    /// <summary>
    /// Used by GoogleLogin 
    /// </summary>
    private void LoginToRcade()
    {
        StartCoroutine(RcadeLogin());
    }
    /// <summary>
    /// Logs into the rcade servers from GoogleLogin
    /// </summary>
    /// <returns>The login.</returns>
    public IEnumerator RcadeLogin()
    {
        string googleID = GooglePlayManager.Instance.player.playerId;
        WWWForm form = new WWWForm();
        form.AddField("googleID", googleID);

        WWW login = new WWW("http://www.appatier.xyz/php/Connectivity/Login.php?googleID=" +'"'+googleID+'"',form);

        yield return login;

        if (!String.IsNullOrEmpty(login.error))
        {
            Debug.LogError("Error On Page : " + login.error);
            SA_StatusBar.text += "Error on page : " + login.error;
        }
        else if (login.text.Length > 0)
        {
            Debug.Log("Result from server is : " + login.text);
            SA_StatusBar.text += "Result from loging in is " + login.text;
            SavePlayerSession(login);
            RcadeServices.Instance.rcadePlayerData.RetrievePlayerData();
        }
        else
        {
            Debug.LogError("We got no responce from the server");
            SA_StatusBar.text += "No responce from rcade server";
        }
    }

    /// <summary>
    /// Logout from google play
    /// </summary>
    public void Logout()
    {
        GooglePlayConnection.Instance.Disconnect();
        SA_StatusBar.text += "We logged out";
        LoadGuestAccount();
        ClosePlayerSession();
        // Show that we logged out
    }

    private void CreateRcader()
    {
        SA_StatusBar.text += "Creating an rcader";
        StartCoroutine(CreateNewUserAccount());
    }
    /// <summary>
    /// Creates a new user account n Rcade Servers 
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreateNewUserAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstName", GooglePlayManager.Instance.player.name);
        form.AddField("googleID", GooglePlayManager.Instance.player.playerId);

        WWW download = new WWW("http://www.appatier.xyz/php/Connectivity/CreateUserAccount.php?firstName="+GooglePlayManager.Instance.player.name +"&googleID="+GooglePlayManager.Instance.player.playerId, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.LogError("We got an error from the server " + download.error);
            SA_StatusBar.text += "Error on creating new player :: " + download.error;
        }
        else
        {
            Debug.Log("New Player Registered Successfully" + download.text);
            SA_StatusBar.text += "New player created on DB" + download.text;
            RcadeServices.Instance.rcadePlayerData.RetrievePlayerData();
        }
    }
    /// <summary>
    /// Checks if we are a new user or not 
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReturnIsNewUser()
    {
        SA_StatusBar.text = "Checking if is new user";
        WWWForm form = new WWWForm();
        string googleID = GooglePlayManager.Instance.player.playerId;
        form.AddField("googleID", googleID);
        string url = "http://www.appatier.xyz/php/Connectivity/UserExists.php?googleID="+'"'+googleID+'"';
        WWW download = new WWW(url, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.LogError("We got an error from the server " + download.error);
            SA_StatusBar.text += "Error From Returning new user is : " + download.error;
        }
        // BUG - For some reason it is returning text from the server 

        else if (download.text.Length > 0)
        {
            SA_StatusBar.text = "::: " + download.text + " :::";
            if (ReturningUser != null)
                ReturningUser();
            SA_StatusBar.text += "We are a returning user";
        }
        else
        {
            if (IsNewUser != null)
                IsNewUser();
            SA_StatusBar.text += "We are a new user";
        }
    }
    #endregion

    #region Utility Functions
    /// <summary>
    /// Auto logs in to google and in turn rcade
    /// </summary>
    private void AutoLogin()
    {
        if (rCade_Connection.HasConnection)
        {
            GooglePlayConnection.Instance.Connect();
            if (!string.IsNullOrEmpty(UserID))
            {
                StartCoroutine(RcadeLogin());
            }
            Debug.Log("We connected via Auto Login");
        }
        else
        {
            guiReferences.playerProfile.GuestAccount();
            // Load guest user 
        }
     
    }
    /// <summary>
    /// Get the connection results from google 
    /// </summary>
    /// <param name="result">Result.</param>
    private void ActionConnectionResultReceived(GooglePlayConnectionResult result)
    {
        if (result.IsSuccess)
        {
            SA_StatusBar.text = "We connected";
            UserID = GooglePlayManager.Instance.player.playerId;
        }
        else
        {
            SA_StatusBar.text = "Connection failed with code: " + result.code.ToString();
        }
    }
    /// <summary>
    /// We logged in
    /// </summary>
    private void OnLoggedIn()
    {
        // Set the user ID -
        // BUG - Sometimes when the player is logged in this is not set, so i believe this is only called when the player actually logs in
        // So for now if you need the ID use --> GooglePlayManager.Instance.Player.playerid;
        UserID = GooglePlayManager.Instance.player.playerId;
        // Update the players profile page
        guiReferences.playerProfile.UpdateScreen();
        SA_StatusBar.text += "We have connected to google.....";
        StartCoroutine(ReturnIsNewUser());
    }
    /// <summary>
    /// When the player logs out 
    /// </summary>
    private void OnPlayerLoggedOut()
    {
        
    }
    /// <summary>
    /// Save the player session with the ID so we can get the
    /// </summary>
    /// <param name="playerData">Player data.</param>
    private void SavePlayerSession(WWW playerData)
    {
        UserID = playerData.text;
        Debug.Log("Json is " + playerData.text);
        PlayerPrefs.SetString("PlayerID", UserID);
    }
    public void ClosePlayerSession()
    {
        PlayerPrefs.DeleteKey("PlayerID");
    }
    #endregion

    #region UserInput
    public void SetEmail(InputField value)
    {
        email = value.text;
    }
    // Using input fields for the text
    public void SetPassWord(InputField value)
    {
        password = value.text;
    }
    public void SetFirstName(InputField value)
    {
        firstName = value.text;
    }
    public void SetLastName(InputField value)
    {
        lastName = value.text;
    }
    #endregion
}


/// NOTES:: 
/// When player logs into google check the DB for the user ID
/// if there is no id then we should create the user
/// 
