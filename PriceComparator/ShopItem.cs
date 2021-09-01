using System.Collections.Generic;

namespace PriceComparator
{
    public class ShopItem
    {
        public List<IPriceSource> PriceSources { get; private set; }

        private string Title { get; set; }
        
        public ShopItem(string title)
        {
            Title = title;
            PriceSources = new();
        }

        public void AddPriceSource(IPriceSource source)
        {
            PriceSources.Add(source);
            source.LoadPrice();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}