using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    private Rigidbody2D _rigidbody;
    
    
    private void Start()
    {
        SetOwnerShipRPC();
    }

    [Rpc(SendTo.Server)]
    void SetOwnerShipRPC()
    {
        GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
        //
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
