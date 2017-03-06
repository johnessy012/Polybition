using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//-- John Esslemont

public class rCade_CreatUser : MonoBehaviour {

    #region Public Variables
    public int playerID;
    public string firstName;
    public string lastName;
    public string email;
    public string password;
    public string locale;
    public string nation;

    public Action OnAccountCreated;
    public Action OnLoggedIn;
    public Action OnLoggedOut;
    public Action OnError;

    #endregion

    #region Private Variables
    private string url;
    [SerializeField]
    [Tooltip("Make sure there is a button attached to this, we will auto matically make it clickable when all data is valid")]
    private Button CreateAccountButton;
    [SerializeField]
    private Text accountMessage;
    #endregion

    #region Local Variables
    #endregion

    #region Built In Functions
    #endregion

    #region Main Functions
    public void CreateUser()
    {
        SetUrl();
        StartCoroutine(CreateNewUserAccount());
    }
    #endregion

    #region Utility Functions
    private IEnumerator CreateNewUserAccount()
    {
        WWWForm form = new WWWForm();
        form.AddField("firstName", firstName);
        form.AddField("lastName", lastName);
        form.AddField("email", email);
        form.AddField("password", password);

        WWW download = new WWW(url, form);

        yield return download;

        if (!string.IsNullOrEmpty(download.error))
        {
            Debug.LogError("We got an error from the server " + download.error);
            accountMessage.text = download.error;
            if (OnError != null)
            {
                OnError();
            }
        }
        else
        {
            Debug.Log("New Player Registered Successfully" + download.text + " :: " + download.size);
            accountMessage.text = download.text;
            if (OnAccountCreated != null)
            {
                OnAccountCreated();
            }

            if (OnLoggedIn != null)
            {
                OnLoggedIn();
            }
        }
    }

    private bool IsUserDataValid()
    {
        bool isValid = firstName.Length > 0 && lastName.Length > 0 && email.Length > 0 && email.Contains("@") && password.Length > 0;
        if (isValid)
        {
            SetUrl();
            Debug.Log(url);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetFirstName(InputField value)
    {
        firstName = value.text;
    }
    public void SetLastName(InputField value)
    {
        lastName = value.text;
    }
    public void SetEmail(InputField value)
    {
        email = value.text;
    }
    public void SetPassWord(InputField value)
    {
        password = value.text;
    }
    public void SetUrl()
    {
        url = "http://appatier.xyz/php/Connectivity/CreateUserAccount.php?firstName=" + firstName + "&lastName=" + lastName + "&email=" + email + "&password=" + password;
    }
    #endregion
}
