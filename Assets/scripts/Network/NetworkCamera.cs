using UnityEngine;
using UdpKit;
using UdpKit.Platform.Photon;
using Photon.Bolt;

public class NetworkCamera : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public GameObject playerCamera;
    public GameObject player;
    //public Vector3 startPlayerPosition;
    public Vector3 centerArea = new Vector3(4.39f, 7.78f, -0.46f);

    private Transform cameraTransform;
    private Transform playerTransform;
    private float cameraYPos = 7.78f;
    private float cameraZPos = -10.46f;
    //private float cameraZPos = -5f;




    private void FixedUpdate()
    {
        if (entity.IsOwner && playerCamera.activeInHierarchy == false)
        {
            playerCamera.SetActive(true);
            cameraTransform = playerCamera.transform;
            playerTransform = player.transform;
        }
        if (entity.IsOwner)
        {
            //cameraTransform.position = new Vector3((centerArea.x + playerTransform.position.x) / 2, cameraYPos, (centerArea.z + playerTransform.position.z) / 2);
            cameraTransform.position = new Vector3((4 * playerTransform.position.x + centerArea.x) / 5 , cameraYPos, cameraZPos + playerTransform.position.z/4);
            cameraTransform.LookAt(playerTransform.position + Vector3.up);
        }

    }
}
