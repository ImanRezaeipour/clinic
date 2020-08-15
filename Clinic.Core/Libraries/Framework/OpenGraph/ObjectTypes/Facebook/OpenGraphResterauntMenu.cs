﻿namespace Boilerplate.Web.Mvc.OpenGraph
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// This object type represents a restaurant's menu. A restaurant can have multiple menus, and each menu has multiple sections.
    /// This object type is not part of the Open Graph standard but is used by Facebook.
    /// See https://developers.facebook.com/docs/reference/opengraph/object-type/restaurant.menu/
    /// </summary>
    public class OpenGraphResterauntMenu : OpenGraphMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphResterauntMenu" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="resterauntUrl">The URL to the page about the restaurant who wrote the menu. This URL must contain profile meta tags <see cref="OpenGraphResteraunt"/>.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        /// <exception cref="System.ArgumentNullException">location is <c>null</c>.</exception>
        public OpenGraphResterauntMenu(string title, OpenGraphImage image, string resterauntUrl, string url = null)
            : base(title, image, url)
        {
            if (resterauntUrl == null)
            {
                throw new ArgumentNullException("resterauntUrl");
            }

            this.ResterauntUrl = resterauntUrl;
        }

        /// <summary>
        /// Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "restaurant: http://ogp.me/ns/restaurant#";

        /// <summary>
        /// Gets the URL to the page about the restaurant who wrote the menu. This URL must contain profile meta tags <see cref="OpenGraphResteraunt"/>.
        /// </summary>
        public string ResterauntUrl { get; }

        /// <summary>
        /// Gets or sets the URL's to the pages about the menu sections. This URL must contain restaurant.section meta tags <see cref="OpenGraphResterauntMenuSection"/>.
        /// </summary>
        public IEnumerable<string> SectionUrls { get; set; }

        /// <summary>
        /// Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.RestaurantMenu;

        /// <summary>
        /// Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder"/> containing the Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("restaurant:restaurant", this.ResterauntUrl);

            if (this.SectionUrls != null)
            {
                foreach (string sectionUrl in this.SectionUrls)
                {
                    stringBuilder.AppendMetaPropertyContent("restaurant:section", sectionUrl);
                }
            }
        }
    }
}
