using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EnemyMovement : NetworkBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private NetworkObject _networkObject;

    private NetworkManager _manager;

    private GameObject _target;

    [SerializeField] private float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!IsServer)
        {
            return;
        }
        
        
        _networkObject = GetComponent<NetworkObject>();
        _networkObject.ChangeOwnership(NetworkManager.ServerClientId);
        _rigidbody2D = GetComponent<Rigidbody2D>();

        

        
        //finds all players and selects a random one as the target
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();

        _target = players[Random.Range(0, players.Length)].gameObject;
        
        
        if (_target == gameObject)
        {
            GetComponent<NetworkObject>().Despawn();
        }
        
    }

    [Rpc(SendTo.Server)]
    void setTargetRPC(ulong PlayerId)
    {
        _target = NetworkManager.SpawnManager.SpawnedObjects[PlayerId].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            Vector2 targetDirection = ((Vector2)_target.transform.position - (Vector2)transform.position).normalized;
            
            
            _rigidbody2D.velocity = targetDirection*speed;
            //_rigidbody2D.velocity = velocity;
        }
    }
}
