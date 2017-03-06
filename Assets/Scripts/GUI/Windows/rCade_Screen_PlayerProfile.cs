using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//-- John Esslemont

public class rCade_Screen_PlayerProfile : GUIWindow {

    #region Public Variables
    #endregion

    #region Private Variables
    [SerializeField]
    private Sprite guestAvatar;

    #endregion

    #region Local Variables

    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Image playerIcon;

    private bool guestAccountLoggedIn;

    /// References
    private rCade_Login_Rcade login;

    #endregion

    #region Built In Functions
    private void Start()
    {
        // Set reference to login 
        login = FindObjectOfType<rCade_Login_Rcade>();
        // Subscribe to the logining in event for rcade servers
    }
    #endregion

    #region Main Functions

    #region JUST FOR UI CAN BE DELETED - Replace with simple bool to active/deactivate windows
    public void OpenMe()
    {
        OpenWindow(Vector3.left * 1200, 1, 0.25f, Ease.OutBack);
    }

    public void CloseMe()
    {
        CloseWindow(Vector3.left * 1200, 0);
        guiReference.exampleScreen.OpenMe();
    }
    #endregion
    public void UpdateScreen()
    {
        playerName.text = GooglePlayManager.Instance.player.name + "(" + GooglePlayManager.Instance.currentAccount + ")";
        playerIcon.sprite = GooglePlayManager.Instance.player.icon.ToSprite();
    }

    public void GuestAccount()
    {
        playerName.text = "Rcader " + Random.Range(1, 500000);
        playerIcon.sprite = guestAvatar;
    }

    public void UpdateTokens(int bronze, int silver, int gold, int platinum)
    {
        
    }

    #endregion

    #region Utility Functions
    #endregion
}
