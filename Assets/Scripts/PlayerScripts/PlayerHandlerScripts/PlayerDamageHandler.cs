using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageHandler : NetworkBehaviour
{
    private PlayerDamageScript _damageScript;
    
    [Rpc(SendTo.Everyone)]
    public void SetValuesRPC(ulong ID)
    {
        _damageScript = NetworkManager.SpawnManager.SpawnedObjects[ID].GetComponent<PlayerDamageScript>();
        
        _damageScript.setHandler(this);
    }
    

    [Rpc(SendTo.Everyone)]
    public void EndGameRPC()
    {
        NetworkManager.Shutdown();
        Destroy(NetworkManager.gameObject); //it is important that the network manager is destroyed before the menu is loaded, otherwise the singleton will stop working
        SceneManager.LoadScene(0);
    }
}
