using UnityEngine;
using UnityEngine.UI;

namespace PSM100.SNL
{
    public class SNL_GameManager : MonoBehaviour
    {
        public Text fpstext;
        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(test), 0, 1);
        }

        // Update is called once per frame
        void test()
        {
            fpstext.text = "" + (int)(1f / Time.unscaledDeltaTime);
        }
    }
}