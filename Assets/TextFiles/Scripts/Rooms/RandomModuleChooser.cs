using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomModuleChooser : RoomChooser
{
    [SerializeField] List<RoomData> StartingRoomOptions;
    [SerializeField] List<RoomOption> BossOptions; 
    [SerializeField] List<RoomOption> ChestOptions;
    [SerializeField] List<RoomOption> Options;
    [SerializeField] int minRooms = 9;
    [SerializeField] int maxRooms = 12;
    [SerializeField] int minChests = 2;
    [SerializeField] int maxChests = 4;  

    public override List<RoomData> ChooseRandomRooms()
    {
        List<RoomData> result = new List<RoomData>();
        
        result.Add(StartingRoomOptions[Random.Range(0, StartingRoomOptions.Count)]);

        int room_count = Random.Range(minRooms, maxRooms + 1);

        for (int i = 0; i < room_count; i++)
        {
            RoomOption options = Options[Random.Range(0, Options.Count)];

            RoomData room = GetRoomData(options);
            
            result.Add(room);
        }

        int chest_count = Random.Range(minChests, maxChests + 1);

        for (int i = 0; i < chest_count; i++)
        {
            int index = Random.Range(1, Options.Count - 1);
            result.Insert(index, GetRoomData(ChestOptions[Random.Range(0, ChestOptions.Count)]));
        }

        return result; 
    }

    public override RoomData GetBossRoom()
    {
        return GetRoomData(BossOptions[Random.Range(0, BossOptions.Count)]);
    }

    private RoomData GetRoomData(RoomOption options)
    {
        Sprite wave = null;

        if (options.WaveOptions.Length > 0)
        {
            wave = options.WaveOptions[Random.Range(0, options.WaveOptions.Length)];
        }

        Sprite destructive_env = null;

        if (options.DestructiveOptions.Length > 0)
        {
            destructive_env = options.DestructiveOptions[Random.Range(0, options.DestructiveOptions.Length)];
        }

        Sprite additive_env = null;

        if (options.AdditiveOptions.Length > 0)
        {
            additive_env = options.AdditiveOptions[Random.Range(0, options.AdditiveOptions.Length)];
        }

        return new RoomData(options.Room, destructive_env, additive_env, wave);
    }
}
