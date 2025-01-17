﻿using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Constants;
namespace ScullFurnaces_32
{
    partial class Scull_Furnaces_Main_Window
    {

        static Scull_Furnaces_AppMain_Class app = (Scull_Furnaces_AppMain_Class)Application.Current;
        public TypeOfParameters _typeOfParameters = new TypeOfParameters();
        public RemembranceOfFileName _remembranceOfFileName = new RemembranceOfFileName();

        public Scull_Furnaces_Main_Window()
        {
            this.DataContext = _typeOfParameters;
            //И где-то здесь мне надо привязать к таймеру выбор параметра в элементе TextBox instantParameterValue
            //но логичнее это сделать в App.xaml.cs, ведь там я привязал дискреты к таймеру!
        }
        void Window_Closing(object sender, CancelEventArgs e)
        {
            app.mySettings.TimeLowerBoundaryForTheCurrent = Int32.Parse(this.begTimeOnXAxis.Ticks.Text);
            app.mySettings.TimeUpperBoundaryForTheCurrent = Int32.Parse(this.endTimeOnXAxis.Ticks.Text);
            app.mySettings.TimeLowerBoundaryForTheVoltage = Int32.Parse(this.begTimeForVoltageOnXAxis.Ticks.Text);
            app.mySettings.TimeUpperBoundaryForTheVoltage = Int32.Parse(this.endTimeForVoltageOnXAxis.Ticks.Text);
            WriteLine("Upper an lower boundaries for time");
            app.mySettings.Save();

        }
        private void filePickerButton_Click(object sender, RoutedEventArgs e)
        {
            // Create the OpenFIleDialog object
            Microsoft.Win32.OpenFileDialog openPicker = new Microsoft.Win32.OpenFileDialog();

            // Add file filters
            openPicker.DefaultExt = ".911";
            openPicker.Filter = "Файлы дискретных и аналоговых данных с гарнисажных печей|*.911";

            // Display the OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openPicker.ShowDialog();

            // Check to see if we have a result 
            if (result == true)
            {
                // Application now has read/write access to the picked file
                // I am saving the file path to a textbox in the UI to display to the user
                // as well as a Globals.fileName variable to pass to a method
                WriteLine("Имя прежде выбранного файла : {0} ", Globals.fileName);
                filePathTextBox.Text = openPicker.FileName.ToString();
                Globals.fileName = filePathTextBox.Text;
                //проверка содержимого файла,его первого блока
                WriteLine(Globals.fileName);
                Globals.readAllTheBytes(Globals.fileName);
                ReadParamFromDisk packedParameters = new ReadParamFromDisk(Globals.fileName); //повторное чтение файла параметров
                                                                                              //в память с инструментарием для работы с ним.
                app.unpackedParameters = new TickParamsAll(packedParameters); //получение распакованного массива
                this._remembranceOfFileName.fileName = Globals.fileName;
                //TimeMover.numberOfSecond = TimeMover.numberOfSecond;
                //voltagePlotting(ParameterName.Напряжение_дуги, voltageGraph);
                //currentPlotting(ParameterName.Ток_общ, currentGraph);
                //vacuumPlotting(ParameterName.Вакуум, vacuumGraph);
                //waterPlotting(ParameterName.Расход_воды, waterGraph);
            }
        }
        private void voltageGraph_Loaded(object sender, RoutedEventArgs e)
        {
            voltagePlotting(ParameterName.Напряжение_дуги, voltageGraph);
        }
        private void currentGraph_Loaded(object sender, RoutedEventArgs e)
        {
            currentPlotting(ParameterName.Ток_общ, currentGraph);
        }
        private void vacuumGraph_Loaded(object sender, RoutedEventArgs e)
        {
            vacuumPlotting(ParameterName.Вакуум, vacuumGraph);
        }
        private void waterGraph_Loaded(object sender, RoutedEventArgs e)
        {
            waterPlotting(ParameterName.Расход_воды, waterGraph);
        }
        private void setMaxYValueForGraphOfVoltage(object sender, EventArgs e)
        {
            if (e is KeyEventArgs && (e as KeyEventArgs).Key != Key.Return) return;

            WriteLine("maxValueForVoltageOnYAxis = {0}", maxValueForVoltageOnYAxis);
            if (app.mySettings.UpperLimitForVoltage != maxValueForVoltageOnYAxis.Text)
            {
                app.mySettings.UpperLimitForVoltage= maxValueForVoltageOnYAxis.Text;
                app.mySettings.Save();
                voltageGraph.Children.Clear();
                voltagePlotting(ParameterName.Напряжение_дуги, voltageGraph);
            }
        }
        private void setMaxYValueForGraphOfCurrent(object sender, EventArgs e)
        {
            if (e is KeyEventArgs && (e as KeyEventArgs).Key != Key.Return) return;

            WriteLine("tbWithMaxYValue = {0}", maxValueForCurrentOnYAxis);
            if (app.mySettings.UpperLimitForCurrent != maxValueForCurrentOnYAxis.Text)
            {
                app.mySettings.UpperLimitForCurrent = maxValueForCurrentOnYAxis.Text;
                app.mySettings.Save();
                currentGraph.Children.Clear();
                currentPlotting(ParameterName.Ток_общ, currentGraph);


            }
        }
        private void setMaxYValueForGraphOfVacuum(object sender, EventArgs e)
        {
            if (e is KeyEventArgs && (e as KeyEventArgs).Key != Key.Return) return;

            WriteLine("tbWithMaxYValue = {0}", maxValueForVacuumOnYAxis);
            if (app.mySettings.UpperLimitForVacuum != maxValueForVacuumOnYAxis.Text)
            {
                app.mySettings.UpperLimitForVacuum = maxValueForVacuumOnYAxis.Text;
                app.mySettings.Save();
                currentGraph.Children.Clear();
                currentPlotting(ParameterName.Вакуум, vacuumGraph);


            }
        }

        public void setMinTimeValue(AlarmEventArgs aea)
        {
            currentGraph.Children.Clear();
            currentPlotting(ParameterName.Ток_общ, currentGraph, aea);

        }
        public void setMaxTimeValue(AlarmEventArgs aea)
        {
            currentGraph.Children.Clear();
            currentPlotting(ParameterName.Ток_общ, currentGraph, null, aea);

        }
        public void setMinTimeForVoltageValue(AlarmEventArgs aea)
        {
            voltageGraph.Children.Clear();
            voltagePlotting(ParameterName.Напряжение_дуги, voltageGraph, aea);

        }
        public void setMaxTimeForVoltageValue(AlarmEventArgs aea)
        {
            voltageGraph.Children.Clear();
            voltagePlotting(ParameterName.Напряжение_дуги, voltageGraph, null, aea);

        }

        private void maxValueForVacuumOnYAxis_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }

}
