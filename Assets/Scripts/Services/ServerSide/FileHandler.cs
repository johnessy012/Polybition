using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text;
//-- John Esslemont

public class FileHandler : MonoBehaviour
{

    public IEnumerator UploadCatalogue(string text)
    {

        //converting the xml to bytes to be ready for upload
        byte[] prizes = Encoding.UTF8.GetBytes(text);

        //generate a long random file name , to avoid duplicates and overwriting
        string fileName = "Catalogue";
        fileName = fileName.ToUpper();
        fileName = fileName + ".json";

        //if you save the generated name, you can make people be able to retrieve the uploaded file, without the needs of listings
        //just provide the level code name , and it will retrieve it just like a qrcode or something like that, please read below the method used to validate the upload,
        //that same method is used to retrieve the just uploaded file, and validate it
        //this method is similar to the one used by the popular game bike baron
        //this method saves you from the hassle of making complex server side back ends which enlists available levels
        //this way you could enlist outstanding levels just by posting the levels code on a blog or forum, this way its easier to share, without the need of user accounts or install procedures
        WWWForm form = new WWWForm();

        form.AddField("json", text);
        string url = "http://www.appatier.xyz/php/Catalogue/Upload.php?json=" + WWW.EscapeURL(text);
        Debug.Log("Json Data is :: " + text);
        //change the url to the url of the php file
        ;
        WWW w = new WWW(url, form);
        Debug.Log("www created");

        yield return w;
        Debug.Log("after yield w" + w.text);
        if (w.error != null)
        {
            Debug.Log("Error From Server : " + w.error);
        }
        else
        {
            Debug.Log("Checking if success");
            //this part validates the upload, by waiting 5 seconds then trying to retrieve it from the web
            if (w.uploadProgress == 1 || w.isDone)
            {
                Debug.Log("Progress is done...");
                yield return new WaitForSeconds(3);
                Debug.Log("We waited for the yield");
                //change the url to the url of the folder you want it the levels to be stored, the one you specified in the php file
                WWW w2 = new WWW("http://www.appatier.xyz/php/Catalogue/" + fileName);
                Debug.Log("Sent the request...");
                yield return w2;
                Debug.Log("Finished Waiting");
                if (w2.error != null)
                {
                    DestroyImmediate(GameObject.Find("Uploading"));
                    //Debug.Log("error 2");
                    Debug.Log(w2.error);
                }
                else
                {
                    //then if the retrieval was successful, validate its content to ensure the level file integrity is intact
                    if (w2.text != null || w2.text != "")
                    {
                        Debug.Log(w2.text);
                        if (w2.text.Contains("itemList"))
                        {
                            //and finally announce that everything went well
                            Debug.Log("Catalogue File " + fileName + " Contents are: \n\n" + w2.text);
                            Debug.Log("Finished Uploading Level " + fileName);
                            DestroyImmediate(GameObject.Find("Uploading"));
                        }
                        else
                        {
                            Debug.Log("Level File " + fileName + " is Invalid");
                            DestroyImmediate(GameObject.Find("Uploading"));
                        }
                    }
                    else
                    {
                        Debug.Log("Level File " + fileName + " is Empty");
                        DestroyImmediate(GameObject.Find("Uploading"));
                    }
                }
            }
            else
            {
                Debug.LogError("Uploading Failed...");
                DestroyImmediate(GameObject.Find("Uploading"));
            }
        }
    }

    public IEnumerator DownloadCatalogue(CatalogueList list)
    {
        WWW catalogueDownload = new WWW("http://www.appatier.xyz/php/Catalogue/CATALOGUE.json");
        yield return catalogueDownload;
        string json = catalogueDownload.text;
        if (catalogueDownload.error != null)
        {
            Debug.LogError("ERROR FROM SERVER :: " + catalogueDownload.error);
            DestroyImmediate(GameObject.Find("Downloading"));
        }
        else
        {
            //then if the retrieval was successful, validate its content to ensure the level file integrity is intact
            if (catalogueDownload.text != null || catalogueDownload.text != "")
            {
                if (catalogueDownload.text.Contains("itemList"))
                {
                    Debug.Log("Successfully Downloaded the file -- Attempting to deserialize to json");
                    AddDownloadedItems(catalogueDownload.text, list);
                    DestroyImmediate(GameObject.Find("Downloading"));
                }
            }
        }
    }

    void AddDownloadedItems(string text, CatalogueList list)
    {
        CatalogueList inventoryItemList = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/CatalogueItem.asset", typeof(CatalogueList)) as CatalogueList;
        JsonUtility.FromJsonOverwrite(text, list);
    }
}


