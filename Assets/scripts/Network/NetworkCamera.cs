using UnityEngine;
using UdpKit;
using UdpKit.Platform.Photon;
using Photon.Bolt;

public class NetworkCamera : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public GameObject playerCamera;
    public GameObject player;
    private Transform cameraTransform;
    private Transform playerTransform;


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
            cameraTransform.position = new Vector3(playerTransform.position.x, 7.6f, -6.8f);
            cameraTransform.LookAt(playerTransform.position + Vector3.up);
        }

    }
}
