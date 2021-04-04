// Copyright (c) 2015-present, Parse, LLC.  All rights reserved.  This source code is licensed under the BSD-style license found in the LICENSE file in the root directory of this source tree.  An additional grant of patent rights can be found in the PATENTS file in the same directory.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Parse.Infrastructure.Extensions;
using Parse.Infrastructure.Utilities;

namespace Parse.Infrastructure.Execution
{
    /// <summary>
    /// ParseCommand is an <see cref="WebRequest"/> with pre-populated
    /// headers.
    /// </summary>
    public class ParseCommand : WebRequest
    {
        public IDictionary<string, object> DataObject { get; private set; }

        public override Stream Data
        {
            get => base.Data ??= DataObject is { } ? new MemoryStream(Encoding.UTF8.GetBytes(JsonUtilities.Encode(DataObject))) : default;
            set => base.Data = value;
        }

        public ParseCommand(string relativeUri, string method, string sessionToken = null, IList<KeyValuePair<string, string>> headers = null, IDictionary<string, object> data = null) : this(relativeUri: relativeUri, method: method, sessionToken: sessionToken, headers: headers, stream: null, contentType: data != null ? "application/json" : null) => DataObject = data;

        public ParseCommand(string relativeUri, string method, string sessionToken = null, IList<KeyValuePair<string, string>> headers = null, Stream stream = null, string contentType = null)
        {
            Path = relativeUri;
            Method = method;
            Data = stream;
            Headers = new List<KeyValuePair<string, string>>(headers ?? Enumerable.Empty<KeyValuePair<string, string>>());

            this.AddHeaderWithValidation("X-Parse-Session-Token", sessionToken);
            this.AddHeaderWithValidation("Content-Type", contentType);
        }

        public ParseCommand(ParseCommand other)
        {
            Resource = other.Resource;
            Path = other.Path;
            Method = other.Method;
            DataObject = other.DataObject;
            Headers = new List<KeyValuePair<string, string>>(other.Headers);
            Data = other.Data;
        }
    }
}
