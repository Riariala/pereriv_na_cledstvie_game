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

public class CharToken : Photon.Bolt.PooledProtocolToken
{
    public bool isPlayer1;
    public override void Read(UdpPacket packet)
    {
        // Deserialize Token data
        isPlayer1 = packet.ReadBool();
    }

    public override void Write(UdpPacket packet)
    {
        // Serialize Token Data
        packet.WriteBool(isPlayer1);
    }

    public override void Reset()
    {
        // Reset Token Data
        isPlayer1 = true;
    }
}
