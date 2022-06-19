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

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Photon.Bolt.EntityBehaviour<ICustomPlayer>
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
        //_rb = transform.GetChild(0).GetComponent<Rigidbody>();
        _rb = transform.GetComponent<Rigidbody>();
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
                Vector3 desiredMovement = (dirForward * _joystick.Vertical) + (dirRight * _joystick.Horizontal);
                Debug.Log("desiredMovement " + (desiredMovement * speed).ToString());
                desiredMovement.Normalize();
                desiredMovement *= speed;
                //_transform.position = desiredMovement + _transform.position;
                Debug.Log("desiredMovement " + (desiredMovement * speed).ToString());
                // _rb.velocity = desiredMovement; 
                //_rb.AddForce(desiredMovement);
                //_transform.position = _transform.position;
                //_transform.Translate(desiredMovement * speed);
                _rb.AddForce(desiredMovement, ForceMode.Impulse);
                _transform.position = _transform.position;
                Debug.Log("_rb.velocity " + _rb.velocity.ToString());
                //_rb.MovePosition(desiredMovement * speed + _rb.position );
                float gip = Mathf.Sqrt((float)Math.Pow(_joystick.Horizontal,2) + (float)Math.Pow(_joystick.Vertical, 2));
                float rot = Mathf.Atan2(_joystick.Horizontal / gip,  _joystick.Vertical / gip) * Mathf.Rad2Deg;
                _transform.rotation = Quaternion.Euler(_transform.eulerAngles.x, rot + Camera.main.transform.eulerAngles.y, _transform.eulerAngles.z);

                
            }
            else { _rb.velocity = new Vector3(0, 0, 0); }
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
