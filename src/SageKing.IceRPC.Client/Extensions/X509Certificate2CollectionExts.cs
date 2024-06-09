using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SageKing.IceRPC.Client.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class X509Certificate2CollectionExts
    {

        /// <summary>
        /// 默认实现
        /// </summary>
        /// <param name="rootCA"></param>
        /// <returns></returns>
        public static RemoteCertificateValidationCallback CreateCustomRootRemoteValidatorDefault(this X509Certificate2 rootCA)
        {
            return (sender, certificate, chain, errors) =>
            {
                using var customChain = new X509Chain();
                customChain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
                customChain.ChainPolicy.DisableCertificateDownloads = true;
                customChain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                customChain.ChainPolicy.CustomTrustStore.Add(rootCA);
                return customChain.Build((X509Certificate2)certificate!);
            };
        }

        /// <summary>
        /// CryptographicException:“A null or disposed certificate was present in CustomTrustStore.”
        /// https://www.meziantou.net/custom-certificate-validation-in-dotnet.htm
        /// </summary>
        /// <param name="trustedRoots"></param>
        /// <param name="intermediates"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static RemoteCertificateValidationCallback CreateCustomRootRemoteValidator(this X509Certificate2 rootCA, X509Certificate2Collection intermediates = null)
        {
            try
            {
                byte[] rootCertificateData = rootCA.GetRawCertData();
                var rootCertificate = new X509Certificate2(rootCertificateData);
                var trustedRoots = new X509Certificate2Collection(rootCertificate);

                if (trustedRoots == null)
                    throw new ArgumentNullException(nameof(trustedRoots));
                if (trustedRoots.Count == 0)
                    throw new ArgumentException("No trusted roots were provided", nameof(trustedRoots));

                // Let's avoid complex state and/or race conditions by making copies of these collections.
                // Then the delegates should be safe for parallel invocation (provided they are given distinct inputs, which they are).
                X509Certificate2Collection roots = new X509Certificate2Collection(trustedRoots);
                X509Certificate2Collection intermeds = null;

                if (intermediates != null)
                {
                    intermeds = new X509Certificate2Collection(intermediates);
                }

                intermediates = null;
                trustedRoots = null;

                return (sender, serverCert, chain, errors) =>
                {
                    // Missing cert or the destination hostname wasn't valid for the cert.
                    if ((errors & ~SslPolicyErrors.RemoteCertificateChainErrors) != 0)
                    {
                        return false;
                    }

                    for (int i = 1; i < chain.ChainElements.Count; i++)
                    {
                        chain.ChainPolicy.ExtraStore.Add(chain.ChainElements[i].Certificate);
                    }

                    if (intermeds != null)
                    {
                        chain.ChainPolicy.ExtraStore.AddRange(intermeds);
                    }

                    chain.ChainPolicy.CustomTrustStore.Clear();
                    chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
                    chain.ChainPolicy.CustomTrustStore.AddRange(roots);
                    return chain.Build((X509Certificate2)serverCert);
                };

            }
            catch (Exception)
            {
                //使用默认
                return rootCA.CreateCustomRootRemoteValidatorDefault();
            }
        }


        /// <summary>
        /// using var handler = new HttpClientHandler();
        /// using var httpClient = new HttpClient(handler);
        /// </summary>
        /// <param name="rootCA"></param>
        /// <param name="intermediates"></param>
        /// <returns></returns>
        public static Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool> CreateCustomRootValidator(this X509Certificate2 rootCA, X509Certificate2Collection intermediates = null)
        {
            RemoteCertificateValidationCallback callback = CreateCustomRootRemoteValidator(rootCA, intermediates);
            return (message, serverCert, chain, errors) => callback(null, serverCert, chain, errors);
        }
    }
}
