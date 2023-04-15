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

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void PlayerSetOnTable()
        {

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

}