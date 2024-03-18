using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface IObjectRepository
    {
        List<ObjectDto> GetAllPublicObjects();
        ObjectDto SetPublic(int objectId);
    }
}
