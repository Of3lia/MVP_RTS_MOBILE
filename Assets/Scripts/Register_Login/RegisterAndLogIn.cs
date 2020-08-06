using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RegisterAndLogIn : MonoBehaviour
{
    #region Declarations

    [SerializeField]
    private InputField registerName, registerPassword, registerRepeatPassword, registerMail, loginName, loginPassword;

    public const string MatchEmailPattern =
        @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
        + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
        + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
        + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    [SerializeField]
    private Text registerConsole, loginConsole;

    [SerializeField]
    private GameObject registerPanel, loginPanel, registerButton;

    #endregion

    #region Common Behaviour
    private void Start()
    {
        registerButton.GetComponent<Button>().Select();
    }
    public void GoToRegister()
    {
        registerPanel.SetActive(true);
        loginPanel.SetActive(false);
    }

    public void GoToLogin()
    {
        registerPanel.SetActive(false);
        loginPanel.SetActive(true);
    }

    private bool ValidateName(string name)
    {
        if (name.Length > 3 && name.Length < 20) { return true; } else { return false; }
    }
    private bool ValidatePassword(string pwd)
    {
        if (pwd.Length > 6 && pwd.Length < 20) { return true; } else { return false; }
    }

    #endregion

    #region Register

    private bool ValidateRepeatPassword(string pwd1, string pwd2)
    {
        if(pwd1 == pwd2) { return true; } else { return false; }
    }
    private bool ValidateEmail(string email)
    {
        if (email != null)
            if(email.Length < 100)  
                return Regex.IsMatch(email, MatchEmailPattern);
        return false;
    }

    public void Register()
    {
        string name = registerName.text;
        string password = registerPassword.text;
        string repeatPassword = registerRepeatPassword.text;
        string mail = registerMail.text;
        if (ValidateName(name))
        {
            if (ValidatePassword(password))
            {
                if (ValidateRepeatPassword(password, repeatPassword))
                {
                    if (ValidateEmail(mail))
                    {
                        registerConsole.text = "Creating Profile...";
                        StartCoroutine(PostRegister(name, password, mail));
                    }
                    else
                        registerConsole.text = "Mail is invalid";
                }
                else
                    registerConsole.text = "Password doesn't match";
            }
            else
                registerConsole.text = "Password is too short or too long \n Must be at least 7 characters and at most 20";
        }
        else
            registerConsole.text = "Name is too short or too long \n Must be at least 4 characters and at most 20";
    }

    private IEnumerator PostRegister(string name, string password, string mail)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("password", password);
        form.AddField("mail", mail);

        using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                registerConsole.text = "No internet connection :(";
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "Success")
                {
                    // GO TO PROFILE
                    registerConsole.text = "Loading Profile...";
                    PlayerPrefs.SetString("userName", name);
                    SceneManager.LoadScene(2);
                }
                else if (www.downloadHandler.text == "NameIsTaken") 
                {
                    registerConsole.text = "Name is already taken";
                }
            }
        }
    }

    #endregion

    #region Login

    public void Login()
    {
        string name = loginName.text;
        string password = loginPassword.text;
        if (ValidateName(name))
        {
            if (ValidatePassword(password))
            {
                loginConsole.text = "Loading Profile...";
                StartCoroutine(PostLogin(name, password));
            }
            else
                loginConsole.text = "Password is too short or too long \n Must be at least 7 characters  and at most 20";
        }
        else
            loginConsole.text = "Name is too short or too long \n Must be at least 4 characters and at most 20";
    }

    private IEnumerator PostLogin(string name, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(GENERAL.SERVER + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                loginConsole.text = "No internet connection :(";
            }
            else
            {
                //Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);

                if (www.downloadHandler.text == "1")
                {
                    // GO TO PROFILE
                    loginConsole.text = "Loading Profile...";
                    PlayerPrefs.SetString("userName", name);
                    SceneManager.LoadScene(2);
                }
                else if (www.downloadHandler.text == "0")
                {
                    loginConsole.text = "Wrong name or password";
                }
            }
        }
    }

    #endregion
}
