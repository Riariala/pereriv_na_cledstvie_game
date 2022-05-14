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


public class changeCharacter : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    private PlayerController Player1_script;
    private PlayerController Player2_script;
    [SerializeField] private FixedJoystick _jystick;
    public PlayerData player_data;

    void OnEnable()
    {
        Player1_script = Player1.GetComponent<PlayerController>();
        Player2_script = Player2.GetComponent<PlayerController>();
        Player1_script.ChangeJoystick(_jystick);
        player_data.SetCharacter(true, Player1);
        //player_data.isPlayer1 = true;
        //player_data.char_player = Player1;
    }

    //public void Change_chars(){
    //    if (player_data.isPlayer1){
    //        player_data.isPlayer1 = false;
    //        Player2_script.ChangeJoystick(_jystick);
    //        Player1_script.ChangeJoystick(null);
    //        player_data.char_player = Player2;
    //    }
    //    else
    //    {
    //        player_data.isPlayer1 = true;
    //        Player1_script.ChangeJoystick(_jystick);
    //        Player2_script.ChangeJoystick(null);
    //        player_data.char_player = Player1;
    //    }
    //}
}
