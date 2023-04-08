using UnityEngine;
namespace PSM100.SNL
{
    public class SNL_SetObjectSize : MonoBehaviour
    {
        public RectTransform referenceBG;
        public RectTransform boardBG;

        private RectTransform myImage;

        public SNL_BoardController boardController;

        private void Awake()
        {
            myImage = GetComponent<RectTransform>();
        }
        private void Start()
        {
            Invoke(nameof(Test), 2f);
        }


        private void Test()
        {

            float refWidth = referenceBG.rect.width;
            float refHeight = referenceBG.rect.height;

            Debug.Log(referenceBG.rect.width + " || " + referenceBG.rect.height);
            Debug.Log(boardBG.rect.width + " || " + boardBG.rect.height);
            Debug.Log(myImage.rect.width + " || " + myImage.rect.height);

            float prefWidth = myImage.rect.width;
            float prefHeight = myImage.rect.height;

            float myWidth = (prefWidth * boardBG.rect.width) / 1080;
            float myHeight = (prefHeight * boardBG.rect.height) / 1080;

            Debug.Log(myWidth + " || " + myHeight);
            myImage.sizeDelta = new Vector2(myWidth, myHeight);

           // myImage.transform.SetParent(boardController.tileList[2].transform.parent);
            //myImage.transform.position = boardController.tileList[2].transform.position;
        }
    }
}
