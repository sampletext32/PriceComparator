using System.Drawing;
using System.Windows.Forms;

namespace PriceComparator
{
    public partial class PriceSourceCard : UserControl
    {
        private IPriceSource _priceSource;

        public PriceSourceCard(IPriceSource priceSource)
        {
            InitializeComponent();
            _priceSource = priceSource;
            Fill();
        }

        private void Fill()
        {
            labelTitle.Text = _priceSource.Producer;
            labelPrice.Text = _priceSource.GetPrice().ToString("F") + " р";
            panel1.BackColor = Color.FromArgb(_priceSource.GetColor());
        }
    }
}