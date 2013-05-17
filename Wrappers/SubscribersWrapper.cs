using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moosend.API.Client.Models;
using System.Web;

namespace Moosend.API.Client.Wrappers
{
    public class SubscribersWrapper
    {
        private ApiManager _Manager;

        internal SubscribersWrapper(ApiManager manager)
        {
            _Manager = manager;
        }

        public Subscriber GetByEmail(Guid mailingListID, String email)
        {
            return _Manager.MakeRequest<Subscriber>(HttpMethod.GET, String.Format("/subscribers/{0}/view", mailingListID), new { Email = email });
        }

        public IList<Subscriber> Subscribe(Guid mailingListID, IList<SubscriberParams> members)
        {
            return _Manager.MakeRequest<IList<Subscriber>>(HttpMethod.POST, String.Format("/subscribers/{0}/subscribe_many", mailingListID), new { 
                Members = members.Select(m => new { 
                    Name = m.Name, 
                    Email = m.Email, 
                    CustomFields = m.CustomFields.Select(c => c.Key + "=" + c.Value).ToList() 
                }).ToList() 
            });
        }

        public Subscriber Subscribe(Guid mailingListID, SubscriberParams member)
        {
            return _Manager.MakeRequest<Subscriber>(HttpMethod.POST, String.Format("/subscribers/{0}/subscribe", mailingListID), new
            {
                Name = member.Name,
                Email = member.Email,
                CustomFields = member.CustomFields.Select(c => c.Key + "=" + c.Value).ToList()
            });
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
                emails = String.Join(",", emails) 
            });
        }
    }
}
