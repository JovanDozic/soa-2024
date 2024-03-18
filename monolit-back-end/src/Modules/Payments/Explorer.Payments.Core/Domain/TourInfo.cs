using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
    public class TourInfo : ValueObject
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Image { get; init; }

        public TourInfo(int id, string name, string image)
        {
            Id = id;
            Name = name;
            Image = image;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
            yield return Image;
        }
    }
}
