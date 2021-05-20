using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Felixo.Library.Entities.Models;
using Felixo.Library.Data.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Felixo.Library.Entities.DTO;
using log4net.Core;
using Microsoft.Extensions.Logging;
using Felixo.Library.Logging.Aspects;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Felixo.Library.Messaging;
using Felixo.Library.Service.Query;
using Felixo.Library.Service.Command;

namespace Felixo.Library.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionAspect]
    public class RequestController : ControllerBase
    {
        private readonly IUnit _unit;
        private readonly IHubContext<Notificationhub> _hubContext;
        private readonly IMediator _mediator;
        private readonly IRabbitMQClientSender _rabbitMqClientSender;
        private readonly ILogger<InfoAspect> _logger;

        //private readonly ILogger<RequestController> _logger;

        //private readonly IMapper _mapper;

        public RequestController(
            IUnit unit,
            IHubContext<Notificationhub> hubContext,
            IMediator mediator,
            IRabbitMQClientSender rabbitMqClientSender,
            ILogger<InfoAspect> logger)
        {
            _unit = unit;
            _hubContext = hubContext;
            _mediator = mediator;
            _rabbitMqClientSender = rabbitMqClientSender;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetRequests")]
        public async Task<IEnumerable<RequestDTO>> GetRequests()
        {
            return await _mediator.Send(new GetRequestsQueryCommand() { });
        }



        [HttpPost]
        [Route("RequestBook")]
        public async Task<HttpResponseMessage> RequestBook([FromBody]object request)
        {
            dynamic param = JObject.Parse(request.ToString());
            var x = param.ApplicationUser.ToString();
            var user = _unit.IdentityUserRepository.GetUserWithRole(param.ApplicationUser.ToString());
            var req = new Request
            {
                BeginDate = DateTime.Parse(param.BeginDate.ToString()),
                Book = param.Book,
                EndDate = DateTime.Parse(param.EndDate.ToString()),
                IsApproved = param.IsApproved.ToString(),
                ApplicationUser = param.user
            };
            await _unit.RequestRepository.Add(req);
            await _unit.Commit();
            //signalr
            _hubContext.Clients.User(param.user.Id).SendAsync("requestdata", req);
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [InfoAspect]
        [HttpPost]
        [Route("RespondRequest")]
        public async Task<HttpResponseMessage> RespondRequest([FromBody]Request request)
        {
            //await _unit.RequestRepository.Update(request, request.Id);
            //await _unit.Commit();
            string clientId = Notificationhub.ConnectionIdUserInfo.ContainsKey(request.ApplicationUser.Id) ? Notificationhub.ConnectionIdUserInfo[request.ApplicationUser.Id] : null;
            if (clientId == null)
                throw new NullReferenceException();
            request.ClientId = clientId;
            var message = new UpdateRequestCommand() { Request = request };
            var res =await _mediator.Send(message);
            _rabbitMqClientSender.Send<UpdateRequestCommand>(message);
            //signalr            

            //await _hubContext.Clients.Client(clientId).SendAsync("requestdata", request);

            await Task.CompletedTask;
            _logger.LogInformation("test");
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}
