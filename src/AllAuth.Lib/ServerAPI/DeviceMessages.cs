namespace AllAuth.Lib.ServerAPI
{
    public sealed class DeviceMessages
    {
        /// <summary>
        /// Each enum member must map to an IMessage classname.
        /// </summary>
        public enum Types
        {
            ServerLoginRequest,
            NewDatabase,
            DeviceToDeviceMessage,
            VerifyDeviceKeysRequest,
            VerifyDeviceKeysResponse,
            LinkedDeviceChange
        }

        public interface IMessage { }

        public class ServerLoginRequest : IMessage
        {
            public string LoginRequestIdentifier;
            public string RequestingDeviceLabel;
            public string RequestingDeviceType;
        }

        public class NewDatabase : IMessage
        {
            public string LinkIdentifier;
        }

        public class DeviceToDeviceMessage : IMessage
        {
            //public string LinkIdentifier;
            public string EncryptedMessage;
        }
        
        public class VerifyDeviceKeysRequest : IMessage
        {
            public string Nonce;
            public string NonceSigned;
        }

        public class VerifyDeviceKeysResponse : IMessage
        {
            public bool Verified;
            public string Nonce;
            public string NonceSigned;
        }

        public class LinkedDeviceChange : IMessage
        {
        }
    }
}
