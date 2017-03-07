using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//-- John Esslemont

public class CreateCatalogueITemList {

    [MenuItem("Assets/Create/Inventory Item List")]
    public static CatalogueList Create()
    {
        CatalogueList asset = ScriptableObject.CreateInstance<CatalogueList>();

        AssetDatabase.CreateAsset(asset, "Assets/CatalogueItem.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
