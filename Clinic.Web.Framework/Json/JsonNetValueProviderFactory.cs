﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Clinic.FrameWork.Json
{
    /// <summary>
    /// </summary>
    public class JsonNetValueProviderFactory : ValueProviderFactory
    {
        /// <summary>
        /// </summary>
        public JsonNetValueProviderFactory()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error,
                Converters = {new ExpandoObjectConverter()}
            };
        }

        /// <summary>
        /// </summary>
        public JsonSerializerSettings Settings { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            if (controllerContext.HttpContext == null ||
                controllerContext.HttpContext.Request == null ||
                controllerContext.HttpContext.Request.ContentType == null)
            {
                return null;
            }

            if (!controllerContext.HttpContext.Request.ContentType.StartsWith(
                "application/json", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (!controllerContext.HttpContext.Request.IsAjaxRequest())
            {
                return null;
            }

            using (var streamReader = new StreamReader(controllerContext.HttpContext.Request.InputStream))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    if (!jsonReader.Read())
                        return null;

                    var jsonSerializer = JsonSerializer.Create(Settings);

                    object jsonObject;
                    switch (jsonReader.TokenType)
                    {
                        case JsonToken.StartArray:
                            jsonObject = jsonSerializer.Deserialize<List<ExpandoObject>>(jsonReader);
                            break;
                        default:
                            jsonObject = jsonSerializer.Deserialize<ExpandoObject>(jsonReader);
                            break;
                    }

                    var backingStore = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    AddToBackingStore(backingStore, string.Empty, jsonObject);
                    return new DictionaryValueProvider<object>(backingStore, CultureInfo.CurrentCulture);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="backingStore"></param>
        /// <param name="prefix"></param>
        /// <param name="value"></param>
        private static void AddToBackingStore(IDictionary<string, object> backingStore, string prefix, object value)
        {
            var dictionary = value as IDictionary<string, object>;
            if (dictionary != null)
            {
                foreach (var entry in dictionary)
                {
                    AddToBackingStore(backingStore, MakePropertyKey(prefix, entry.Key), entry.Value);
                }
                return;
            }

            var list = value as IList;
            if (list != null)
            {
                for (var index = 0; index < list.Count; index++)
                {
                    AddToBackingStore(backingStore, MakeArrayKey(prefix, index), list[index]);
                }
                return;
            }

            backingStore[prefix] = value;
        }

        /// <summary>
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string MakeArrayKey(string prefix, int index)
        {
            return prefix + "[" + index.ToString(CultureInfo.InvariantCulture) + "]";
        }

        /// <summary>
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static string MakePropertyKey(string prefix, string propertyName)
        {
            return string.IsNullOrWhiteSpace(prefix) ? propertyName : prefix + "." + propertyName;
        }
    }
}