using System.Collections.Generic;

namespace BND.Websites.BackOffice.SanctionListsManagement.Web.Common
{
    
    /// <summary>
    /// Class RssItem.
    /// </summary>
    public class RssItem
    {
        /// <summary>
        /// Gets or sets the number of record.
        /// </summary>
        /// <value>The no.</value>
        public int No { get; set; }

        /// <summary>
        /// Gets or sets The title of the item.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets The URL of the item.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the pub date, indicates when the item was published
        /// </summary>
        /// <value>The pub date.</value>
        public string PubDate { get; set; }
    }

    /// <summary>
    /// The RssResponse class 
    /// reference : http://www.rssboard.org/rss-specification#ltttlgtSubelementOfLtchannelgt
    /// </summary>
    public class RssResponse
    {
        /// <summary>
        /// Gets or sets the title of Rss.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description of Rss.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the published date for Rss, Eu has this data, NL didn't have.
        /// </summary>
        /// <value>The pub date.</value>
        public string PubDate { get; set; }

        /// <summary>
        /// Gets or sets the link of Rss.
        /// </summary>
        /// <value>The link.</value>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the language of Rss.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// The collection of items in Rss file.
        /// </summary>
        public ICollection<RssItem> Items;

        /// <summary>
        /// Initializes a new instance of the <see cref="RssResponse"/> class.
        /// </summary>
        public RssResponse()
        {
            Items = new List<RssItem>();
        }
    }
}