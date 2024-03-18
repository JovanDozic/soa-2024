using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Preference : Entity
    {
        public long UserId { get; init; }
        public int Difficulty { get; init; } // od 1 do 5
        public MeansOfTransport Transport {  get; init; }
        public string Tags { get; init; }

        public Preference(long userId, int difficulty, MeansOfTransport transport, string tags)
        {
            UserId = userId;
            Difficulty = difficulty;
            Transport = transport;
            Tags = tags;
            Validate();
        }

        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (Difficulty < 1 || Difficulty > 5) throw new ArgumentException("Invalid Difficulty");
            if (string.IsNullOrEmpty(Tags)) throw new ArgumentException("Invalid Tags");
        }
    }

    public enum MeansOfTransport
    {
        Walking,
        Bicycle,
        Car,
        Boat
    }
}
