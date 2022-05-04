using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationController : MonoBehaviour, LateInitializable
{
    [SerializeField] SingleRoomDrawer SingleRoomDrawer;
    [SerializeField] RoomDataSupplier RoomDataSupplier;

    public void LateInit()
    {
        Generate();
    }

    public void Generate()
    {
        List<RoomData> datas = RoomDataSupplier.ChooseRooms();
        (Vector2Int size, Vector2Int offset) sizeoffset = CalculateSizeAndOffset(datas);

        Vector2Int size = sizeoffset.size;
        Vector2Int offset = sizeoffset.offset;

        print("found size and offset: " + size + ", " + offset);

        TraverseManager.Initialize(size.x, size.y, offset, MapDrawer.TileSize);

        foreach (RoomData d in datas)
        {
            SingleRoomDrawer.DrawRoom(d);
        }
    }

    public (Vector2Int, Vector2Int) CalculateSizeAndOffset(List<RoomData> roomDatas) 
    {
        int xmax = 0;
        int ymax = 0;

        int ymin = 10000;
        int xmin = 10000;

        foreach (RoomData d in roomDatas)
        {
            int xcand = d.Offset.x + (d.XSize * MapDrawer.TileSize);
            if (xcand > xmax)
            {
                xmax = xcand;
            }

            int ycand = d.Offset.y + (MapDrawer.TileSize * d.YSize);
            if (ycand > ymax)
            {
                ymax = ycand;
            }

            if (d.Offset.x < xmin)
            {
                xmin = d.Offset.x;
            }
            if (d.Offset.y < ymin)
            {
                ymin = d.Offset.y;
            }
        }

        return (new Vector2Int((xmax - xmin) / MapDrawer.TileSize, (ymax - ymin) / MapDrawer.TileSize), new Vector2Int(xmin, ymin));
    }
}
