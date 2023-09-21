using System;

namespace Protocol
{
    public static class Parser 
    {
        public enum Flag
        {
            PositionData = 0,
            AvatarData = 1,
            RoomEntry = 2,
            RoomExit = 3,
            Reaction = 4,
            ChatData = 7,
        }

        private const int HeaderSize = 2;

        public static (byte[] header, byte[] body) Split(in byte[] data)
        {
            return (data[..HeaderSize], data[HeaderSize..]);
        }

        public static (Flag flag, byte userID, byte roomID) AnalyzeHeader(in byte[] header)
        {
            var flag = (Flag)(header[0] & 0b0000_1111);
            var uid = (byte)((header[0] & 0b1111_0000) >> 4 | (header[1] & 0b0000_0001) << 4);
            var rid = (byte)(header[1] & 0b1111_1110 >> 1);
            return (flag, uid, rid);
        }
    
        public static byte[] CreateHeader(Flag flag, byte userID, byte roomID)
        {
            if (!((byte)flag <= 0b0000_1111) | !(userID <= 0b0001_1111) | !(roomID <= 0b0111_1111))
            {
                throw new Exception("Error Message");
            }

            var data = new byte[] { 0, 0 };
            data[0] |= (byte)((byte)flag & 0b0000_1111);
            data[0] |= (byte)((userID & 0b0000_1111) << 4);
            data[1] |= (byte)((userID & 0b0001_0000) >> 4);
            data[1] |= (byte)((roomID & 0b0111_1111) << 1);

            return data;
        }

        public static (int userID,byte[] body) GetData(byte[] data)
        {
            byte[] header, body;
            ushort userID;

            (header,body) = Split(data);
            (_,userID,_) = AnalyzeHeader(header);

            return (userID, body);
        }
    }
}
