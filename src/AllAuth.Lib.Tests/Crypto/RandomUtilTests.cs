using System;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    [TestFixture]
    public class RandomUtilTests
    {
        [Test]
        public void GenerateRandomString()
        {
            var randomString1 = RandomUtil.GenerateRandomString(10);
            var randomString2 = RandomUtil.GenerateRandomString(10);
            
            Assert.That(randomString1.Length, Is.EqualTo(10));
            Assert.That(randomString2.Length, Is.EqualTo(10));
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }
        
        [Test]
        public void GenerateRandomStringWithOptions()
        {
            var randomStringOptions = 
                RandomUtil.StringGeneratorOptions.Digits |
                RandomUtil.StringGeneratorOptions.LowerCase |
                RandomUtil.StringGeneratorOptions.UpperCase |
                RandomUtil.StringGeneratorOptions.Special;

            var randomString1 = RandomUtil.GenerateRandomString(11, randomStringOptions);
            var randomString2 = RandomUtil.GenerateRandomString(11, randomStringOptions);

            Assert.That(randomString1.Length, Is.EqualTo(11));
            Assert.That(randomString2.Length, Is.EqualTo(11));
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }

        [Test]
        public void GenerateHumanReadableRandomString()
        {
            var randomString1 = RandomUtil.GenerateHumanReadableRandomString(6);
            var randomString2 = RandomUtil.GenerateHumanReadableRandomString(6);

            Assert.That(randomString1.Length, Is.EqualTo(6));
            Assert.That(randomString2.Length, Is.EqualTo(6));
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }

        [Test]
        public void GenerateOtpSecret()
        {
            var randomString1 = RandomUtil.GenerateOtpSecret();
            var randomString2 = RandomUtil.GenerateOtpSecret();

            Assert.That(randomString1, Is.Not.Empty);
            Assert.That(randomString2, Is.Not.Empty);
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }
        
        [Test]
        public void GenerateDatabaseKey()
        {
            var randomString1 = RandomUtil.GenerateDatabaseKey();
            var randomString2 = RandomUtil.GenerateDatabaseKey();

            Assert.That(randomString1, Is.Not.Empty);
            Assert.That(randomString2, Is.Not.Empty);
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }

        [Test]
        public void GenerateRandomByteString()
        {
            var randomString1 = RandomUtil.GenerateRandomByteString(20);
            var randomString2 = RandomUtil.GenerateRandomByteString(20);
            
            Assert.DoesNotThrow(() => Convert.FromBase64String(randomString1));
            Assert.DoesNotThrow(() => Convert.FromBase64String(randomString2));

            Assert.That(randomString1, Is.Not.Empty);
            Assert.That(randomString2, Is.Not.Empty);
            Assert.That(randomString1, Is.Not.EqualTo(randomString2));
        }
    }
}
