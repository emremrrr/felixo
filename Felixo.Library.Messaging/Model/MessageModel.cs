using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Messaging.Model
{
    public class MessageModel
    {
        public object Message { get; set; }

        public string MessageType { get; set; }
        public string MessageAssembly { get; set; }
    }
}
