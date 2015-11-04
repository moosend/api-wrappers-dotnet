using System;
using System.Collections.Generic;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Wrappers
{
    public interface ISubscribersWrapper
    {
        Subscriber GetByEmail(Guid mailingListID, string email);
        void Remove(Guid mailingListID, string email);
        void Remove(Guid mailingListID, IList<string> emails);
        Subscriber Subscribe(Guid mailingListID, SubscriberParams member);
        IList<Subscriber> Subscribe(Guid mailingListID, IList<SubscriberParams> members);
        void Unsubscribe(Guid mailingListID, Guid campaignID, string email);
    }
}