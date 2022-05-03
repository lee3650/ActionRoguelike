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
        (Vector2Int, Vector2Int) sizeoffset = CalculateSizeAndOffset(datas);

        Vector2Int size = sizeoffset.Item1;
        Vector2Int offset = sizeoffset.Item2;

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
            if (d.Offset.x + d.XSize > xmax)
            {
                xmax = d.Offset.x + d.XSize;
            }
            if (d.Offset.y + d.YSize > ymax)
            {
                ymax = d.Offset.y + d.YSize;
            }

            if (d.Offset.x + d.XSize < xmin)
            {
                xmin = d.Offset.x + d.XSize;
            }
            if (d.Offset.y + d.YSize < ymin)
            {
                ymin = d.Offset.y + d.YSize;
            }
        }

        return (new Vector2Int(xmax - xmin, ymax - ymin), new Vector2Int());
    }
}
