using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

public class NetworkManager : MonoBehaviour
{
    private GameObject networkManager;
	private ChatManager chatManager;
	private TcpClient clientSocket;
	private NetworkStream serverStream;
	private Thread receiveThread;
	private bool socketReady = false;
	private byte[] buffer = new byte[1024];
    
    // Start is called before the first frame update
    void Start()
    {
		chatManager = GameObject.Find("MenuController").GetComponent<ChatManager>();
        try{
			clientSocket = new TcpClient (Constants.REMOTE_HOST, Constants.REMOTE_PORT);
			serverStream = clientSocket.GetStream();
			Debug.Log("Connected");
		}
		// receiveThread = new Thread(new ThreadStart(ReceiveData));
        // receiveThread.Start();
		catch (SocketException e) {
        	Debug.LogError("SocketException: " + e);
    	}
    	catch (Exception e) {
    	    Debug.LogError("Exception: " + e);
    	}
    }

    void Update()
    {
		if (serverStream != null && serverStream.DataAvailable)
        {
            string message = ReceiveMessage();
			chatManager.UpdateChatOutput(message);
            Debug.Log("Received message from server: " + message);
        }
	}

	public void send(string msg){
		byte[] bytes = Encoding.UTF8.GetBytes(msg+"\n");
		serverStream.Write(bytes, 0, bytes.Length);

		Debug.LogFormat("Message '{0}' send to server successfully!", msg);
	}

	private string ReceiveMessage()
    {
        byte[] data = new byte[1024];
        int bytes = serverStream.Read(data, 0, data.Length);
        string message = Encoding.ASCII.GetString(data, 0, bytes);
        return message;
    }


}
