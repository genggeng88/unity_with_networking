using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : MonoBehaviour {

    public TMP_InputField chatInput;
    public TMP_Text chatOutput;
    public Button sendButton;
    public NetworkManager nManager;

    private string userName = "Player";
    private List<string> chatLog = new List<string>();

    void Start() {
        nManager = GameObject.Find("NetworkController").GetComponent<NetworkManager>();
        chatInput = GameObject.Find("msgInput").GetComponent<TMP_InputField>();
        chatOutput = GameObject.Find("msgDisplay").GetComponent<TMP_Text>();
        sendButton = GameObject.Find("msgEnter").GetComponentInChildren<Button>();

        Debug.Log("chatInput got name: " + chatInput.name);
        Debug.Log("chatOutput got name: " + chatOutput.name);
        Debug.Log("sendButton got name: " + sendButton.name);
    }

    void Update() {
        sendButton.onClick.AddListener (delegate { OnSendButton (); });
        chatInput.onEndEdit.AddListener (delegate { OnChatInputEnd (); });
    }

    void OnSendButton() {
        if (chatInput.text.Length > 0) {
            string message = userName + ": " + chatInput.text;
            nManager.send(message);
            chatInput.text = "";
        }
    }

    void OnChatInputEnd () {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            OnSendButton ();
        }
    }

    public void UpdateChatOutput (string message) {
        chatLog.Add (message);
        chatOutput.text = "";
        for (int i = 0; i < chatLog.Count; i++) {
            chatOutput.text += chatLog[i] + "\n";
        }
    }
}
