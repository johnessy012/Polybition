using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//-- John Esslemont

public class Rcade_Notifacations : MonoBehaviour {

    #region Public Variables
    #endregion

    #region Private Variables
    private NotifacationData currentNotifacation;
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions
    // Local Notifacations

    // This will use the info from the inspector - Not very helpfull in my eyes but putting it here anyways just incase; 
    public void BuildSimpleNotifacation()
    {
        AndroidNotificationBuilder builder = new AndroidNotificationBuilder(
        SA.Common.Util.IdFactory.NextId, "Local Notification Title", "This is local notification", 10);

        AndroidNotificationManager.Instance.ScheduleLocalNotification(builder);
    }

    public void ShowLocalNotifacation(string id)
    {
        GetNotifacationByName(id);
        AndroidNotificationBuilder builder = new AndroidNotificationBuilder(currentNotifacation.notifacationID, currentNotifacation.title, currentNotifacation.body, currentNotifacation.timer);
        builder.SetIconName(currentNotifacation.iconName);
        builder.SetVibration(currentNotifacation.vibrate);
        builder.SetSoundName(currentNotifacation.notifaacationSoundName);
        AndroidNotificationManager.Instance.ScheduleLocalNotification(builder);
    }

    // Tasks
    /// <summary>
    /// Setup events - 
    ///     User came back with a click on the notifacation. 
    ///     User Failed To Respond to this - Analytics
    /// </summary>
    #endregion

    #region Utility Functions
    [System.Serializable]
    /// For these to work with custom notifacations you need to make sure that you put the sound files in Plugins/Android/AN_Res/res/raw
    /// and Icons should be saved in Plugins/Android/AN_Res/res/drawable
    /// IMPORTANT - SOUND AND SPIRTE SHOUD ALL BE LOWER CASE
    public class NotifacationData
    {
        public string notifacationName;
        public int notifacationID;
        public string title;
        public string body;
        [Tooltip("Make sure this is named in all lower case")]
        public string iconName;
        [Tooltip("Make sure this is named in all lower case")]
        public string notifaacationSoundName;
        [Tooltip("Time before it is loaded in seconds")]
        public int timer;
        public bool vibrate;
    }
    // So you can see them in the inspector
    public List<NotifacationData> notifacations;
    public void GetNotifacationByName(string notName)
    {
        foreach (NotifacationData nc in notifacations)
        {
            if (notName == nc.notifacationName)
            {
                currentNotifacation = nc;
            }
        }
    }

    #endregion
}
