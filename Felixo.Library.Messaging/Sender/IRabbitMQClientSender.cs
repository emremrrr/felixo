using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Messaging
{
    public interface IRabbitMQClientSender
    {
        void Send<T>(T param);
    }
}
