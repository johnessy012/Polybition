using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- John Esslemont

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
