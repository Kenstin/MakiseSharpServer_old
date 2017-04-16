using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace MakiseSharpServer.Utility
{
    public class Sha1
    {
        /// <summary>
        /// Verifies a SHA1 hash using a signature and a PEM public key
        /// </summary>
        /// <param name="hash">Hash of the message to be verified</param>
        /// <param name="signature">Signature</param>
        /// <param name="publickey">RSA public key in PEM format</param>
        /// <returns>True if verified successfully</returns>
        public static bool VerifySignature(byte[] hash, byte[] signature, string publickey)
        {
            return VerifySignature(hash, signature, Convert.FromBase64String(Keys.Dearmor(publickey)));
        }

        /// <summary>
        /// Verifies a SHA1 hash using a signature and a public key
        /// </summary>
        /// <param name="hash">Hash of the message to be verified</param>
        /// <param name="signature">Signature</param>
        /// <param name="publickey">RSA public key</param>
        /// <returns>True if verified successfully</returns>
        public static bool VerifySignature(byte[] hash, byte[] signature, byte[] publickey)
        {
            var asymmetricKeyParameter = PublicKeyFactory.CreateKey(publickey);
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

        public static byte[] DigestMessage(string message, Encoding encoding)
        {
            return SHA1.Create().ComputeHash(encoding.GetBytes(message));
        }
    }
}
