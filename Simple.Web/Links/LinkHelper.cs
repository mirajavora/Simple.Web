﻿namespace Simple.Web.Links
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;

    /// <summary>
    /// Helper methods for working with RESTful links.
    /// </summary>
    public static class LinkHelper
    {
        private static readonly ConcurrentDictionary<Type, ILinkBuilder> LinkBuilders = new ConcurrentDictionary<Type, ILinkBuilder>();

        /// <summary>
        /// Gets the links for a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A readonly <see cref="ICollection&lt;Link&gt;"/> containing all available links for the model.</returns>
        public static ICollection<Link> GetLinksForModel(object model)
        {
            if (model == null) throw new ArgumentNullException("model");

            return LinkBuilders.GetOrAdd(model.GetType(), CreateBuilder).LinksForModel(model);
        }

        /// <summary>
        /// Gets the canonical link for a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A <see cref="Link"/> object representing the canonical URI for the model, if one is found.</returns>
        public static Link GetCanonicalLinkForModel(object model)
        {
            if (model == null) throw new ArgumentNullException("model");

            return LinkBuilders.GetOrAdd(model.GetType(), CreateBuilder).CanonicalForModel(model);
        }

        private static ILinkBuilder CreateBuilder(Type modelType)
        {
            var linkList = new List<Link>();
            foreach (var type in ExportedTypeHelper.FromCurrentAppDomain(LinkAttributeBase.Exists))
            {
                var attributesForModel = LinkAttributeBase.Get(type, modelType);
                if (attributesForModel.Count == 0) continue;
                linkList.AddRange(attributesForModel.Select(a => new Link(type, a.UriTemplate, a.GetRel(), a.Type, a.Title)));
            }

            if (linkList.Count > 0)
            {
                return new LinkBuilder(linkList);
            }

            return LinkBuilder.Empty;
        }
    }

}