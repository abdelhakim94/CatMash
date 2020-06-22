using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Catmash.DataRepository;
using System.Linq;

namespace Catmash.Web.Hubs
{
    public class CatmashNotificationHub : Hub
    {
        private readonly ICatmashRepository repository;

        public CatmashNotificationHub(ICatmashRepository repository) => this.repository = repository;

        public async Task UpdateVotesInClients()
        {
            var totalVotes = (await repository.RetrieveAllAsync())
                    .Aggregate(ulong.MinValue, (votes, image) => votes += image.Votes);
            await Clients.All.SendAsync("UpdateVotes", totalVotes);
        }
    }
}