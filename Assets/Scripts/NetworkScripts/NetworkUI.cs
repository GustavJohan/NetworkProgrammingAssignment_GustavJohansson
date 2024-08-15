using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkUI : MonoBehaviour
{
    private NetworkManager NetworkManager;

    private void Start()
    {
            NetworkManager = GetComponent<NetworkManager>();
    }

    public void Host()
    {
        SceneManager.LoadScene(1);


        SceneManager.sceneLoaded += startHosting;
    }

    private void startHosting(Scene scene, LoadSceneMode mode)
    {
        NetworkManager.StartHost();
        SceneManager.sceneLoaded -= startHosting;
    }

    public void Join()
    {
        SceneManager.LoadScene(1);
        SceneManager.sceneLoaded += startJoining;
    }

    void startJoining(Scene scene, LoadSceneMode mode)
    {
        NetworkManager.StartClient();
        SceneManager.sceneLoaded -= startJoining;
    }

    
}
