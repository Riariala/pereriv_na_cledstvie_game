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
    public bool click;
    public bool next;
    public bool ask;
    public int dialogId;
    public string actionsSaver;
    public PlayerData data;
    public ActionsSaver actions;
    public JournalInfo journal;

    public override void BoltStartBegin()
    {
        BoltNetwork.RegisterTokenClass<CharToken>();

    }

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
            isPlayer1 = !isPlayer1;
            actions.setDefault();
            journal.clearAll();
        }
        var spawnPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        if (isPlayer1)
        {
            var player = BoltNetwork.Instantiate(player1, spawnPos, Quaternion.identity);
            player.name = "Rogers";
            PlayerController player_script = player.GetComponent<PlayerController>();
            player_script.ChangeJoystick(_joystick);
            Debug.Log("Player 1 " + player.name);
        }
        else
        {
            var player = BoltNetwork.Instantiate(player2, spawnPos, Quaternion.identity);
            player.name = "Mary";
            PlayerController player_script = player.GetComponent<PlayerController>();
            player_script.ChangeJoystick(_joystick);
            Debug.Log("Player 2  " + player.name);
        }

    }

    public override void OnEvent(PlayerCharacter evnt)
    {
        isPlayer1=evnt.IsPlayer1;
        data.isPlayer1 = isPlayer1;
        Debug.Log(isPlayer1);
    }

    public override void OnEvent(IsBusy evnt)
    {
        isBusy = evnt.Busy;
        Debug.Log(isBusy);
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

    /*public override void SceneLoadLocalDone(string map)
    {
        SpawnServerPlayer();
    }*/

}
