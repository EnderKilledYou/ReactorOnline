using System;
using System.Threading;

namespace MechWarrior
{
    public abstract class MechState
    {
        public string ThreadName { get; set; }
        public bool Complete { get; set; } = false;
        public bool Success { get; set; } = false;
        public Exception Exception { get; set; }
        public string StateMessage { get; set; }
        public CancellationToken Cancel { get; set; }
        public DateTime QueueEnterTime { get; set; } = DateTime.MinValue;
        public DateTime WorkStartTime { get; set; } = DateTime.MinValue;
        public bool WorkStarted { get; set; } = false;

        public abstract bool FuckEmThatsWhy();

        public override string ToString()
        {
            return $"{ThreadName}: {StateMessage}";
        }
    }
}