using System.Text;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    public class EncryptedDataWithPasswordTests
    {
        private const string TestString = "My!Test£String$01234";
        private const string TestPassword = "MyPassword123#1";

        [Test]
        public void EncryptedDataWithPasswordString()
        {
            var encryptedData = new EncryptedDataWithPassword(TestString, TestPassword);
            var encryptedDataString = encryptedData.ToString();
            var decryptedTestString = EncryptedDataWithPassword.DecryptData(TestPassword, encryptedDataString);
            Assert.AreEqual(TestString, decryptedTestString);
        }
        
        [Test]
        public void EncryptedDataWithPasswordBytes()
        {
            var encryptedData = new EncryptedDataWithPassword(Encoding.UTF8.GetBytes(TestString), TestPassword);
            var encryptedDataString = encryptedData.ToString();
            var decryptedTestBytes = EncryptedDataWithPassword.DecryptDataAsBytes(encryptedDataString, TestPassword);
            Assert.AreEqual(TestString, Encoding.UTF8.GetString(decryptedTestBytes));
        }
    }
}
