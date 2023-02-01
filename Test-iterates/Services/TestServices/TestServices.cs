using Test_iterates.Entities;

namespace Test_iterates.Services.TestServices
{
    public class TestServices : ITestServices 
    {
        private readonly BreweryWholesaleContext _breweryWholesaleontext;
        public TestServices()
        {
            _breweryWholesaleontext = new BreweryWholesaleContext();
        }

        public string GetTest()
        {
            List<Beer> listBeear = _breweryWholesaleontext.Beers.ToList();
            return "test";
        }
    }
}
