﻿#pragma checksum "..\..\Clock.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A36252FFF9A3714E0EBBD1B382B85808C2CC326F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ScullFurnaces_32;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ScullFurnaces_32 {
    
    
    /// <summary>
    /// Clock
    /// </summary>
    public partial class Clock : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 156 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hourBox;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button hoursUp;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button hoursDown;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minutesUp;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minutesDown;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button secondsUp;
        
        #line default
        #line hidden
        
        
        #line 170 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button secondsDown;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Ticks;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ticksUp;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\Clock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ticksDown;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        
        #line 3 "..\..\Clock.xaml"

public ClockWatch clockWatch;
public Clock()
{
	InitializeComponent();
	clockWatch = (ClockWatch)Resources["newClock"];
}

private void clockButton_Click(object O,RoutedEventArgs e)
{
	Button someButton = O as Button;
	switch(someButton.Name)
	{
		case "hoursUp":
			UpHour();
			break;
		case "hoursDown":
			DownHour();
			break;
		case "minutesUp":
			UpMinute();
			break;
		case "minutesDown":
			DownMinute();
			break;
		case "secondsUp":
			UpSecond();
			break;
		case "secondsDown":
			DownSecond();
			break;
		case "ticksUp":
			UpTick();
			break;
		case "ticksDown":
			DownTick();
			break;
	}
}
private void DownSecond()
{
	if(clockWatch.Seconds>0)
		clockWatch.Seconds--;
	else
		if(DownMinute())
		clockWatch.Seconds=59;
}
private void UpSecond()
{
	if(clockWatch.Seconds<59)
		clockWatch.Seconds++;
	else
		if(UpMinute())
		clockWatch.Seconds=0;
}

private bool DownMinute()
{
	if(clockWatch.Minutes>0)
	{
		clockWatch.Minutes--;
		return true;
	}	
	else
		
		if(DownHour())
		{
			clockWatch.Minutes=59;
			return true;
		}
		else
			return false;
}
private bool UpMinute()
{
	if(clockWatch.Minutes<59)
	{
		clockWatch.Minutes++;
		return true;
	}	
	else
		
		if(UpHour())
		{
			clockWatch.Minutes=0;
			return true;
		}
		else
			return false;
}

private bool DownHour()
{
	if(clockWatch.Hours>0)
	{
		clockWatch.Hours--;
		return 	true;
	}
	else
	return false;	
}
private bool UpHour()
{
	if(clockWatch.Hours<23)
	{
		clockWatch.Hours++;
		return 	true;
	}
	else
	return false;	
}
private bool UpTick()
{
	if(clockWatch.Ticks<(24*60*60-1))
	{
		clockWatch.Ticks++;
		return 	true;
	}
	else
	return false;	
}
private bool DownTick()
{
	if(clockWatch.Ticks>0)
	{
		clockWatch.Ticks--;
		return 	true;
	}
	else
	return false;	
}

        #line default
        #line hidden
        
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ScullFurnaces-32;component/clock.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Clock.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.hourBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.hoursUp = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\Clock.xaml"
            this.hoursUp.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.hoursDown = ((System.Windows.Controls.Button)(target));
            
            #line 160 "..\..\Clock.xaml"
            this.hoursDown.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.minutesUp = ((System.Windows.Controls.Button)(target));
            
            #line 164 "..\..\Clock.xaml"
            this.minutesUp.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.minutesDown = ((System.Windows.Controls.Button)(target));
            
            #line 165 "..\..\Clock.xaml"
            this.minutesDown.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.secondsUp = ((System.Windows.Controls.Button)(target));
            
            #line 169 "..\..\Clock.xaml"
            this.secondsUp.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.secondsDown = ((System.Windows.Controls.Button)(target));
            
            #line 170 "..\..\Clock.xaml"
            this.secondsDown.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Ticks = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.ticksUp = ((System.Windows.Controls.Button)(target));
            
            #line 174 "..\..\Clock.xaml"
            this.ticksUp.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ticksDown = ((System.Windows.Controls.Button)(target));
            
            #line 175 "..\..\Clock.xaml"
            this.ticksDown.Click += new System.Windows.RoutedEventHandler(this.clockButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

