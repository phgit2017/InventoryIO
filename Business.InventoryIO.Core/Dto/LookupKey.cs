using System;
using System.Collections.Generic;
using System.Text;

namespace Business.InventoryIO.Core.Dto
{
    public static class LookupKey
    {
        public static class OrderType
        {
            public const int Single = 1;
            public const int Batch = 2;
        }

        public static class OrderTransactionType
        {
            public const int PurchaseOrderId = 1;
            public const int SalesOrderId = 2;
            public const int CorrectionId = 3;
            public const int QueueOrderId = 4;
            public const int UpdateProductDetailsId = 5;
        }

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
