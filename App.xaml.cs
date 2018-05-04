﻿using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using static Constants;
using static System.Console;
using System.Linq;
using System.Windows.Data;
using System.Globalization;
using System.IO;
namespace ScullFurnaces_32
{
    partial class Scull_Furnaces_AppMain_Class : Application
    {
        public Button show_number_button;
        public MyUserSettings mySettings;
        public Binding colorBinding;
        public ColorSource colorSource;
        public Scull_Furnaces_Main_Window scull_Furnaces_Main_Window;
        public TickParamsAll unpackedParameters;
        public void Startup_Procedure(object x, System.EventArgs e)
        {
            Console.WriteLine("Запуск главной формы");

            Application app = Current;
            //Считываем с диска файл с параметрами,имя файла запомнено с помощью 
            //механизма ApplicationSettingBase 
            mySettings = new MyUserSettings();
            scull_Furnaces_Main_Window = new Scull_Furnaces_Main_Window();
            scull_Furnaces_Main_Window.InitializeComponent();
            //Устанавливаем имя считываемого файла из сохранённого значания, оно будет обновлятся
            //после диалога с пользователем
            TabControlData[0].canvasForDiplayedPlot = scull_Furnaces_Main_Window.voltageGraph;
            TabControlData[1].canvasForDiplayedPlot = scull_Furnaces_Main_Window.currentGraph;
            TabControlData[2].canvasForDiplayedPlot = scull_Furnaces_Main_Window.vacuumGraph;
            TabControlData[3].canvasForDiplayedPlot = scull_Furnaces_Main_Window.waterGraph;
            scull_Furnaces_Main_Window._remembranceOfFileName.fileName = mySettings.FileName;
            //записываем его в глобальное поле как для доступа из консольной программы 
            Globals.fileName = scull_Furnaces_Main_Window._remembranceOfFileName.fileName;
            WriteLine("Считанное имя файла : {0}", Globals.fileName);
            scull_Furnaces_Main_Window._remembranceOfFileName.PropertyChanged += rememberTheChosenFileName;

            Globals.readAllTheBytes(Globals.fileName); //проверка файла через чтение его в память и проверку первого блока
            ReadParamFromDisk packedParameters = new ReadParamFromDisk(Globals.fileName); //повторное чтение файла параметров
                                                                                          //в память с инструментарием для работы с ним.
            unpackedParameters = new TickParamsAll(packedParameters); //получение распакованного массива
                                                                      //File.WriteAllBytes(Globals.fileName+".bin", unpackedParameters.inflatedParameters); //запись на диск 
                                                                      //распакованного массива


            scull_Furnaces_Main_Window.maxValueForCurrentOnYAxis.Text = mySettings.UpperLimitForCurrent;
            //scull_Furnaces_Main_Window.maxValueForCurrentOnYAxis.DataContext = mySettings;
            scull_Furnaces_Main_Window.maxValueForVoltageOnYAxis.Text = mySettings.UpperLimitForVoltage;

            scull_Furnaces_Main_Window.begTimeOnXAxis.clockWatch.Alarm_On = false; //не обнавлять график при начальной установке диапазона времени для отображения 
            scull_Furnaces_Main_Window.endTimeOnXAxis.clockWatch.Alarm_On = false; //не обнавлять график при начальной установке диапазона времени для отображения 
            scull_Furnaces_Main_Window.begTimeForVoltageOnXAxis.clockWatch.Alarm_On = false;
            scull_Furnaces_Main_Window.endTimeForVoltageOnXAxis.clockWatch.Alarm_On = false;

            scull_Furnaces_Main_Window.begTimeOnXAxis.Ticks.Text = mySettings.TimeLowerBoundaryForTheCurrent.ToString();
            scull_Furnaces_Main_Window.endTimeOnXAxis.Ticks.Text = mySettings.TimeUpperBoundaryForTheCurrent.ToString();
            scull_Furnaces_Main_Window.begTimeForVoltageOnXAxis.Ticks.Text = mySettings.TimeLowerBoundaryForTheVoltage.ToString();
            scull_Furnaces_Main_Window.endTimeForVoltageOnXAxis.Ticks.Text = mySettings.TimeUpperBoundaryForTheVoltage.ToString();

            scull_Furnaces_Main_Window.begTimeOnXAxis.clockWatch.AlarmProcedure += scull_Furnaces_Main_Window.setMinTimeValue;
            scull_Furnaces_Main_Window.endTimeOnXAxis.clockWatch.AlarmProcedure += scull_Furnaces_Main_Window.setMaxTimeValue;
            scull_Furnaces_Main_Window.begTimeForVoltageOnXAxis.clockWatch.AlarmProcedure += scull_Furnaces_Main_Window.setMinTimeForVoltageValue;
            scull_Furnaces_Main_Window.endTimeForVoltageOnXAxis.clockWatch.AlarmProcedure += scull_Furnaces_Main_Window.setMaxTimeForVoltageValue;

            scull_Furnaces_Main_Window.momentOfTime.clockWatch.AlarmProcedure += scull_Furnaces_Main_Window.setParameterValueOnTheTab;
            scull_Furnaces_Main_Window.momentOfTime.clockWatch.AlarmProcedure += testAlarm;
            scull_Furnaces_Main_Window.begTimeOnXAxis.clockWatch.Alarm_On = true;   //обновлять график при изменении нижней границы диапазона времени	
            scull_Furnaces_Main_Window.endTimeOnXAxis.clockWatch.Alarm_On = true; //обновлять график при изменении верхней границы диапазона времени
            scull_Furnaces_Main_Window.begTimeForVoltageOnXAxis.clockWatch.Alarm_On = true;
            scull_Furnaces_Main_Window.endTimeForVoltageOnXAxis.clockWatch.Alarm_On = true;

            scull_Furnaces_Main_Window.unpackedParameters = this.unpackedParameters;
            scull_Furnaces_Main_Window.WindowStyle = WindowStyle.ToolWindow;
            scull_Furnaces_Main_Window.Show();
            //Создаём табло с набором дискретных параметров и привязкой каждого из них к свойству parameterState
            //в обьекте класса ColorSource
            ParameterAllValues parameterDescription = null;
            foreach (var pair in ParameterData)
            {
                parameterDescription = pair.Value;
                if (parameterDescription.parameterType == ParameterType.дискретный)
                {
                    DiscretePlaque btnControl = new DiscretePlaque() { Content = parameterDescription.parameterName };
                    btnControl.discreteNumber = pair.Key; // the key in the Dictionary remembered
                                                          
                    ColorSource colorSourceForAParameter = new ColorSource(); //в свойстве parameterState этого объекта храниться состояние дискрета вкл/выкл
                    //опеределяющее цвет привязанного к нему Plaque, информационной таблички-кнопки.
                    parameterDescription.colorBinding.Source = colorSourceForAParameter;
                    parameterDescription.colorBinding.Converter = colorSourceForAParameter;

                    btnControl.SetBinding(Button.BackgroundProperty, parameterDescription.colorBinding);
                    btnControl.Template = (ControlTemplate)scull_Furnaces_Main_Window.FindResource("MyBetterButtonTemplate");

                    scull_Furnaces_Main_Window.uniGrid.Children.Add(btnControl);
                }
            }

            //Устанаваливаем номер вкладки на окне из сохранённого значения, он будет обновлятся далее
            //автоматически через привязку BINDING к номеру вкладки в XAML окна

            scull_Furnaces_Main_Window._typeOfParameters.PropertyChanged += SelectThePageWithParameters;
            scull_Furnaces_Main_Window._typeOfParameters.PropertyChanged += testSelectPage;
            scull_Furnaces_Main_Window._typeOfParameters.theNumberOfTab = mySettings.SelectedIndex;
            TimeMover.numberOfSecond = 0;

        }
        void SelectThePageWithParameters(object o, PropertyChangedEventArgs a)
        {
            WriteLine("void SelectThePageWithParameters(object o, PropertyChangedEventArgs a)");
            scull_Furnaces_Main_Window.setParameterValueOnTheTab(new AlarmEventArgs() { TicksToAlarm = scull_Furnaces_Main_Window.momentOfTime.clockWatch.Ticks });
            scull_Furnaces_Main_Window.Background = chooseTheColorScheme(scull_Furnaces_Main_Window._typeOfParameters.theNumberOfTab);
            mySettings.SelectedIndex = scull_Furnaces_Main_Window._typeOfParameters.theNumberOfTab;
            mySettings.Save();

        }
        void testAlarm(AlarmEventArgs ae) { WriteLine("test Alarm"); }
        void testSelectPage(object o, PropertyChangedEventArgs a) { WriteLine("test Select Page"); }

        void rememberTheChosenFileName(object o, PropertyChangedEventArgs a)
        {
            mySettings.FileName = scull_Furnaces_Main_Window._remembranceOfFileName.fileName;
            mySettings.Save();

        }
        public Brush chooseTheColorScheme(int TabNumber)
        {
            switch (TabNumber)
            {
                case 0:
                    return Brushes.Blue;
                case 1:
                    return Brushes.Blue;
                case 2:
                    return Brushes.Blue;
                case 3:
                    return Brushes.Blue;
                default:
                    return Brushes.Red;
            }

        }


    }

    public class MyUserSettings : ApplicationSettingsBase
    {
        //public static MyUserSettings Default = new MyUserSettings();
        static string hiddenSettingsFilePath = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
        static string localSettingsFilePath = AppDomain.CurrentDomain.BaseDirectory + "user.config";
        static MyUserSettings()
        {
            if (File.Exists(localSettingsFilePath) & File.Exists(hiddenSettingsFilePath))
                File.Copy(localSettingsFilePath, hiddenSettingsFilePath, true);
        }


        Scull_Furnaces_AppMain_Class app = (Scull_Furnaces_AppMain_Class)Application.Current;
        [ApplicationScopedSetting()]
        public Brush Background
        {
            get
            {
                return ((Brush)this["Background"]);
            }
            set
            {
                this["Background"] = app.chooseTheColorScheme(app.scull_Furnaces_Main_Window._typeOfParameters.theNumberOfTab);
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public Int32 SelectedIndex
        {
            get
            {
                return ((Int32)this["SelectedIndex"]);
            }
            set
            {
                this["SelectedIndex"] = (Int32)value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("20170925.911")]
        public string FileName
        {
            get
            {
                return ((string)this["FileName"]);
            }
            set
            {
                this["FileName"] = (string)value;
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("100")]
        public string UpperLimitForVoltage
        {
            get
            {
                return ((string)this["UpperLimitForVoltage"]);
            }
            set
            {
                this["UpperLimitForVoltage"] = (string)value;
                //			this.Save();
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("100")]
        public string UpperLimitForCurrent
        {
            get
            {
                return ((string)this["UpperLimitForCurrent"]);
            }
            set
            {
                this["UpperLimitForCurrent"] = (string)value;
                //			this.Save();
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("86399")]
        public int TimeUpperBoundaryForTheVoltage
        {
            get
            {
                return ((int)this["TimeUpperBoundaryForTheVoltage"]);
            }
            set
            {
                this["TimeUpperBoundaryForTheVoltage"] = (int)value;
                //			this.Save();
            }

        }
        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public int TimeLowerBoundaryForTheVoltage
        {
            get
            {
                return ((int)this["TimeLowerBoundaryForTheVoltage"]);
            }
            set
            {
                this["TimeLowerBoundaryForTheVoltage"] = (int)value;
                //			this.Save();
            }

        }
        [UserScopedSetting()]
        [DefaultSettingValue("86399")]
        public int TimeUpperBoundaryForTheCurrent
        {
            get
            {
                return ((int)this["TimeUpperBoundaryForTheCurrent"]);
            }
            set
            {
                this["TimeUpperBoundaryForTheCurrent"] = (int)value;
                //			this.Save();
            }
        }
        [UserScopedSetting()]
        [DefaultSettingValue("0")]
        public int TimeLowerBoundaryForTheCurrent
        {
            get
            {
                return ((int)this["TimeLowerBoundaryForTheCurrent"]);
            }
            set
            {
                this["TimeLowerBoundaryForTheCurrent"] = (int)value;
                //			this.Save();
            }

        }

        public override void Save()
        {
            base.Save();
            File.Copy(hiddenSettingsFilePath, localSettingsFilePath, true);
        }

    }

    public class ColorSource : INotifyPropertyChanged, IValueConverter
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _parameterState;
        public int parameterState
        {
            get
            {
                return _parameterState;
            }
            set
            {
                _parameterState = value;
                OnPropertyChanged("parameterState");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush background = (int)value == 0 ? Brushes.Red : Brushes.Green;
            return background;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
