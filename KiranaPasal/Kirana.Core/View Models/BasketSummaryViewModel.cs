using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kirana.Core.View_Models
{
    public class BasketSummaryViewModel
    {
        public int BasketCount { get; set; }//prop indicating the amount of items in the basket
        public decimal BasketTotal { get; set; }//and the total value in the basket
        public BasketSummaryViewModel()
        {

        }
        public BasketSummaryViewModel(int basketCount, decimal basketTotal)//user can pass in default values when creating the viewmodel
        {
            this.BasketCount = basketCount;
            this.BasketTotal = basketTotal;
        }
    }
}
