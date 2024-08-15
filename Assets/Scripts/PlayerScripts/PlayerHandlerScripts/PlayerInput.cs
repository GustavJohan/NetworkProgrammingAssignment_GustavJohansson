using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    public bool IsActive = false;

    [Rpc(SendTo.Everyone)]
    public void SetValuesRPC(ulong ID)
    {
        _rigidbody = NetworkManager.SpawnManager.SpawnedObjects[ID].GetComponent<Rigidbody2D>();
        IsActive = true;
        print(_rigidbody);
    }

    void Update()
    {
        if (IsLocalPlayer && IsActive)
        {
            
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized * (20);

            MoveRPC(move);
        }
        
        
    }

    
    [Rpc(SendTo.Server)]
    public void MoveRPC(Vector3 data)
    {
        _rigidbody.velocity = data;
    }
}
