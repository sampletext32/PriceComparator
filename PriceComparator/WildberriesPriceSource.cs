using System;
using System.Diagnostics;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace PriceComparator
{
    public class WildberriesPriceSource : IPriceSource
    {
        public string Tag => "Ozon";

        private string WebPageUrl = "https://www.wildberries.ru/catalog/{0}/detail.aspx?targetUrl=BP";
        private string ApiUrl = "https://napi.wildberries.ru/api/catalog/{0}/detail.aspx?_app-type=sitemobile&targetUrl=BP";
        
        public string Producer { get; private set; }
        public string Articul { get; set; }

        private bool _loaded;
        private float _price;

        public WildberriesPriceSource(string articul)
        {
            Articul = articul;
            _price = 0f;
        }

        public float GetPrice()
        {
            if (!_loaded)
            {
                throw new Exception("Price was not loaded. Call LoadPrice first!");
            }

            return _price;
        }

        public void LoadPrice()
        {
            try
            {
                var webClient = new WebClient();
                var htmlString = webClient.DownloadString(string.Format(WebPageUrl, Articul));
                var document = new HtmlDocument();
                document.LoadHtml(htmlString);

                var priceSpan = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'price-block__final-price')]");

                var priceSpanInnerText = priceSpan.InnerText.Trim().Replace("&#xA0;₽", "");

                _price = float.Parse(priceSpanInnerText);
                Producer = LoadSupplierName();
                _loaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private string LoadSupplierName()
        {
            var webClient = new WebClient();
            webClient.Headers.Set("user-agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Mobile Safari/537.36");
            webClient.Headers.Set("wb-apptype", "sitemobile");
            webClient.Headers.Set("wb-appversion", "403");
            
            var apiDataString = webClient.DownloadString(string.Format(ApiUrl, Articul));

            var apiResponse = JsonConvert.DeserializeObject<dynamic>(apiDataString);
            var name = apiResponse?.data?.colors?[0].nomenclatures?[0].supplierInfo?.supplierName ?? "Не удалось распознать";
            return name;
        }

        public int GetColor()
        {
            return unchecked((int)0xFFEC238D);
        }
    }
}