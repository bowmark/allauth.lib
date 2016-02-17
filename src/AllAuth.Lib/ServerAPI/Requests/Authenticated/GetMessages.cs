using System;
using System.Collections.Generic;
using AllAuth.Lib.Crypto;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetMessages : AuthenticatedRequest<GetMessages.ResponseParams>
    {
        // No request parameters
        
        public new class ResponseParams
        {
            public List<ResponseParamsMessage> Messages;
        }

        public class ResponseParamsMessage
        {
            public string Identifier;
            public DeviceMessages.Types Type;
            public string Content;
            
            public void SetContent(DeviceMessages.IMessage messageContents)
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter());
                Content = JsonConvert.SerializeObject(messageContents, settings);
            }

            public T GetContent<T>() where T : DeviceMessages.IMessage
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter());
                return JsonConvert.DeserializeObject<T>(Content, settings);
            }
        }
        
        public DeviceToDeviceMessages.Envelope DecryptClientMessage(string message, string senderPublicKeyPem)
        {
            var decryptedMessage = EncryptedDataWithKeyPairSigned.DecryptData(
                message, ApiClient.PrivateKeyPem, senderPublicKeyPem);

            var messageEnvelope = 
                JsonConvert.DeserializeObject<DeviceToDeviceMessages.EnvelopeSerialised>(decryptedMessage);

            DeviceToDeviceMessages.IMessage deviceMessage;
            switch (messageEnvelope.Type)
            {
                case DeviceToDeviceMessages.Types.NewSecret:
                    deviceMessage = 
                        JsonConvert.DeserializeObject<DeviceToDeviceMessages.NewSecret>(messageEnvelope.Message);
                    break;

                case DeviceToDeviceMessages.Types.RequestEntrySecrets:
                    deviceMessage =
                        JsonConvert.DeserializeObject<DeviceToDeviceMessages.RequestEntrySecrets>(messageEnvelope.Message);
                    break;

                case DeviceToDeviceMessages.Types.SendEntrySecrets:
                    deviceMessage =
                        JsonConvert.DeserializeObject<DeviceToDeviceMessages.SendEntrySecrets>(messageEnvelope.Message);
                    break;

                case DeviceToDeviceMessages.Types.DeleteSecret:
                    deviceMessage =
                        JsonConvert.DeserializeObject<DeviceToDeviceMessages.DeleteSecret>(messageEnvelope.Message);
                    break;

                case DeviceToDeviceMessages.Types.DeleteEntry:
                    deviceMessage =
                        JsonConvert.DeserializeObject<DeviceToDeviceMessages.DeleteEntry>(messageEnvelope.Message);
                    break;

                default:
                    // If you're here, you probably just forgot to the new D2D message you created to 
                    // the list above.
                    throw new Exception("Unexpected message type");
            }

            return new DeviceToDeviceMessages.Envelope
            {
                Type = messageEnvelope.Type,
                Message = deviceMessage
            };
        }
    }
}
