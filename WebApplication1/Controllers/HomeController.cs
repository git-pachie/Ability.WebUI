using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(string customerID, string Name)
        {

            string url = "https://access.abilitynetwork.com/access/password/generate";
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Headers.Add("X-Access-Version", "1");
            request.Method = "POST"; var clientCertStore = new X509Store(StoreName.My);
            clientCertStore.Open(OpenFlags.OpenExistingOnly & OpenFlags.ReadOnly);

            var cert = clientCertStore.Certificates.Find(X509FindType.FindBySerialNumber, "011c05", false).OfType<X509Certificate2>().FirstOrDefault();

            //var certs = store.Certificates.Find(X509FindType.FindBySerialNumber, "011c05", true);
            //certs.OfType<X509Certificate>().FirstOrDefault();

            request.ClientCertificates = new X509Certificate2Collection(cert);

            WebResponse response = request.GetResponse(); string responseBody = null; using (var reader = new StreamReader(response.GetResponseStream()))
            {
                responseBody = reader.ReadToEnd();


                // database

                var model = new Models.Customer();
                model.CustomerId = customerID;
                model.CustomernAME = Name;
                model.LastName = "Gatdula";
                model.AbilityPassword = responseBody;

                //return Content(responseBody);

                return Json(new { result = model }, JsonRequestBehavior.AllowGet);
            }
            //Console.WriteLine(responseBody);

            //return Content("Test home");
        }

        public ActionResult SampleTable()
        {
            var models = new List<Models.Customer>();


            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });
            models.Add(new Models.Customer { AbilityPassword = "abc", CustomerId = "3452345", CustomernAME = "abasfaf", LastName = "Leo" });




            return View(models);
        }
    }
}