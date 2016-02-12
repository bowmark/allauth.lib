using System;

namespace AllAuth.Lib.Utils
{
    public static class Validation
    {
        public static bool EmailAddress(string address)
        {
            // Simple email address validation: http://stackoverflow.com/a/1374644
            try
            {
                var addressCheck = new System.Net.Mail.MailAddress(address);
                if (!addressCheck.Address.Equals(address))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
