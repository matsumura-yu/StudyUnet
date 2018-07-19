using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player12 : NetworkBehaviour {

	// Use this for initialization
		// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NetworkManager.singleton.StopHost();
        }
	}
}
