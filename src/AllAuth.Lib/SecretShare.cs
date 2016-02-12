using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AllAuth.Lib
{
    public class SecretShare
    {
        public SecretShares.SecretShareType Type;
        public string SharedSecret;
        public string EncryptedData;

        public override string ToString()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());
            return JsonConvert.SerializeObject(this, settings);
        }
    }

    public class SecretShares
    {
        public enum SecretShareType
        {
            StringSplit
        }

        public static SecretShare[] SplitKey(Crypto.EncryptedData encryptedData)
        {
            var keySplitIndex = (int)Math.Round((double)encryptedData.Key.Length / 2);

            var split1 = encryptedData.Key.Substring(0, keySplitIndex);
            var split2 = encryptedData.Key.Substring(keySplitIndex);

            var encryptedDataString = encryptedData.ToString();

            return new[]
            {
                new SecretShare { Type = SecretShareType.StringSplit, EncryptedData = encryptedDataString, SharedSecret = split1}, 
                new SecretShare { Type = SecretShareType.StringSplit, EncryptedData = encryptedDataString, SharedSecret = split2}, 
            };
        }

        public static string JoinKey(SecretShare[] shares)
        {
            if (!shares[0].Type.Equals(shares[1].Type))
            {
                throw new Exception("Split type is not the same across shares");
            }
            if (!shares[0].EncryptedData.Equals(shares[1].EncryptedData))
            {
                throw new Exception("Encrypted data not same across shares");
            }

            var splitType = shares[0].Type;
            var splitKey1 = shares[0].SharedSecret;
            var splitKey2 = shares[1].SharedSecret;
            var encryptedData = shares[0].EncryptedData;

            // TODO: handle different kinds of splits
            var joinedKey = splitKey1 + splitKey2;

            return Crypto.EncryptedData.DecryptData(joinedKey, encryptedData);
        }

        public static SecretShare DeserializeSecretShare(string serializedSecretShare)
        {
            return JsonConvert.DeserializeObject<SecretShare>(serializedSecretShare);
        }
    }
}
