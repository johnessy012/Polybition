using UnityEngine;
using System.Collections;
[System.Serializable]                                                           //  Our Representation of an InventoryItem
public class CatalogueItem
{
    public string databaseID;
    public string itemName = "New Item";                                     
    public Texture2D itemThumb = null;
    public Texture2D itemIcon = null;
    public string shortDescripion;
    public string fullDescription;                                 
    public int priceInBronze;
    public int priceInSilver;
    public int priceInGold;
    public int priceInPlatinum;
}

