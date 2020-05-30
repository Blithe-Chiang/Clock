using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock.ViewModel
{
    public class DateTimeInfoModel : ViewModelBase
    {


        private string dateText;

        public string DateText
        {
            get { return dateText; }
            set
            {
                dateText = value;
                this.RaisePropertyChanged("DateText");
            }
        }


        private string lunarDateText;

        public string LunarDateText
        {
            get { return lunarDateText; }
            set
            {
                lunarDateText = value;
                this.RaisePropertyChanged("LunarDateText");
            }
        }

        private string dayOfWeekText;

        public string DayOfWeekText
        {
            get { return dayOfWeekText; }
            set
            {
                dayOfWeekText = value;
                this.RaisePropertyChanged("DayOfWeekText");
            }
        }


        private string timeText;

        public string TimeText
        {
            get { return timeText; }
            set
            {
                timeText = value;
                this.RaisePropertyChanged("TimeText");
            }
        }

    }
}
