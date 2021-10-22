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
            for (int i = 0; i < 50; i++)
            {
                MechManager.AddWork(new WaitCallback(Test), new SlowWiggie("the url with http:// or ip",SomePort,10000)
                {
                    ThreadName = $"Test {i}",
                    Cancel = tsource.Token,


                });
            }
    
            int Iterations = 0;
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
        static void Test(object? stater)
        {
            var state = (MechState)stater;
            state.WorkStartTime = DateTime.Now;
            state.WorkStarted = true;
            state.FuckEmThatsWhy();            
            state.Complete = true;
            state.Success = true;
        }
    }
}
