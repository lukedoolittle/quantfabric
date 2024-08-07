﻿using System;

namespace Material.Framework.Exceptions
{
    public class LocationException : Exception
    {
        public LocationException(string message):  base(message)
        { }

        public LocationException(string message, Exception exception) : 
            base(message, exception)
        { }

        public LocationException() : base()
        { }
    }
}
