using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UNOProjectCO3.Game_Connection_Algorithms;

namespace UNOProjectCO3.Games
{
    public class ServerListBackend
    {
        #region Properties
        public const int MyCommunicationPort = 55000;
        internal static readonly IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
        internal static readonly IPEndPoint multicastEndpoint;
        UdpClient udp;

        public event Action<GameHostEntry> EntryReceived;
        #endregion

        static ServerListBackend()
        {
            multicastEndpoint = new IPEndPoint(multicastaddress, MyCommunicationPort);
        }

        public ServerListBackend()
        {
            GameHost.AnyGameStateChanged += ThisHostStateChanged;
            udp = new UdpClient();
            udp.ExclusiveAddressUse = false;
            udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp.JoinMulticastGroup(multicastaddress);

            udp.Client.Bind(new IPEndPoint(IPAddress.Any, MyCommunicationPort));

            var listenerThread = new Thread(this.listenerThread);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        ~ServerListBackend()
        {
            GameHost.AnyGameStateChanged -= ThisHostStateChanged;
            udp.Close();
        }

        void ThisHostStateChanged(object sender, GameStateChangedArgs ea)
        {
            SendHostUpdate(ea.Host);
        }

        public void SendExistenceRequest()
        {
            udp.Send(new[] { (byte)InteractionMessage.PingRequest }, 1, multicastEndpoint);
        }

        public void SendHostUpdate()
        {
            if (GameHost.IsHosting)
                SendHostUpdate(GameHost.theInstance);
        }

        public void SendHostUpdate(GameHost host)
        {
            var d = GameHostEntry.SerializeHost(host);
            if (d != null && udp.Client != null)
                udp.Send(d, d.Length, multicastEndpoint);
        }

        void listenerThread()
        {
            while (udp.Client != null)
            {
                IPEndPoint targetAddress = null;
                var data = udp.Receive(ref targetAddress);

                switch ((InteractionMessage)data[0])
                {
                    case InteractionMessage.PingRequest:
                        SendHostUpdate();
                        break;
                    case InteractionMessage.PingAnswer:
                        if (EntryReceived != null)
                        {
                            var gh = GameHostEntry.FromBytes(targetAddress, data, 1);
                            EntryReceived(gh);
                        }
                        break;
                }
            }
        }
    }
}
