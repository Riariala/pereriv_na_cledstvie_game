using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit;
using UnityEngine.SceneManagement;
using UdpKit.Platform.Photon;
using Photon.Bolt;
using Photon.Bolt.Matchmaking;
using Photon.Bolt.Utils;


public class PlayerController : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{
    public Rigidbody _rb;
    public GameObject playerCamera;
    private Transform _transform;
    [SerializeField] private FixedJoystick _joystick;
    private Collider _collider;
    private Vector3 closest_point;
    private Vector3 change_pos;
    public float speed = 0.1f;
    private Transform playerCamera_transf;
    public NetworkCamera playerCameraScript;


    public override void Attached()
    {
        _rb = transform.GetChild(0).GetComponent<Rigidbody>();
        state.SetTransforms(state.PlayerTransform, transform);
        playerCamera_transf = playerCamera.transform;
        _transform = transform;
    }

    public override void SimulateOwner()
    {
        if (!(_joystick is null))
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                Vector3 dirRight = playerCamera_transf.right;
                dirRight.y = 0f;
                dirRight.Normalize();
                Vector3 dirForward = playerCamera_transf.forward;
                dirForward.y = 0f;
                dirForward.Normalize();
                Debug.Log(dirForward);
                Vector3 desiredMovement = (dirForward * _joystick.Vertical) + (dirRight * _joystick.Horizontal);
                desiredMovement.Normalize();
                desiredMovement *= speed;
                Debug.Log("normal " + desiredMovement.x.ToString() + " " + desiredMovement.z.ToString());
                _transform.position = desiredMovement + _transform.position;
                float gip = Mathf.Sqrt((float)Math.Pow(_joystick.Horizontal,2) + (float)Math.Pow(_joystick.Vertical, 2));
                float rot = Mathf.Atan2(_joystick.Horizontal / gip,  _joystick.Vertical / gip) * Mathf.Rad2Deg;
                _transform.Rotate(Vector3.up, rot - _transform.rotation.eulerAngles.y);
            }
        }
    }

        
    public void ChangeJoystick(FixedJoystick newJstk)
    {
        _joystick = newJstk;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.name == "EventColliders")
        {
            var otherData = other.GetComponent<CameraColliderData>();
            playerCameraScript.cameraYPos = otherData.cameraYPos;
            playerCameraScript.cameraZPos = otherData.cameraZPos;
            playerCameraScript.cameraXPos = otherData.cameraXPos;
            playerCameraScript.camXmodif = otherData.camXmodif;
            playerCameraScript.camZmodif = otherData.camZmodif;

        }
        
    }
}
