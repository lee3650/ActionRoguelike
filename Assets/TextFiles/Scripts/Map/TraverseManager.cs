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
        if (WorldPointInBounds(wx, wy))
        {
            int nx = TransformX(wx);
            int ny = TransformY(wy);
            return RoomMap[nx, ny];
        }
        return null; 
    }

    public static void SetRoom(int wx, int wy, Room room)
    {
        if (WorldPointInBounds(wx, wy))
        {
            int nx = TransformX(wx);
            int ny = TransformY(wy);

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
        return LocalPointInBounds(nx, ny) && Traversable[nx, ny];
    }

    public static bool WorldPointInBounds(int x, int y)
    {
        int nx = TransformX(x);
        int ny = TransformY(y);

        return LocalPointInBounds(nx, ny);
    }

    public static bool LocalPointInBounds(int lx, int ly)
    {
        return lx >= 0 && lx < xSize && ly >= 0 && ly < ySize;
    }

    private static Vector2Int TransformToLocalCoords(int x, int y)
    {
        return new Vector2Int(TransformX(x), TransformY(y));
    }

    private static int TransformX(int x)
    {
        return (x - origin.x) / tileSize;
    }

    private static int TransformY(int y)
    {
        return (y - origin.y) / tileSize;
    }

    public static void SetPoint(int wx, int wy, bool val)
    {
        int nx = TransformX(wx);
        int ny = TransformY(wy);

        Prereq.Assert(LocalPointInBounds(nx, ny), string.Format("Tried to set a point out of bounds: ({0}, {1}), with max x and y ({2}, {3}) and origin {4}", wx, wy, xSize, ySize, origin));

        Traversable[nx, ny] = val; 
    }

    public static void PrintMaps()
    {
        print("Printing traversal map!");

        string map = "";

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                char val = Traversable[x, y] ? 'X' : ' ';
                map += val; 
            }
            map += "\n";
        }

        print(map);

        print("Printing room map!");

        map = "";

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                char val = RoomMap[x, y] == null ? ' ' : 'R';
                map += val;
            }
            map += "\n";
        }
     
        print(map);
    }

    public static void ResetState()
    {
        Traversable = null;
    }
}
