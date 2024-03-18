using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Payments.Core.Domain
{
    public class Wallet : Entity
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; private set; }
        [JsonPropertyName("Coins")]
        public double Coins { get; private set; }

        [JsonConstructor]
        public Wallet(int userId, double coins)
        {
            UserId = userId;
            Coins = coins;
        }

        public void AddCoins(double coins)
        {
            Coins += coins;
        }
    }
}
