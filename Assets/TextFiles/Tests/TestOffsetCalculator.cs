using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestOffsetCalculator
{
    private ManualRoomSupplier rs;
    List<RoomData> datas; 

    [SetUp] 
    public void Setup()
    {
        rs = Resources.Load<ManualRoomSupplier>("TestResources/TestManualSupplier");
        
        datas = rs.ChooseRooms();
    }

    [Test]
    public void CanCalculateOffset()
    {
        RoomData parent = datas[0];

        parent.Offset = new Vector2Int(-8, -8);

        (bool success, Vector2Int parentDoor) = OffsetCalculator.GetDoorPosition(parent, Vector2Int.up);
        (bool success2, Vector2Int childDoor) = OffsetCalculator.GetDoorPosition(datas[1], Vector2Int.down);

        Vector2Int offset = OffsetCalculator.GetOffset(parent, parentDoor, childDoor, Vector2Int.up);

        Assert.AreEqual(new Vector2Int(-16, 8), offset);
    }

    [Test]
    public void OffsetCalculatorReturnsFalse()
    {
        RoomData room = datas[1];

        (bool sucess, Vector2Int pos) = OffsetCalculator.GetDoorPosition(room, Vector2Int.up);

        Assert.AreEqual(false, sucess);
    }

    [Test]
    public void OffsetCalculatorFindsDoors()
    {
        RoomData d = datas[0];

        (bool sucess, Vector2Int pos) result = OffsetCalculator.GetDoorPosition(d, Vector2Int.up);

        Assert.AreEqual(true, result.sucess);
        Assert.AreEqual(result.pos, new Vector2Int(3, 7));

        result = OffsetCalculator.GetDoorPosition(d, Vector2Int.down);

        Assert.AreEqual(true, result.sucess);
        Assert.AreEqual(result.pos, new Vector2Int(3, 0));

        result = OffsetCalculator.GetDoorPosition(d, Vector2Int.left);

        Assert.AreEqual(true, result.sucess);
        Assert.AreEqual(result.pos, new Vector2Int(0, 3));

        result = OffsetCalculator.GetDoorPosition(d, Vector2Int.right);

        Assert.AreEqual(true, result.sucess);
        Assert.AreEqual(result.pos, new Vector2Int(7, 3));
    }
}
