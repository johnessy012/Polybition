using UnityEngine;
using System.Collections;

[System.Serializable]                                                      
public class CatalogueItem
{
    public string databaseID;
    public string itemName = "New Item";
    public enum PrizeMachine { Bronze, Silver, Gold, Platinum };
    public PrizeMachine myMachine;                     
    public string itemThumbName;
    public string itemIconName;
    public string shortDescripion;
    public string fullDescription;                                 
    public int priceInBronze;
    public int priceInSilver;
    public int priceInGold;
    public int priceInPlatinum;
}

