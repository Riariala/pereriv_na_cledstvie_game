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
    public GameObject changeCharBtn;
    private GameObject RogersSingle;
    private GameObject MarySingle;


    void OnEnable()
    {
        Player1_script = Player1.GetComponent<PlayerController>();
        Player2_script = Player2.GetComponent<PlayerController>();
        Player1_script.ChangeJoystick(_jystick);
        player_data.isPlayer1 = true;
        if (player_data.gametype != 0)
        {
            var character = PlayerCharacter.Create();
            character.IsPlayer1 = true;
            character.Send();
        }
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
        if (player_data.gametype == 0 || player_data.gametype == 3)
        {
            changeCharBtn.SetActive(true);
            spawnCharsInSingle();
        }
        else { changeCharBtn.SetActive(false); }
    }

    public void Change_chars(){
        player_data.isPlayer1 = !player_data.isPlayer1;
        if (player_data.gametype != 0)
        {
            var character = PlayerCharacter.Create();
            character.IsPlayer1 = player_data.isPlayer1;
            character.Send();


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
        else { changeCharsSingle();  }
    }

    public void spawnCharsInSingle()
    {
        player_data.isBusy = false;
        player_data.isGameJustStarted = new List<bool>();
        player_data.isGameJustStarted.Add(true);
        player_data.isGameJustStarted.Add(true);
        //player_data.isGameOver = false;
        player_data.isGameOver = new List<bool>();
        player_data.isGameOver.Add(false);
        player_data.isGameOver.Add(false);
        player_data.dialogId = 0;
        Vector3 spawnPos;
        spawnPos = new Vector3(6f, 0, -9f);
        RogersSingle = Instantiate(Player1, spawnPos, Quaternion.identity);
        RogersSingle.name = "Rogers";
        //RogersSingle.GetComponent<PlayerController>().ChangeJoystick(_jystick);
        PlayerController player_script = RogersSingle.GetComponent<PlayerController>();
        player_script.ChangeJoystick(_jystick);
        RogersSingle.GetComponent<NetworkCamera>().isPlayer1Belong = true;
        spawnPos = new Vector3(-1.5f, 0, 43f);
        MarySingle = Instantiate(Player2, spawnPos, Quaternion.identity);
        MarySingle.name = "Mary";
        MarySingle.GetComponent<NetworkCamera>().isPlayer1Belong = false;
    }

    public void changeCharsSingle()
    {
        if (player_data.isPlayer1)
        {
            RogersSingle.GetComponent<PlayerController>().ChangeJoystick(_jystick);
            RogersSingle.GetComponent<NetworkCamera>().cameraOn();
            MarySingle.GetComponent<PlayerController>().deleteJoystick();
            MarySingle.GetComponent<NetworkCamera>().cameraOut();
        }
        else
        {
            MarySingle.GetComponent<PlayerController>().ChangeJoystick(_jystick);
            MarySingle.GetComponent<NetworkCamera>().cameraOn();
            RogersSingle.GetComponent<PlayerController>().deleteJoystick();
            RogersSingle.GetComponent<NetworkCamera>().cameraOut();
        }
    }
}
