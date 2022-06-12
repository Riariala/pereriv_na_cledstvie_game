using UnityEngine;
using UdpKit.Platform.Photon;
using Photon.Bolt;

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
    public int dialogId;
    public string dialogSaverData;
    public string actionsSaver;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;

    /*public override void BoltStartBegin()
    {
        BoltNetwork.RegisterTokenClass<CharToken>();

    }*/

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        /*data.SetCharacter(true);
        Debug.Log(data);
        if (BoltNetwork.IsServer)
        {
            Debug.Log("сервер");
            handler.SetValueChar();
        }
        
        Debug.Log(reader.isPlayer1);
        Debug.Log("варенец");

        isPlayer1 = data.isPlayer1;
        Debug.Log(data.isPlayer1);*/
        if (BoltNetwork.IsClient)
        {
            Debug.Log("Client");
            isPlayer1 = !isPlayer1;
            actions.setDefault();
            journal.clearAll();
        }
        Debug.Log("data.isPlayer1 before " + data.isPlayer1.ToString());
        data.isPlayer1 = isPlayer1;
        data.isBusy = false;
        data.isGameJustStarted = true;
        data.isGameOver = false;
        Debug.Log("data.isPlayer1 after " + data.isPlayer1.ToString());
        Vector3 spawnPos;
        if (isPlayer1)
        {
            spawnPos = new Vector3(6f, 0, -9f);
            var player = BoltNetwork.Instantiate(player1, spawnPos, Quaternion.identity);
            player.name = "Rogers";
            PlayerController player_script = player.GetComponent<PlayerController>();
            player_script.ChangeJoystick(_joystick);
            Debug.Log("Player 1 " + player.name);
        }
        else
        {
            spawnPos = new Vector3(-1.5f, 0, 43f);
            var player = BoltNetwork.Instantiate(player2, spawnPos, Quaternion.identity);
            player.name = "Mary";
            PlayerController player_script = player.GetComponent<PlayerController>();
            player_script.ChangeJoystick(_joystick);
            Debug.Log("Player 2  " + player.name);
        }
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
        //data.isPlayer1 = isPlayer1;
        //Debug.Log(isPlayer1);
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
    /*public override void SceneLoadLocalDone(string map)
    {
        SpawnServerPlayer();
    }*/

}
