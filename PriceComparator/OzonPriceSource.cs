using System;
using System.Diagnostics;
using System.Net;
using HtmlAgilityPack;

namespace PriceComparator
{
    public class OzonPriceSource : IPriceSource
    {
        public string Tag => "Ozon";
        public string Producer { get; private set; }
        public string Url { get; set; }

        private bool _loaded;
        private float _price;

        public OzonPriceSource(string url)
        {
            Url = url;
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
                var htmlString = new WebClient().DownloadString(Url);

                var document = new HtmlDocument();
                document.LoadHtml(htmlString);

                var priceSpan = document.DocumentNode.SelectSingleNode("//span[contains(@class, 'c2h5')]");
                var producerA = document.DocumentNode.SelectSingleNode("//div[contains(@class, 'e7j4')]/a");
                
                var priceSpanInnerText = priceSpan.InnerText.Trim().Replace("&#xA0;", "").Replace("₽", "").Trim();

                _price = float.Parse(priceSpanInnerText);
                Producer = Uri.UnescapeDataString(producerA.InnerText.Replace("quot;", "\""));
                _loaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public int GetColor()
        {
            return unchecked((int)0xFF04B8F0);
        }
    }
}