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


public class changeCharacter : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    [SerializeField] private GameObject Player1;
    [SerializeField] private GameObject Player2;
    private PlayerController Player1_script;
    private PlayerController Player2_script;
    [SerializeField] private FixedJoystick _jystick;
    public PlayerData player_data;
    public CharHandler handler;

    void OnEnable()
    {
        Player1_script = Player1.GetComponent<PlayerController>();
        Player2_script = Player2.GetComponent<PlayerController>();
        Player1_script.ChangeJoystick(_jystick);
        player_data.isPlayer1 = true;
        var character = PlayerCharacter.Create();
        character.IsPlayer1 = true;
        character.Send();
        if (player_data.isPlayer1)
        {
            Player1_script.ChangeJoystick(_jystick);
            player_data.SetObjectCharacter(Player1);
        }
        else
        {
            Player2_script.ChangeJoystick(_jystick);
            player_data.SetObjectCharacter(Player2);
        }
    }

    public void Change_chars(){
        var character = PlayerCharacter.Create();
        character.IsPlayer1 = !player_data.isPlayer1;
        character.Send();
        player_data.isPlayer1 = !player_data.isPlayer1;
        if (player_data.isPlayer1)
        {
            player_data.SetObjectCharacter(Player1);
            Player1_script.ChangeJoystick(_jystick);
        }
        else
        {
            Player2_script.ChangeJoystick(_jystick);
            player_data.SetObjectCharacter(Player2);
        }
    }
}
