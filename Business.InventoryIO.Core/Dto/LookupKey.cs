﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public static class LookupKey
    {
        public static class SessionVariables
        {
            public const string UserId = "UserId";
            public const string UserName = "UserName";
            public const string UserFullName = "UserFullName";
            public const string UserRoleName = "UserRoleName";
        }
    }
    public static class Messages
    {
        public const string ProductCodeValidation = "Duplicate product code.";
        public const string UserNameValidation = "Duplicate username.";
        public const string CustomerCodeValidation = "Duplicate customer code.";
        public const string SupplierCodeValidation = "Duplicate supplier code.";
        public const string ErrorOccuredDuringProcessing = "An error occured during the process. Please check the details, refresh the page, and try again.";
        public const string ServerError = "Server Error";
        public const string SessionUnavailable = "Session is unavailable";
        public const string UnauthorizeAccess = "Unauthorized access";
    }
}
