using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipCalculator
{
    public class TipModel : INotifyPropertyChanged
    {
        double bill, tipCustomPercentage,
            tipTen, tipFifteen, tipTwenty, tipCustom, tipCustomValue,
            totalTen, totalFifteen, totalTwenty, totalCustom;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Bill
        {
            set
            {
                if (bill != value)
                {
                    bill = value;
                    //OnPropertyChanged("Bill");
                    CalculatorTip();
                }
            }
            get
            {
                return bill;
            }
        }

        public double TipTen
        {
            set
            {
                if (tipTen != value)
                {
                    tipTen = value;
                    OnPropertyChanged("TipTen");
                }
            }
            get
            {
                return tipTen;
            }
        }

        public double TotalTen
        {
            set
            {
                if (totalTen != value)
                {
                    totalTen = value;
                    OnPropertyChanged("TotalTen");
                }
            }
            get
            {
                return totalTen;
            }
        }

        public double TipFifteen
        {
            set
            {
                if (tipFifteen != value)
                {
                    tipFifteen = value;
                    OnPropertyChanged("TipFifteen");
                }
            }
            get
            {
                return tipFifteen;
            }
        }

        public double TotalFifteen
        {
            set
            {
                if (totalFifteen != value)
                {
                    totalFifteen = value;
                    OnPropertyChanged("TotalFifteen");
                }
            }
            get
            {
                return totalFifteen;
            }
        }

        public double TipTwenty
        {
            set
            {
                if (tipTwenty != value)
                {
                    tipTwenty = value;
                    OnPropertyChanged("TipTwenty");
                }
            }
            get
            {
                return tipTwenty;
            }
        }

        public double TotalTwenty
        {
            set
            {
                if (totalTwenty != value)
                {
                    totalTwenty = value;
                    OnPropertyChanged("TotalTwenty");
                }
            }
            get
            {
                return totalTwenty;
            }
        }
        public double TipCustomPercentage
        {
            set
            {
                if (tipCustomPercentage != value)
                {
                    tipCustomPercentage = value;
                    OnPropertyChanged("TipCustomPercentage");
                }
            }
            get
            {
                return tipCustomPercentage;
            }
        }

        public double TipCustomValue
        {
            set
            {
                if (tipCustomValue != value)
                {
                    tipCustomValue = value;
                    OnPropertyChanged("TipCustomValue");
                    CalculatorCustomTip();
                }
            }
            get
            {
                return tipCustomValue;
            }
        }

        public double TipCustom
        {
            set
            {
                if (tipCustom != value)
                {
                    tipCustom = value;
                    OnPropertyChanged("TipCustom");
                }
            }
            get
            {
                return tipCustom;
            }
        }

        public double TotalCustom
        {
            set
            {
                if (totalCustom != value)
                {
                    totalCustom = value;
                    OnPropertyChanged("TotalCustom");
                }
            }
            get
            {
                return totalCustom;
            }
        }

        void CalculatorTip()
        {
            TipTen = bill * 0.10;
            TipFifteen = bill * 0.15;
            TipTwenty = bill * 0.20;

            TotalTen = bill + tipTen;
            TotalFifteen = bill + tipFifteen;
            TotalTwenty = bill + tipTwenty;

            CalculatorCustomTip();
        }

        void CalculatorCustomTip()
        {
            TipCustom = bill * tipCustomValue;
            TipCustomPercentage = Math.Round(tipCustomValue * 100);
            TotalCustom = bill + tipCustom;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
