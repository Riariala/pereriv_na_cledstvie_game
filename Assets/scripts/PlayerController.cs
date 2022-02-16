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

//[RequireComponent(typeof(Rigidbody)), typeof(BoxCollider)))]

public class PlayerController : Photon.Bolt.EntityBehaviour<ICustomPlayer>//MonoBehaviour
{
    private Rigidbody _rb;
    //private Transform _transform;
    [SerializeField] private FixedJoystick _joystick;
    //[SerializeField] private Animator _animator;

    //[SerializeField] private float speed =  10f;
    [SerializeField] private float speed = 0.25f;

    /*void Start()
    {
        //_rb = GetComponent<Rigidbody>();
        //state.SetTransforms(state.PlayerTransform, transform);
    }*/
    public override void Attached()
    {
        //_rb = GetComponent<Rigidbody>();
        Debug.Log("Attached");
        state.SetTransforms(state.PlayerTransform, transform);
        //ChangeJoystick(_joystick);
        //_transform = transform;
    }

    // void Update()
    // {

    // }

    /*public override void Attached()
    {
        _rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.PlayerTransform, transform);
    }*/

    public override void SimulateOwner()
    {

        if (!(_joystick is null))
        {
            Debug.Log("S2");
            transform.position = transform.position + new Vector3(_joystick.Horizontal, -transform.position.y, _joystick.Vertical) * speed * BoltNetwork.FrameDeltaTime;
            //_rb.velocity = new Vector3(_joystick.Horizontal * speed * BoltNetwork.FrameDeltaTime, _rb.velocity.y, _joystick.Vertical * speed* BoltNetwork.FrameDeltaTime);
        }
    }
        

    public void ChangeJoystick(FixedJoystick newJstk)
    {
        _joystick = newJstk;
    }
}
