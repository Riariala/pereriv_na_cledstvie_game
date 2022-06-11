using UnityEngine;
using UdpKit;
using UdpKit.Platform.Photon;
using Photon.Bolt;

public class NetworkCamera : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public GameObject playerCamera;
    public GameObject player;
    //public Vector3 centerArea = new Vector3(4.39f, 7.78f, -0.46f);

    private Transform cameraTransform;
    private Transform playerTransform;
    public float cameraYPos = 13.75f;
    public float cameraZPos = 10.5f;
    public float cameraXPos = 8f;
    public float camXmodif = 0.5f;
    public float camZmodif = 0.6f;

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
            cameraTransform.position = new Vector3(camXmodif * playerTransform.position.x + cameraXPos,
                cameraYPos,
                cameraZPos + camZmodif * playerTransform.position.z);
            cameraTransform.LookAt(playerTransform.position + Vector3.up);
        }

    }
}
