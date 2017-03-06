using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- John Esslemont

// Main class for all our google service needs
public class rCade_Services : MonoBehaviour {

    #region Public Variables
    #endregion

    #region Private Variables

    private rCade_Login loginServices;

    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    private void Start()
    {
        if (!loginServices)
            loginServices = FindObjectOfType<rCade_Login>();
    }

    #endregion

    #region Main Functions


    public void LogOut()
    {
        loginServices.Logout();
    }

    // Login 
    // Logout 
    // Auto Login 
    // Login Call Back
    // Logout Callback

    // Achievements
    // Show Achievements
    // Hide Achievements
    // Complete an achievement - By Name
    // Complete an Achievement - By ID

    

    #endregion

    #region Utility Functions
    #endregion
}
