using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Framework
{
    public class TcpException : Exception
    {
        public TcpException(string Message) : base(Message)
        { }

        public TcpException(string Message, Exception ex) : base(Message, ex)
        { }

        public TcpException(Exception ex) : base(ex.Message, ex)
        { }
    }
}
