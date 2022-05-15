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
    public Rigidbody _rb;
    public GameObject playerCamera;
    private Transform _transform;
    [SerializeField] private FixedJoystick _joystick;
    private Collider _collider;
    private Vector3 closest_point;
    private Vector3 change_pos;
    //[SerializeField] private Animator _animator;

    //[SerializeField] private float speed =  10f;
    private float speed = 5f;
    private Transform playerCamera_transf;


    public override void Attached()
    {
        _rb = transform.GetChild(0).GetComponent<Rigidbody>();
        //_rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.PlayerTransform, transform);
        playerCamera_transf = playerCamera.transform;

        //ChangeJoystick(_joystick);
        //_transform = _rb.transform;
        _transform = transform;
    }

    public override void SimulateOwner()
    {
        if (!(_joystick is null))
        {

            _rb.velocity = new Vector3(_joystick.Horizontal * speed * BoltNetwork.FrameDeltaTime, _rb.velocity.y, _joystick.Vertical * speed* BoltNetwork.FrameDeltaTime);
            _transform.position = new Vector3(_joystick.Horizontal * speed * BoltNetwork.FrameDeltaTime + _transform.position.x, _transform.position.y, _transform.position.z + _joystick.Vertical * speed * BoltNetwork.FrameDeltaTime);
            //Debug.Log(playerCamera_transf.rotation.y);
        }
    }

    public void GoBack()
    {
        Debug.Log(_joystick);
        //_rb.velocity = new Vector3(0,0,0);
    }
        
    public void ChangeJoystick(FixedJoystick newJstk)
    {
        _joystick = newJstk;
    }

    /*void OnCollisionEnter(Collision other)
    {
        Debug.Log("Толк");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Толк2");
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }*/
}
