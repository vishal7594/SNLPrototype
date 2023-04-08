using System;
using UnityEngine;

public class SNL_SignUpController : MonoBehaviour
{

    public static Action SignUpSendAction;

    private void OnEnable()
    {
        SignUpSendAction += SignUpSend;
    }

    private void OnDisable()
    {
        SignUpSendAction -= SignUpSend;
    }

    public void SignUpSend()
    {
        Debug.Log("SignUpSend");

        // SignUpEvent signUpEvent = new SignUpEvent();

        // signUpEvent.en = "SIGNUP";
        SignUpData data = new SignUpData();
        data.userId = "1";
        data.userName = "vishal";
        data.userProfile = "No";
        data.mobileNumber = 1234567890;
        data.chips = 10;
        data.token = "1";
        data.bootValue = 1;

        // signUpEvent.data = data;
        SNL_SocketConnection.SendDataToSocketAction(JsonUtility.ToJson(data), OnCallBackSingUp, CustomEvents.SIGNUP.ToString());
    }

    private void OnCallBackSingUp(string response)
    {
        Debug.Log("sign Up " + response);
    }

}
[System.Serializable]
public class SignUpData
{
    public string userId;
    public string userName;
    public string userProfile;
    public int mobileNumber;
    public int chips;
    public string token;
    public int bootValue;

}
[System.Serializable]
public class SignUpEvent
{
    public string en;
    public SignUpData data;
}


