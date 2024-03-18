using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class PublicRegistrationRequest : Entity
    {
        public int ObjectId { get; init; }
        public string ObjectName { get; init; }
        public int TourId { get; init; }
        public string PointName { get; init; }
        public string Comment { get; init; }
        public RequestStatus Status { get; init; }

        public PublicRegistrationRequest(int objectId, string objectName, int tourId, string pointName, string comment, RequestStatus status)
        {
            ObjectId = objectId;
            ObjectName = objectName;
            TourId = tourId;
            PointName = pointName;
            Comment = comment;
            Status = status;
            Validate();
        }
        private void Validate()
        {
            if (ObjectId == 0) throw new ArgumentException("Invalid object id.");
            if (TourId == 0) throw new ArgumentException("Invalid tour id.");
        }
    }
}

public enum RequestStatus
{
    Pending,
    Rejected,
    Approved
}
