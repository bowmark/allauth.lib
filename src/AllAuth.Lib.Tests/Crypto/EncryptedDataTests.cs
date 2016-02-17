using System.Text;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    public class EncryptedDataTests
    {
        private const string TestString = "My!Test£String$01234";

        [Test]
        public void EncryptedDataString()
        {
            var encryptedData = new EncryptedData(TestString);
            var encryptedDataString = encryptedData.ToString();
            var decryptedTestString = EncryptedData.DecryptData(encryptedData.Key, encryptedDataString);
            Assert.AreEqual(TestString, decryptedTestString);
        }
        
        [Test]
        public void EncryptedDataBytes()
        {
            var encryptedData = new EncryptedData(Encoding.UTF8.GetBytes(TestString));
            var encryptedDataString = encryptedData.ToString();
            var decryptedTestStringBytes = EncryptedData.DecryptDataAsBytes(encryptedData.Key, encryptedDataString);
            Assert.AreEqual(TestString, Encoding.UTF8.GetString(decryptedTestStringBytes));
        }
    }
}
