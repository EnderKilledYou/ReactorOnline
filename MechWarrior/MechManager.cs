using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq; 
using System.Threading;

namespace MechWarrior
{

    public class MechManager
    {
        private static readonly ConcurrentDictionary<string, MechState> threadwork;

        static MechManager()
        {
            threadwork = new ConcurrentDictionary<string, MechState>();
            ThreadPool.SetMinThreads(10, 2);
            ThreadPool.SetMaxThreads(100, 10);
        }

        public static bool WorkInProgress => threadwork.Values.Any(a => !a.Complete);

        public static List<MechState> ThreadStates => threadwork.Values.ToList();

        public static List<MechState> RunningThreadStates => threadwork.Values.Where(A => A.WorkStarted == true).ToList();

        public static void AddWork(WaitCallback wait, MechState state)
        {
            if (threadwork.ContainsKey(state.ThreadName))
            {
                throw new ArgumentException("A thread with that name already exists");
            }
            if (!threadwork.TryAdd(state.ThreadName, state))
            {
                throw new ArgumentException("Couldn't add thread to bag"); 
            }
            if (!ThreadPool.QueueUserWorkItem(wait, state))
            {
                throw new QueueWorkException("Can't queue any more work");
            }
            state.QueueEnterTime = DateTime.Now;
        }
    }
}
