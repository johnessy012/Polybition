using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//-- John Esslemont

public static class ConvertCatalogueToJson{

    #region Public Variables
    public static string FilePath = Application.dataPath + "/Admin/Catalogue.json";
    public static string text;
    public static string JsonInfo;
    #endregion

    #region Private Variables
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions

    public static void SaveAllItems(CatalogueList list)
    {
        if (!File.Exists(FilePath))
        {
            Debug.LogWarning("There is no file here so we should create one");
            StreamWriter sw = File.CreateText(FilePath);
            sw.Close();
        }

        text = String.Empty;
        text = JsonUtility.ToJson(list, true);
        Debug.Log("Uploaded Json :: " + text);
        //Debug.Log(text);
        // Check to see if the file exists

        StreamWriter nsw = new StreamWriter(FilePath);
        nsw.Write(text);
        nsw.Close();
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Uploading";
        sphere.AddComponent<FileHandler>();
        sphere.GetComponent<FileHandler>().StartCoroutine(sphere.GetComponent<FileHandler>().UploadCatalogue(text));
    }

    public static void DownloadFile(CatalogueList list)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.name = "Downloading";
        sphere.AddComponent<FileHandler>();
        sphere.GetComponent<FileHandler>().StartCoroutine(sphere.GetComponent<FileHandler>().DownloadCatalogue(list));
    }
    #endregion

    #region Utility Functions
    #endregion
}
