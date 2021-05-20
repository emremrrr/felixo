using Felixo.Library.Messaging.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Felixo.Library.Messaging
{
    public class RabbitMQClientSender : IRabbitMQClientSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _username;
        private IConnection _connection;

        public RabbitMQClientSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;

            CreateConnection();
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    UserName = _username,
                    Password = _password,
                    HostName = _hostname,
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");

            }
        }



        public void Send<T>(T param)
        {
            if (ConnectionExists())
            {
                using (var channel=_connection.CreateModel())
                {
                    channel.QueueDeclare(_queueName, false, false, false, null);
                    var msg = new MessageModel() {MessageType=$"{typeof(T).FullName}",MessageAssembly=$"{typeof(T).Assembly.GetName().Name}",Message =param };
                    var jsnstr = JsonConvert.SerializeObject(msg,Formatting.Indented);
                    var body = Encoding.UTF8.GetBytes(jsnstr);
                    channel.BasicPublish("", _queueName, null, body);
                }
            }
        }


        public bool ConnectionExists()
        {
            if (_connection != null)
                return true;
            CreateConnection();
            return _connection != null;
        }
    }
}
