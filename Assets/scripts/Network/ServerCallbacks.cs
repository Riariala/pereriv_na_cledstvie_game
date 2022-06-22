using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class ServerCallbacks : Photon.Bolt.GlobalEventListener
{
    public changeCharacter cngChar;
    public PlayerData playerData;
    public GameMenu gameMenu;
    public static event Action<bool> OnConnectResult;

    public override void Connected(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            if (playerData.gametype == 3)
            {
                Debug.Log(cngChar);
                cngChar.cngCharBtnSet(false);
            }
            else 
            {
                gameMenu.showLoading(false);
            }
        }
    }

    public override void Disconnected(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            if (playerData.gametype == 3)
            {
                Debug.Log(cngChar);
                cngChar.cngCharBtnSet(true);
            }
            else 
            {
                gameMenu.showLoading(true);
                //gameMenu.toMainMenu();
            }
        }
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        SceneManager.LoadScene("MainMenu");
    }



}