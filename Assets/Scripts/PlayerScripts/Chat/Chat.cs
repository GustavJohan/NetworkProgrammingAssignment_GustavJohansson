using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Chat : NetworkBehaviour
{
    private TMP_InputField _chatBox;

    [SerializeField] private TMP_Text chatLog;
    // Start is called before the first frame update
    void Start()
    {
        _chatBox = GetComponent<TMP_InputField>();
    }

    public void SendChatMessage()
    {
        if (System.Text.ASCIIEncoding.ASCII.GetByteCount(_chatBox.text) < 128)
        {
            UpdateChatlogRPC(new FixedString128Bytes(_chatBox.text), (int)NetworkManager.LocalClientId);
        }
        else
        {
            UpdateChatlogRPC(new FixedString128Bytes("message to large"), (int)NetworkManager.LocalClientId);
        }
        
        
        _chatBox.text = String.Empty;

    }

    [Rpc(SendTo.Everyone)]
    void UpdateChatlogRPC(FixedString128Bytes message, int playerID)
    {
        chatLog.text += "\n" + "Player " + playerID + ": " + message;
    }
}
