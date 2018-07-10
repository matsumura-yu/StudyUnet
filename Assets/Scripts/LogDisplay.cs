using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LogDisplay : MonoBehaviour {

    // ログの最大個数
    [SerializeField] int m_MaxLogCount = 20;

    // 表示領域
    [SerializeField] Rect m_Area = new Rect(220, 0, 400, 400);

    // ログの文字列を入れておくQueue
    Queue<string> m_LogMessages = new Queue<string>();

    // 文字列の結合
    StringBuilder m_StringBuilder = new StringBuilder();

    // Use this for initialization

    private void Start()
    {
        Application.logMessageReceived += LogReceived;
    }


    // ログ出力時に呼んでもらう関数
    void LogReceived(string text, string stackTrace, LogType type)
    {
        m_LogMessages.Enqueue(text);

        // ログの個数が上限を超えたら最古の物を削除する
        while(m_LogMessages.Count > m_MaxLogCount)
        {
            m_LogMessages.Dequeue();
        }
    }

    void OnGUI()
    {
        // StringBuilderの内容をリセット
        m_StringBuilder.Length = 0;

        foreach(string s in m_LogMessages)
        {
            m_StringBuilder.Append(s).Append(System.Environment.NewLine);
        }

        // 画面に表示
        GUI.Label(m_Area, m_StringBuilder.ToString());
    }
}
