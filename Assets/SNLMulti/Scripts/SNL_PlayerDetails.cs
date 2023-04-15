using TMPro;
using UnityEngine;

public class SNL_PlayerDetails : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private int seatIndex;
    [SerializeField] private float chipsAmount;
    [SerializeField] private bool isMyPlayer;

    public void PlayerDetailsDataSet(PlayerDetails playerDetail)
    {
        userNameText.text = playerDetail.userName;
        seatIndex = playerDetail.seatIndex;
        chipsAmount = playerDetail.chipsAmount;
        isMyPlayer = playerDetail.isMyPlayer;

        gameObject.name = playerDetail.userName;
    }
}
