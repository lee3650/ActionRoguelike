using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestRoomPlacer
{
    private ManualRoomChooser ManualRoomChooser;
    private List<RoomData> ChosenRooms;

    [SetUp]
    public void Setup()
    {
        ManualRoomChooser = Resources.Load<ManualRoomChooser>("TestResources/ManualChooser");
        ChosenRooms = ManualRoomChooser.ChooseRandomRooms();
    }

    [Test]
    public void RoomPlacerPlacesSingleRoomCorrectly()
    {
        List<RoomData> ExistingRooms = new List<RoomData>();

        ExistingRooms.Add(ChosenRooms[0]);

        RoomData cur = ChosenRooms[1];
        cur.Parent = ChosenRooms[0];

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),
        };

        (bool success, Vector2Int offset, int branchLength) = SingleRoomPlacer.PlaceRoom(0, directions, cur, ExistingRooms);

        Assert.AreEqual(true, success);
        Assert.AreEqual(new Vector2Int(-16, 8), offset);
        Assert.AreEqual(1, branchLength);
    }

    [Test]
    public void RoomPlacerReturnsFalseForImpossibleSituation()
    {
        List<RoomData> ExistingRooms = new List<RoomData>();

        ExistingRooms.Add(ChosenRooms[0]);

        RoomData cur = ChosenRooms[1];
        cur.Parent = ChosenRooms[0];

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, -1),
        };

        (bool success, Vector2Int offset, int branchLength) = SingleRoomPlacer.PlaceRoom(0, directions, cur, ExistingRooms);

        Assert.AreEqual(false, success);
    }
    
    [Test]
    public void RoomPlacerBacktracks()
    {
        List<RoomData> ExistingRooms = new List<RoomData>();

        ExistingRooms.Add(ChosenRooms[0]);
        ExistingRooms.Add(ChosenRooms[2]);

        ChosenRooms[2].Parent = ChosenRooms[0];

        RoomData cur = ChosenRooms[1];
        cur.Parent = ChosenRooms[2];

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),
        };

        (bool success, Vector2Int offset, int branchLength) = SingleRoomPlacer.PlaceRoom(1, directions, cur, ExistingRooms);

        Assert.AreEqual(true, success);
        Assert.AreEqual(new Vector2Int(-16, 8), offset);
        Assert.AreEqual(1, branchLength);
    }
}
