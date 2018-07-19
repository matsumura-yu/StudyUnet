using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManagerHUD : MonoBehaviour {

    GameObject m_MainUIs;

    GameObject m_ConnectingText;

    enum ConnectionState
    {
        Server,
        Host,
        RemoteClientConnected,
        RemoteClientConnecting,
        Nothing,
    }

    ConnectionState GetConnectionState()
    {
        // サーバーが起動している場合
        if (NetworkServer.active)
        {
            // クライアントとして接続している場合
            if (NetworkManager.singleton.IsClientConnected())
            {
                // ホスト
                return ConnectionState.Host;
            }
            else
            {
                return ConnectionState.Server;
            }
            // クライアントとして接続している場合
        }
        else if (NetworkManager.singleton.IsClientConnected())
        {
            return ConnectionState.RemoteClientConnected;
        }
        else
        {
            NetworkClient client = NetworkManager.singleton.client;

            if(client != null && client.connection != null && client.connection.connectionId != -1)
            {
                return ConnectionState.RemoteClientConnecting;
            }
            else
            {
                return ConnectionState.Nothing;
            }
        }

    }
	// Use this for initialization
	void Start () {
        m_MainUIs = GameObject.Find("MainUIs");
        m_ConnectingText = GameObject.Find("ConnectingText");
	}
	
	// Update is called once per frame
	void Update () {
        ConnectionState state = GetConnectionState();

        if(state == ConnectionState.RemoteClientConnecting)
        {
            m_MainUIs.SetActive(false);
            m_ConnectingText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                NetworkManager.singleton.StopHost();
            }
            
        }
        else
        {
            m_MainUIs.SetActive(true);
            m_ConnectingText.SetActive(false);
        }
    }

    public void OnServerButtonClicked()
    {
        NetworkManager.singleton.StartServer();
    }

    public void OnHostButtonClicked()
    {
        NetworkManager.singleton.StartHost();
    }

    public void OnClientButtonClicked()
    {
        InputField input = GameObject.Find("ServerAddressInputField").GetComponent<InputField>();
        NetworkManager.singleton.networkAddress = input.text;
        NetworkManager.singleton.StartClient();
    }
}
