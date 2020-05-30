using Clock.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Interop;

namespace Clock.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        CultureInfo culture = new System.Globalization.CultureInfo("zh-CN");
        private WindowState currentWindowState;
        private double windowOpacity;
        public MainViewModel()
        {
            WindowOpacity = 0.9;
            currentWindowState = WindowState.Normal;
            SetUpTimer();

            SetOpacity = new RelayCommand<string>(SetOpacityAction);

            SetWindowStateToNormal = new RelayCommand(SetWindowStateToNormalAction);
            SetWindowStateToMinimized = new RelayCommand(SetWindowStateToMinimizedAction);
            Close = new RelayCommand(CloseAction);

            EnableMouseThrough = new RelayCommand(EnableMouseThroughAction);
            DisableMouseThrough = new RelayCommand(DisableMouseThroughAction);
        }

        public RelayCommand Close { get; set; }
        public WindowState CurrentWindowState
        {
            get { return currentWindowState; }
            set
            {
                currentWindowState = value;
                this.RaisePropertyChanged("CurrentWindowState");
            }
        }

        public DateTimeInfoModel DateTimeInfo { get; set; }
        public RelayCommand DisableMouseThrough { get; set; }
        public RelayCommand EnableMouseThrough { get; set; }
        public Window MainWindow { get; set; }
        public RelayCommand<string> SetOpacity { get; set; }

        public RelayCommand SetWindowStateToMinimized { get; set; }

        public RelayCommand SetWindowStateToNormal { get; set; }

        public double WindowOpacity
        {
            get { return windowOpacity; }
            set
            {
                windowOpacity = value;
                this.RaisePropertyChanged("WindowOpacity");
            }
        }

        private void CloseAction()
        {
            Application.Current.Shutdown();
        }

        private void DisableMouseThroughAction()
        {
            IntPtr hwnd = new WindowInteropHelper(MainWindow).Handle;
            Win32.MakeNormal(hwnd);
        }

        private void EnableMouseThroughAction()
        {
            IntPtr hwnd = new WindowInteropHelper(MainWindow).Handle;
            Win32.MakeTransparent(hwnd);
        }
        private void SetOpacityAction(string obj)
        {
            WindowOpacity = double.Parse(obj);
        }

        private void SetUpTimer()
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

        private void SetWindowStateToMinimizedAction()
        {
            CurrentWindowState = WindowState.Minimized;
        }

        private void SetWindowStateToNormalAction()
        {
            CurrentWindowState = WindowState.Normal;
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;


            #region 拼接农历日期
            var str = ChinaDate.GetChinaDate(now);
            var animal = str[1];
            var gv = str.Substring(3, 2);
            var date = str.Split()[1];

            var f = $"{gv} {animal}年 {date}";
            #endregion
            //Debug.WriteLine($"INFO {f}");

            DateTimeInfo.LunarDateText = f;
            DateTimeInfo.TimeText = now.ToString("HH:mm:ss", culture);
            DateTimeInfo.DayOfWeekText = now.ToString("dddd", culture);
            DateTimeInfo.DateText = now.ToString("yyyyMMMMdd", culture) + "日";
        }

    }
}