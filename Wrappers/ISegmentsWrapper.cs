using System;
using System.Collections.Generic;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Wrappers
{
    public interface ISegmentsWrapper
    {
        void Delete(Guid mailingListID, int segmentID);
        IList<Segment> FindAllForMailingList(Guid mailingListID);
        Segment FindByID(Guid mailingListID, int segmentID);
        PagedList<Subscriber> GetMembers(Segment segment, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500);
        PagedList<Subscriber> GetMembers(Guid mailingListID, int segmentID, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500);
        void Save(Segment segment);
    }
}