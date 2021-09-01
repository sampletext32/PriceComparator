using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PriceComparator
{
    public partial class Form1 : Form
    {
        private List<ShopItem> _shopItems;

        public Form1()
        {
            InitializeComponent();
            _shopItems = new();

            var shopItem1 = new ShopItem("Satya / Благовония Супер Хит /Super Hit. 30гр., набор из 2 блоков по 15 гр.");
            
            shopItem1.AddPriceSource(new WildberriesPriceSource("10383792"));
            shopItem1.AddPriceSource(new OzonPriceSource("https://www.ozon.ru/product/blagovoniya-satya-superhit-satya-super-hit-nabor-iz-2-upakovok-30-gr-239267659/"));
            shopItem1.AddPriceSource(new WildberriesPriceSource("34839544"));
            shopItem1.AddPriceSource(new WildberriesPriceSource("36027650"));


            _shopItems.Add(shopItem1);
            
            var shopItem2 = new ShopItem("Благовония \"Сатья Суперхит / Satya Super Hit\", набор из 2 упаковок, 30 гр.");
            
            shopItem2.AddPriceSource(new OzonPriceSource("https://www.ozon.ru/product/blagovoniya-satya-superhit-satya-super-hit-nabor-iz-2-upakovok-30-gr-239267659/"));
            shopItem2.AddPriceSource(new OzonPriceSource("https://www.ozon.ru/product/blagovoniya-super-hit-satya-super-hit-satya-15g-v-upakovke-2-pachki-231295686/"));
            shopItem2.AddPriceSource(new OzonPriceSource("https://www.ozon.ru/product/blagovoniya-satya-superhit-satya-super-hit-nabor-iz-2-upakovok-30-gr-241747873/"));
            
            _shopItems.Add(shopItem2);
            
            FillItems();
        }

        private void FillItems()
        {
            listBoxItems.Items.Clear();
            listBoxItems.Items.AddRange(_shopItems.ToArray());
        }

        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedIndex == -1)
            {
                return;
            }
            
            flowLayoutPanel1.Controls.Clear();

            var shopItem = _shopItems[listBoxItems.SelectedIndex];
            foreach (var shopItemPriceSource in shopItem.PriceSources)
            {
                var priceSourceCard = new PriceSourceCard(shopItemPriceSource);
                flowLayoutPanel1.Controls.Add(priceSourceCard);
            }
        }
    }
}