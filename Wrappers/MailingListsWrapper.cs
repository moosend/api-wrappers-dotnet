using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities.LinqBridge;
using System.Text;
using Moosend.API.Client.Models;
using Newtonsoft.Json;
using Moosend.API.Client.Serialization;

namespace Moosend.API.Client.Wrappers
{
    public class MailingListsWrapper : IMailingListsWrapper
    {
        private IApiManager _Manager;

        internal MailingListsWrapper(IApiManager manager)
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

        public PagedList<Subscriber> GetSubscribers(Guid mailingListID, SubscribeType status, DateTime? since = null, int page = 1, int pageSize = 500)
        {
            var result = _Manager.MakeRequest<SerializableMailingListMemberCollection>(HttpMethod.GET, String.Format("/lists/{0}/subscribers/{1}", mailingListID, status.ToString()), new
            {
                Since = since,
                Page = page,
                PageSize = pageSize
            }).PagedList;

            // populate custom fields with subscriber id, because it is not returned by the response
            result.ForEach(subscriber => subscriber.CustomFields.ToList().ForEach(cf => cf.SubscriberID = subscriber.ID));
            
            return result;
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
    }
}