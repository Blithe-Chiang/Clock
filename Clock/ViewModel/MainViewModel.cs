using Clock.Services;
using GalaSoft.MvvmLight;
using System;
using System.Globalization;
using System.Timers;

namespace Clock.ViewModel
{
    public class MainViewModel : ViewModelBase
    {


        public DateTimeInfoModel DateTimeInfo { get; set; }

        public MainViewModel()
        {
            DateTimeInfo = new DateTimeInfoModel()
            {
                DateText = "2020年05月29日",
                LunarDateText = "庚子 鼠年 闰四月初七",
                DayOfWeekText = "星期五",
                TimeText = "08:57:12"
            };

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        CultureInfo culture = new System.Globalization.CultureInfo("zh-CN");
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Debug.WriteLine($"Info ... {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            var now = DateTime.Now;


            var str = ChinaDate.GetChinaDate(now);
            var animal = str[1];
            var gv = str.Substring(3, 2);
            var date = str.Split()[1];
            
            var f = $"{gv} {animal}年 {date}";

            //Debug.WriteLine($"INFO {f}");


            DateTimeInfo.LunarDateText = f;
            DateTimeInfo.TimeText = now.ToString("HH:mm:ss", culture);
            DateTimeInfo.DayOfWeekText = now.ToString("dddd", culture);
            DateTimeInfo.DateText = now.ToString("yyyyMMMMdd", culture) + "日";
        }

    }
}