using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace UNOProjectCO3
{
    public abstract class HostBackEnd : IDisposable
    {
        public const int myPort = 55001;
        UdpClient UDP;
        ManualResetEvent resetEvent = new ManualResetEvent(true);
        bool disposed;
        Thread Listener;
        public IPEndPoint Address { get { return UDP.Client.LocalEndPoint as IPEndPoint; } }
        protected abstract void DataRecieved(IPEndPoint ep, BinaryReader data);

        protected void Send(MemoryStream ms, IPEndPoint ep)
        {
            Send(ms.ToArray(), ep);
        }

        protected void Send(byte[] data, IPEndPoint ep)
        {
            if (!disposed)
            {
                UDP.Send(data, data.Length, ep);
            }

        }

        public HostBackEnd(int thePort = 0)
        {
            UDP = new UdpClient();
            UDP.ExclusiveAddressUse = false;
            UDP.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            if (myPort > 0)
            {
                UDP.Client.Bind(new IPEndPoint(IPAddress.Any, myPort));
            }
            InitListener();

        }

        public void ListenerTh()
        {
            while (!UDP.Client.IsBound)
            {
                Thread.Sleep(400);
            }

            while (UDP.Client != null && resetEvent.WaitOne(0))
            {
                byte[] data = null;
                IPEndPoint targetAddress = null;
                try
                {
                    data = UDP.Receive(ref targetAddress);
                }
                catch (SocketException theException)

                {
                    if (resetEvent.WaitOne(0))
                    {
                        throw theException;
                    }
                }
                if (data != null)
                {
                    using (var ms = new MemoryStream(data))
                    using (var br = new BinaryReader(ms))
                        DataRecieved(targetAddress, br);
                }
            }
        }

        public void InitListener()
        {
            if (Listener != null && Listener.IsAlive)
                return;
            Listener = new Thread(ListenerTh);
            Listener.IsBackground = true;
            Listener.Start();
        }

        public virtual void Dispose()
        {
            resetEvent.Reset();
            disposed = true;
            UDP.Close();
        }

        HostBackEnd()
        {
            Dispose();
        }

        void listenerTh()
        {
            while (!UDP.Client.IsBound)
                Thread.Sleep(250);

            while (UDP.Client != null && resetEvent.WaitOne(0))
            {
                byte[] data = null;
                IPEndPoint targetAddress = null;
                try
                {
                    data = UDP.Receive(ref targetAddress);
                }
                catch (SocketException ex)
                {
                    if (resetEvent.WaitOne(0))
                        throw ex;
                }

                if (data != null)
                {
                    using (var ms = new MemoryStream(data))
                    using (var br = new BinaryReader(ms))
                        DataRecieved(targetAddress, br);
                }
            }
        }
    }
}
