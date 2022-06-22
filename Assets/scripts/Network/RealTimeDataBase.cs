using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class RealTimeDataBase : MonoBehaviour
{
    DatabaseReference reference;


    public void writeNewUser(string userId, int money)
    {
        Debug.Log(FirebaseDatabase.DefaultInstance.RootReference);
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        User user = new User(userId, money);
        string json = JsonUtility.ToJson(user);
        Debug.Log(user.UID);
        Debug.Log(reference);

        reference.Child("money_base").Child(user.UID).SetRawJsonValueAsync(json);
    }
}
