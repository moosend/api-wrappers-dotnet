using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moosend.Api.Common.Models;

namespace Moosend.Api.Client
{
    public partial class MoosendApiClient
    {
        /// <summary>
        ///     Searches for a subscriber with the specified email address in the specified mailing list and returns detailed information such as id, name, date created, date unsubscribed, status and custom fields.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> CancellationToken. </param>
        public async Task<Subscriber> GetSubscriberByEmailAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Subscriber>(HttpMethod.Get, string.Format("/subscribers/{0}/view", mailingListId), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Searches for a subscriber with the specified unique id in the specified mailing list and returns detailed information such as email, name, date created, date unsubscribed, status and custom fields.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="subcriberId"> The id of the subscriber being searched. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<Subscriber> GetSubscriberByIdAsync(Guid mailingListId, Guid subcriberId, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<Subscriber>(HttpMethod.Get, string.Format("/subscribers/{0}/find/{1}", mailingListId, subcriberId), null, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Adds a new subscriber to the specified mailing list.
        ///     If there is already a subscriber with the specified email address in the list, an update will be performed instead.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="member"> New member's parameters. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns> The new subscriber. </returns>
        public async Task<Subscriber> SubscribeMemberAsync(Guid mailingListId, SubscriberParams member, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = member.Name,
                Email = member.Email,
                CustomFields = member.CustomFields.Select(c => c.Key + "=" + c.Value)
            };

            return await SendAsync<Subscriber>(HttpMethod.Post, string.Format("/subscribers/{0}/subscribe", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Updates a subscriber in the specified mailing list. You can even update the subscribers email, if he has not unsubscribed.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add the new member. </param>
        /// <param name="subscriberId"> The id of the subscriber to be updated. </param>
        /// <param name="updatedMember"> Subscriber parameters to update. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<Subscriber> UpdateMemberAsync(Guid mailingListId, Guid subscriberId, SubscriberParams updatedMember, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Name = updatedMember.Name,
                Email = updatedMember.Email,
                CustomFields = updatedMember.CustomFields.Select(c => c.Key + "=" + c.Value)
            };

            return await SendAsync<Subscriber>(HttpMethod.Post, string.Format("/subscribers/{0}/update/{1}", mailingListId, subscriberId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     This method allows you to add multiple subscribers in a mailing list with a single call. 
        ///     If some subscribers already exist with the given email addresses, they will be updated.
        ///     If you try to add a subscriber with an invalid email address, this attempt will be ignored, as the process will skip to the next subscriber automatically.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to add subscribers to. </param>
        /// <param name="subscribers"> A list of subscribers to add to the mailing list. You may specify the email address, the name and the custom fields for each subscriber. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<IEnumerable<Subscriber>> SubscribeManyAsync(Guid mailingListId, IList<SubscriberParams> subscribers, CancellationToken token = default(CancellationToken))
        {
            var parameters = new
            {
                Subscribers = subscribers.Select(m => new
                {
                    Name = m.Name,
                    Email = m.Email,
                    CustomFields = m.CustomFields.Select(c => c.Key + "=" + c.Value)
                })
            };

            return await SendAsync<IEnumerable<Subscriber>>(HttpMethod.Post, string.Format("/subscribers/{0}/subscribe_many", mailingListId), parameters, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Unsubscribes a subscriber from the specified mailing list and the specified campaign. 
        ///     The subscriber is not deleted, but moved to the supression list. 
        ///     This call will take into account the setting you have in "Supression list and unsubscribe settings" and will remove the subscriber from all other mailing lists or not accordingly.
        /// </summary>
        /// <param name="mailingListId">
        ///     The ID of the mailing list to unsubscribe the subscriber from. 
        ///     If also omitted, the email address of the subscriber will be unsubscribed from all mailing lists. </param>
        /// <param name="campaignId">
        ///     The ID of the campaign from which the subscriber unsubscribed. It can be omitted if no such information is available.
        /// </param>
        /// <param name="email"> The email address of the subscriber to be supressed. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> UnsubscribeMemberAsync(Guid mailingListId, Guid? campaignId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/{1}/unsubscribe", mailingListId, campaignId), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a subscriber from the specified mailing list permanently (without moving to the supression list).
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to search the subscriber in. </param>
        /// <param name="email"> The email address of the subscriber being searched. </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> RemoveMemberAsync(Guid mailingListId, string email, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/remove", mailingListId), new { Email = email }, token).ConfigureAwait(false);
        }

        /// <summary>
        ///     Removes a list of subscribers from the specified mailing list permanently (without putting them in the supression list). 
        ///     Any invalid email addresses specified will be ignored.
        /// </summary>
        /// <param name="mailingListId"> The ID of the mailing list to remove subscribers from. </param>
        /// <param name="emails"> A list of email addresses to be removed </param>
        /// <param name="token"> Cancellation Token. </param>
        /// <returns></returns>
        public async Task<bool> RemoveManyAsync(Guid mailingListId, IList<string> emails, CancellationToken token = default(CancellationToken))
        {
            return await SendAsync<bool>(HttpMethod.Post, string.Format("/subscribers/{0}/remove_many", mailingListId), new { emails = string.Join(",", emails.ToArray()) }, token).ConfigureAwait(false);
        }
    }
}