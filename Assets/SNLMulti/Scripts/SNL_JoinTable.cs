using System.Collections.Generic;
using UnityEngine;
namespace PSM100.SNL
{
    public class SNL_JoinTable : MonoBehaviour
    {

        public JoinTable joinTable;
        public List<SNL_PlayerDetails> playerInfoList;


        // Start is called before the first frame update
        void Start()
        {
            PlayerSetOnTable(joinTable);
        }

        private void PlayerSetOnTable(JoinTable joinTableData)
        {
            int mySeatIndex = joinTableData.playerDetails.Find((player) => player.isMyPlayer == true).seatIndex;
            List<int> updateIndexList = new List<int>();

            int refe = mySeatIndex;
            for (int i = 0; i < 6; i++)
            {
                updateIndexList.Add(refe);
                refe = (refe + 1 > 5) ? 0 : refe + 1;
            }
            List<SNL_PlayerDetails> refplayerInfoList = new List<SNL_PlayerDetails>();
            refplayerInfoList.AddRange(playerInfoList);

            playerInfoList = new List<SNL_PlayerDetails>();
            for (int i = 0; i < refplayerInfoList.Count; i++)
            {
                SNL_PlayerDetails store = refplayerInfoList[updateIndexList.IndexOf(i)];
                playerInfoList.Insert(i, store);
            }

            for (int i = 0; i < joinTableData.playerDetails.Count; i++)
            {
                playerInfoList[joinTable.playerDetails[i].seatIndex].PlayerDetailsDataSet(joinTableData.playerDetails[i]);
            }

        }
    }
}

[System.Serializable]
public class JoinTable
{
    public List<PlayerDetails> playerDetails;

}
[System.Serializable]
public class PlayerDetails
{
    public int seatIndex;
    public string userName;
    public float chipsAmount;
    public bool isMyPlayer;
}