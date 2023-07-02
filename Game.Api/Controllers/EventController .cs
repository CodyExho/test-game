using Microsoft.AspNetCore.Mvc;
using Game.Common.Models;
using Game.Operations;
using Game.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Game.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventOperations _eventOperations;
        private readonly IHubContext<NewsHub> _newsHub;
        public EventController(IEventOperations eventOperations, IHubContext<NewsHub> hub)
        {
            _eventOperations = eventOperations;
            _newsHub = hub;
        }



        [HttpGet]
        [Route("")]
        public IEnumerable<EventModel> Get()
        {
            return _eventOperations.Get();
        }

        [HttpPost]
        [Route("")]
        public async Task Create([FromBody] EventModel model)
        {
            await _newsHub.Clients.All.SendAsync("newsUpdate", JsonConvert.SerializeObject(new List<AbstractModel> { model })).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("Failed to start: {0}", task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Success! Sent the data");
                    // Do more stuff here
                }
            });
            await _eventOperations.CreateEventAsync(model);
        }
    }
}