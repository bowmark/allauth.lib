using System.Collections.Generic;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace AllAuth.Lib.ServerAPI
{
    public class DeviceToDeviceMessages
    {
        /// <summary>
        /// Each enum member must map to an IMessage class.
        /// </summary>
        public enum Types
        {
            NewSecret,
            RequestEntrySecrets,
            SendEntrySecrets,
            DeleteSecret,
            DeleteEntry
        }

        public class EnvelopeSerialised
        {
            public Types Type;
            public string Message;
        }

        public class Envelope
        {
            public Types Type;
            public IMessage Message;
        }

        public interface IMessage { }

        public class NewSecret : IMessage
        {
            public string LinkIdentifier;
            public List<NewSecretItem> Secrets;
        }

        public class NewSecretItem : IMessage
        {
            public string EntryIdentifier;
            public string SecretIdentifier;
            public string Secret;
        }

        public class RequestEntrySecrets : IMessage
        {
            public string EntryIdentifier;
            public List<string> SecretIdentifiers = new List<string>();
        }

        public class SendEntrySecrets : IMessage
        {
            public string OriginalMessageIdentifier;
            public bool RequestAccepted;
            public Dictionary<string, string> Secrets = new Dictionary<string, string>();
        }

        public class DeleteSecret : IMessage
        {
            public string SecretIdentifier;
        }

        public class DeleteEntry : IMessage
        {
            public string LinkIdentifier;
            public string EntryIdentifier;
        }
    }
}
