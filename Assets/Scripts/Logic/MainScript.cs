    using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainScript : MonoBehaviour
{
    public GameObject cell;
    GameObject[][] CellMatrix;
    List<FigureTile> Tiles;
    Figure CurrentFigure;
    FigureTile[][] TilesMatrix;
    int matrixSize_y = 20;
    int matrixSize_x = 10;
    public float gameTick;
    public float quickedGameTick;
    float currentGameTick = 0;
    float gmTick;
    public int tetrisScoreCount;
    Figure nextFigure;
    private void Awake()
    {
        Initialization();
    }

    void Initialization()
    {
        Tiles = new List<FigureTile>();
        CreateCellMatrix();
        CreateField();
        gmTick = gameTick;
        
    }
    void CreateField()
    {
        for (int j = 0; j < matrixSize_y; j++)
        {
            for (int i = 0; i < matrixSize_x; i++)
            {
                var temp = Instantiate(cell, transform);
                CellMatrix[j][i] = temp;
                SetCellColor(i, j);
                //SetNumber((int)i, (int)j);

            }

        }
    }

    void CreateCellMatrix()
    {

        CellMatrix = new GameObject[matrixSize_y][];
        for (int i = 0; i < CellMatrix.Length; i++)
        {
            CellMatrix[i] = new GameObject[matrixSize_x];
        }
    }
    void SetCellColor(float x, float y, Color color = default)
    {
        int i = (int)x,
            j = (int)y;
        if (color == default)
        {
            var result = FoundXYTile(i, j);
            if (result == null)
                CellMatrix[j][i].GetComponent<Image>().color = new Color((y + 1) / Mathf.Pow(matrixSize_x, 1 / 3), x / matrixSize_x, 0.2f);
            else
                CellMatrix[j][i].GetComponent<Image>().color = result.color;
        }
        else
        {
            CellMatrix[j][i].GetComponent<Image>().color = color;
        }
    }
    void UpdateColors()
    {
        SetFieldColor();
        SetTilesColor();
        SetFigureColor();
    }
    void SetFieldColor()
    {
        for (int j = 0; j < matrixSize_y; j++)
        {
            for (int i = 0; i < matrixSize_x; i++)
            {
                SetCellColor(i, j);
            }

        }
    }
    void SetTilesColor()
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            SetCellColor(Tiles[i].position.x, Tiles[i].position.y);
        }
    }
    void SetFigureColor()
    {
        for (int i = 0; i < CurrentFigure.Tiles.Count; i++)
        {
            SetCellColor(CurrentFigure.Tiles[i].position.x, CurrentFigure.Tiles[i].position.y, CurrentFigure.Tiles[i].color);
        }
    }
    FigureTile FoundXYTile(int i, int j)
    {
        for (int k = 0; k < Tiles.Count; k++)
        {
            if (Tiles[k].position == new Vector2Int(i, j))
            {
                return Tiles[k];
            }
        }
        return null;
    }
    void SetNumber(int x, int y)
    {
        int i = x, j = y;
        CellMatrix[j][i].transform.GetChild(0).gameObject.GetComponent<Text>().text = i + " " + j;
    }

    void CheckCoords(Figure figure)
    {
        if (ChechSideCollision(figure))
        {
            figure.GoToOldPosition();
        }
        if (CheckColision(figure))
        {
            figure.GoToOldPosition();
        }
        UpdateColors();
        
    }

    bool CheckColision(Figure figure)
    {
        for (int i = 0; i < figure.Tiles.Count; i++)
        {
            for (int j = 0; j < Tiles.Count; j++)
            {
                if (figure.Tiles[i].position == Tiles[j].position)
                {
                    return true;
                }
            }
        }
        for (int i = 0; i < figure.Tiles.Count; i++)
        {
            if (figure.Tiles[i].position.y >= matrixSize_y)
            {
                return true;
            }
        }
        return false;
    }
    bool ChechSideCollision(Figure figure)
    {
        for (int i = 0; i < figure.Tiles.Count; i++)
        {
            if (figure.Tiles[i].position.x >= matrixSize_x || figure.Tiles[i].position.x < 0)
            {
                return true;
            }
        }
        return false;
    }

    int FoundSideCollision(Figure figure)
    {
        int maxSize_x = matrixSize_x - 1;
        int max = 0;
        int min = 0;
        for (int i = 0; i < figure.Tiles.Count; i++)
        {
            if (figure.Tiles[i].position.x >= maxSize_x)
            {
                if (figure.Tiles[i].position.x - maxSize_x > max)
                    max = figure.Tiles[i].position.x - maxSize_x;
            }
            else if (figure.Tiles[i].position.x < 0)
            {
                if (figure.Tiles[i].position.x - 0 < min)
                    min = figure.Tiles[i].position.x - 0;
            }
        }
        if(Mathf.Abs(max)> Mathf.Abs(min))
            return max;
        else
            return min;
    }

    void NextFigure()
    {
        CurrentFigureToTiles();
        CreateNewFigure();
    }
    void CurrentFigureToTiles()
    {
        for (int i = 0; i < CurrentFigure.Tiles.Count; i++)
        {
            Tiles.Add(CurrentFigure.Tiles[i]);
        }
    }
    Figure RandomFigure()
    {
        Color color = Random.ColorHSV(0.2f, 0.9f);
        int random = Random.Range(0, 7);
        switch (random)
        {
            case 0: return new TFigure(color);
            case 1: return new JFigure(color);
            case 2: return new LFigure(color);
            case 3: return new OFigure(color);
            case 4: return new SFigure(color);
            case 5: return new ZFigure(color);
            case 6: return new IFigure(color);
            default:
                return new TFigure();
        }
    }
    void TetrisCheck()
    {
        CreateTilesMatrix();
        FillTilesMatrix();
        List<int> rowsDeleted = new List<int>();
        for (int i = 0; i < TilesMatrix.Length; i++)
        {
            if (CheckLine(i))
            {
                rowsDeleted.Add(i);
                DeleteRow(i);
            }
        }
        ClearDeletedTiles();
        if (rowsDeleted.Count != 0)
        {
            DropDownLines(rowsDeleted);
            ObjectManager.GetScoreAnimation().AddScore(rowsDeleted.Count);
        }
        
    }
    void DropDownLines(List<int> rowsDeleted)
    {
        for (int i = 0; i < rowsDeleted.Count; i++)
        {
            for (int j = 0; j < rowsDeleted[i]; j++)
            {
                for (int k = 0; k < TilesMatrix[j].Length; k++)
                {
                    if (TilesMatrix[j][k] != null)
                    {
                        TilesMatrix[j][k].position += Vector2Int.up;
                    }
                }
            }
        }
    }
    void ClearDeletedTiles()
    {
        for (int i = 0; i < Tiles.Count;)
        {
            if (Tiles[i].isDelete)
            {
                Tiles.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }
    void DeleteRow(int row)
    {
        for (int i = 0; i < TilesMatrix[row].Length; i++)
        {
            TilesMatrix[row][i].Delete();
            TilesMatrix[row][i] = null;
        }
    }
    bool CheckLine(int row)
    {
        for (int i = 0; i < TilesMatrix[row].Length; i++)
        {
            if (TilesMatrix[row][i] == null)
            {
                return false;
            }
        }
        return true;

    }


    void FillTilesMatrix()
    {
        for (int i = 0; i < Tiles.Count; i++)
        {

            TilesMatrix[Tiles[i].position.y][Tiles[i].position.x] = Tiles[i];
        }

    }
    void CreateTilesMatrix()
    {
        TilesMatrix = new FigureTile[matrixSize_y][];
        for (int i = 0; i < TilesMatrix.Length; i++)
        {
            TilesMatrix[i] = new FigureTile[matrixSize_x];
        }
    }
    void CreateNewFigure()
    {
        CurrentFigure = nextFigure;
        nextFigure = RandomFigure();
        ObjectManager.GetNextFigureScript().FigureShow(nextFigure);
        TetrisCheck();
        UpdateColors();
    }
    void CheckColisions() 
    {
        if (CheckColision(CurrentFigure))
        {
            CurrentFigure.GoToOldPosition();
        }
        if (ChechSideCollision(CurrentFigure))
        {
            CurrentFigure.Move(FoundSideCollision(CurrentFigure) * Vector2Int.left);
        }
    }   
    void Rotate()
    {
        CurrentFigure.Rotate();
        CheckColisions();
    }
    void Reflect()
    {
        CurrentFigure.Reflect();
        CheckColisions();
    }
    void GameTick()
    {
        CurrentFigure.Move(Vector2Int.up);
        if (CheckColision(CurrentFigure))
        {
            CurrentFigure.GoToOldPosition();
            NextFigure();
        }
        UpdateColors();
    }
    void Start()
    {
        nextFigure = RandomFigure();
        CreateNewFigure();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            CurrentFigure.Move(Vector2Int.left);
            CheckCoords(CurrentFigure);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            CurrentFigure.Move(Vector2Int.right);
            CheckCoords(CurrentFigure);
        }

        if (Input.GetKey(KeyCode.S))
        {
            gmTick = quickedGameTick;
        }
        else
        {
            gmTick = gameTick;
        }


        if (Input.GetKeyUp(KeyCode.E))
        {

            Rotate();
            CheckCoords(CurrentFigure);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Reflect();
        }
        if (currentGameTick >= gmTick)
        {
            currentGameTick = 0;
            GameTick();
        }
        currentGameTick += Time.deltaTime;
    }

}

