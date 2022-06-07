using UnityEngine;
using UdpKit.Platform.Photon;
using Photon.Bolt;

[BoltGlobalBehaviour]
public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player1;
    public GameObject player2;
    public bool isPlayer1;
    public CharReader reader;

    public override void BoltStartBegin()
    {
        BoltNetwork.RegisterTokenClass<CharToken>();

    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        if (BoltNetwork.IsClient)
        {
            isPlayer1 = !reader.isPlayer1;
        }
        var spawnPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        if (isPlayer1)
        {
            BoltNetwork.Instantiate(player1, spawnPos, Quaternion.identity);
            Debug.Log("1");
        }
        else
        {
            BoltNetwork.Instantiate(player2, spawnPos, Quaternion.identity);
            Debug.Log("2");
        }
        
    }

    /*public override void SceneLoadLocalDone(string map)
    {
        SpawnServerPlayer();
    }*/

}
