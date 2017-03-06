using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Screen_Example : GUIWindow {



    #region Public Variables
    #endregion

    #region Private Variables
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    private void Start()
    {
        // This is for testing only
        OpenMe();
    }
    #endregion

    #region Main Functions

    // Opens this window
    public void OpenMe()
    {
        OpenWindow(Vector3.up * 1200, 0.5f, 0.1f, Ease.OutBack);
    }

    // Closes this window with any particular needs
    public void CloseMe()
    {
        CloseWindow(Vector3.down * 600, 0);
    }

    #endregion

    #region Utility Functions
    #endregion

}
