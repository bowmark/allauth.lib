using System.Text.RegularExpressions;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    public class PasswordHashTests
    {
        private const string TestString = "My!Test£String$01234";

        [Test]
        public void PasswordHashDefault()
        {
            var hash = PasswordHash.CreateHash(TestString);

            Assert.IsNotEmpty(hash);
            Assert.IsTrue(Regex.IsMatch(hash, "^\\d+:[A-Za-z0-9+/=]+:[A-Za-z0-9+/=]+$"));
            Assert.IsTrue(PasswordHash.ValidatePassword(TestString, hash));
        }

        [Test]
        public void PasswordHashFixedIterations()
        {
            var hash = PasswordHash.CreateHash(TestString, 101);

            Assert.IsNotEmpty(hash);
            Assert.IsTrue(Regex.IsMatch(hash, "^\\d+:[A-Za-z0-9+/=]+:[A-Za-z0-9+/=]+$"));
            Assert.IsTrue(PasswordHash.ValidatePassword(TestString, hash));
        }

        [Test]
        public void PasswordHashFixedIterationsOutputSalt()
        {
            string salt;
            var hash = PasswordHash.CreateHash(TestString, 102, out salt);

            Assert.IsNotEmpty(hash);
            Assert.IsTrue(PasswordHash.ValidatePassword(TestString, "102:"+salt+":"+hash));
        }

        [Test]
        public void PasswordHashFixedIterationsAndSalt()
        {
            // Fixed iterations and salt leads to a deterministic output
            const int iterations = 1000;
            const string salt = "lfMbfWe/SAKgp6dCYU1HKw==";

            const string expectedHash = "mDy8GkpL+1VxrAdMF9ACKugShmln637M6SjWJUYpG+Q=";
            var hash = PasswordHash.CreateHash(TestString, iterations, salt);

            Assert.AreEqual(hash, expectedHash);
        }
    }
}
