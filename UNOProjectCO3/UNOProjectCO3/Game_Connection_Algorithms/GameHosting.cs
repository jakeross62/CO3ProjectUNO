using System;
using System.Net;
using UNOProjectCO3.Games;
using System.IO;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public class GameHosting
    {
        public long HostId;
        public string GameTitle;
        public int PlayerCount;
        public int MaxPlayers;
        public GameState State;
        public IPEndPoint Address;

        public override bool Equals(object obj)
        {
            var o = obj as GameHosting;
            return o != null && o.Address.Equals(Address) && o.HostId == HostId;
        }

        public override int GetHashCode()
        {
            return Address.GetHashCode() + (int)HostId;
        }

        public static byte[] SerializeHost(GameHosting host)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write((byte)InteractionMessage.PingAnswer);
                w.Write(host.HostId);
                w.Write(host.PlayerCount);
                w.Write(host.MaxPlayers);
                w.Write((byte)host.State);
                w.Write(host.GameTitle);
                return ms.GetBuffer();
            }
        }

        public static GameHosting FromBytes(IPEndPoint address, byte[] data, int index)
        {
            var ms = new MemoryStream(data, index, data.Length - index);
            var r = new BinaryReader(ms);
            var gh = new GameHosting();
            gh.Address = address;
            gh.HostId = r.ReadInt64();
            gh.PlayerCount = r.ReadInt32();
            gh.MaxPlayers = r.ReadByte();
            gh.State = (GameState)r.ReadByte();
            gh.GameTitle = r.ReadString();
            r.Close();
            ms.Close();
            return gh;
        }

        public override string ToString()
        {
            string msg;
            switch (State)
            {
                case GameState.WaitingForPlayers:
                case GameState.GameFinished:
                    msg = "Open";
                    break;
                default:
                    msg = "Closed";
                    break;
            }
            return string.Format("{4},IP {0} ({1}/{2} Players, {3})", Address.Address, PlayerCount, MaxPlayers, msg, GameTitle);
        }
    }
}
