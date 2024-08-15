using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerShooting : NetworkBehaviour
{
    private LineRenderer _lineRenderer;

    private bool _lineRenderSet = false;

    [SerializeField] private GameObject bullet;
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderSet = true;
    }

    [Rpc(SendTo.Everyone)]
    public void UpdateLineRPC(Vector2 aimPos)
    {
        if (_lineRenderSet == false)
        {
            return;
        }
        //aimPos -= (Vector2)transform.position;
        
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, aimPos);
    }

    [Rpc(SendTo.Server)]
    public void SpawnBulletRPC(Vector2 direction)
    {
        NetworkObject bulletObj = Instantiate(bullet).GetComponent<NetworkObject>();

        bulletObj.transform.position = transform.position;

        direction -= (Vector2)transform.position;

        float angle = MathF.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
        
        bulletObj.transform.Rotate(0,0,angle - 90);
        
        bulletObj.Spawn();
    }
}
