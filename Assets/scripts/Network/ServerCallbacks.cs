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
                cngChar.cngCharBtnSet(false);
            }
            else 
            {
                gameMenu.showLoading(false);
            }
        }
        else
        {
            if (BoltNetwork.IsClient)
            {
                if (playerData.gametype == 3)
                {
                    cngChar.cngCharBtnSet(false);
                }
            }
        }
        //Debug.Log("Connected!");
    }

    public override void Disconnected(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            if (playerData.gametype == 3)
            {
                Vector3 newpos = new Vector3(6f, 0, -9f);
                // принять координаты персонажа от отсоединившегося второго игрока и создать персонажа

                cngChar.CreateCharacter(!playerData.isPlayer1, newpos);

                cngChar.cngCharBtnSet(true);
            }
            else
            {
                gameMenu.showLoading(true);
            }
        }
        else
        {
            if (BoltNetwork.IsClient)
            {
                // перед отсоединением: отправить "первому игроку" данные из JournalInfo И (!) КООРДИНАТЫ (!) персонажа.

            }
        }
        //Debug.Log("Disconnected...");
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        SceneManager.LoadScene("MainMenu");
    }
}