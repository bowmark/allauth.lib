using System;
using System.IO;
using System.Text;

namespace AllAuth.Lib.Crypto
{
    public class EncryptedDataWithPassword
    {
        public enum KeyDerivationMethods
        {
            Pbkdf2Sha1
        }

        public KeyDerivationMethods KeyDerivationMethod { get; private set; }
        public int KeyDerivationIterations { get; private set; }
		public string KeyDerivationsSalt { get { return Convert.ToBase64String(_keyDerivationSalt); } }

        private const KeyDerivationMethods DefaultKeyDerivationMethod = KeyDerivationMethods.Pbkdf2Sha1;

        /// <summary>
        /// This isn't used to protect the password, it's only used to generate a key suitable for the 
        /// selected encryption method. 
        /// 
        /// Do not rely on this as a true password derivation iteration count. 
        /// </summary>
        private const int DefaultKeyIterations = 1;

        private EncryptedData _encryptedData;
        private byte[] _keyDerivationSalt;

        public EncryptedDataWithPassword(string dataToEncrypt, string password)
        {
            EncryptData(Encoding.UTF8.GetBytes(dataToEncrypt), password);
        }

        public EncryptedDataWithPassword(byte[] dataToEncrypt, string password)
        {
            EncryptData(dataToEncrypt, password);
        }

        private void EncryptData(byte[] dataToEncrypt, string password)
        {
            KeyDerivationMethod = DefaultKeyDerivationMethod;
            KeyDerivationIterations = DefaultKeyIterations;

            string salt;
            var hash = PasswordHash.CreateHash(password, KeyDerivationIterations, out salt);
            _keyDerivationSalt = Convert.FromBase64String(salt);
            _encryptedData = new EncryptedData(dataToEncrypt, Convert.FromBase64String(hash));
        }

        public override string ToString()
        {
            using (var memStream = new MemoryStream())
            {
                if (DefaultKeyIterations.GetType() != typeof(int))
                    throw new Exception("Expected key interation value to be int32");

                memStream.Write(BitConverter.GetBytes(KeyDerivationIterations), 0, 4);
                memStream.WriteByte((byte)_keyDerivationSalt.Length);
                memStream.Write(_keyDerivationSalt, 0, _keyDerivationSalt.Length);

                var encryptedData = _encryptedData.ToBytes();
                memStream.Write(encryptedData, 0, encryptedData.Length);
                
                return Convert.ToBase64String(memStream.ToArray());
            }
        }

        public static string DecryptData(string password, string serializedEncryptedData)
        {
            return Encoding.UTF8.GetString(DecryptDataAsBytes(serializedEncryptedData, password));
        }

        public static byte[] DecryptDataAsBytes(string serializedEncryptedData, string password)
        {
            var encryptedData = Convert.FromBase64String(serializedEncryptedData);

            using (var encryptedDataStream = new MemoryStream(encryptedData))
            using(var encryptedDataWithoutPassword = new MemoryStream())
            {
                var iterationsBytes = new byte[4];
                var bytesRead = encryptedDataStream.Read(iterationsBytes, 0, 4);
                if (bytesRead != 4)
                    throw new Exception("Unexpected end of encrypted data (expected iteration count)");
                var iterations = BitConverter.ToInt32(iterationsBytes, 0);

                var saltNumBytes = encryptedDataStream.ReadByte();
                if (saltNumBytes == -1)
                    throw new Exception("Unexpected end of encrypted data (expected salt size)");

                var salt = new byte[saltNumBytes];
                bytesRead = encryptedDataStream.Read(salt, 0, saltNumBytes);
                if (bytesRead != saltNumBytes)
                    throw new Exception("Unexpected end of encrypted data (expected salt)");
                
                var encryptionKey = PasswordHash.CreateHash(password, iterations, Convert.ToBase64String(salt));
                
                encryptedDataStream.CopyTo(encryptedDataWithoutPassword);
                
                return EncryptedData.DecryptDataAsBytes(
                    Convert.FromBase64String(encryptionKey), encryptedDataWithoutPassword.ToArray());
            }
        }
    }
}