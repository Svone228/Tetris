using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NextFigureScript : MonoBehaviour
{
    public GameObject Tile;
    public Vector2Int FieldSize;
    public GameObject Field;
    List<GameObject> tiles;
    GameObject[][] TileMatrix;
    public Color color;
    // Start is called before the first frame update
    private void Awake()
    {
        CreateTileMatrix();
        tiles = new List<GameObject>();
        CreateField();
        SetFieldColor();
    }
    void CreateTileMatrix() 
    {
        TileMatrix = new GameObject[FieldSize.x][];
        for (int i = 0; i < FieldSize.x; i++)
        {
            TileMatrix[i] = new GameObject[FieldSize.y];
        }
    }
    void CreateField()
    {
        for (int i = 0; i < FieldSize.x; i++)
        {
            for (int j = 0; j < FieldSize.y; j++)
            {
                var temp = Instantiate(Tile,Field.transform);
                tiles.Add(temp);
                temp.transform.GetChild(0).gameObject.GetComponent<Text>().text=i + " " + j;
                TileMatrix[i][j] = temp;
            }
        }
    }
    void SetFieldColor() 
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].GetComponent<Image>().color = color;   
        }
    }
    public void FigureShow(Figure figure) 
    {
        SetFieldColor();
        for (int i = 0; i < figure.Tiles.Count; i++)
        {
            Vector2Int position = figure.Tiles[i].position - figure.offset;
            TileMatrix[position.x][position.y].GetComponent<Image>().color = figure.Tiles[i].color;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
