using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerHandler : NetworkBehaviour
{
    [SerializeField] private GameObject _playerObject;

    private PlayerInput _playerInputHandler;

    private PlayerDamageHandler _playerDamageHandler;

    private PlayerAiming _playerAiming;

    private NetworkObject _assignedPlayer;
    private void Start()
    {
        if (IsLocalPlayer)
        {
            _playerInputHandler = GetComponent<PlayerInput>();
            print(_playerInputHandler);
            _playerDamageHandler = GetComponent<PlayerDamageHandler>();
            print(_playerDamageHandler);
            _playerAiming = GetComponent<PlayerAiming>();
            
            SpawnPlayerObjectRPC();
            
        }
    }

    [Rpc(SendTo.Server)]
    void SpawnPlayerObjectRPC()
    {
        NetworkObject AssignedPlayer = Instantiate(_playerObject).GetComponent<NetworkObject>();
        AssignedPlayer.Spawn();
        FindPlayerRPC(AssignedPlayer.NetworkObjectId);
        
    }

    [Rpc(SendTo.Everyone)]
    void FindPlayerRPC(ulong ID)
    {
        _assignedPlayer = NetworkManager.SpawnManager.SpawnedObjects[ID];
        
        print(_assignedPlayer);
        _playerInputHandler.SetValuesRPC(ID);
        _playerDamageHandler.SetValuesRPC(ID);
        _playerAiming.SetValuesRPC(ID);
    }
}
