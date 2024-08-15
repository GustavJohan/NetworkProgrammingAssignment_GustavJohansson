using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerDamageScript : MonoBehaviour
{
    private PlayerDamageHandler _damageHandler;
    
    private int Health = 3;

    public void setHandler(PlayerDamageHandler handler)
    {
        _damageHandler = handler;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("Ouch");
            DamageTaken();
            other.gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }
    
    public void DamageTaken()
    {
        Health--;

        if (Health < 0)
        {
            /*_damageScript.GetComponent<NetworkObject>().Despawn(true);
            GetComponent<PlayerInput>().IsActive = false;
            GetComponent<PlayerAiming>().IsActive = false;*/
            _damageHandler.EndGameRPC();
        }
        
        print("Player nr: " + _damageHandler.OwnerClientId + " Has takenDamage");
    }
}
