using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//-- John Esslemont

public class WebService_Select_Team : MonoBehaviour {
    
    // Chosen Team 
    public int userTeam;
    // Set the chosen team - Used by GUI
    public void SetTeam(int team)
    {
        userTeam = team;
    }
    /// <summary>
    /// This starts the process of sending the chosen toeam to the server.
    /// </summary>
    public void SelectTeam()
    {
        StartCoroutine(WS_SelectTeam(userTeam));
    }
    
    /// <summary>
    /// Send chosen team to the server with the ID of the players google account
    /// </summary>
    /// <param name="teamID"></param>
    /// <returns></returns>
    private IEnumerator WS_SelectTeam(int teamID)
    {
        if (!PlayerPrefs.HasKey("PlayerID"))
        {
            Debug.LogError("We do not have a players ID to contact the DB - Either log in then call this method or something else has gone wrong");
            yield break;
        }

        userTeam = teamID;
        string url = "http://www.appatier.xyz/php/Connectivity/SelectTeam.php?teamID=" + userTeam.ToString() + "&googleID=" + '"' +GooglePlayManager.Instance.player.playerId+'"';
        WWWForm form = new WWWForm();
        form.AddField("googleID", GooglePlayManager.Instance.player.playerId);
        form.AddField("teamID", userTeam);
        WWW login = new WWW(url);

        yield return login;

        if (!String.IsNullOrEmpty(login.error))
        {
            SA_StatusBar.text += "Error On Page : " + login.error;
            Debug.LogError("Error On Page : " + login.error);
        }
        else if (login.text.Length > 0)
        {
            SA_StatusBar.text += "Result from server is : " + login.text;
        }
        else
        {
            SA_StatusBar.text += "We got no responce from the server";
        }
    }
}
