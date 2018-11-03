using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
//using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


        public static string postXMLData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.Headers.Add("X-Access-Version", "1");
            //request.Headers.Add("Accept", "text/xml");
            //request.Headers.Add("Content-Type", "text/xml");
            request.ContentLength = bytes.Length;
            request.Method = "POST";

            var clientCertStore = new X509Store(StoreName.My);
            clientCertStore.Open(OpenFlags.OpenExistingOnly & OpenFlags.ReadOnly);

            var cert = clientCertStore.Certificates.Find(X509FindType.FindBySerialNumber, "011c05", false).OfType<X509Certificate2>().FirstOrDefault();



            request.ClientCertificates = new X509Certificate2Collection(cert);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }

        static void Main(string[] args)
        {












            using (System.IO.StreamReader sr = new StreamReader(@"C:\Users\pachie\source\repos\ConsoleApp1\ConsoleApp1\XMLFile1.xml"))
            {

                var xml = sr.ReadToEnd();

                postXMLData("https://access.abilitynetwork.com/access/csi/cmn", xml);


            }


           

            //string url = "https://access1.abilitynetwork.com/access/password/generate";
            string url = "https://access.abilitynetwork.com/access/password/generate";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Headers.Add("X-Access-Version", "1");
            //request.Headers.Add("Content-Type", "text/xml");
            //request.Headers.Add("Accept", "text/xml");
            request.Method = "POST";
            var clientCertStore = new X509Store(StoreName.My);
            clientCertStore.Open(OpenFlags.OpenExistingOnly & OpenFlags.ReadOnly);

            var cert = clientCertStore.Certificates.Find(X509FindType.FindBySerialNumber, "011c05", false).OfType<X509Certificate2>().FirstOrDefault();

            //var certs = store.Certificates.Find(X509FindType.FindBySerialNumber, "011c05", true);
            //certs.OfType<X509Certificate>().FirstOrDefault();

            request.ClientCertificates = new X509Certificate2Collection(cert);



            WebResponse response = request.GetResponse();
            string responseBody = null;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();

                










            }
            Console.WriteLine(responseBody);









            X509Store store = new X509Store("MY", StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;
            X509Certificate2Collection fcollection = (X509Certificate2Collection)collection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Test Certificate Select", "Select a certificate from the following list to get information on that certificate", X509SelectionFlag.MultiSelection);
            Console.WriteLine("Number of certificates: {0}{1}", scollection.Count, Environment.NewLine);

            foreach (X509Certificate2 x509 in scollection)
            {
                try
                {
                    byte[] rawdata = x509.RawData;
                    Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine);
                    Console.WriteLine("Friendly Name: {0}{1}", x509.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Certificate Verified?: {0}{1}", x509.Verify(), Environment.NewLine);
                    Console.WriteLine("Simple Name: {0}{1}", x509.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
                    Console.WriteLine("Signature Algorithm: {0}{1}", x509.SignatureAlgorithm.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Private Key: {0}{1}", x509.PrivateKey.ToXmlString(false), Environment.NewLine);
                    Console.WriteLine("Public Key: {0}{1}", x509.PublicKey.Key.ToXmlString(false), Environment.NewLine);
                    Console.WriteLine("Certificate Archived?: {0}{1}", x509.Archived, Environment.NewLine);
                    Console.WriteLine("Length of Raw Data: {0}{1}", x509.RawData.Length, Environment.NewLine);
                    X509Certificate2UI.DisplayCertificate(x509);
                    x509.Reset();
                }
                catch (CryptographicException)
                {
                    Console.WriteLine("Information could not be written out for this certificate.");
                }
            }
            store.Close();

        }
    }
}
