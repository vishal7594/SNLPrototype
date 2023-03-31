using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PSM100.SNL
{
    public class SNL_TileDetails : MonoBehaviour
    {
        public Image tileBG;
        public TMP_Text numberText;
        public GameObject winTrophy;

        
        [System.Serializable]
        public struct TopArrow
        {
            public GameObject rightArrow;
            public GameObject leftArrow;
        }

        public TopArrow topArrow;


        public TileDetail tileDetail;
        [System.Serializable]
        public struct TileDetail
        {
            public int tileNumber;
        }
    }
}