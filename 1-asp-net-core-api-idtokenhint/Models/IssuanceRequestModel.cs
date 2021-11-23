// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using Newtonsoft.Json;

namespace AspNetCoreVerifiableCredentials
{
    /// <summary>
    /// The issuance request payload contains information about your verifiable credentials issuance request.
    /// </summary>
    public class IssuanceRequestModel
    {
        /// <summary>
        /// Determines whether a QR code is included in the response of this request. 
        /// Present the QR code and ask the user to scan it. Scanning the QR code launches the authenticator app with this issuance request. 
        /// When you set the value to false, use the return url property to render a deep link.
        /// </summary>
        [JsonProperty("includeQRCode")]
        public bool IncludeQRCode { get; set; }

        /// <summary>
        /// Allows the developer to asynchronously get information on the flow during the verifiable credential issuance process. 
        // For example, the developer might want a call when the user has scanned the QR code.
        /// </summary>
        [JsonProperty("callback")]
        public CallbackType Callback { get; set; }

        /// <summary>
        /// The issuer's decentralized identifier (DID).
        /// </summary>
        [JsonProperty("authority")]
        public string Authority { get; set; }

        /// <summary>
        /// Provides information about the issuer that can be displayed in the authenticator app.
        /// </summary>
        [JsonProperty("registration")]
        public RequestRegistration Registration { get; set; }

        /// <summary>
        /// Provides information about the issuance request.
        /// </summary>
        [JsonProperty("issuance")]
        public RequestIssuance Issuance { get; set; }

        public IssuanceRequestModel()
        {
            Issuance = new RequestIssuance();
            Callback = new CallbackType();
            Registration = new RequestRegistration();
        }
    }
    public class IssuanceRequestHeaders
    {
        [JsonProperty("api-key")]
        public string ApiKey { get; set; }
    }

    /// <summary>
    /// The Request Service REST API generates several events to the callback endpoint. 
    /// Those events allow you to update the UI and continue the process after the results are returned to the application
    /// </summary>
    public class CallbackType
    {
        /// <summary>
        /// URI to the callback endpoint of your application. 
        /// Make sure you use ngrok or something like that when running on a local devbox. 
        // The MS AAD VC Request service needs to be able to reach the callback URI
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Associates with the state passed in the original payload.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Optional. You can include a collection of HTTP headers required by the receiving end of the POST message. 
        /// The headers should only include the api-key or any header required for authorization
        /// </summary>
        [JsonProperty("headers", NullValueHandling = NullValueHandling.Ignore)]
        public IssuanceRequestHeaders Headers { get; set; }
    }

    /// <summary>
    /// The RequestRegistration type provides information registration for the issuer.
    /// </summary>
    public class RequestRegistration
    {
        /// <summary>
        /// A display name of the issuer of the verifiable credential.
        /// </summary>
        [JsonProperty("clientName", NullValueHandling = NullValueHandling.Ignore)]
        public string ClientName { get; set; }

        /// <summary>
        /// Optional. The URL for the issuer logo.
        /// </summary>
        [JsonProperty("logoUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string LogoUrl { get; set; }

        /// <summary>
        /// Optional. The URL for the terms of use of the verifiable credential that you are issuing.
        /// </summary>
        [JsonProperty("termsOfServiceUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string TermsOfServiceUrl { get; set; }
    }

    /// <summary>
    /// The PIN type defines a PIN code that can be displayed as part of the issuance. 
    /// PIN is optional, and, if used, should always be sent out-of-band.
    /// </summary>
    public class PinType
    {
        /// <summary>
        /// Contains the PIN value in plain text. 
        // When you're using a hashed PIN, the value property contains the salted hash, base64 encoded.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// The length of the PIN code. The default length is 6, the minimum is 4, and the maximum is 16.
        /// </summary>
        [JsonProperty("length")]
        public int Length { get; set; }
    }

    public class IssuanceRequestClaims
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string email { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string given_name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string family_name { get; set; }
    }
    /// <summary>
    /// The RequestIssuance type provides information required for verifiable credential issuance. 
    /// There are currently three input types that you can send in RequestIssuance. 
    /// Azure AD Verifiable Credentials uses these types to insert claims into a verifiable credential, 
    /// and attest to that information with the issuer's DID.
    /// </summary>
    public class RequestIssuance
    {
        /// <summary>
        /// The verifiable credential type. 
        /// Should match the type as defined in the verifiable credential manifest. For example: VerifiedCredentialExpert
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The URL of the verifiable credential manifest document.
        /// </summary>
        [JsonProperty("manifest")]
        public string Manifest { get; set; }

        /// <summary>
        /// Optional. A PIN number to provide extra security during issuance. For PIN code flow, this property is required. 
        /// You generate a PIN code, and present it to the user in your app. The user must provide the PIN code that you generated
        /// </summary>
        [JsonProperty("pin", NullValueHandling = NullValueHandling.Ignore)]
        public PinType PIN { get; set; }

        /// <summary>
        /// Optional. Include a collection of assertions made about the subject in the verifiable credential. 
        /// For ID token hint flow, it's important that you provide the user's claims, such as: email, first name and last name.
        /// </summary>
        [JsonProperty("claims", NullValueHandling = NullValueHandling.Ignore)]
        public IssuanceRequestClaims Claims { get; set; }
    }

}



