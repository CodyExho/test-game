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
    public class OfferController : ControllerBase
    {
        private readonly IOfferOperations _offerOperations;
        private readonly IHubContext<NewsHub> _newsHub;
        public OfferController(IOfferOperations offerOperations, IHubContext<NewsHub> hub)
        {
            _offerOperations = offerOperations;
            _newsHub = hub;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<OfferModel> Get()
        {
            return _offerOperations.Get();
        }

        [HttpPost]
        [Route("")]
        public async Task Create([FromBody] OfferModel model)
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
            }); await _offerOperations.CreateOfferAsync(model);
        }
    }
}