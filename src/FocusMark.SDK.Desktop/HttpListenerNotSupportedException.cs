﻿using System;
using System.Runtime.Serialization;

namespace Focusmark.SDK.Account
{
    [Serializable]
    internal class HttpListenerNotSupportedException : Exception
    {
        public HttpListenerNotSupportedException()
        {
        }

        public HttpListenerNotSupportedException(string message) : base(message)
        {
        }

        public HttpListenerNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HttpListenerNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}