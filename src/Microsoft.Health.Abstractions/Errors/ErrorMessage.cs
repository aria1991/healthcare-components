// -------------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License (MIT). See LICENSE in the repo root for license information.
// -------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Microsoft.Health.Abstractions.Errors
{
    public class ErrorMessage
    {
        /// <summary>
        /// A unique identifier for the ErrorMessage
        /// <remarks>
        /// This Id can be generated by the caller, or left null.
        /// If null, the IErrorMessageService will generate an Id.
        /// </remarks>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The type of error.
        /// </summary>
        public string ErrorType { get; set; }

        /// <summary>
        /// A string describing the error that occurred.
        /// </summary>
        public string ErrorString { get; set; }

        /// <summary>
        /// The time when the error occurred.
        /// </summary>
        /// <remarks>
        /// This property represents the time the error occurred. It is not the time that the error was written to the destination.
        /// </remarks>
        public DateTimeOffset ErrorTime { get; set; }

        /// <summary>
        /// A property bag for data which can provide additional context for the error.
        /// </summary>
        public IDictionary<string, object> Values { get; private set; }

        public void SetValues(IDictionary<string, object> dictionary)
        {
            Values = dictionary;
        }
    }
}
