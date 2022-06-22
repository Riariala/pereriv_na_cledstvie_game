using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string UID;
    public int money;

    public User()
    {
    }

    public User(string UID, int money)
    {
        this.UID = UID;
        this.money = money;
    }
}
