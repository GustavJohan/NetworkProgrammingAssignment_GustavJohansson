using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class PlayerAiming : NetworkBehaviour
{
    private PlayerShooting _playerShooting;

    public bool IsActive = false;

    private Camera _camera;

    [Rpc(SendTo.Everyone)]
    public void SetValuesRPC(ulong ID)
    {
        _playerShooting = NetworkManager.SpawnManager.SpawnedObjects[ID].GetComponent<PlayerShooting>();

        IsActive = true;
        
        _camera = Camera.main;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && IsLocalPlayer)
        {
            _playerShooting.UpdateLineRPC(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -100))*-1);

            if (Input.GetMouseButtonDown(0))
            {
                _playerShooting.SpawnBulletRPC(_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -100))*-1);
            }
        }
        
    }
}
