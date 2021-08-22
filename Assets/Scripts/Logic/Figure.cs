using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Figure
{
    protected List<Vector2Int> OldPositions;
    public List<FigureTile> Tiles;
    protected FigureTile RotationPoint;
    int matrixSize_y;
    int matrixSize_x;
    public Vector2Int offset;
    public virtual void Move(Vector2Int direction) 
    {
        CreateOldPositions();
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].position += direction;
            
        }
    }
    public virtual void Reflect()
    {
        if (RotationPoint != null)
        {
            CreateOldPositions();
            for (int i = 0; i < Tiles.Count; i++)
            {
                ReflectFromPoint(Tiles[i], RotationPoint.position);
            }
        }
        else
        {
            Debug.Log("NE SDELAL");
        }
    }
    public void GoToOldPosition() 
    {
        if(OldPositions.Count == Tiles.Count) 
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles[i].position = OldPositions[i];
            }
        }
        else
        {
            Debug.Log("Go to Old position bug!");
        }
    }
    public Figure(int matrixSize_x = 10, int matrixSize_y = 10) 
    {
        Tiles = new List<FigureTile>();
        this.matrixSize_x = matrixSize_x;
        this.matrixSize_y = matrixSize_y;
    }
    protected void CreateOldPositions() 
    {
        OldPositions = new List<Vector2Int>();
        for (int i = 0; i < Tiles.Count; i++)
        {
            OldPositions.Add(Tiles[i].position);
        }
    }
    protected void setColor(Color color) 
    {
        if (color == default)
            color = Color.white;
        for (int i = 0; i < Tiles.Count; i++)
        {
            Tiles[i].color = color;
        }   
    }
    public virtual void Rotate(float angle = 90) 
    {
        if (RotationPoint != null)
        {
            CreateOldPositions();
            for (int i = 0; i < Tiles.Count; i++)
            {
                RotatePointAroundPoint(Tiles[i], RotationPoint.position);
            }
        }
        else 
        {
            Debug.Log("NE SDELAL");
        }
    }
    static void RotatePointAroundPoint(FigureTile TilePoint, Vector2 rotationPoint, float angle = 90)
    {
        Vector2 point = TilePoint.position;
        point -= rotationPoint;
        float newX = point.x * Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * angle)) - point.y * Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * angle));
        float newY = point.x * Mathf.Round(Mathf.Sin(Mathf.Deg2Rad * angle)) + point.y * Mathf.Round(Mathf.Cos(Mathf.Deg2Rad * angle));
        
        var result = new Vector2Int((int) newX,(int) newY) + rotationPoint;
        TilePoint.position = new Vector2Int((int)result.x,(int) result.y);
    }
    static void ReflectFromPoint(FigureTile TilePoint, Vector2 rotationPoint) 
    {
        Vector2 point = TilePoint.position;
        point -= rotationPoint;
        float newX = point.x * -1;
        float newY = point.y;

        var result = new Vector2Int((int)newX, (int)newY) + rotationPoint;
        TilePoint.position = new Vector2Int((int)result.x, (int)result.y);
    }
}
public class TFigure : Figure
{
    
    public TFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y) 
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(3, 1));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));

        RotationPoint = tile;

        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 1));
        Tiles.Add(tile);
        
        CreateOldPositions();
        
        setColor(color);

        offset = new Vector2Int(3, 0);
    }
}
public class JFigure : Figure
{
    
    public JFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(5, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 1));
        Tiles.Add(tile);
        RotationPoint = tile;
        tile = new FigureTile(new Vector2Int(5, 2));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 2));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(4, 0);
    }
}
public class LFigure : Figure
{
   
    public LFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));
        Tiles.Add(tile);
        RotationPoint = tile;
        tile = new FigureTile(new Vector2Int(4, 2));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 2));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(4, 0);
    }
}
public class IFigure : Figure
{
    
    public IFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 2));
        Tiles.Add(tile);
        RotationPoint = tile;
        tile = new FigureTile(new Vector2Int(4, 3));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(4, 0); 
    }
}
public class OFigure : Figure
{
    public override void Rotate(float angle = 90)
    {
        
    }
    public OFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 1));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(4, 0);
    }
}
public class SFigure : Figure
{
   
    public SFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(5, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));
        Tiles.Add(tile);
        RotationPoint = tile;
        tile = new FigureTile(new Vector2Int(3, 1));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(3, 0);
    }
}
public class ZFigure : Figure
{
    
    public ZFigure(Color color = default, int matrixSize_x = 10, int matrixSize_y = 20) : base(matrixSize_x, matrixSize_y)
    {
        //Tiles start with start positions;
        FigureTile tile = new FigureTile(new Vector2Int(3, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 0));
        Tiles.Add(tile);
        tile = new FigureTile(new Vector2Int(4, 1));
        Tiles.Add(tile);
        RotationPoint = tile;
        tile = new FigureTile(new Vector2Int(5, 1));
        Tiles.Add(tile);
        CreateOldPositions();
        setColor(color);
        offset = new Vector2Int(3, 0);
    }
}
public class FigureTile
{
    public bool isDelete = false;
    public Vector2Int position;
    public Color color;
    public FigureTile(Vector2Int position, Color color = default)
    {
        if (color == default)
            color = Color.white;
        this.color = color;
        this.position = position;
    }
    public void Delete() 
    {
        isDelete = true;
    }
}
