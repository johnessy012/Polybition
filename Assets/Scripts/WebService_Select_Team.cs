using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//-- John Esslemont

public class WebService_Select_Team : MonoBehaviour {
    
    public int userTeam;
    public int userRCadeID;
//    public void SetPlayerData(int rcadeID, string googleiD, string first, string last, string email, string pass, int team, string locale, string nation)
//    {
//        userRCadeID = rcadeID;
//        userGoogleID = googleiD;
//        userFirstName = first;
//        userLastName = last;
//        userEmail = email;
//        userPassword = pass;
//        userTeam = team;
//        userNation = nation;
//        userLocale = locale;
//    }

    public void SetTeam(int team)
    {
        userTeam = team;
    }

    public void SelectTeam()
    {
        StartCoroutine(WS_SelectTeam(userTeam));
    }

    string teamURL;
    private IEnumerator WS_SelectTeam(int teamID)
    {
        if (!PlayerPrefs.HasKey("PlayerID"))
        {
            Debug.LogError("We do not have a players ID to contact the DB - Either log in then call this method or something else has gone wrong");
            yield break;
        }
        userTeam = teamID;
        userRCadeID = PlayerPrefs.GetInt("PlayerID");
        teamURL = "appatier.xyz/php/Connectivity/SelectTeam.php?teamID=" + userTeam.ToString() + "&ID=" + userRCadeID.ToString();
        WWWForm form = new WWWForm();
        form.AddField("ID", userRCadeID);
        form.AddField("teamID", userTeam);
        WWW login = new WWW(teamURL);

        yield return login;

        if (!String.IsNullOrEmpty(login.error))
        {
            Debug.LogError("Error On Page : " + login.error);
        }
        else if (login.text.Length > 0)
        {
            Debug.Log("Result from server is : " + login.text);
        }
        else
        {
            Debug.LogError("We got no responce from the server");
        }
    }
}
