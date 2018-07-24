using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class MyNetworkManager : NetworkManager {

    MyNetworkDiscovery m_NetworkDiscovery;
	// Use this for initialization
	void Start () {
        m_NetworkDiscovery = GetComponent<MyNetworkDiscovery>();
	}

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,250,20), "ホストとして起動"))
        {
            StartHost();
            m_NetworkDiscovery.Initialize();
            m_NetworkDiscovery.StartAsServer();
        }

        if(GUI.Button(new Rect(0,30,250,20), "クライアントとして接続"))
        {
            m_NetworkDiscovery.Initialize();
            m_NetworkDiscovery.StartAsClient();
        }
    }

    public override void OnStopServer()
    {
        m_NetworkDiscovery.StopBroadcast();
    }
}
