using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;

public class CharReader : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public bool isPlayer1;
    public override void Attached()
    {
        var CharToken = ProtocolTokenUtils.GetToken<CharToken>();
        //var CharToken=(CharToken)entity.Char;
        //isPlayer1 = CharToken.isPlayer1;
        Debug.Log(CharToken);
        Debug.Log("Сделаль");
        //var readerChar = player1.GetComponent<BoltEntity>().Character;//(CharToken)entity.Character;
    }
}