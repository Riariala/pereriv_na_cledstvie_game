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
    private float speed = 0.1f;
    private Transform playerCamera_transf;


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
            float rad = playerCamera_transf.rotation.y * Mathf.PI;
            _transform.position = new Vector3(_transform.position.x + _joystick.Horizontal * speed * Mathf.Cos(rad) + _joystick.Vertical * speed * Mathf.Sin(rad),
                _transform.position.y,
                _transform.position.z + _joystick.Horizontal * speed * Mathf.Sin(rad) + _joystick.Vertical * speed * Mathf.Cos(rad));
        }
    }

        
    public void ChangeJoystick(FixedJoystick newJstk)
    {
        _joystick = newJstk;
    }

}
