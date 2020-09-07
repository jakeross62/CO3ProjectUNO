using System;
using System.IO;
using System.Net;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.Games;
using System.Net.Sockets;

namespace UNOProjectCO3
{
    public abstract class gameConnection : HostBackEnd
    {
        public IPEndPoint HostAddress { get; private set; }
        public long HostId { get; private set; }
        readonly long ConnectId = IDGenerator.GenerateID();
        public long PlayerId { get; private set; }
        public string PlayerName;
        bool connected;

        public bool IsConnected
        {
            get { return connected; }
        }

        bool isPlayerReady;

        public bool IsPlayerReady
        {
            get
            {
                return isPlayerReady;
            }
            set
            {
                SetPlayerReady(value);
            }
        }
        public bool IsGameStartable { get; private set; }

        public delegate void NoArgDelegate();
        public event NoArgDelegate Connected;
        public delegate void DisconnectedHandler(ClientMessages msg, string reason);
        public event DisconnectedHandler Disconnected;
        public delegate void ChatHandler(string Name, string message);
        public event ChatHandler ChatArrived;
        public event NoArgDelegate GameStarted;
        public event Action<bool> GameFinished;
        public event Action<bool> ReadyStateChanged;
        public event Action<string> OtherPlayerLeft;
        public delegate void GeneralPlayerInfoHandler(string nick, bool isReady, object furtherInfo);
        public event GeneralPlayerInfoHandler GeneralPlayerInfoReceived;


        public static gameConnection Create(IPAddress ip, long hostId, IGameHostCreation theGameHost)
        {
            var Connection = theGameHost.CreateConnection();
            Connection.HostId = hostId;
            Connection.HostAddress = new IPEndPoint(ip, myPort);
            return Connection;
        }

        public virtual void Initialize(string Name)
        {
            SendConnectionRequest(Name);
        }

        void SendConnectionRequest(string Name)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.Connect);
                w.Write(ConnectId);
                w.Write(Name);
                Send(ms, HostAddress);
            }
        }

        protected virtual void gameConnected()
        {
            connected = true;
            if (Connected != null)
                gameConnected();
        }

        protected virtual void gameDisconnected(ClientMessages msg, string reason)
        {
            connected = false;
            if (Disconnected != null)
                Disconnected(msg, reason);
        }

        public void Disconnect()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.Disconnect);
                w.Write(PlayerId);
                Send(ms, HostAddress);
            }
        }

        protected override void DataRecieved(IPEndPoint ep, BinaryReader r)
        {
            var id = r.ReadInt64();
            if (PlayerId != 0)
            {
                if (id != PlayerId)
                    return;
            }
            else if (ConnectId != id)
                return;
            var msg = (ClientMessages)r.ReadByte();
            string Name;

            switch (msg)
            {
                case ClientMessages.JoinGranted:

                    PlayerId = r.ReadInt64();
                    PlayerName = r.ReadString();

                    gameConnected();
                    break;
                case ClientMessages.Kicked:
                case ClientMessages.Disconnected:
                case ClientMessages.Timeout:
                case ClientMessages.JoinDenied:
                    gameDisconnected(msg, r.ReadString());
                    break;
                case ClientMessages.ServerShutdown:
                    gameDisconnected(msg, string.Empty);
                    break;
                case ClientMessages.OtherPlayerLeft:
                    PlayerName = r.ReadString();
                    OnOtherPlayerLeft(PlayerName);
                    if (OtherPlayerLeft != null)
                        OtherPlayerLeft(PlayerName);
                    break;
                case ClientMessages.IsReady:
                    isPlayerReady = r.ReadBoolean();
                    if (ReadyStateChanged != null)
                        ReadyStateChanged(isPlayerReady);
                    break;
                case ClientMessages.PlayerInfo:
                    OnPlayerInfoReceived(r);
                    break;
                case ClientMessages.ChatMessage:
                    Name = r.ReadString();
                    var chat = r.ReadString();
                    if (ChatArrived != null)
                        ChatArrived(Name, chat);
                    break;
                case ClientMessages.GameStarted:
                    OnGameStarted();
                    if (GameStarted != null)
                        GameStarted();
                    break;
                case ClientMessages.GameFinished:
                    var aborted = r.ReadBoolean();
                    OnGameFinished(aborted);
                    if (GameFinished != null)
                        GameFinished(aborted);
                    break;
                case ClientMessages.GameData:
                    OnGameDataReceived(r);
                    break;
                case ClientMessages.GeneralPlayersInfo:
                    var count = r.ReadByte();
                    while (count-- > 0)
                    {
                        Name = r.ReadString();
                        var isReady = r.ReadBoolean();
                        OnGeneralPlayerInfoReceived(Name, isReady, r);
                    }
                    break;
            }
        }
        protected virtual void OnPlayerInfoReceived(BinaryReader r) { }
        protected virtual void OnOtherPlayerLeft(string nick) { }

        public void AcquirePlayerInfo()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GetPlayerInfo);
                w.Write(PlayerId);

                Send(ms, HostAddress);
            }
        }

        protected void FireGeneralPlayerInfoReceivedEvent(string Name, bool isReady, object furtherInfo = null)
        {
            if (GeneralPlayerInfoReceived != null)
                GeneralPlayerInfoReceived(Name, isReady, furtherInfo);
        }

        protected virtual void OnGeneralPlayerInfoReceived(string Name, bool isReady, BinaryReader r)
        {
            FireGeneralPlayerInfoReceivedEvent(Name, isReady, null);
        }

        public void AcquireGeneralPlayerInfo()
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GetPlayersInfo);
                w.Write(PlayerId);

                Send(ms, HostAddress);
            }
        }

        void SetPlayerReady(bool ready)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.SetReadyState);
                w.Write(PlayerId);
                w.Write(ready);
                Send(ms, HostAddress);
            }
        }

        public void SendChat(string message)
        {
            if (!string.IsNullOrEmpty(message))
                using (var ms = new MemoryStream())
                using (var w = new BinaryWriter(ms))
                {
                    w.Write(HostId);
                    w.Write((byte)HostMessage.ChatMessage);
                    w.Write(PlayerId);
                    w.Write(message);
                    Send(ms, HostAddress);
                }
        }

        protected virtual void OnGameDataReceived(BinaryReader r) { }
        protected virtual void OnGameStarted() { }
        protected virtual void OnGameFinished(bool aborted) { }

        public void SendGameData(MemoryStream ms)
        {
            SendGameData(ms.ToArray());
        }

        public void SendGameData(byte[] data)
        {
            using (var ms = new MemoryStream())
            using (var w = new BinaryWriter(ms))
            {
                w.Write(HostId);
                w.Write((byte)HostMessage.GameData);
                w.Write(PlayerId);
                w.Write(data);

                Send(ms, HostAddress);
            }
        }
    }
}
