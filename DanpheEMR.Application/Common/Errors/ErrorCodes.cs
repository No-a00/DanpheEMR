namespace DanpheEMR.Application.Common.Errors
{
    public static class ErrorCodes
    {
        public static class Patients
        {
            public const string NotFound = "Patient.NotFound";
            public const string CodeAlreadyExists = "Patient.CodeAlreadyExists";
        }

        public static class Pharmacy
        {
            public const string OutOfStock = "Pharmacy.OutOfStock";
            public const string BatchExpired = "Pharmacy.BatchExpired";
        }
    }
}