using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//-- John Esslemont

public class WebService_Send_Tokens : MonoBehaviour {
    private int playerID;
    public int bronzeTokens, silverTokens, goldTokens, platinumTokens;

    #region Used Only For Deomstration UI
    public void SetBronzeTokens(InputField bronze)
    {
        bronzeTokens = int.Parse(bronze.text);
    }

    public void SetSilverTokens(InputField silver)
    {
        silverTokens = int.Parse(silver.text);
    }

    public void SetGoldTokens(InputField gold)
    {
        goldTokens = int.Parse(gold.text);
    }

    public void SetPlatinumTokens(InputField platinum)
    {
        platinumTokens = int.Parse(platinum.text);
    }
    #endregion
    public void SetAllTokens(int bronze, int silver, int gold, int platinum)
    {
        bronzeTokens = bronze;
        silverTokens = silver;
        goldTokens = gold;
        platinumTokens = platinum;
    }

    public void SendTokens()
    {
        StartCoroutine(WS_SendTokens(bronzeTokens, silverTokens, goldTokens, platinumTokens));
    }

    string tokenURL;
    private IEnumerator WS_SendTokens(int bronze, int sliver, int gold, int platinum)
    {
        if (!PlayerPrefs.HasKey("PlayerID"))
        {
            Debug.LogError("We do not have a players ID to contact the DB - Either log in then call this method or something else has gone wrong");
            yield break;
        }
        playerID = PlayerPrefs.GetInt("PlayerID");
        tokenURL = "appatier.xyz/php/Connectivity/PlayerTokens.php?bronzeTokens="+bronzeTokens.ToString()+"&silverTokens="+silverTokens.ToString()+"&goldTokens="+goldTokens.ToString()+"&platinumTokens="+platinumTokens.ToString()+"&ID="+playerID;
        Debug.Log(tokenURL);
        WWWForm form = new WWWForm();
        form.AddField("ID", playerID);
        form.AddField("bronzeTokens", bronzeTokens);
        form.AddField("silverTokens", silverTokens);
        form.AddField("goldTokens", goldTokens);
        form.AddField("platinumTokens", platinumTokens);
        WWW login = new WWW(tokenURL);

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
