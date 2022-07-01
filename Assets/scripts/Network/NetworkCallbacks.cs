
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
    public string journalInfo;
    public bool isPlayer1_forStart;
    public Vector3 newcharPosition;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;
    public changeCharacter _changeCharacter;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        //isPlayer1 = true;
        if (BoltNetwork.IsClient)
        {
            var askHost = AskForData.Create();
            askHost.Ask = true;
            askHost.Send();
            //isPlayer1 = !isPlayer1;
            actions.setDefault();
            journal.clearAll();
            data.gametype = 4;
        }
        else
        {
            //data.isPlayer1 = true;
            GameObject player;
            if (data.gametype == 3)
            {
                data.dialogId = 0;
                player = createCharacterByType(false, new Vector3(-1.5f, 0, 43f));
                _changeCharacter.setMary(player);
                //player.GetComponent<NetworkCamera>().cameraOut();
                player = createCharacterByType(true, new Vector3(6f, 0, -9f));
                _changeCharacter.setRogers(player);
            }
            data.isPlayer1 = true;
            if (data.gametype != 3)
            {
                if (data.isPlayer1)
                {
                    data.dialogId = 0;
                    player = createCharacterByType(true, new Vector3(6f, 0, -9f));
                }
                else
                {
                    player = createCharacterByType(false, new Vector3(-1.5f, 0, 43f));
                }
                player.GetComponent<NetworkCamera>().cameraOn();
                PlayerController player_script = player.GetComponent<PlayerController>();
                player_script.ChangeJoystick(_joystick);
            }
        }
        data.isBusy = false;
        data.isGameJustStarted = new List<bool>();
        data.isGameJustStarted.Add(true);
        data.isGameJustStarted.Add(true);
        data.isGameOver = new List<bool>();
        data.isGameOver.Add(false);
        data.isGameOver.Add(false);

    }

    private GameObject createCharacterByType(bool first, Vector3 spawnpos)
    {
        GameObject player;
        if (first)
        {
            player = BoltNetwork.Instantiate(player1, spawnpos, Quaternion.identity);
            player.name = "Rogers";
        }
        else 
        {
            player = BoltNetwork.Instantiate(player2, spawnpos, Quaternion.identity);
            player.name = "Mary";
        }
        return player;
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
        isPlayer1_forStart = evnt.isPlayer1;
        newcharPosition = evnt.Position;
        journalInfo = evnt.JournalInfo;
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

    public void createSecondPlayer(bool isfirst, Vector3 pos)
    {
        GameObject player;
        player = createCharacterByType(isfirst, pos);
        player.GetComponent<NetworkCamera>().cameraOn();
        PlayerController player_script = player.GetComponent<PlayerController>();
        player_script.ChangeJoystick(_joystick);
    }


}
