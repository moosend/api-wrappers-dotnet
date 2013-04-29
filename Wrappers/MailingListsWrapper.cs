using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moosend.API.Client.Models;
using Moosend.API.Client.Serialization;

namespace Moosend.API.Client.Wrappers
{
    public class MailingListsWrapper
    {
        private ApiManager _Manager;

        internal MailingListsWrapper(ApiManager manager)
        {
            _Manager = manager;
        }

        public PagedList<MailingList> FindAllActive(int page = 1, int pageSize = 10)
        {
            return _Manager.MakeRequest<SerializableMailingListCollection>(HttpMethod.GET, String.Format("/lists/{0}/{1}", page, pageSize)).PagedList;
        }

        public Guid Create(String name, String confirmationPage = null, String redirectAfterUnsubscribePage = null)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, "/lists/create", new { 
                Name = name, 
                ConfirmationPage = confirmationPage, 
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage 
            });
        }

        public PagedList<MailingListMember> GetMembers(Guid mailingListID, SubscribeType status, DateTime? since = null, int page = 1, int pageSize = 500)
        {
            return _Manager.MakeRequest<SerializableMailingListMemberCollection>(HttpMethod.GET, String.Format("/lists/{0}/subscribers/{1}", mailingListID, status.ToString()), new
            {
                Since = since,
                Page = page,
                PageSize = pageSize
            }).PagedList;
        }

        public MailingList FindByID(Guid mailingListID, Boolean withStatistics = true)
        {
            return _Manager.MakeRequest<MailingList>(HttpMethod.GET, String.Format("/lists/{0}/details", mailingListID), new { 
                withStatistics = withStatistics 
            });
        }

        public void Delete(Guid mailingListID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/delete", mailingListID));
        }

        public Guid CreateCustomField(Guid mailingListID, String name, CustomFieldType type, Boolean isRequired, String context = null)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, String.Format("/lists/{0}/customfields/create", mailingListID), new { 
                Name = name, 
                Type = type, 
                IsRequired = isRequired, 
                Context = context 
            });
        }

        public void UpdateCustomField(Guid mailingListID, CustomFieldDefinition customField)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("lists/{0}/customfields/{1}/update", mailingListID, customField.ID), new { 
                Name = customField.Name, 
                Type = customField.Type, 
                IsRequired = customField.IsRequired, 
                Context = customField.Context 
            });
        }

        public void DeleteCustomField(Guid mailingListID, Guid customFieldID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/customfields{1}/delete", mailingListID, customFieldID));
        }

        public IList<Segment> GetSegments(Guid mailingListID)
        {
            return _Manager.MakeRequest<IList<Segment>>(HttpMethod.GET, String.Format("/lists/{0}/segments", mailingListID));
        }

        public Guid CreateSegment(Guid mailingListID, String name, SegmentMatchType matchType)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, String.Format("/lists/{0}/segments/create", mailingListID), new { 
                Name = name,
                MatchType = matchType
            });
        }

        public void UpdateSegment(Guid mailingListID, Segment segment)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/update", mailingListID, segment.ID), new {
                Name = segment.Name,
                MatchType = segment.MatchType            
            });
        }

        public void DeleteSegment(Guid mailingListID, Guid segmentID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments{1}/delete", mailingListID, segmentID));
        }

        public Guid AddSegmentCriteria(Guid mailingListID, Guid segmentID, String field, SegmentCriteriaComparer comparer, String value, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/add", mailingListID, segmentID), new { 
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }

        public void UpdateSegmentCriteria(Guid mailingListID, Guid segmentID, SegmentCriteria criteria)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/{2}/update", mailingListID, segmentID, criteria.ID), new {
                Field = criteria.Field,
                Comparer = criteria.Comparer,
                Value = criteria.Value,
                DateFrom = criteria.DateFrom,
                DateTo = criteria.DateTo            
            });
        }

        public void RemoveSegmentCriteria(Guid mailingListID, Guid segmentID, Guid criteriaID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments/{1}/criteria/{2}/delete", mailingListID, segmentID, criteriaID));
        }

        public PagedList<MailingListMember> GetSegmentMembers(Guid mailingListID, Guid segmentID, SubscribeType status, int page = 1, int pageSize = 500)
        {
            return _Manager.MakeRequest<SerializableMailingListMemberCollection>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/members", mailingListID, segmentID), new { 
                Status = status, 
                Page = page, 
                PageSize = pageSize 
            }).PagedList;
        }

        public Segment FindSegmentByID(Guid mailingListID, int segmentID)
        {
            return _Manager.MakeRequest<Segment>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/details", mailingListID, segmentID));
        }

    }
}