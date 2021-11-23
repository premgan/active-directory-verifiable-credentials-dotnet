// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using System.Collections.Generic;
using Newtonsoft.Json;

namespace AspNetCoreVerifiableCredentials
{
    /// <summary>
    /// 
    /// </summary>
    public class PresentationRequestModel
    {
        /// <summary>
        /// Determines whether a QR code is included in the response of this request. 
        /// Present the QR code and ask the user to scan it. Scanning the QR code launches the authenticator app with this presentation request. 
        /// When you set the value to false, use the return url property to render a deep link.
        /// </summary>
        [JsonProperty("includeQRCode")]
        public bool IncludeQRCode { get; set; }

        /// <summary>
        /// Allows the developer to asynchronously get information on the flow during the verifiable credential presentation process. 
        // For example, the developer might want a call when the user has scanned the QR code.
        /// </summary>
        [JsonProperty("callback")]
        public CallbackType Callback { get; set; }

        /// <summary>
        /// The verifier's decentralized identifier (DID).
        /// </summary>
        [JsonProperty("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// Provides information about the verifier
        /// </summary>
        [JsonProperty("registration")]
        public PresentationRequestRegistration Registration { get; set; }

        /// <summary>
        /// Provides information about the verifiable credentials presentation request.
        /// </summary>
        [JsonProperty("presentation")]
        public RequestPresentation Presentation { get; set; }

        public PresentationRequestModel()
        {
            Callback = new CallbackType();
            Presentation = new RequestPresentation();
            Presentation.RequestedCredentials = new List<RequestCredential>();
            Registration = new PresentationRequestRegistration();
        }
    }

    /// <summary>
    /// Provides information registration for the verifier. 
    /// </summary>
    public class PresentationRequestRegistration
    {
        /// <summary>
        /// A display name of the issuer of the verifiable credential. 
        // This name will be presented to the user in the authenticator app. 
        /// </summary>
        [JsonProperty("clientName", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientName { get; set; }

        /// <summary>
        /// The purpose for this presentation request
        /// </summary>
        [JsonProperty("purpose", NullValueHandling = NullValueHandling.Ignore)]
        public string Purpose { get; set; }
    }

    public class RequestCredential
    {
        public RequestCredential()
        {
            AcceptedIssuers = new List<string>();
        }

        /// <summary>
        /// The verifiable credential type. The type must match the type as defined in the issuer verifiable 
        /// credential manifest (for example, VerifiedCredentialExpert).
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Provide information about the purpose of requesting this verifiable credential.
        /// </summary>
        [JsonProperty("purpose")]
        public string Purpose { get; set; }

        /// <summary>
        /// A collection of issuers' DIDs that could issue the type of verifiable credential that subjects can present.
        /// </summary>
        [JsonProperty("acceptedIssuers")]
        public List<string> AcceptedIssuers { get; set; }
    }

    /// <summary>
    /// Provides information required for verifiable credential presentation
    /// </summary>
    public class RequestPresentation
    {
        /// <summary>
        /// Determines whether a receipt should be included in the response of this request. 
        /// The receipt contains the original payload sent from the authenticator to the Verifiable Credentials service. 
        /// The receipt is useful for troubleshooting, and shouldn't be set by default. 
        /// In the OpenId Connect SIOP request, the receipt contains the ID token from the original request.
        /// </summary>
        [JsonProperty("includeReceipt")]
        public bool IncludeReceipt { get; set; }

        /// <summary>
        /// A collection of RequestCredential objects
        /// </summary>
        [JsonProperty("requestedCredentials")]
        public List<RequestCredential> RequestedCredentials { get; set; }
    }
}



