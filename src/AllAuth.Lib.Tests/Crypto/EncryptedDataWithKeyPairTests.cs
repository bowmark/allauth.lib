﻿using System.Text;
using AllAuth.Lib.Crypto;
using NUnit.Framework;

namespace AllAuth.Lib.Tests.Crypto
{
    public class EncryptedDataWithKeyPairTests
    {
        private const string TestString = "My!Test£String$01234";

        private const string RsaPublicKeyPem = "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlHupFc5Q7fwng63EEFvu\r\nLUBXcYkoHjFTqLIQQA3LFjj69pfYKy2xdr9n7uQseeH2+1/Ik+CkIiP5uA8V1T1K\r\nyeh702i5LeykxFhSFDeEZXm94FK6iXyOSgrCSIIxsi72sXgWjZGKEfwJDw+4qxpQ\r\nwWxgKH4Ky5MAVpoJU/FmoEpDeg8WDaLqK8f7hM8j/lWxbJOFYbn2Dgu9FmAkw904\r\n5lwadDyau9qWOwrusf3cH1upKdL4vNMml2XykYYJo5qXA9g0sYwv/R5ZFZ67U780\r\nTebYIjr0bna+77Bsg04jgWYdQ8/J1Ec8p11TfC8x7MJnsLtE7EJfP/6jFDY4H0jF\r\nawIDAQAB\r\n-----END PUBLIC KEY-----";
        private const string RsaPrivateKeyPem = "-----BEGIN PRIVATE KEY-----\r\nMIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCUe6kVzlDt/CeD\r\nrcQQW+4tQFdxiSgeMVOoshBADcsWOPr2l9grLbF2v2fu5Cx54fb7X8iT4KQiI/m4\r\nDxXVPUrJ6HvTaLkt7KTEWFIUN4Rleb3gUrqJfI5KCsJIgjGyLvaxeBaNkYoR/AkP\r\nD7irGlDBbGAofgrLkwBWmglT8WagSkN6DxYNouorx/uEzyP+VbFsk4VhufYOC70W\r\nYCTD3TjmXBp0PJq72pY7Cu6x/dwfW6kp0vi80yaXZfKRhgmjmpcD2DSxjC/9HlkV\r\nnrtTvzRN5tgiOvRudr7vsGyDTiOBZh1Dz8nURzynXVN8LzHswmewu0TsQl8//qMU\r\nNjgfSMVrAgMBAAECggEAFK/pSLykLDwc70Ixq7Z7Mnp2lGlvxEvKuZlcHT3ZZTZk\r\nH3Yxa8v5H9p0FbsG8qZHvbxvGsyfYZL8kvx7EL4c4jujVjrkI0tcphnIucCwZV5M\r\nVOmBzBpP+BiT+qaKW6Z3wBXDTU8RMFyakIAivP1fcfYdAZc/VB9I2p1Uc7Rm6tRS\r\nJ1nileKc9kFGdXpV5qEuKMbqESf9Lh48OvJflRxGQTe1R019dxRFHUX5RjRJT2bH\r\n7/KPMXob9tn2Zs0JV0CJK39lwY9kvJGPihTJLS8mEi8D5CqrfImCYGvhB/PvryFs\r\nkLxZOCTCpywvU85pyxy9QTBaidd9vQW3GSRM3jzxaQKBgQDRan1+hWlBhN974SuT\r\ni7aehXkgj2LZKiecYtrHgHwj3JY9AzY0lvhWwP+bMijLziS4l2krcybU7/ehqt6D\r\nJ4KaJVnQZZSzrIbzbJkM1GxKXGQPrvUxc3jx9I84ljJNT0OO3HUyGkbVqC4P7ncy\r\nGFc/wIIuVMUtsy0lwYPeoKDTOQKBgQC1g0CfiwhenW825ScgQScYkUCMCxJCRjzM\r\ncNBL9J98l7OGOlOuB1cg1MreObg/ALHgaC0DlupiiIgbhoIjjG27O95ecC+pxeJ4\r\n3vEmwyn7cW+fVECuw8SrLNT3nUBVCsg+d1P1Y31dGznd0nHDoRPVkDn0qDBXnUqR\r\nbMAdi9HpwwKBgDF1Swo0i/VTYkypk1cXahvqPf84ngnX8N74Yd84MxltIXZracPl\r\n2+TxU+zdXdE0cGvAJrCCU6t6uStPZZnoHOo25A1Z0FLW7lBTV5o2GRU+2MrzcSkq\r\nsmfCcIjWwC6OB9CXtH4Owqf3Plm44iHMqd5+osA6eQ8gAAuotnI/0XvRAoGBAIGh\r\n1IRdEkQoYeYtsEzL2zUjf5rEyWyFpyRHKawlb8XWvgMOIvc2DNbIwn4FcP2l7GOd\r\nYxCeNpTad4JZBRaIU1IWBtEhhBjW19CFQGrzlUcbk6GR1YQ7EnlB0nNpXimeQrbC\r\nGDc3r6/QIvq2MkOKBZVPpOSDPctE7Z2AspAnbGprAoGBALVYPfhlv3ISUJP6Ajlu\r\ncyZpXePCCyYjPgvUb7MKhIjM1XI+j1w3WZ4DdLT28Kkd7ZCfvICJC6ng5v2EsGPo\r\nnl5ucB5qvx9a0dZ8J1vfAykENmmThLPGrW8azLxGssR7SlA7TFuOO2nS2fRO7OWB\r\ncBtPdCFPxCRzeYu3+0uLzy+X\r\n-----END PRIVATE KEY-----";
        
        [Test]
        public void TestEncryptedDataWithKeyPairRsaString()
        {
            var encryptedDataWithKeyPair = new EncryptedDataWithKeyPair(TestString, RsaPublicKeyPem);
            var encryptedDataWithKeyPairString = encryptedDataWithKeyPair.ToString();

            Assert.IsNotEmpty(encryptedDataWithKeyPair.EncryptedKey);

            var decryptedTestString = EncryptedDataWithKeyPair.DecryptData(
                encryptedDataWithKeyPairString, RsaPrivateKeyPem);

            Assert.AreEqual(TestString, decryptedTestString);
        }

        [Test]
        public void TestEncryptedDataWithKeyPairRsaBytes()
        {
            var encryptedDataWithKeyPair = new EncryptedDataWithKeyPair(
                Encoding.UTF8.GetBytes(TestString), RsaPublicKeyPem);
            var encryptedDataWithKeyPairString = encryptedDataWithKeyPair.ToString();

            Assert.IsNotEmpty(encryptedDataWithKeyPair.EncryptedKey);

            var decryptedTestBytes = EncryptedDataWithKeyPair.DecryptDataAsBytes(
                encryptedDataWithKeyPairString, RsaPrivateKeyPem);

            Assert.AreEqual(TestString, Encoding.UTF8.GetString(decryptedTestBytes));
        }

        [Test]
        public void TestEncryptedDataWithKeyPairRsaDecrypt()
        {
            const string encryptedDataString = "AAEAAIHvW57RC3MbJPipwsh03cCEuVSXBfbUPY5+PD8xhxtmgx6TTxlOs+nNSdtA/VQWV6/B8aeD7/Yrqj4+a6SNTMOXgcGlLUCkxXgw7gBBzmgj2sNYSaFI09Oifqip6FPVS+kVoS//z9hoTZqyxj6eogbou5Ap/nLJbjqZ62cx0g7VmNijzirUMQKAwoc/+h+COQNMOQJa69pPTHUzAakxL4AtwHmKodGS7SXlKM+ZrjMOEOarJ43mXAuOoWsp5uZM7sw72F0TuVtBFH0jVMzzV5UhonZFInltbTPX7CyxmpEX3WJANlpDzHZbYUAouLyXT4USUvA2Br29ouzRsLAM9YgAWY4LBLp0MZGq3NELCEYbGYWBHvuAZaIf3YeooX5sYAg5YQoVAXezD6ULf6qF7Ceb";

            var decryptedTestString = EncryptedDataWithKeyPair.DecryptData(encryptedDataString, RsaPrivateKeyPem);
            Assert.AreEqual(TestString, decryptedTestString);
        }
    }
}
