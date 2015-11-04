using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities.LinqBridge;
using System.Text;
using Moosend.API.Client.Models;
using System.Web;

namespace Moosend.API.Client.Wrappers
{
    public class SubscribersWrapper : ISubscribersWrapper
    {
        private IApiManager _Manager;

        internal SubscribersWrapper(IApiManager manager)
        {
            _Manager = manager;
        }

        public Subscriber GetByEmail(Guid mailingListID, String email)
        {
            return _Manager.MakeRequest<Subscriber>(HttpMethod.GET, String.Format("/subscribers/{0}/view", mailingListID), new { Email = email });
        }

        public IList<Subscriber> Subscribe(Guid mailingListID, IList<SubscriberParams> members)
        {
            var result = _Manager.MakeRequest<IList<Subscriber>>(HttpMethod.POST, String.Format("/subscribers/{0}/subscribe_many", mailingListID), new {
                Subscribers = members.Select(m => new
                { 
                    Name = m.Name, 
                    Email = m.Email, 
                    CustomFields = m.CustomFields.Select(c => c.Key + "=" + c.Value).ToList() 
                }).ToList() 
            });

            // populate custom fields with subscriber id, because it is not returned by the response
            result.ToList().ForEach(subscriber => subscriber.CustomFields.ToList().ForEach(cf => cf.SubscriberID = subscriber.ID));

            return result;
        }

        public Subscriber Subscribe(Guid mailingListID, SubscriberParams member)
        {
            var subscriber = _Manager.MakeRequest<Subscriber>(HttpMethod.POST, String.Format("/subscribers/{0}/subscribe", mailingListID), new
            {
                Name = member.Name,
                Email = member.Email,
                CustomFields = member.CustomFields.Select(c => c.Key + "=" + c.Value).ToList()
            });

            // populate custom fields with subscriber id, because it is not returned by the response
            subscriber.CustomFields.ToList().ForEach(cf => cf.SubscriberID = subscriber.ID);

            return subscriber;
        }

        public void Unsubscribe(Guid mailingListID, Guid campaignID, String email)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/subscribers/{0}/{1}/unsubscribe", mailingListID, campaignID), new { 
                Email = email 
            });
        }

        public void Remove(Guid mailingListID, String email)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/subscribers/{0}/remove", mailingListID), new { 
                Email = email 
            });
        }

        public void Remove(Guid mailingListID, IList<String> emails)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/subscribers/{0}/remove_many", mailingListID), new { 
                emails = String.Join(",", emails.ToArray()) 
            });
        }
    }
}
