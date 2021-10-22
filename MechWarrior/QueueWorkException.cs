using System;
using System.Runtime.Serialization;

namespace MechWarrior
{
    [Serializable]
    internal class QueueWorkException : Exception
    {
        public QueueWorkException()
        {
        }

        public QueueWorkException(string message) : base(message)
        {
        }

        public QueueWorkException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QueueWorkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}