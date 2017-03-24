using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-- John Esslemont

public class RcadeServices : AMN_Singleton<RcadeServices>{

    public rCade_Login rcadeLogin;
    public WebService_RetrievePlayerData rcadePlayerData;
    public WebService_Select_Team rcadeSelectTeam;
    public WebService_Send_Tokens rcadeTokens;
    public rCade_Achievements rcadeAchievements;
    public rCade_Notifacations rcadeNotifacations;
    public rCade_Connection rcadeConnection;
    public rCade_GPS rcadeGPS;
    public rCade_Gallary rCadeGallery;
    public rCade_SpeechRecogniser rcadeSpeech;
    public rCade_TextToSpeech rcadeSpeechToText;

}
