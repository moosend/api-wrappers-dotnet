using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moosend.API.Client.Models;
using Moosend.API.Client.Serialization;

namespace Moosend.API.Client.Wrappers
{
    public class SegmentsWrapper
    {
        private ApiManager _Manager;

        internal SegmentsWrapper(ApiManager manager)
        {
            _Manager = manager;
        }

        public IList<Segment> FindAllForMailingList(Guid mailingListID)
        {
            var segments = _Manager.MakeRequest<IList<Segment>>(HttpMethod.GET, String.Format("/lists/{0}/segments", mailingListID));
            foreach (Segment s in segments)
            {
                s.MailingListID = mailingListID;
            }
            return segments;
        }

        private int Create(Guid mailingListID, String name, SegmentMatchType matchType = SegmentMatchType.All)
        {
            return _Manager.MakeRequest<int>(HttpMethod.POST, String.Format("/lists/{0}/segments/create", mailingListID), new
            {
                Name = name,
                MatchType = matchType
            });
        }

        private void Update(Segment segment)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/update", segment.MailingListID, segment.ID), new
            {
                Name = segment.Name,
                MatchType = segment.MatchType
            });
        }

        public void Delete(Guid mailingListID, int segmentID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments/{1}/delete", mailingListID, segmentID));
        }

        public void Save(Segment segment)
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
                segment.ID = Create(segment.MailingListID, segment.Name, segment.MatchType);

                // inserting criteria for the new segment as well
                foreach (SegmentCriteria criteria in segment.Criteria)
                {
                    String field = criteria.Field.ToString();
                    if (criteria.Field == SegmentCriteriaField.CustomField) field = criteria.CustomFieldID.Value.ToString();
                    AddCriteria(segment.MailingListID, segment.ID, field, criteria.Comparer, criteria.Value, criteria.DateFrom, criteria.DateTo);
                }
            }
            else
            {
                // or we are updating an existing segment
                Update(segment);

                // updating criteria for the existing segment as well
                // we will load the existing segment from the database to compare it with our modified segment
                Segment existing = FindByID(segment.MailingListID, segment.ID);

                List<int> existingCriteriaIDs = existing.Criteria.Select(x => x.ID).ToList();
                List<int> segmentCriteriaIDs = segment.Criteria.Where(x => x.ID != 0).Select(x => x.ID).ToList();

                // find with criteria where deleted
                foreach (int idToDelete in existingCriteriaIDs.Where(id => !segmentCriteriaIDs.Contains(id)))
                {
                    RemoveCriteria(segment.MailingListID, segment.ID, idToDelete);
                }

                // find which criteria where modified
                foreach (int idToUpdate in segmentCriteriaIDs.Where(id => existingCriteriaIDs.Contains(id)))
                {
                    SegmentCriteria criteria = segment.Criteria.Single(c => c.ID == idToUpdate);
                    UpdateCriteria(segment.MailingListID, segment.ID, criteria);
                }

                // find which criteria where added
                foreach (SegmentCriteria criteria in segment.Criteria.Where(x => x.ID == 0))
                {
                    String field = criteria.Field.ToString();
                    if (criteria.Field == SegmentCriteriaField.CustomField) field = criteria.CustomFieldID.Value.ToString();
                    AddCriteria(segment.MailingListID, segment.ID, field, criteria.Comparer, criteria.Value, criteria.DateFrom, criteria.DateTo);
                }
            }

            // reload segment to populate the object with all properties
            Segment reloaded = FindByID(segment.MailingListID, segment.ID);
            Utilities.CopyProperties<Segment>(reloaded, segment);
        }


        private int AddCriteria(Guid mailingListID, int segmentID, String field, SegmentCriteriaComparer comparer, String value, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            return _Manager.MakeRequest<int>(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/add", mailingListID, segmentID), new
            {
                Field = field,
                Comparer = comparer,
                Value = value,
                DateFrom = dateFrom,
                DateTo = dateTo
            });
        }

        private void UpdateCriteria(Guid mailingListID, int segmentID, SegmentCriteria criteria)
        {
            _Manager.MakeRequest(HttpMethod.POST, String.Format("/lists/{0}/segments/{1}/criteria/{2}/update", mailingListID, segmentID, criteria.ID), new
            {
                Field = criteria.Field,
                Comparer = criteria.Comparer,
                Value = criteria.Value,
                DateFrom = criteria.DateFrom,
                DateTo = criteria.DateTo
            });
        }

        private void RemoveCriteria(Guid mailingListID, int segmentID, int criteriaID)
        {
            _Manager.MakeRequest(HttpMethod.DELETE, String.Format("/lists/{0}/segments/{1}/criteria/{2}/delete", mailingListID, segmentID, criteriaID));
        }

        public PagedList<Subscriber> GetMembers(Segment segment, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500)
        {
            return GetMembers(segment.MailingListID, segment.ID, status, page, pageSize);
        }

        public PagedList<Subscriber> GetMembers(Guid mailingListID, int segmentID, SubscribeType status = SubscribeType.Subscribed, int page = 1, int pageSize = 500)
        {
            return _Manager.MakeRequest<SerializableMailingListMemberCollection>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/members", mailingListID, segmentID), new
            {
                Status = status,
                Page = page,
                PageSize = pageSize
            }).PagedList;
        }

        public Segment FindByID(Guid mailingListID, int segmentID)
        {
            var segment = _Manager.MakeRequest<Segment>(HttpMethod.GET, String.Format("/lists/{0}/segments/{1}/details", mailingListID, segmentID));
            segment.MailingListID = mailingListID;
            return segment;
        }

    }
}