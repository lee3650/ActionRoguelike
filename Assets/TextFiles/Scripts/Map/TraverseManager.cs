using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraverseManager : MonoBehaviour
{
    private static bool[,] Traversable;
    private static Room[,] RoomMap; 

    private static int xSize, ySize;

    private static Vector2Int origin;

    private static int tileSize;

    public static void Initialize(int xSize, int ySize, Vector2Int center, int tile_size)
    {
        TraverseManager.xSize = xSize;
        TraverseManager.ySize = ySize;
        tileSize = tile_size;
        origin = center;

        Traversable = new bool[xSize, ySize];
        RoomMap = new Room[xSize, ySize];
    }

    public static Room GetRoom(int wx, int wy)
    {
        if (PointInBounds(wx, wy))
        {
            int nx = TransformX(wx);
            int ny = TransformX(wy);
            return RoomMap[nx, ny];
        }
        return null; 
    }

    public static void SetRoom(int wx, int wy, Room room)
    {
        if (PointInBounds(wx, wy))
        {
            int nx = TransformX(wx);
            int ny = TransformX(wy);
            RoomMap[nx, ny] = room; 
        }
    }

    public static bool IsPointTraversable(Vector2Int pos)
    {
        return IsPointTraversable(pos.x, pos.y);
    }

    public static bool IsPointTraversable(int x, int y)
    {
        int nx = TransformX(x);
        int ny = TransformY(y);
        return PointInBounds(x, y) && Traversable[nx, ny];
    }

    public static bool PointInBounds(int x, int y)
    {
        int nx = TransformX(x);
        int ny = TransformY(y);
        return nx >= 0 && nx < xSize && ny >= 0 && ny < ySize;
    }

    private static Vector2Int TransformToLocalCoords(int x, int y)
    {
        return new Vector2Int(TransformX(x), TransformY(y));
    }

    private static int TransformX(int x)
    {
        return (x / tileSize) - origin.x;
    }

    private static int TransformY(int y)
    {
        return (y / tileSize) - origin.y;
    }

    public static void SetPoint(int wx, int wy, bool val)
    {
        Prereq.Assert(PointInBounds(wx, wy), "Tried to set a point out of bounds: (" + wx + ", " + wy + ")");

        int nx = TransformX(wx);
        int ny = TransformY(wy);
        Traversable[nx, ny] = val; 
    }

    public static void ResetState()
    {
        Traversable = null;
    }
}
