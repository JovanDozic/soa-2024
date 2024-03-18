using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Club : Entity
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string URL { get; init; }
        public int OwnerId { get; init; }
       

        public Club() { }
        public Club(string name, string description, string url, int ownerId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            URL = url;
            OwnerId = ownerId;
            
        }
    }
}
