using System;
using System.ComponentModel;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    [TestFixture]
    public class AsymmetricCryptoUtilTests
    {
        private const string TestString = "My!Test£String$01234";

        private const string RsaPublicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlHupFc5Q7fwng63EEFvu\r\nLUBXcYkoHjFTqLIQQA3LFjj69pfYKy2xdr9n7uQseeH2+1/Ik+CkIiP5uA8V1T1K\r\nyeh702i5LeykxFhSFDeEZXm94FK6iXyOSgrCSIIxsi72sXgWjZGKEfwJDw+4qxpQ\r\nwWxgKH4Ky5MAVpoJU/FmoEpDeg8WDaLqK8f7hM8j/lWxbJOFYbn2Dgu9FmAkw904\r\n5lwadDyau9qWOwrusf3cH1upKdL4vNMml2XykYYJo5qXA9g0sYwv/R5ZFZ67U780\r\nTebYIjr0bna+77Bsg04jgWYdQ8/J1Ec8p11TfC8x7MJnsLtE7EJfP/6jFDY4H0jF\r\nawIDAQAB\r\n-----END PUBLIC KEY-----";
        private const string RsaPrivateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCUe6kVzlDt/CeD\r\nrcQQW+4tQFdxiSgeMVOoshBADcsWOPr2l9grLbF2v2fu5Cx54fb7X8iT4KQiI/m4\r\nDxXVPUrJ6HvTaLkt7KTEWFIUN4Rleb3gUrqJfI5KCsJIgjGyLvaxeBaNkYoR/AkP\r\nD7irGlDBbGAofgrLkwBWmglT8WagSkN6DxYNouorx/uEzyP+VbFsk4VhufYOC70W\r\nYCTD3TjmXBp0PJq72pY7Cu6x/dwfW6kp0vi80yaXZfKRhgmjmpcD2DSxjC/9HlkV\r\nnrtTvzRN5tgiOvRudr7vsGyDTiOBZh1Dz8nURzynXVN8LzHswmewu0TsQl8//qMU\r\nNjgfSMVrAgMBAAECggEAFK/pSLykLDwc70Ixq7Z7Mnp2lGlvxEvKuZlcHT3ZZTZk\r\nH3Yxa8v5H9p0FbsG8qZHvbxvGsyfYZL8kvx7EL4c4jujVjrkI0tcphnIucCwZV5M\r\nVOmBzBpP+BiT+qaKW6Z3wBXDTU8RMFyakIAivP1fcfYdAZc/VB9I2p1Uc7Rm6tRS\r\nJ1nileKc9kFGdXpV5qEuKMbqESf9Lh48OvJflRxGQTe1R019dxRFHUX5RjRJT2bH\r\n7/KPMXob9tn2Zs0JV0CJK39lwY9kvJGPihTJLS8mEi8D5CqrfImCYGvhB/PvryFs\r\nkLxZOCTCpywvU85pyxy9QTBaidd9vQW3GSRM3jzxaQKBgQDRan1+hWlBhN974SuT\r\ni7aehXkgj2LZKiecYtrHgHwj3JY9AzY0lvhWwP+bMijLziS4l2krcybU7/ehqt6D\r\nJ4KaJVnQZZSzrIbzbJkM1GxKXGQPrvUxc3jx9I84ljJNT0OO3HUyGkbVqC4P7ncy\r\nGFc/wIIuVMUtsy0lwYPeoKDTOQKBgQC1g0CfiwhenW825ScgQScYkUCMCxJCRjzM\r\ncNBL9J98l7OGOlOuB1cg1MreObg/ALHgaC0DlupiiIgbhoIjjG27O95ecC+pxeJ4\r\n3vEmwyn7cW+fVECuw8SrLNT3nUBVCsg+d1P1Y31dGznd0nHDoRPVkDn0qDBXnUqR\r\nbMAdi9HpwwKBgDF1Swo0i/VTYkypk1cXahvqPf84ngnX8N74Yd84MxltIXZracPl\r\n2+TxU+zdXdE0cGvAJrCCU6t6uStPZZnoHOo25A1Z0FLW7lBTV5o2GRU+2MrzcSkq\r\nsmfCcIjWwC6OB9CXtH4Owqf3Plm44iHMqd5+osA6eQ8gAAuotnI/0XvRAoGBAIGh\r\n1IRdEkQoYeYtsEzL2zUjf5rEyWyFpyRHKawlb8XWvgMOIvc2DNbIwn4FcP2l7GOd\r\nYxCeNpTad4JZBRaIU1IWBtEhhBjW19CFQGrzlUcbk6GR1YQ7EnlB0nNpXimeQrbC\r\nGDc3r6/QIvq2MkOKBZVPpOSDPctE7Z2AspAnbGprAoGBALVYPfhlv3ISUJP6Ajlu\r\ncyZpXePCCyYjPgvUb7MKhIjM1XI+j1w3WZ4DdLT28Kkd7ZCfvICJC6ng5v2EsGPo\r\nnl5ucB5qvx9a0dZ8J1vfAykENmmThLPGrW8azLxGssR7SlA7TFuOO2nS2fRO7OWB\r\ncBtPdCFPxCRzeYu3+0uLzy+X\r\n-----END PRIVATE KEY-----";

        private const string EccPublicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEuF8pYWTD0qpFFplbo3SU00PRZAnb\r\nJ6dNQS7xGayVeHd0+ER4kUB/F4YGDTFHcNCOaWYI2bzf7Lq6lwkrgOClug==\r\n-----END PUBLIC KEY-----\r\n";
        private const string EccPrivateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nME0CAQAwEwYHKoZIzj0CAQYIKoZIzj0DAQcEMzAxAgEBBCCs8ebY+dRSi+0Cp5ev\r\nKgMBlerHL3b6+leGvHZ47jgklaAKBggqhkjOPQMBBw==\r\n-----END PRIVATE KEY-----\r\n";

        private const string DesPublicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMIIBtjCCASsGByqGSM44BAEwggEeAoGBAJd3/vqce38K41OZzDTW5y7yO29/Zc92\r\ngqkkkmSVt7T4epjBL+/2B288N9mJOezPdG3eHF2ZJLTnJV77QssZeDLgHOniYBSX\r\nrUHtHF2l8q4XbzfTR5s92FtMnW7jim8ECzm+VQYozerEGb+r9qRHNPv5xUdcMzV6\r\ndr0L9tbYOSXpAhUA64PFDESrkFr5LtppKM0pFLoM/NMCgYBB5vyc7WevVBTmscgO\r\nP4MUIGdfHy7GoiexdTNFf7SnKHPS/urCpeayoNpr9dclxcKV3sgIdS/kgDp8OK/+\r\nQP2Wt1rXf5MRqwG/Uqj7WMYjvGjKyCHlzsi7kgnD/vWS/WHxCmH9gTen+54HN2vp\r\n78W3IiUDr/F7i5pqFilotnD0VQOBhAACgYAfuOT6FWYterRwAe2ot0Ij8qOSRK2m\r\nAvjDEErST4ocTMwH3x2Cjzo4iAhwZULFdJPH3qz6Y3ypVAyZKYoAXYzsNKldzh/R\r\n2JOicynp7ShHtMMnvMB/ArVhX+sjzWGmuWsWWAsqPbQ4SzwmADsIUvf+yDiXfGp+\r\nipIhaOg3/yytYA==\r\n-----END PUBLIC KEY-----\r\n";
        private const string DesPrivateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nMIIBSwIBADCCASsGByqGSM44BAEwggEeAoGBAJd3/vqce38K41OZzDTW5y7yO29/\r\nZc92gqkkkmSVt7T4epjBL+/2B288N9mJOezPdG3eHF2ZJLTnJV77QssZeDLgHOni\r\nYBSXrUHtHF2l8q4XbzfTR5s92FtMnW7jim8ECzm+VQYozerEGb+r9qRHNPv5xUdc\r\nMzV6dr0L9tbYOSXpAhUA64PFDESrkFr5LtppKM0pFLoM/NMCgYBB5vyc7WevVBTm\r\nscgOP4MUIGdfHy7GoiexdTNFf7SnKHPS/urCpeayoNpr9dclxcKV3sgIdS/kgDp8\r\nOK/+QP2Wt1rXf5MRqwG/Uqj7WMYjvGjKyCHlzsi7kgnD/vWS/WHxCmH9gTen+54H\r\nN2vp78W3IiUDr/F7i5pqFilotnD0VQQXAhUAjHB9qKjoxWOU4lZFeIV3cj7YFJ8=\r\n-----END PRIVATE KEY-----\r\n";
        
        [Test]
        public void GenerateKeyPairDefault()
        {
            // Generation of a keyPair is non-deterministic, so we'll perform some extra checks to be sure.
            var keyPair = AsymmetricCryptoUtil.GenerateKeyPair();
            Assert.IsNotEmpty(keyPair.PrivatePem);
            Assert.IsNotEmpty(keyPair.PublicPem);
            Assert.IsTrue(VerifyKeyPairSignatures(TestString, keyPair.PrivatePem, keyPair.PublicPem));
        }
        
        [Test]
        public void GenerateKeyPairRsa()
        {
            // Generation of a keyPair is non-deterministic, so we'll perform some extra checks to be sure.
            var keyPair = AsymmetricCryptoUtil.GenerateKeyPair(KeyPair.KeyTypes.Rsa);
            Assert.IsNotEmpty(keyPair.PrivatePem);
            Assert.IsNotEmpty(keyPair.PublicPem);
            Assert.IsTrue(VerifyKeyPairSignatures(TestString, keyPair.PrivatePem, keyPair.PublicPem));
        }

        [Test]
        public void GenerateKeyPairEcc()
        {
            // Generation of a keyPair is non-deterministic, so we'll perform some extra checks to be sure.
            var keyPair = AsymmetricCryptoUtil.GenerateKeyPair(KeyPair.KeyTypes.Ecc);
            Assert.IsNotEmpty(keyPair.PrivatePem);
            Assert.IsNotEmpty(keyPair.PublicPem);
            Assert.IsTrue(VerifyKeyPairSignatures(TestString, keyPair.PrivatePem, keyPair.PublicPem));
        }
        
        [Test]
        public void GenerateKeyPairUnsupported()
        {
            // Ensure we have the correct first value of the enum.
            Assert.AreEqual((double) KeyPair.KeyTypes.Ecc, 0);

            const KeyPair.KeyTypes invalidEnumType = KeyPair.KeyTypes.Ecc - 1;
            Assert.Throws<InvalidEnumArgumentException>(() => AsymmetricCryptoUtil.GenerateKeyPair(invalidEnumType));
        }

        [Test]
        public void CreateVerifySignatureRsa()
        {
            Assert.IsTrue(VerifyKeyPairSignatures(TestString, RsaPrivateKeyPem, RsaPublicKeyPem));
        }
        
        [Test]
        public void CreateVerifySignatureEcc()
        {
            Assert.IsTrue(VerifyKeyPairSignatures(TestString, EccPrivateKeyPem, EccPublicKeyPem));
        }

        [Test]
        public void CreateVerifySignatureUnsupported()
        {
            Assert.Throws<ArgumentException>(() => AsymmetricCryptoUtil.CreateSignature(TestString, DesPrivateKeyPem));
            Assert.Throws<ArgumentException>(() => AsymmetricCryptoUtil.VerifySignature(TestString, "", DesPublicKeyPem));
        }

        private bool VerifyKeyPairSignatures(string message, string privateKeyPem, string publicKeyPem)
        {
            var signature = AsymmetricCryptoUtil.CreateSignature(message, privateKeyPem);
            return AsymmetricCryptoUtil.VerifySignature(message, signature, publicKeyPem);
        }
    }
}
