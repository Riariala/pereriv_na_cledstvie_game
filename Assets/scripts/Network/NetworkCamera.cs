using UnityEngine;
using UdpKit;
using UdpKit.Platform.Photon;
using Photon.Bolt;

public class NetworkCamera : Photon.Bolt.EntityBehaviour<ICustomPlayer>
{
    public GameObject playerCamera;

    private void Update()
    {
        if (entity.IsOwner && playerCamera.activeInHierarchy == false)
        {
            playerCamera.SetActive(true);
        }
    }
}
