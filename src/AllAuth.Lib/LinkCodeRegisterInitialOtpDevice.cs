using System;
using System.Text;

namespace AllAuth.Lib
{
    public class LinkCodeRegisterInitialOtpDevice
    {
        private readonly int _linkCodeVersion;
		public bool ServerHttps { get; private set; }
		public string ServerHost { get; private set; }
		public int ServerPort { get; private set; }
		public int ServerApiVersion { get; private set; }
		public string LoginIdentifier { get; private set; }
		public string EmailAddress { get; private set; }
		public string InitiatingDevicePublicKeyPem { get; private set; }
		public bool RecoveryRequired { get; private set; }
        
        public LinkCodeRegisterInitialOtpDevice(bool serverHttps, string serverHost, int serverPort, 
            int serverApiVersion, string loginIdentifier, string emailAddress, 
            string devicePublicKeyPem, bool recoveryRequired)
        {
            _linkCodeVersion = 1;
            ServerHttps = serverHttps;
            ServerHost = serverHost;
            ServerPort = serverPort;
            ServerApiVersion = serverApiVersion;
            LoginIdentifier = loginIdentifier;
            EmailAddress = emailAddress;
            InitiatingDevicePublicKeyPem = devicePublicKeyPem;
            RecoveryRequired = recoveryRequired;
        }

        /// <summary>
        /// Create a new object from a serialized linkcode.
        /// </summary>
        /// <param name="linkCode"></param>
        public LinkCodeRegisterInitialOtpDevice(string linkCode)
        {
            var code = Encoding.UTF8.GetString(Convert.FromBase64String(linkCode));
            var codeParts = code.Split(new[] {'|'});

            if (codeParts.Length != 9)
                throw new Exception("Link code not valid");

            try
            {
                _linkCodeVersion = int.Parse(codeParts[0]);
            }
            catch (Exception)
            {
                throw new Exception("Link code version invalid");
            }
            if (_linkCodeVersion != 1)
                throw new Exception("Incorrect link code version");

            try
            {
                ServerHttps = bool.Parse(codeParts[1]);
            }
            catch (Exception)
            {
                throw new Exception("Server HTTPS value invalid");
            }

            ServerHost = codeParts[2];

            try
            {
                ServerPort = int.Parse(codeParts[3]);
            }
            catch (Exception)
            {
                throw new Exception("Server port invalid");
            }

            try
            {
                ServerApiVersion = int.Parse(codeParts[4]);
            }
            catch (Exception)
            {
                throw new Exception("Server API version invalid");
            }

            LoginIdentifier = codeParts[5];
            EmailAddress = codeParts[6];
            InitiatingDevicePublicKeyPem = codeParts[7];

            try
            {
                RecoveryRequired = bool.Parse(codeParts[8]);
            }
            catch (Exception)
            {
                throw new Exception("Recovery flag invalid");
            }
        }
        
        /// <summary>
        /// Serializes the link code to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var code =
                _linkCodeVersion + "|" + ServerHttps + "|" + ServerHost + "|" + ServerPort + "|" + 
                ServerApiVersion + "|" + LoginIdentifier + "|" + EmailAddress + "|" + 
                InitiatingDevicePublicKeyPem + "|" + RecoveryRequired;

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
        }
    }
}
