using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IMembershipRequestRepository 
    {
        MembershipRequest Create (MembershipRequest request);
       
        MembershipRequest Update (MembershipRequest request);
        MembershipRequest Get (int id);
    }
}
