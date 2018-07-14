using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
public class MessageTest : MonoBehaviour
{

    // 他のプログラムと被らないようにする
    const int Port = 7777;

    NetworkClient client;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //1キーでサーバーの機能が起動し、ネットワーク越しの通信を待ち受けます。
        //2キーでHandler（イベント受信時に呼ばれる関数）を登録します。
        //3キーでサーバーからクライアントに向けてメッセージHogeを送信します。
        //4キーでサーバーからクライアントに向けてメッセージFugaを送信します。


        //Qキーでクライアントのインスタンスを生成します。
        //Wキーでクライアントの接続状態をログに出します。
        //Eキーでサーバーに接続します（今回は自身のIPアドレスを決め打ちしています。ここを変えれば、別のマシンに接続します）
        //RキーでHandlerを登録します。
        //Tキーでクライアントからサーバーに向けてメッセージHogeを送信します。
        //Yキーでクライアントからサーバーに向けてメッセージFugaを送信します。

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print("サーバー起動");
            NetworkServer.Listen(Port);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // MessageIDに紐づいた関数を登録
            // Handlerで登録するメソッドは引数にNetworkMessageを取る必要あり
            print("Handlerを登録");
            NetworkServer.RegisterHandler(MyMsg.Hoge, OnHogeReceived);
            NetworkServer.RegisterHandler(MyMsg.Fuga, OnFugaReceived);
            NetworkServer.RegisterHandler(MyMsg.Custom, OnCustomMessageRecerved);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            print("サーバーから全クライアントへMsgHogeを送信");
            NetworkServer.SendToAll(MyMsg.Hoge, new EmptyMessage());

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            print("サーバーから全クライアントへMsgFugaを送信");
            NetworkServer.SendToAll(MyMsg.Fuga, new EmptyMessage());

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            print("サーバーから全クライアントへ123を送信");
            NetworkServer.SendToAll(MyMsg.Int, new IntegerMessage(123));
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            print("サーバーから全クライアントへ文字列を送信");
            NetworkServer.SendToAll(MyMsg.String, new StringMessage("こんにちは"));
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            print("クライアント生成");
            client = new NetworkClient();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            print("client.isConnected:" + client.isConnected);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            print("クライアントをサーバーへ接続");
            client.Connect("127.0.0.1", Port);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            print("Handlerを登録");
            client.RegisterHandler(MyMsg.Hoge, OnHogeReceived);
            client.RegisterHandler(MyMsg.Fuga, OnFugaReceived);
            client.RegisterHandler(MyMsg.Int, OnIntegerMessageReceived);
            client.RegisterHandler(MyMsg.String, OnStringMessageReceived);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            print("クライアントからサーバーへMsgHogeを送信");
            client.Send(MyMsg.Hoge, new EmptyMessage());
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            print("クライアントからサーバーへMsgFugaを送信");
            client.Send(MyMsg.Fuga, new EmptyMessage());
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            print("クライアントからサーバーへCustomMessageを送信");
            CustomMessage m = new CustomMessage();
            m.intValue = 1234;
            m.floatValue = 567.8f;
            m.stringValue = "あいうえお";
            m.vector3Value = new Vector3(1, 2, 3);
            client.Send(MyMsg.Custom, m);
        }


    }

    private void OnCustomMessageRecerved(NetworkMessage netMsg)
    {
        CustomMessage m = netMsg.ReadMessage<CustomMessage>();

        Debug.LogFormat("CustomMessageを受信: {0}, {1}, {2}, {3}", m.intValue, m.floatValue, m.stringValue, m.vector3Value);
    }

    private void OnStringMessageReceived(NetworkMessage netMsg)
    {
        print("netMsg: " + netMsg);
        string value = netMsg.ReadMessage<StringMessage>().value;

        print("StringMessageを受信: " + value);
    }

    private void OnIntegerMessageReceived(NetworkMessage netMsg)
    {
        print("netMsg: " + netMsg);
        int value = netMsg.ReadMessage<IntegerMessage>().value;

        print("IntegerMessageを受信: " + value);
    }

    private void OnFugaReceived(NetworkMessage netMsg)
    {
        print("netMsg: " + netMsg);
        print("MsgFugaを受信");
    }

    private void OnHogeReceived(NetworkMessage netMsg)
    {
        print("netMsg: " + netMsg);
        print("MsgHogeを受信");
    }


    // sendで送るためにMessageBaseを継承
    class CustomMessage : MessageBase
    {
        internal int intValue;
        internal float floatValue;
        internal string stringValue;
        internal Vector3 vector3Value;
    }
}