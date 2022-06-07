using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class CharHandler : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public PlayerData data;
    public bool isPlayer1;
    //public GameObject player1;

    public void SetValueChar()
    {
        isPlayer1 = data.isPlayer1;
        var token = new CharToken();
        token.isPlayer1= isPlayer1;
        //BoltNetwork.Instantiate(BoltPrefabs.Test, token);
        //Debug.Log(token.isPlayer1);
        //Debug.Log("Сделаль11111111111");
    }

}
