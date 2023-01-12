using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.IO.Ports;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using MB;

namespace WinBrake
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel MyModel { get; private set; }//OxyPlot function   
        public OxyPlot.Series.LineSeries I, D, V, U;//OxyPlot function
  
        public static DispatcherTimer ScanTimer;

        public static Double t;       
        public static  Int16 current, distance, voltage;
        public static float revolution,revolinv,distgraf,voltgraf;
        public static int n;
        public static string[] ports;
        public static List<string> portsList;
        public static UInt16 SpacePar,GreasePar;
        public static bool COMMAND;

        private void SetPar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UInt16.TryParse(Space.Text,out SpacePar);
            Modbus.HOLDREGS[0].w = SpacePar;
            UInt16.TryParse(Grease.Text, out GreasePar);
            Modbus.HOLDREGS[1].w = GreasePar;
            Modbus.ReqStruc.unitId = 0x1;
            Modbus.ReqStruc.funCode = 0x10;
            Modbus.ReqStruc.startAdr.w = Modbus.HOLDADR0;
            Modbus.ReqStruc.quantity.w = 4;
            SetPar.Background = Brushes.LightPink;
            COMMAND = true;
        }

       
        private void GetPar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Modbus.ReqStruc.unitId = 0x1;
            Modbus.ReqStruc.funCode = 0x3;
            Modbus.ReqStruc.startAdr.w = Modbus.HOLDADR0;
            Modbus.ReqStruc.quantity.w = 4;
            GetPar.Background = Brushes.LightPink;
            COMMAND = true;
        }


        //     public static SerialPort serial;
        Modbus mbus;
        public DateTime StarTime,LasTime;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.MyModel = new PlotModel
            {//OxyPlot function
                Title = "MOTOR", TitleColor = OxyColor.FromArgb(0xff, 0xff, 0, 0),
            };
            MyModel.Axes.Clear();
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 60.0, AbsoluteMinimum = -10.0, AbsoluteMaximum = 61.00 , Title = "t[s]" }); ;
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -100, Maximum = 100, AbsoluteMinimum = -200, AbsoluteMaximum = 200, Title = "[%]" });
            MyModel.Axes[1].TextColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].TicklineColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].TitleColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].AxislineColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            I = new OxyPlot.Series.LineSeries();//OxyPlot function...
            I.StrokeThickness = 1.5;
            I.Title = "I";
            I.Color = OxyColor.FromRgb(0xff, 0, 0);
            I.TextColor= OxyColor.FromArgb(0xff,0xff, 0, 0);
            I.FontSize = 12;
            I.Font = "normal";
            this.MyModel.Series.Add(I);
            D = new OxyPlot.Series.LineSeries();
            D.Color = OxyColor.FromRgb(0, 0xff, 0);
            D.StrokeThickness = 1.5;
            this.MyModel.Series.Add(D);
            V = new OxyPlot.Series.LineSeries();
            V.Color = OxyColor.FromRgb(0,0, 0xff);
            V.StrokeThickness = 1.5;
            this.MyModel.Series.Add(V);        
            U = new OxyPlot.Series.LineSeries();
            U.Color = OxyColor.FromRgb(0xff, 0x7f, 0);
            U.StrokeThickness = 1.5;
            this.MyModel.Series.Add(U);
            ScanTimer = new DispatcherTimer();
            ScanTimer.Tick += new EventHandler(ScanTimer_Tick);
            ScanTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);//  250ms interval
            ScanTimer.Start();//t
            mbus = new Modbus();
            COM.Content = MB.ser.serial.PortName;
            Modbus.MBrec += new Modbus.rec(RewP);
            Modbus.INPREGS[0].w = 0x22;//t
            LasTime=StarTime = DateTime.Now;
       

        }
        public void RewP()
        {
            if (B.CRCERR)
            {
                B.CRCERR = false;
                COM.Background = Brushes.LightPink;
            }
            else if(COMMAND)
            {
                COMMAND = false;
                if (Modbus.RespStruc.funCode == 0x3)
                {
                    GetPar.Background = Brushes.LightGreen;
                    Space.Text = Modbus.HOLDREGS[0].w.ToString();
                    Grease.Text = Modbus.HOLDREGS[1].w.ToString();
                }
                else
                {
                    SetPar.Background = Brushes.LightGreen;
                }
            }
            else
            {
                COM.Background = Brushes.LightGreen;
                current = (Int16)Modbus.INPREGS[0].w;
                distance = (Int16)Modbus.INPREGS[1].w;
                distgraf = (int)distance >> 6;
                revolinv = Modbus.ConvRegsToFloat(Modbus.INPREGS, 2);
                if (revolinv != 0)
                    revolution = (((3800000 / 2) / 2) / revolinv);
                voltage = (Int16)Modbus.INPREGS[4].w;
                voltgraf = (int)voltage >> 4;
                TimeSpan time = DateTime.Now - StarTime;
                TimeSpan dtime = DateTime.Now - LasTime;
                // t += 0.1;
                t = time.TotalMilliseconds/1000;
                if (dtime.TotalSeconds >= 60)
                {
                    MyModel.Axes[0].Minimum = time.TotalSeconds;
                    MyModel.Axes[0].AbsoluteMinimum = time.TotalSeconds-1;
                    MyModel.Axes[0].Maximum = time.TotalSeconds+60;
                    MyModel.Axes[0].AbsoluteMaximum = time.TotalSeconds+61;
                    LasTime = DateTime.Now;
                  //  t = 0;
                    I.Points.Clear();
                    D.Points.Clear();
                    V.Points.Clear();
                    U.Points.Clear();
                }
                I.Points.Add(new DataPoint(t, current));
                D.Points.Add(new DataPoint(t, distgraf));
                V.Points.Add(new DataPoint(t, revolution));
                U.Points.Add(new DataPoint(t, voltgraf));
                this.MyModel.InvalidatePlot(true);
            }

        }

        public void ScanTimer_Tick(object sender, EventArgs et)
        {
            try
            {
                if (COMMAND)
                {
                  //  Space.Background = Brushes.LightPink;
                }
                else
                {
                    Modbus.ReqStruc.unitId = 0x1;
                    Modbus.ReqStruc.funCode = 0x4;
                    Modbus.ReqStruc.startAdr.w = (ushort)(Modbus.INPADR0);
                    Modbus.ReqStruc.quantity.w = 5;              
                }
                Modbus.Require(Modbus.ReqStruc);
            }            
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Timer tick except:\n" + e.ToString());

            }
        }
    }
}
