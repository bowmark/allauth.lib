/**
 * NOTE: This file should not be used for new additions. Use the Requests namespace instead. 
 */
namespace AllAuth.Lib.ServerAPI
{
    public interface IRequest {}
    
    
    //
    // Authenticated Requests
    //

    public class GetUserRequest : IRequest {}

    public class GetServerInfoRequest : IRequest { }

    public class GetDevicesKeysRequest : IRequest { }
    
    public class InitiateDeviceLoginRequest : IRequest { }

    public class RegisterOtpDeviceRequest : IRequest {}
    
    public class UploadDeviceBackupRequest : IRequest
    {
        public string Type;
        public string Hash;
        public string Data;
    }
}
