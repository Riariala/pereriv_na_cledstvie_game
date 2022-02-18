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
    //private Transform _transform;
    [SerializeField] private FixedJoystick _joystick;
    private Collider _collider;
    private Vector3 closest_point;
    private Vector3 change_pos;
    //[SerializeField] private Animator _animator;

    //[SerializeField] private float speed =  10f;
    private float speed = 170f;

    
    public override void Attached()
    {
        _rb = transform.GetChild(0).GetComponent<Rigidbody>();
        //_rb = GetComponent<Rigidbody>();
        state.SetTransforms(state.PlayerTransform, transform);
        
        //ChangeJoystick(_joystick);
        //_transform = transform;
    }

    public override void SimulateOwner()
    {

        if (!(_joystick is null))
        {
            //transform.position = transform.position + new Vector3(_joystick.Horizontal, -transform.position.y, _joystick.Vertical) * speed * BoltNetwork.FrameDeltaTime;
            _rb.velocity = new Vector3(_joystick.Horizontal * speed * BoltNetwork.FrameDeltaTime, _rb.velocity.y, _joystick.Vertical * speed* BoltNetwork.FrameDeltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log((other.transform.position-transform.position).normalized);
            //transform.position = transform.position + (other.transform.position-transform.position).normalized * -0.1f;
            //transform.position = other.transform.position - (other.transform.position - transform.position);
            //Debug.Log(other.bounds.size);
            //Debug.Log(this.GetComponent<CapsuleCollider>());
            Vector3 normalised_distance = (other.transform.position-transform.position).normalized * -1;
            Vector3 other_size = other.bounds.size;
            Vector3 distance_teleport = new Vector3(normalised_distance.x * other_size.x, transform.position.y, normalised_distance.z * other_size.z);
            //transform.position = distance_teleport;
            this.GetComponent<Rigidbody>().AddForce (normalised_distance  * 10F);
            //transform.position = transform.position + transform.GetComponent<Rigidbody>().velocity * (- BoltNetwork.FrameDeltaTime * speed);
            //this.GetComponent<Rigidbody>().velocyty = 
            //this.gameObject.GetComponent<PlayerController>().GoBack();
            //_rb.velocity = new Vector3(0,0,0);
            //Debug.Log(this.GetComponent<CapsuleCollider>().gameObject.name);
            // Transform tr = this.GetComponent<CapsuleCollider>().gameObject.transform;
            // tr.position = new Vector3( -1 * _joystick.Horizontal * speed * BoltNetwork.FrameDeltaTime + tr.position.x, tr.position.y, -1 * _joystick.Vertical * speed * BoltNetwork.FrameDeltaTime + tr.position.z);
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
}
