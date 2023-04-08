using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

namespace PSM100.SNL
{
    public class SNL_BoardController : MonoBehaviour
    {
        public int boardSize;
        public float spaceBetweenTiles;

        [SerializeField] private SNL_TileDetails tilePrefab;
        [SerializeField] private Transform tilePrefabParent, referenceParent;

        [SerializeField] private Sprite oddTileBG, evenTileBG;

        [SerializeField] private List<int> referenceNumberList;
        public List<SNL_TileDetails> tileList;
        private void TileSizeSetAsPerScreenWidth(Transform _tilesPrefabParent)
        {
            float refWidth = referenceParent.GetComponent<RectTransform>().rect.width;
            referenceParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, refWidth);
            float width = _tilesPrefabParent.GetComponent<RectTransform>().rect.width;
            float height = _tilesPrefabParent.GetComponent<RectTransform>().rect.height;

            int tileSizeX = (int)(width - (spaceBetweenTiles * (boardSize + 1))) / boardSize;
            int tileSizeY = (int)(height - (spaceBetweenTiles * (boardSize + 1))) / boardSize;

            _tilesPrefabParent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(Mathf.Min(tileSizeX, tileSizeY), Mathf.Min(tileSizeX, tileSizeY));
        }

        private void Start()
        {
            BoardGenerate();
        }
        private void BoardGenerate()
        {
            TileSizeSetAsPerScreenWidth(tilePrefabParent);
            int referenceIndex = 0;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    SNL_TileDetails tileClone = Instantiate(tilePrefab, tilePrefabParent);
                    tileList.Add(tileClone);
                    tileClone.gameObject.name = "" + referenceNumberList[referenceIndex];
                    tileClone.tileBG.sprite = ((i + j) % 2).Equals(0) ? evenTileBG : oddTileBG;
                    tileClone.numberText.text = "" + referenceNumberList[referenceIndex];
                    tileClone.tileDetail.tileNumber = referenceNumberList[referenceIndex];
                    referenceIndex++;
                }
            }
            tileList.Sort((a, b) => a.tileDetail.tileNumber.CompareTo(b.tileDetail.tileNumber));
        }
    }
}
