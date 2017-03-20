using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//-- John Esslemont

public class WebService_RetrievePlayerData : MonoBehaviour {

    #region Public Variables

    public int userRCadeID;
    public string userGoogleID;
    public string userFirstName;
    public string userLastName;
    public string userEmail;
    public string userPassword;
    public int userTeam;
    public string userNation;
    public string userLocale;
    public int userLogins;
    public int bronzeTokens, silverTokens, goldTokens, platinumTokens;

    #endregion

    #region Private Variables
    [SerializeField]
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions
    public void RetrievePlayerData()
    {
        StartCoroutine(WS_GetPlayerData());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            RetrievePlayerData();
        }
    }
    #endregion

    #region Utility Functions

    private IEnumerator WS_GetPlayerData()
    {
        if (!PlayerPrefs.HasKey("PlayerID"))
        {
            Debug.LogError("We do not have a players ID to contact the DB - Either log in then call this method or something else has gone wrong");
            yield break;
        }
        userRCadeID = PlayerPrefs.GetInt("PlayerID");
        string playerURL = "appatier.xyz/php/Connectivity/RetrievePlayerData.php?ID="+userRCadeID.ToString();
        WWWForm form = new WWWForm();
        WWW login = new WWW(playerURL);

        yield return login;

        if (!String.IsNullOrEmpty(login.error))
        {
            Debug.LogError("Error On Page : " + login.error);
        }
        else if (login.text.Length > 0)
        {
            Debug.Log("Result from server is : " + login.text);
            DesirializePlayerData(login.text);
        }
        else
        {
            Debug.LogError("We got no responce from the server");
        }
    }

    private void DesirializePlayerData(string json)
    {
        Debug.Log("JSON DATA : " + json);
        string newJson = TrimEnds(json);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(newJson);
        userRCadeID = playerData.ID;
        userFirstName = playerData.firstName;
        userLastName = playerData.lastName;
        userEmail = playerData.email;
        userLocale = playerData.locale;
        userTeam = playerData.teamID;
        userNation = playerData.nation;
        userLogins = playerData.logins;
        bronzeTokens = playerData.bronzeTokens;
        silverTokens = playerData.silverTokens;
        goldTokens = playerData.goldTokens;
        platinumTokens = playerData.platinumTokens;
    }
    // Trim the [ & ] from the returned object from the server
    private string TrimEnds(string text)
    {
        char[] ends = {'[', ']'};
        string newText = text.TrimEnd(ends);
        string useText = newText.TrimStart(ends);
        return useText;
    }

    #endregion
}
[Serializable]    
public class PlayerData
{
    public int ID;
    public string firstName;
    public string lastName;
    public string email;
    public string password;
    public int bronzeTokens;
    public int silverTokens;
    public int goldTokens;
    public int platinumTokens;
    public string locale;
    public string nation;
    public bool maleOrFemale;
    public int teamID;
    public int logins;
}
