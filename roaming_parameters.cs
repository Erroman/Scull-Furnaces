﻿using System;
using System.Windows;
using static Constants;
using static TransformWorldToScreen;
namespace ScullFurnaces_32
{
    partial class Scull_Furnaces_Main_Window
    {
        public void setParameterValueOnTheTab(AlarmEventArgs aea)
        {
            TickParamsAll unpackedParameters = app.unpackedParameters;
            short intAssembled;
            TabData tab = TabControlData[_typeOfParameters.theNumberOfTab];

            if ((tab.parameterType) == (ParameterType.дискретный))
            {
                TimeMover.numberOfSecond = aea.TicksToAlarm;
                instantParameterValue.Visibility = Visibility.Collapsed;
                instantParameterName.Visibility = Visibility.Collapsed;
            }
            else
            //устанавливается значение аналогового параметра
            //при выборе вкладки или изменении значения таймера
            {
                int iParam = (int)tab.parameterName + aea.TicksToAlarm * Constants.ParamsBlockLengthInBytes;
                System.Console.WriteLine("iParam = {0}", iParam);
                intAssembled = BitConverter.ToInt16(unpackedParameters.inflatedParameters, iParam);
                //Globals.swapBytes(ref intAssembled);
                string unitOfMeasure;
                switch (Constants.ParameterData[(int)(tab.parameterName)].parameterUnit)
                {
                    case ParameterUnit._:
                        unitOfMeasure = "";
                        break;
                    case ParameterUnit.м3_ч:
                        unitOfMeasure = "м3/ч";
                        break;
                    case ParameterUnit.мм_рт_ст:
                        if (intAssembled < 0)
                        {
                            unitOfMeasure = "мм рт.ст";
                            intAssembled &= 0x777;
                        }
                        else
                            unitOfMeasure = "мк";
                        break;
                    default:
                        unitOfMeasure = "";
                        break;
                }
                if (tab.canvasForDiplayedPlot != null) Console.WriteLine(tab.canvasForDiplayedPlot.Name);
                if (tab.canvasForDiplayedPlot != null) doTheLineDrawing(tab.canvasForDiplayedPlot, WtoD(new Point(aea.TicksToAlarm, 0)),tab);
                instantParameterValue.Visibility = Visibility.Visible;
                instantParameterName.Visibility = Visibility.Visible;
                instantParameterValue.Content = String.Format("{0} " + unitOfMeasure, (float)intAssembled / 10);
                instantParameterName.Content = tab.parameterNameToDisplay;
            }
        }
    }
}