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
    public PlayerData playerData;
    public bool isPlayer1Belong;
    public float cameraYPos = 13.75f;
    public float cameraZPos = 10.5f;
    public float cameraXPos = 8f;
    public float camXmodif = 0.5f;
    public float camZmodif = 0.6f;

    private void FixedUpdate()
    {
        if (playerData.gametype != 0 && playerData.gametype != 3)
        {
            if (entity.IsOwner && !playerCamera.activeInHierarchy)
            {
                cameraOn();
            }
            if (entity.IsOwner)
            {
                cameraTransform.position = new Vector3(camXmodif * playerTransform.position.x + cameraXPos,
                    cameraYPos,
                    cameraZPos + camZmodif * playerTransform.position.z);
                cameraTransform.LookAt(playerTransform.position + Vector3.up);
            }
        }
        else
        {
            if (isPlayer1Belong == playerData.isPlayer1 && !playerCamera.activeInHierarchy)
            {
                cameraOn();
            }
            if (isPlayer1Belong == playerData.isPlayer1)
            {
                cameraTransform.position = new Vector3(camXmodif * playerTransform.position.x + cameraXPos,
                    cameraYPos,
                    cameraZPos + camZmodif * playerTransform.position.z);
                cameraTransform.LookAt(playerTransform.position + Vector3.up);
            }
        }
    }

    public void cameraOut()
    {
        playerCamera.SetActive(false);
    }

    public void cameraOn()
    {
        playerCamera.SetActive(true);
        cameraTransform = playerCamera.transform;
        playerTransform = player.transform;
    }
}
