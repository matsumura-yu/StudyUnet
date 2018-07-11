using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player9 : NetworkBehaviour {

    // 全体で同期したいデータ
    [SyncVar]
    int m_Count = 0;

    // 表示に使うもの
    TextMesh m_CountText;
	// Use this for initialization
	void Start () {
        // 子のオブジェクトのコンポーネントを取得
        m_CountText = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        // ローカルプレイヤーのみ処理
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CmdCountUp();
            }
        }

        m_CountText.text = m_Count.ToString();
	}

    // SyncVarはサーバーからクライアントへの一方向同期
    // サーバーのデータを変更するようにCommandで書き換える
    [Command]
    void CmdCountUp()
    {
        m_Count++;
    }
}
