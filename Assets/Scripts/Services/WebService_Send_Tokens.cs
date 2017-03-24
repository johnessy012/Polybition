using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
//-- John Esslemont
// -------- Still getting worke on (Was busy adding things like team tokens etc, so right now the structure is not here, its best not working with this
// just now as the strcuture will change) - This may or may not work --------
public class WebService_Send_Tokens : MonoBehaviour {
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

    public void SendTokens()
    {
        StartCoroutine(WS_SendTokens(bronzeTokens, silverTokens, goldTokens, platinumTokens));
    }

    string tokenURL;
    private IEnumerator WS_SendTokens(int bronze, int sliver, int gold, int platinum)
    {
        tokenURL = "http://www.appatier.xyz/php/Connectivity/PlayerTokens.php?bronzeTokens="+bronzeTokens.ToString()+"&silverTokens="+silverTokens.ToString()+"&goldTokens="+goldTokens.ToString()+"&platinumTokens="+platinumTokens.ToString()+"&googleID="+'"'+GooglePlayManager.Instance.player.playerId+'"' + "&team"+WebService_Select_Team.userTeam;
        Debug.Log(tokenURL);
        WWWForm form = new WWWForm();
        form.AddField("googleID", GooglePlayManager.Instance.player.playerId);
        form.AddField("bronzeTokens", bronzeTokens);
        form.AddField("silverTokens", silverTokens);
        form.AddField("goldTokens", goldTokens);
        form.AddField("platinumTokens", platinumTokens);
        form.AddField("teamID", WebService_Select_Team.userTeam);
        WWW tokens = new WWW(tokenURL);

        yield return tokens;

        if (!String.IsNullOrEmpty(tokens.error))
        {
            Debug.LogError("Error On Page : " + tokens.error);
        }
        else if (tokens.text.Length > 0)
        {
            SA_StatusBar.text += "Result from server is : " + tokens.text + "Google ID is " + GooglePlayManager.Instance.player.playerId;
            Debug.Log("Result from server is : " + tokens.text);
        }
        else
        {
            SA_StatusBar.text += "We got no responce from the server";
            Debug.LogError("We got no responce from the server");
        }
    }
}
