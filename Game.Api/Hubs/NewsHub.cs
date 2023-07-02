using Game.Common.Models;
using Game.Operations;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Game.Api.Hubs
{
    public class NewsHub : Hub
    {
        private readonly IOfferOperations _offerOperations;
        private readonly IEventOperations _eventOperations;
        public NewsHub(IOfferOperations offerOperations, IEventOperations eventOperations)
        {
            _offerOperations = offerOperations;
            _eventOperations = eventOperations;
        }

        public async Task GetData(string userId)
        {
            await Clients.User(Context.User.Identity.Name).SendAsync("newsUpdate", JsonConvert.SerializeObject((_offerOperations.Get() as IEnumerable<AbstractModel>).Concat(_eventOperations.Get())));
        }
    }
}
