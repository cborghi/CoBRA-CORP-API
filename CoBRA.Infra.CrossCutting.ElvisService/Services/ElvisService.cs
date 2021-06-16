using CoBRA.Infra.CrossCutting.ElvisService.Interfaces;
using CoBRA.Infra.CrossCutting.ElvisService.ViewModels;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoBRA.Infra.CrossCutting.ElvisService.Services
{
    public class ElvisService : IElvisService
    {
        private string _urlLoginApi, _urlGetItem;

        public ElvisService(IConfiguration config)
        {
            _urlLoginApi = config.GetSection("API-ELVIS").GetSection("URL-AUTHENTICATION").Value; ;
            _urlGetItem = config.GetSection("API-ELVIS").GetSection("URL-GET-ITEM").Value;
        }

        public ElvisService(string urlLogin, string urlGetItem)
        {
            _urlLoginApi = urlLogin;
            _urlGetItem = urlGetItem;
        }
 
        public async Task<ItemViewModel> ObterItemsElvis()
        {
            ItemViewModel item = new ItemViewModel();
            try
            {

                using (var api = new HttpClient())
                {
                    var uriAuth = _urlLoginApi;
                    var content = new StringContent("", Encoding.UTF8, "application/json");
                    var result = await api.PostAsync(uriAuth, null);
                    if (result.IsSuccessStatusCode)
                    {
                        var uriGet = _urlGetItem;
                        result = await api.GetAsync(uriGet);
                        if (result.IsSuccessStatusCode)
                        {
                            string data = await result.Content.ReadAsStringAsync();
                            item = JsonConvert.DeserializeObject<ItemViewModel>(data);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return item;
        }

        //public async Task<byte[]> GerarCSVElvis()
        //{

        //   List<HitsViewModel> elvisItem = new List<HitsViewModel>();

        //    var csv = new StringBuilder();
        //    string linha = string.Format("{0};{1};{2};{3};{4};{5};", "C7_PRODUTO", "C7_QUANT", "C7_PRECO", "C7_TOTAL", "C7_CC", "C7_OBS");

        //    csv.AppendLine(linha);
        //    foreach (var item in elvisItem)
        //    {

        //        csv.AppendLine(linha);
        //    }

        //    byte[] arquivo = ASCIIEncoding.ASCII.GetBytes(csv.ToString());
        //}
    }
}
