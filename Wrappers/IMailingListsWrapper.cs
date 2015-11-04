using System;
using Moosend.API.Client.Models;

namespace Moosend.API.Client.Wrappers
{
    public interface IMailingListsWrapper
    {
        Guid Create(string name, string confirmationPage = null, string redirectAfterUnsubscribePage = null);
        Guid CreateCustomField(Guid mailingListID, string name, CustomFieldType type, bool isRequired, string context = null);
        void Delete(Guid mailingListID);
        void DeleteCustomField(Guid mailingListID, Guid customFieldID);
        PagedList<MailingList> FindAllActive(int page = 1, int pageSize = 10);
        MailingList FindByID(Guid mailingListID, bool withStatistics = true);
        PagedList<Subscriber> GetSubscribers(Guid mailingListID, SubscribeType status, DateTime? since = default(DateTime?), int page = 1, int pageSize = 500);
        void Save(MailingList list);
        Guid Update(MailingList list);
        void UpdateCustomField(Guid mailingListID, CustomFieldDefinition customField);
    }
}