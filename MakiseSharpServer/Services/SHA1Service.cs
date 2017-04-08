using System;
using System.Security.Cryptography;
using MakiseSharpServer.Utility;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace MakiseSharpServer.Services
{
    public class SHA1Service
    {
        /// <summary>
        /// Verifies a SHA1 hash using a signature and a public key
        /// </summary>
        /// <param name="hash">Hash of the message to be verified</param>
        /// <param name="signature">Signature</param>
        /// <param name="publickey">RSA public key in PEM format</param>
        /// <returns>True if verified successfully</returns>
        public static bool VerifySignature(byte[] hash, byte[] signature, string publickey)
        {
            var asymmetricKeyParameter = PublicKeyFactory.CreateKey(Convert.FromBase64String(Keys.Dearmor(publickey)));
            var rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            var rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };
            var rsa = RSA.Create();
            rsa.ImportParameters(rsaParameters);
            return rsa.VerifyHash(hash, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }
    }
}
