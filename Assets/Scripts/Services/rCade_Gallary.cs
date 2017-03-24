using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//-- John Esslemont

/// <summary>
/// This class allows you to open the gallery, select a pic with a public variable that is filled with the chosen image.
/// When you call TakeScreenShot it will automatically take a screen shot and save to the gallery. 
/// 
/// There are also events that you can subscribe too, OnImagePicked, OnImageSaved if you need to find out when this happens. 
/// 
/// To Take a screen shot call TakeScreenShot()
/// 
/// To Open the gallery on device call OpenGallery()
/// 
/// To Save the screen shot to the gallery call SaveScreenShotToGallary() Although im not sure this is 100% needed as im sure it saves anyway 
/// </summary>
public class rCade_Gallary : MonoBehaviour {

    #region Public Variables
    public Image pickedImage;
    #endregion

    #region Private Variables
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions

    public void TakeScreenShot()
    {
        AndroidCamera.Instance.SaveScreenshotToGallery("Screenshot" + AndroidCamera.GetRandomString());
    }

    public void SaveScreenShotToGallary(Texture2D tex)
    {
        AndroidCamera.Instance.OnImageSaved += OnImageSaved;
        AndroidCamera.Instance.SaveImageToGallery(tex, "Screenshot" + AndroidCamera.GetRandomString());
    }

    public void OpenGallery()
    {
        AndroidCamera.Instance.GetImageFromGallery();
        AndroidCamera.Instance.OnImagePicked += OnImagePicked;
    }
    #endregion

    #region Utility Functions
    private void OnImageSaved (GallerySaveResult result) {
        AndroidCamera.Instance.OnImageSaved -= OnImageSaved;

        if(result.IsSucceeded) {
            AN_PoupsProxy.showMessage("Saved", "Image saved to gallery \n" + "Path: " + result.imagePath);
            SA_StatusBar.text =  "Image saved to gallery";
        } else {
            AN_PoupsProxy.showMessage("Failed", "Image save to gallery failed");
            SA_StatusBar.text =  "Image save to gallery failed";
        }
    }
    private void OnImagePicked(AndroidImagePickResult result) {
        AndroidCamera.Instance.OnImagePicked -= OnImagePicked;

        if (result.IsSucceeded) {
            AN_PoupsProxy.showMessage ("Image Pick Rsult", "Succeeded, path: " + result.ImagePath);
            pickedImage.GetComponent<Image> ().sprite = result.Image.ToSprite();
        } else {
            AN_PoupsProxy.showMessage ("Image Pick Rsult", "Failed");
        }   
    }
    #endregion
}
