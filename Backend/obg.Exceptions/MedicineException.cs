﻿using System;
using System.Runtime.Serialization;

namespace obg.Exceptions
{
    [Serializable]
    public class MedicineException : Exception
    {
        public MedicineException()
        {
        }

        public MedicineException(string message) : base(message) { }

    }
}
