using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

[BoltGlobalBehaviour(BoltNetworkModes.Server)]
public class ServerCallbacks : Photon.Bolt.GlobalEventListener
{
    /*public NetworkCallbacks callbacks;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;*/


    public override void Connected(BoltConnection connection)
    {
        /*var log = LogEvent.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();*/
        /*var startData = StartData.Create();
        startData.DialogId = 12;
        startData.ActionsSaver = "בפעמם";
        startData.Send();*/
        //data.dialogId = callbacks.dialogId;
        var ask = AskForData.Create();
        ask.Ask = true;
        ask.Send();
    }
}