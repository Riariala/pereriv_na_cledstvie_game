using UnityEngine;
using UdpKit.Platform.Photon;
using Photon.Bolt;


public class NetworkCallbacks : GlobalEventListener
{
    public GameObject player1;
    public GameObject player2;

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        var spawnPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        BoltNetwork.Instantiate(player1, spawnPos, Quaternion.identity);
    }

    /*public override void SceneLoadLocalDone(string map)
    {
        SpawnServerPlayer();
    }*/

}
