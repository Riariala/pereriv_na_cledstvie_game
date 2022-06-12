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
    public static event Action<bool> OnConnectResult;

    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        SceneManager.LoadScene("MainMenu");
    }
}