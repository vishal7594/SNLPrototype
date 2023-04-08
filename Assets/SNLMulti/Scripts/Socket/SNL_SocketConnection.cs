using BestHTTP.SocketIO3;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SNL_SocketConnection : MonoBehaviour
{
    public SocketState socketState;
    public ServerMode serverMode;
    public List<string> socketURL;
    public SocketManager socketManager;
    public static Action<string, Action<string>, string> SendDataToSocketAction;


    private void OnEnable()
    {
        SendDataToSocketAction += SendDataToSocket;
    }

    private void Start()
    {
        SocketInitiated();
    }

    private void SocketInitiated()
    {
        InvokeRepeating(nameof(InternetCheck), 0, 2f);
    }

    private void InternetCheck()
    {
        if (IsInternetConnectionAvailable())
        {
            if (socketState == SocketState.Disconnect || socketState == SocketState.Error || socketState == SocketState.None)
            {
                SocketConnet(socketURL[(int)serverMode]);
            }
        }
        else
        {
            NoInternetIssue();
        }
    }

    private void NoInternetIssue()
    {
        if (socketState != SocketState.None)
        {
            ForcefullyCloseSocket();
        }
    }

    internal bool IsInternetConnectionAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }


    private void SocketConnet(string socketURL)
    {
        Debug.Log("Socket Connecting...");
        socketState = SocketState.Connecting;
        socketURL += "/socket.io/";
        Debug.Log("socketURL || " + socketURL);

        socketManager = new SocketManager(new Uri(socketURL), OverridedSocketOptions());

        socketManager.Socket.On(SocketIOEventTypes.Connect, SocketConnected);

        socketManager.Socket.On(SocketIOEventTypes.Disconnect, SocketDisconnected);

        socketManager.Socket.On<Error>(SocketIOEventTypes.Error, (errorData) =>
        {
            SocketError(errorData);
        });

        RegisterCustomEvents();

    }

    private void RegisterCustomEvents()
    {
        CustomEvents[] events = Enum.GetValues(typeof(CustomEvents)) as CustomEvents[];

        for (int i = 0; i < events.Length; i++)
        {
            string en = events[i].ToString();

            socketManager.Socket.On<Socket, string>(en, (socket, response) =>
            {
                Debug.Log("Received Event => <color=cyan>" + en + "</color>");
                SNL_SocketEventReceiver.SocketEventCallbackAction(response);
            });
        }

    }

    private void SendDataToSocket(string data, Action<string> OnCallBack, string eventName)
    {
        Debug.Log("<color=red> <<< SEND >>>  </color><color=white> " + eventName + " </color> \n" + data + " << :: SendDataToServerEnd >>> ");
        //socketManager.Socket.Emit(eventName, OnCallBack, data.ToString());
        socketManager.Socket.ExpectAcknowledgement<string>(OnCallBack).Volatile().Emit(eventName, data.ToString());
    }


    private void SocketError(Error errordata)
    {
        Debug.Log(errordata);
    }

    private void SocketDisconnected()
    {
        Debug.Log("Socket Disconnected");
        socketState = SocketState.Disconnect;
    }

    private void SocketConnected()
    {
        Debug.Log("Socket Connected");
        socketState = SocketState.Open;

        SNL_SignUpController.SignUpSendAction?.Invoke();

    }

    internal void ForcefullyCloseSocket()
    {
        Debug.Log("<<< Socket_Connection :: Socket Disconnected Forcefully >>>");
        socketState = SocketState.Disconnect;
        socketManager.Socket.Disconnect();
    }
    private SocketOptions OverridedSocketOptions()
    {
        SocketOptions socketOption = new SocketOptions();
        socketOption.ConnectWith = BestHTTP.SocketIO3.Transports.TransportTypes.WebSocket;
        socketOption.Reconnection = false;
        socketOption.ReconnectionAttempts = int.MaxValue;
        socketOption.ReconnectionDelay = TimeSpan.FromMilliseconds(1000);
        socketOption.ReconnectionDelayMax = TimeSpan.FromMilliseconds(5000);
        socketOption.RandomizationFactor = 0.5f;
        socketOption.Timeout = TimeSpan.FromMilliseconds(3000);
        socketOption.AutoConnect = true;
        socketOption.QueryParamsOnlyForHandshake = true;
        return socketOption;
    }
}


