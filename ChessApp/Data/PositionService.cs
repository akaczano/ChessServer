using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessModels;

namespace ChessApp.Data {
    public class PositionService {

        public ChessPosition Position { get; set; } = new ChessPosition();
        public List<ChessClient> _clients;

        public PositionService() {
            _clients = new List<ChessClient>();
        }

        public Task<ChessPosition> GetPositionAsync() {
            return Task.FromResult(Position);
        }

        public void TellEverybody() {
            foreach (ChessClient client in _clients) {
                client.NotifyMove();
            }
        }

        public void JoinParty(ChessClient client) {
            _clients.Add(client);
        }

    }

    public interface ChessClient {
        public void NotifyMove();
    }
}
