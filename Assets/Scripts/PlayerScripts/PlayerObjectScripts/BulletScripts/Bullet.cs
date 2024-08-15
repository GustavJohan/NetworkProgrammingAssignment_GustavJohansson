using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _speed = 5;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetupBulletRPC();
    }

    [Rpc(SendTo.Server)]
    void SetupBulletRPC()
    {
        GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.isKinematic = false;
    }
    private void Update()
    {
        setSpeedRPC();
    }

    [Rpc(SendTo.Server)]
    void setSpeedRPC()
    {
        _rigidbody2D.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsServer)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<NetworkObject>().Despawn();
            }
        
            if (!other.CompareTag("Player"))
            { 
                GetComponent<NetworkObject>().Despawn();
            }
            
        }
    }
}
