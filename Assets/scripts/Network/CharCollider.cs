using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCollider : MonoBehaviour
{
    public CapsuleCollider charCol;
    public CapsuleCollider blockerCol;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(charCol, blockerCol, true);
    }
}
