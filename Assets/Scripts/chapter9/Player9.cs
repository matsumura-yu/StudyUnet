using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player9 : NetworkBehaviour {

    // 全体で同期したいデータ
    [SyncVar(hook = "HookCountChanged")]
    int m_Count = 0;

    // 表示に使うもの
    TextMesh m_CountText;
	// Use this for initialization
	void Start () {
        // 子のオブジェクトのコンポーネントを取得
        m_CountText = GetComponentInChildren<TextMesh>();

        // SyncVarで一斉同期されてるが見た目の初期化を忘れがち
        m_CountText.text = m_Count.ToString();
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

        // SyncVar変数が変更されてないのに描写を書き換えるという無駄な処理になっている
        // m_CountText.text = m_Count.ToString();
	}

    // SyncVarはサーバーからクライアントへの一方向同期
    // サーバーのデータを変更するようにCommandで書き換える
    [Command]
    void CmdCountUp()
    {
        m_Count++;
    }

    // hook関数は戻り値なし引数はSyncVarと揃える
    void HookCountChanged(int newValue)
    {
        // hookすると引数に新しい値が入ってくる
        // 自動で変更してくれなくなるので自分で値を代入
        m_Count = newValue;

        // 変更されたときだけしてほしい処理を書く
        m_CountText.text = m_Count.ToString();
    }
}
