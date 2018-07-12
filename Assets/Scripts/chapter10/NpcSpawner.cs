using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NpcSpawner : MonoBehaviour
{

    // 生成するPrefab
    [SerializeField] GameObject m_NpcPrefab;

    // 生成する間隔(秒)
    [SerializeField] float m_SpawnInterval = 3f;

    // 最後に生成した時間
    float m_LastSpawnTime = float.MinValue;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 今の時間が最後にSpawnしてからInterval分以上経過している場合
        if (Time.time >= m_LastSpawnTime + m_SpawnInterval)
        {
            // サーバー内でInstantiate
            GameObject npc = Instantiate(m_NpcPrefab);

            // 位置をランダムに指定
            // 高さは０
            npc.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            // Spawnして全クライアント上で生成
            NetworkServer.Spawn(npc);

            // 最後に生成した時間を記録
            m_LastSpawnTime = Time.time;
        }

    }
}