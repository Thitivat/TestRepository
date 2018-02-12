using System.Collections.Generic;
using System.Threading.Tasks;

using BND.Services.Matrix.Entities;
using BND.Services.Matrix.Entities.QueryStringModels;
using BND.Services.Matrix.Enums;

namespace BND.Services.Matrix.Proxy.Interfaces
{
    /// <summary>
    /// The MessagesApi interface.
    /// </summary>
    public interface IMessagesApi
    {
        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> of <see cref="List{T}"/> where T is of type <see cref="Message"/>.
        /// </returns>
        Task<List<Message>> GetMessages(MessageFilters filters, string accessToken);

        /// <summary>
        /// Gets the message details.
        /// </summary>
        /// <param name="id"> The message id. </param>
        /// <param name="msgType"> The <see cref="EnumMessageTypes"/> </param>
        /// <param name="accessToken"> The access token. </param>
        /// <returns>
        /// The <see cref="MessageDetail"/>.
        /// </returns>
        Task<MessageDetail> GetMessageDetails(int id, EnumMessageTypes msgType, string accessToken);
    }
}
