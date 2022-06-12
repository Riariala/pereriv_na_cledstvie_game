using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

//[BoltGlobalBehaviour(BoltNetworkModes.Client)]
public class ServerCallbacks : Photon.Bolt.GlobalEventListener
{
    /*public NetworkCallbacks callbacks;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;*/

    public static event Action<bool> OnConnectResult;

    public override void Connected(BoltConnection connection)
    {
        /*var log = LogEvent.Create();
        log.Message = string.Format("{0} connected", connection.RemoteEndPoint);
        log.Send();*/
        /*var startData = StartData.Create();
        startData.DialogId = 12;
        startData.ActionsSaver = "бфтон";
        startData.Send();*/
        //data.dialogId = callbacks.dialogId;
        /*var ask = AskForData.Create();
        ask.Ask = true;
        ask.Send();*/
    }
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        SceneManager.LoadScene("MainMenu");
        /*registerDoneCallback(() =>
        {
            Invoke("StartBoltServer", 0.5f);
        });*/
    }


   /* public override void ConnectFailed(UdpKit.UdpEndPoint endpoint, Photon.Bolt.IProtocolToken token)
    {
        base.ConnectFailed(endpoint, token);
        Debug.Log("Он ушёл. Не жди.");
        sendResult(false);
    }
    public override void Disconnected(BoltConnection connection)
    {
        if (BoltNetwork.Server.Equals(connection))
        {
            BoltLog.Warn("Disconnected from the server");
        }
    }
    void sendResult(bool success)
    {
        if (null != OnConnectResult)
        {
            OnConnectResult.Invoke(success);
            OnConnectResult = null;
        }
    }*/




}