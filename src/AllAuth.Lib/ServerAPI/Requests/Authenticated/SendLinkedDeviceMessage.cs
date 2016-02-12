using System;
using AllAuth.Lib.Crypto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SendLinkedDeviceMessage : AuthenticatedRequest<SendLinkedDeviceMessage.ResponseParams>
    {
        //public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public int SecondsValidFor { get { return Get<int>(); } set { Set(value); } }
        public string Message { get { return Get<string>(); } set { Set(value); } }

        private DeviceToDeviceMessages.IMessage _message;
        private string _receiverPublicKeyPem;
        
        public new class ResponseParams
        {
            public string MessageIdentifier;
        }

        public void SetMessage(DeviceToDeviceMessages.IMessage message, string receiverPublicKeyPem)
        {
            _message = message;
            _receiverPublicKeyPem = receiverPublicKeyPem;
        }
        
        public new ResponseParams GetResponse(ApiClient apiClient)
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());
            var messageSerialised = JsonConvert.SerializeObject(_message, settings);

            var messageEnvelope = new DeviceToDeviceMessages.EnvelopeSerialised
            {
                Type = (DeviceToDeviceMessages.Types) Enum.Parse(
                    typeof(DeviceToDeviceMessages.Types), _message.GetType().Name),
                Message = messageSerialised
            };

            var messageEnvelopeSerialised = JsonConvert.SerializeObject(messageEnvelope, settings);

            Message = new EncryptedDataWithKeyPairSigned(
                messageEnvelopeSerialised, _receiverPublicKeyPem, apiClient.PrivateKeyPem).ToString();
            
            return base.GetResponse(apiClient);
        }
    }
}
