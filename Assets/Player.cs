using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// NetworkBehaviourの継承
public class Player : NetworkBehaviour {

    Text m_ChatHistory;
    InputField m_ChatInputField;
	// Use this for initialization
	void Start () {
        m_ChatHistory = GameObject.Find("ChatHistory").GetComponent<Text>();
	}

    // ローカルプレイヤーが初期化される際に呼ばれる
    public override void OnStartLocalPlayer()
    {
        m_ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update () {
        // 他のプレイヤーには何も行わない
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            // 文字列が入力されている場合
            if(m_ChatInputField.text.Length > 0)
            {
                // Commandを使って文字列をサーバーへ送信
                CmdPost(m_ChatInputField.text);

                // ブランクを空にする処理
                m_ChatInputField.text = "";
            }
        }
	}

    // 文字列をサーバーへ送信
    [Command]
    void CmdPost(string text)
    {
        RpcPost(text);
    }

    // ClientRpcは各クライアントで実行される
    [ClientRpc]
    void RpcPost(string text)
    {
        // ChatHistoryに文字列を追加
        m_ChatHistory.text += (text + System.Environment.NewLine);
        Debug.Log(m_ChatHistory.text);
    }
}
