using Assets.Database;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SignupLogin : MonoBehaviour
{
    public TMP_InputField loginUsernameInput;
    public TMP_InputField loginPasswordInput;
    public Button loginButton;
    public TMP_Text loginErrorText;

    public TMP_InputField signupUsernameInput;
    public TMP_InputField signupPasswordInput;
    public Button signupButton;
    public TMP_Text signupErrorText;
    

    public void Start()
    {
        loginPasswordInput.contentType = TMP_InputField.ContentType.Password;
        loginButton.onClick.AddListener(HandleLoginSubmit);

        signupPasswordInput.contentType = TMP_InputField.ContentType.Password;
        signupButton.onClick.AddListener(HandleSignupSubmit);
    }

    async void HandleLoginSubmit()
    {
        string username = loginUsernameInput.text;
        string password = loginPasswordInput.text;

        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            loginErrorText.text = "Username and password cannot be empty.";
            return;
        }

        try
        {
            var user = await Database.SignIn(username, password);
            Debug.Log(user.DisplayName + " " + user.Id);
        } catch(System.Exception e)
        {
            loginErrorText.text = e.Message;
        }
    }

    async void HandleSignupSubmit()
    {
        string username = signupUsernameInput.text;
        string password = signupPasswordInput.text;

        Debug.Log("here");
        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            signupErrorText.text = "Username and password cannot be empty.";
            return;
        }
        Debug.Log("here2");
        try
        {
            var user = await Database.SignUp(username, password);
            Debug.Log(user.DisplayName + " " + user.Id);
        } catch(System.Exception e)
        {
            signupErrorText.text = e.Message;
        }
    }
}
