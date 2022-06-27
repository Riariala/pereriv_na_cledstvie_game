
using System.Collections.Generic;
using UnityEngine;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using System.Linq;

[BoltGlobalBehaviour]
public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player1;
    public GameObject player2;
    public FixedJoystick _joystick;
    public bool isPlayer1;
    public bool isBusy;
    public bool isBusyAnswered;
    public bool click;
    public bool next;
    public bool ask;
    public bool clickDialog;
    public bool isGameOver;
    public bool isOverAns;
    public int dialogId;
    public string dialogSaverData;
    public string actionsSaver;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        isPlayer1 = true;
        if (BoltNetwork.IsClient)
        {
            isPlayer1 = !isPlayer1;
            actions.setDefault();
            journal.clearAll();
            data.gametype = 4;
        }
        data.isPlayer1 = isPlayer1;
        data.isBusy = false;
        data.isGameJustStarted = new List<bool>();
        data.isGameJustStarted.Add(true);
        data.isGameJustStarted.Add(true);
        data.isGameOver = new List<bool>();
        data.isGameOver.Add(false);
        data.isGameOver.Add(false);
        Vector3 spawnPos;
        GameObject player;
        //if (data.gametype != 3)
        //{
            if (isPlayer1)
            {
                data.dialogId = 0;
                spawnPos = new Vector3(6f, 0, -9f);
                player = BoltNetwork.Instantiate(player1, spawnPos, Quaternion.identity);
                player.name = "Rogers";
                //player.GetComponent<NetworkCamera>().cameraOn();
                //PlayerController player_script = player.GetComponent<PlayerController>();
                //player_script.ChangeJoystick(_joystick);
            }
            else
            {
                spawnPos = new Vector3(-1.5f, 0, 43f);
                player = BoltNetwork.Instantiate(player2, spawnPos, Quaternion.identity);
                player.name = "Mary";
                //player.GetComponent<NetworkCamera>().cameraOn();
                //PlayerController player_script = player.GetComponent<PlayerController>();
                //player_script.ChangeJoystick(_joystick);
            }
            player.GetComponent<NetworkCamera>().cameraOn();
            PlayerController player_script = player.GetComponent<PlayerController>();
            player_script.ChangeJoystick(_joystick);
        //}
        if (BoltNetwork.IsClient)
        {
            var askHost = AskForData.Create();
            askHost.Ask = true;
            askHost.Send();
        }
    }

    public override void OnEvent(PlayerCharacter evnt)
    {
        isPlayer1=evnt.IsPlayer1;
    }

    public override void OnEvent(IsBusy evnt)
    {
        isBusy = evnt.Busy;
        isBusyAnswered = true;
    }
    public override void OnEvent(ClickOnPlayer evnt)
    {
        click = evnt.Click;
    }
    public override void OnEvent(NextDialog evnt)
    {
        next = evnt.Next;
    }

    public override void OnEvent(StartData evnt)
    {
        dialogId = evnt.DialogId;
        actionsSaver = evnt.ActionsSaver;
    }
    public override void OnEvent(AskForData evnt)
    {
        ask = evnt.Ask;
    }

    public override void OnEvent(DialogSaverEvent evnt)
    {
        dialogSaverData = evnt.Data;
    }

    public override void OnEvent(ClickDialog evnt)
    {
        clickDialog = evnt.Click;
    }

    public override void OnEvent(IsGameOverCheck evnt)
    {
        isGameOver = evnt.IsGameOver;
    }

    public override void OnEvent(IsGameOverAns evnt)
    {
        isOverAns = evnt.IsOverAns;
    }
}
