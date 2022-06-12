using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
//using Unity.TextMeshPro;
//using TMPro;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField usernameRegisterField;
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    //public InputField passwordRegisterVerifyField;
    public Text warningRegisterText;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //InitializeFirebase();
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Ошибка регистрации {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Ошибка входа";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Email отсутствует";
                    break;
                case AuthError.MissingPassword:
                    message = "Пароль отсутствует";
                    break;
                case AuthError.WrongPassword:
                    message = "Неверный пароль";
                    break;
                case AuthError.InvalidEmail:
                    message = "Недействительный email";
                    break;
                case AuthError.UserNotFound:
                    message = "Данный аккаунт не существует";
                    break;
            }
            warningLoginText.text = message;
            Debug.Log(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Вы успешно вошли";
            Debug.Log("Logged In");
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        InitializeFirebase();
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Введите логин";
        }
        /*else if(passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }*/
        else if (_password == "")
        {
            warningRegisterText.text = "Введите пароль";
        }
        else if (_password.Length < 6)
        {
            warningRegisterText.text = "Пароль слишком короткий";
        }
        else 
        {
            //Call the Firebase auth signin function passing the email and password
            Debug.Log("трунь");
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            Debug.Log("кукуруза");
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Ошибка регистрации {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Ошибка Регистрации";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Отсутствует email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Отсутствует пароль";
                        break;
                    case AuthError.WeakPassword:
                        message = "Слабый пароль";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email уже используется";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Ошибка регистрации {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Ошибка логина";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        //UIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }
}
