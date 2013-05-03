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
            var lists = _Manager.MakeRequest<SerializableMailingListCollection>(HttpMethod.GET, String.Format("/lists/{0}/{1}", page, pageSize)).PagedList;
            foreach (MailingList list in lists)
            {
                foreach (CustomFieldDefinition customField in list.CustomFieldsDefinition)
                {
                    customField.MailingListID = list.ID;
                }
            }
            return lists;
        }

        public Guid Create(String name, String confirmationPage = null, String redirectAfterUnsubscribePage = null)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, "/lists/create", new { 
                Name = name, 
                ConfirmationPage = confirmationPage, 
                RedirectAfterUnsubscribePage = redirectAfterUnsubscribePage 
            });
        }

        public Guid Update(MailingList list)
        {
            return _Manager.MakeRequest<Guid>(HttpMethod.POST, String.Format( "/lists/{0}/update", list.ID), list);
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
            var list = _Manager.MakeRequest<MailingList>(HttpMethod.GET, String.Format("/lists/{0}/details", mailingListID), new { 
                withStatistics = withStatistics 
            });
            foreach (CustomFieldDefinition customField in list.CustomFieldsDefinition)
            {
                customField.MailingListID = list.ID;
            }
            return list;
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
            var segments = _Manager.MakeRequest<IList<Segment>>(HttpMethod.GET, String.Format("/lists/{0}/segments", mailingListID));
            foreach (Segment s in segments)
            {
                s.MailingListID = mailingListID;
            }
            return segments;
        }

        private int CreateSegment(Guid mailingListID, String name, SegmentMatchType matchType = SegmentMatchType.All)
        {
            return _Manager.MakeRequest<int>(HttpMethod.POST, String.Format("/lists/{0}/segments/create", mailingListID), new { 
                Name = name,
                MatchType = matchType
            });
        }

        private void UpdateSegment(Segment segment)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/update", segment.MailingListID, segment.ID), new
            {
                Name = segment.Name,
                MatchType = segment.MatchType
            });
        }

        public void DeleteSegment(Guid mailingListID, int segmentID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments/{1}/delete", mailingListID, segmentID));
        }

        public void Save(MailingList list)
        {         
            // do some validation
            foreach (CustomFieldDefinition customField in list.CustomFieldsDefinition)
            {
                if (customField.MailingListID != list.ID && customField.MailingListID != Guid.Empty)
                {
                    throw new InvalidOperationException("Cannot save custom field that belongs to another mailing list");
                }
            }

            if (list.ID == Guid.Empty)
            {
                list.ID = Create(list.Name, list.ConfirmationPage, list.RedirectAfterUnsubscribePage);

                foreach (CustomFieldDefinition customField in list.CustomFieldsDefinition)
                {
                    CreateCustomField(list.ID, customField.Name, customField.Type, customField.IsRequired, customField.Context);
                }
            }
            else
            {
                Update(list);

                MailingList existing = FindByID(list.ID);

                List<Guid> existingCustomFieldIDs = existing.CustomFieldsDefinition.Select(x => x.ID).ToList();
                List<Guid> currentCustomFieldIDs = list.CustomFieldsDefinition.Where(x => x.ID != Guid.Empty).Select(x => x.ID).ToList();

                // find with custom fields where deleted
                foreach (Guid idToDelete in existingCustomFieldIDs.Where(id => !currentCustomFieldIDs.Contains(id)))
                {
                    DeleteCustomField(list.ID, idToDelete);
                }

                // find which custom fields where modified
                foreach (Guid idToUpdate in currentCustomFieldIDs.Where(id => existingCustomFieldIDs.Contains(id)))
                {
                    CustomFieldDefinition customField = list.CustomFieldsDefinition.Single(c => c.ID == idToUpdate);
                    UpdateCustomField(list.ID, customField);
                }

                // find which custom fields where added
                foreach (CustomFieldDefinition customField in list.CustomFieldsDefinition.Where(x => x.ID == Guid.Empty))
                {
                    CreateCustomField(list.ID, customField.Name, customField.Type, customField.IsRequired, customField.Context);
                }

            }

            MailingList reloaded = FindByID(list.ID, true);
            Utilities.CopyProperties<MailingList>(reloaded, list);
        }

        public void SaveSegment(Segment segment)
        {
            // do some validation
            foreach (SegmentCriteria criteria in segment.Criteria)
            {
                if (criteria.SegmentID != segment.ID && criteria.SegmentID != 0)
                {
                    throw new InvalidOperationException("Cannot save criteria that belong to another segment");
                }
            }

            if (segment.ID == 0)
            {
                // we are inserting a new segment
                segment.ID = CreateSegment(segment.MailingListID, segment.Name, segment.MatchType);

                // inserting criteria for the new segment as well
                foreach (SegmentCriteria criteria in segment.Criteria)
                {
                    String field = criteria.Field.ToString();
                    if (criteria.Field == SegmentCriteriaField.CustomField) field = criteria.CustomFieldID.Value.ToString();
                    AddSegmentCriteria(segment.MailingListID, segment.ID, field, criteria.Comparer, criteria.Value, criteria.DateFrom, criteria.DateTo);
                }
            }
            else
            {
                // or we are updating an existing segment
                UpdateSegment(segment);

                // updating criteria for the existing segment as well
                // we will load the existing segment from the database to compare it with our modified segment
                Segment existing = FindSegmentByID(segment.MailingListID, segment.ID);

                List<int> existingCriteriaIDs = existing.Criteria.Select(x => x.ID).ToList();
                List<int> segmentCriteriaIDs = segment.Criteria.Where(x => x.ID != 0).Select(x => x.ID).ToList();

                // find with criteria where deleted
                foreach (int idToDelete in existingCriteriaIDs.Where(id => !segmentCriteriaIDs.Contains(id)))
                {
                    RemoveSegmentCriteria(segment.MailingListID, segment.ID, idToDelete);
                }

                // find which criteria where modified
                foreach (int idToUpdate in segmentCriteriaIDs.Where(id => existingCriteriaIDs.Contains(id)))
                {
                    SegmentCriteria criteria = segment.Criteria.Single(c => c.ID == idToUpdate);
                    UpdateSegmentCriteria(segment.MailingListID, segment.ID, criteria);
                }

                // find which criteria where added
                foreach (SegmentCriteria criteria in segment.Criteria.Where(x => x.ID == 0))
                {
                    String field = criteria.Field.ToString();
                    if (criteria.Field == SegmentCriteriaField.CustomField) field = criteria.CustomFieldID.Value.ToString();
                    AddSegmentCriteria(segment.MailingListID, segment.ID, field, criteria.Comparer, criteria.Value, criteria.DateFrom, criteria.DateTo);
                }
            }

            // reload segment to populate the object with all properties
            Segment reloaded = FindSegmentByID(segment.MailingListID, segment.ID);
            Utilities.CopyProperties<Segment>(reloaded, segment);
        }


        private int AddSegmentCriteria(Guid mailingListID, int segmentID, String field, SegmentCriteriaComparer comparer, String value, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return _Manager.MakeRequest<int>(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/add", mailingListID, segmentID), new { 
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }

        private void UpdateSegmentCriteria(Guid mailingListID, int segmentID, SegmentCriteria criteria)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/{2}/update", mailingListID, segmentID, criteria.ID), new {
                Field = criteria.Field,
                Comparer = criteria.Comparer,
                Value = criteria.Value,
                DateFrom = criteria.DateFrom,
                DateTo = criteria.DateTo            
            });
        }

        private void RemoveSegmentCriteria(Guid mailingListID, int segmentID, int criteriaID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments/{1}/criteria/{2}/delete", mailingListID, segmentID, criteriaID));
        }

        public PagedList<MailingListMember> GetSegmentMembers(Segment segment, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500)
        {
            return GetSegmentMembers(segment.MailingListID, segment.ID, status, page, pageSize);
        }

        public PagedList<MailingListMember> GetSegmentMembers(Guid mailingListID, int segmentID, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500)
        {
            return _Manager.MakeRequest<SerializableMailingListMemberCollection>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/members", mailingListID, segmentID), new { 
                Status = status, 
                Page = page, 
                PageSize = pageSize 
            }).PagedList;
        }

        public Segment FindSegmentByID(Guid mailingListID, int segmentID)
        {
            var segment = _Manager.MakeRequest<Segment>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/details", mailingListID, segmentID));
            segment.MailingListID = mailingListID;
            return segment;
        }

    }
}