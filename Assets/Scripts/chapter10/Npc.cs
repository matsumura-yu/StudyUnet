using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Npc : NetworkBehaviour {
    // 移動速度
    [SerializeField] float m_Speed = 3f;

    Vector3 m_Desitnation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // サーバー以外の処理
        // NPCはサーバーのみで一括管理したい
        // クライアントでも動かすと目的地を両方で算出されてかくかくする
        if (!isServer)
        {
            return;
        }

        // 目的地の方に回転
        transform.LookAt(m_Desitnation);

        // 移動距離
        float moveDistance = m_Speed * Time.deltaTime;

        // 目的地までの距離を計算
        float distanceToDestination = Vector3.Distance(transform.position, m_Desitnation);

        // 到着した場合の処理
        if (distanceToDestination <= moveDistance)
        {
            if(Random.value < 0.5f) {
                // 全端末で一斉削除
                NetworkServer.Destroy(gameObject);
                return;
            }

            // 目的地に移動
            transform.position = m_Desitnation;

            // ランダムで新しい目的地を決定
            // 高さは前と一緒
            m_Desitnation = new Vector3(Random.Range(-5f, 5f), transform.position.y, Random.Range(-5f, 5f));

        }
        else
        {
            // 到着してない場合の処理
            // 移動
            transform.Translate(Vector3.forward * moveDistance);
        }
	}
}
