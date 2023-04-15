using TMPro;
using UnityEngine;

public class SNL_PlayerDetails : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI userNameText;
    [SerializeField] private int seatIndex;
    [SerializeField] private float chipsAmount;

    private void  PlayerDetailsDataSet(PlayerDetails playerDetail)
    {
        userNameText.text = playerDetail.userName;
        seatIndex = playerDetail.seatIndex;
        chipsAmount = playerDetail.chipsAmount;
    }
}
