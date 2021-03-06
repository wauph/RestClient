﻿using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

namespace Rest.Client.Serializers
{
    public class JsonMediaTypeSerializer : IMediaTypeSerializer
    {
        private readonly List<string> m_supportedMediaTypes;
        private readonly JsonSerializer _jsonSerializer;

        public JsonMediaTypeSerializer()
        {
            _jsonSerializer = new JsonSerializer();
            m_supportedMediaTypes = new List<string> { MediaTypes.ApplicationJson, MediaTypes.TextJson };
        }

        public IEnumerable<string> SupportedMediaTypes
        {
            get { return m_supportedMediaTypes; }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return _jsonSerializer.Deserialize<T>(jsonTextReader);
            }
        }

        public object Serialize(object body)
        {
            return JsonConvert.SerializeObject(body);
        }
    }
}