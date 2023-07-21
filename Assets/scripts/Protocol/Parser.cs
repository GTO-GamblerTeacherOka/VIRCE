using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

public static class Parser
{
    public enum Flag{
        PositionData = 0,
        AvatarDats = 1,
        RoomEntry = 2,
        ExitRoom = 3,
        SendReaction = 4,
        ReceiveReaction = 5,
        ChatData = 7,
        GetRemoteEndPoint = 8,
        ReceiveRemoteEndPoint = 9
    }

    private const int HeaderSize = 2;

    public static (byte[] header, byte[] body)split(in byte[] data)
    {
        return (data[..HeaderSize], data[HeaderSize..]);
    }

    public static (Flag flag, ushort userID, ushort roomID) AnalyzeHeader(in byte[] header)
    {
        var flag = (Flag)(header[0] & 0b0000_1111);
        var uid = (ushort)((header[0] & 0b1111_0000) >> 4 | (header[1] & 0b0000_0001) << 4);
        var rid = (ushort)(header[1] & 0b1111_1110 >> 1);
        return (flag, uid, rid);
    }
    
    public static byte[] CreateHeader(Flag flag, ushort userID, ushort roomID)
    {
        if (!((byte)flag > 0b0000_1111) | !((byte)userID > 0b0001_1111) | !((byte)roomID > 0b0111_1111))
        {
            throw new Exception("Error Message");
        }

        var byteFlag = Convert.ToByte((byte)flag & 0b0000_1111);
        var byteUid0 = Convert.ToByte(((byte)userID & 0b0000_1111) << 4);
        var byteUid1 = Convert.ToByte(((byte)userID & 0b0001_0000) >> 4);
        var byteRid = Convert.ToByte(((byte)roomID & 0b0111_1111) << 1);
        var data = new byte[] { 0, 0 };
        
        data[0] |= byteFlag;
        data[0] |= byteUid0;
        data[1] |= byteUid1;
        data[1] |= byteRid;

        return data;
    }
    
}
