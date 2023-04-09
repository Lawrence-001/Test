using Microsoft.AspNetCore.Mvc;
using ContinentsService;
using BootstrapLayout.Models;
using System.ServiceModel;

namespace BootstrapLayout.Controllers
{
    public class ContinentsController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
            var client = new CountryInfoServiceSoapTypeClient(binding, endpoint);
            //CountryInfoServiceSoapTypeClient client = new CountryInfoServiceSoapTypeClient(new BasicHttpBinding(), new EndpointAddress("http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso"));

            var continentData = await client.ListOfContinentsByNameAsync();

            var result = await client.ListOfContinentsByNameAsync();

            var continents = new List<Continent>();
            foreach (var continent in continentData.Body.ListOfContinentsByNameResult)
            {
                continents.Add(new Continent { Code = continent.sCode, Name = continent.sName });
            }
            return View(continents);
        }

    }
}
