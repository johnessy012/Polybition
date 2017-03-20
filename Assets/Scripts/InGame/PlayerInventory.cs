using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//-- John Esslemont

public class PlayerInventory : MonoBehaviour {

    #region Public Variables
    [System.Serializable]
    public class Item
    {
        public string name;
        public string dbID;
        public int amount;
        public Image thumbnail;
        public Image icon;
    }

    public List<Item> items;
    #endregion

    #region Private Variables
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GetItemByID("Facebook");
    }

    public void AddItem(string ID, int amount)
    {
        GetItemByID(ID).amount += amount;
    }

    public void UseItem(string ID, int amount)
    {
        GetItemByID(ID).amount -= amount;
    }
    #endregion

    #region Utility Functions

    public Item GetItemByID(string ID)
    {
        foreach (var item in items)
        {
            if (item.dbID == ID)
            {
                Debug.Log("Found the Item");
                return item;
            }
        }
        return null;
    }
    public void GetDataFromServer()
    {
        // Grab all data from the player 
        // Pull the json 
        // Create new items based on tables pulled
    }
    #endregion
}
