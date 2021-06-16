using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
namespace CoBRA.Infra.CrossCutting.ElvisService.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestApiElvis()
        {

            string urlLogin = "http://192.168.7.38/services/login?cred=cGFnYW1lbnRvLmFiZDokQHRlZCMlMjAxOA&username=pagamento.abd&password=$@ted#%2018&returnProfile=true";
            string urlGetItem = "http://192.168.7.38/services/search?q=-isPaid:*%20AND%20-usageFee:0%20AND%20usageFee:*%20AND%20licenseAgreement:*&authcred=cGFnYW1lbnRvLmFiZDokQHRlZCMlMjAxOA";
            Services.ElvisService service = new Services.ElvisService(urlLogin, urlGetItem);
            var item = await service.ObterItemsElvis();
            Assert.IsNotNull(item);
        }
    }
}
