using System;
using UnityEngine;
namespace PSM100.SNL
{
    public class SNL_SocketEventReceiver : MonoBehaviour
    {
        public static Action<string> SocketEventCallbackAction;

        private void OnEnable()
        {
            SocketEventCallbackAction += OnSocketEventreceiver;
        }

        private void OnDisable()
        {
            SocketEventCallbackAction -= OnSocketEventreceiver;

        }
        private void OnSocketEventreceiver(string responseData)
        {
            Debug.Log("Response data => " + responseData);
        }
    }
}