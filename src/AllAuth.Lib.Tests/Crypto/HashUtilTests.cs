using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    [TestFixture]
    public class HashUtilTests
    {
        private const string TestEmail = "my.email+address@example.com";
        private const string TestString = "My!Test£String$01234";
        
        [Test]
        public void Sha256()
        {
            const string expectedHash = "95JEluN3rPNcb8GZSQevYsBxZ++3Dg+tO3Bu61JkLok=";
            var hash = HashUtil.Sha256(TestString);
            Assert.AreEqual(hash, expectedHash);
        }

        [Test]
        public void Sha512()
        {
            const string expectedHash = "raSyontAIKcz/u7O7VeZC6dnbh8nCIpvRML6pP1mIDICg4FDmPXbtOOhtYS81RCB0yzcuY27zEEy5UXnZ6q2Uw==";
            var hash = HashUtil.Sha512(TestString);
            Assert.AreEqual(hash, expectedHash);
        }

        [Test]
        public void GenerateServerManagerRecoveryPasswordHash()
        {
            const string expectedHash = "oYbw1O4KdzhTqG6Na/x7K3+9VJ9cUEydA7Y6hX2J/A8=";
            var hash = HashUtil.GenerateServerManagerRecoveryPasswordHash(TestEmail, TestString);
            Assert.AreEqual(hash, expectedHash);
        }
        
        [Test]
        public void GenerateServerRecoveryPasswordHash()
        {
            const string expectedHash = "FSde/d4dRX1Pcv/Z5B6H7qK7iYqf+TL7LTusIPE3ApE=";
            var hash = HashUtil.GenerateServerRecoveryPasswordHash(TestEmail, TestString);
            Assert.AreEqual(hash, expectedHash);
        }

        [Test]
        public void GenerateDatabaseBackupPasswordHash()
        {
            const string expectedHash = "RWWTotNptlhNXBtw7nEJac+wVFwpamxixb9PyFXlRMY=";
            var hash = HashUtil.GenerateDatabaseBackupPasswordHash(TestEmail, TestString);
            Assert.AreEqual(hash, expectedHash);
        }

        [Test]
        public void GenerateDeviceBackupPasswordCheckHash()
        {
            const string expectedHash = "2+NklvsydrnVc21cNRVTDkZzCxfOlQ0mmrwLwXkaiTk=";
            var hash = HashUtil.GenerateDeviceBackupPasswordCheckHash(TestEmail, TestString);
            Assert.AreEqual(hash, expectedHash);
        }

        [Test]
        public void GenerateDeviceBackupPasswordHash()
        {
            const string expectedHash = "J3FYY8bD0X1qObbJAx2udjWGGmVvPZcT+cjGClnOihg=";
            var hash = HashUtil.GenerateDeviceBackupPasswordHash(TestEmail, TestString);
            Assert.AreEqual(hash, expectedHash);
        }
    }
}
