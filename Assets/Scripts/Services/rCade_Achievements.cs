using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- John Esslemont

// To use this script all you need to use is 3 methods

// OpenAchievementWindow will open the window

// Unlock Achievement By Id will take a string which is the id of the achievment held on the google play developer console. 

// Add To Incremental Achievement takes a value and a string with the same above, apart from the value is a points based system. So if the achievement requires the player
// To have played 100 game then each time they play a game you would add by 1, with the id of the acheievement. 

// The event of an acheievement being updated is handled by google with the call back OnAchievementUpdated

public class rCade_Achievements : MonoBehaviour {

    #region Built In Functions
    private void Start()
    {
        // Async for acheivement updating
        GooglePlayManager.ActionAchievementUpdated += OnAchievementUpdated;
    }
    #endregion

    #region Main Functions

    /// <summary>
    /// To be called by a button when the player wants to see thier achievements 
    /// </summary>
    public void OpenAchievementWindow()
    {
        GooglePlayManager.Instance.ShowAchievementsUI();
        Debug.Log("Showing the achievements");
    }

    public void UnlockAchievementByID(string id)
    {
        GooglePlayManager.Instance.UnlockAchievementById(id);
    }

    public void AddToIncrementalAchievement(int value, string id)
    {
        GooglePlayManager.Instance.IncrementAchievementById(id, value);
    }

    /// <summary>
    /// Async method that is called when we get a response from google with acehievements
    /// </summary>
    /// <param name="result"></param>
    private void OnAchievementUpdated(GP_AchievementResult result)
    {
        AN_PoupsProxy.showMessage("Achievment Updated ", "Id: " + result.achievementId + "\n status: " + result.Message);
    }

    #endregion
}
