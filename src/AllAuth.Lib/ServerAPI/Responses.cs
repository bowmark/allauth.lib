using System;
using System.Collections.Generic;

/**
 * NOTE: This file should not be used for new additions. Use the Requests namespace instead. 
 */
namespace AllAuth.Lib.ServerAPI
{
    public interface IResponse { }

    public class ErrorResponse : IResponse
    {
        public string ErrorCode;
        public string Error;
    }
    
    public class GetUserResponse : IResponse
    {
        public bool SecondDeviceSetupCompleted;
        public bool DatabaseDeviceCryptoKeySet;
        public bool DatabaseRecoverySetup;
        public bool DeviceRecoverySetup;
        public List<GetUserResponseLinkItem> Links;
    }

    public class GetUserResponseLinkItem
    {
        public string Identifier;
        public string Label;
        public bool HasBackup;
        public DateTime LastBackup;
    }
    
    public class GetDevicesKeysResponse : IResponse
    {
        public string[] PublicKeys;
    }

    public class GetServerInfoResponse : IResponse
    {
        public string ServerIdentifier;
        public string ServerLabel;
    }

    public class InitiateDeviceLoginResponse : IResponse
    {
        public string LoginRequestIdentifier;
    }

    public class RegisterOtpDeviceResponse : IResponse
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Algorithm { get; set; }
        public int Digits { get; set; }
        public int Counter { get; set; }
        public int Period { get; set; }
    }
    
    public class InitiateLinkResponse : IResponse
    {
        public string LinkIdentifier;
    }
    
    public class UploadDeviceBackupResponse : IResponse {}
}
