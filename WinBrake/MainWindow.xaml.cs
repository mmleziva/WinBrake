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
//using System;
using System.Windows.Threading;
using System.IO.Ports;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using MB;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace WinBrake
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel MyModel { get; private set; }//OxyPlot function   
        public OxyPlot.Series.LineSeries I, J, D, V, U;//OxyPlot function
        public static DispatcherTimer ScanTimer;
        public static Double t, ts, ta=0,dt,tsa=0;       
        public static  Int16 current, distance, voltage,errorsrec;
        public static float revolution,distgraf,voltgraf, curturn;
        public static float KM = (float)160 / 10; //KM=(float)128/10;
        public static float KD = (float)225.5;
        public static float SpacePar,  MomentPar, ClosingPar, GredisPar,CurtPar,CurtMem;
        public static Int32  revolinv;
        public static int n, dtn,SAMPLETIME=50,SAMPLESREAD=5,WORDSREAD=6;
        public static string[] ports;
        public static List<string> portsList;
        public static UInt16 ForcePar, CyclePar,NoforcePar;
        Modbus mbus;
        public DateTime StarTime;
        public static bool COMMAND;
        public static TimeSpan time;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.MyModel = new PlotModel
            {//OxyPlot function
                Title = "MOTOR",
                TitleColor = OxyColor.FromArgb(0xff, 0xff, 0, 0),
            };
            MyModel.Axes.Clear();
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 60.0, AbsoluteMinimum = -10.0, AbsoluteMaximum = 61.00, Title = "t[s]" }); ;
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -120, Maximum = 140, AbsoluteMinimum = -200, AbsoluteMaximum = 200, Title = "[%]" });
            MyModel.Axes[1].TextColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].TicklineColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].TitleColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            MyModel.Axes[1].AxislineColor = OxyColor.FromRgb((byte)0xff, 0, 0);
            I = new OxyPlot.Series.LineSeries();//OxyPlot function...
            I.StrokeThickness = 1.5;
            I.Title = "I";
            I.Color = OxyColor.FromRgb(0xff, 0, 0);
            I.TextColor = OxyColor.FromArgb(0xff, 0xff, 0, 0);
            I.FontSize = 12;
            I.Font = "normal";
            this.MyModel.Series.Add(I);
            J = new OxyPlot.Series.LineSeries();//OxyPlot function...
            J.StrokeThickness = 1;
            J.Title = "J";
            J.Color = OxyColor.FromRgb(0xff, 0, 0);
            J.TextColor = OxyColor.FromArgb(0xff, 0xff, 0, 0);
            J.FontSize = 12;
            J.Font = "normal";
            J.LineStyle = LineStyle.Dash;
            this.MyModel.Series.Add(J);
            D = new OxyPlot.Series.LineSeries();
            D.Color = OxyColor.FromRgb(0, 0xff, 0);
            D.StrokeThickness = 1.5;
            this.MyModel.Series.Add(D);
            V = new OxyPlot.Series.LineSeries();
            V.Color = OxyColor.FromRgb(0, 0, 0xff);
            V.StrokeThickness = 1.5;
            this.MyModel.Series.Add(V);
            U = new OxyPlot.Series.LineSeries();
            U.Color = OxyColor.FromRgb(0xff, 0x7f, 0);
            U.StrokeThickness = 1.5;
            this.MyModel.Series.Add(U);
            ScanTimer = new DispatcherTimer();
            ScanTimer.Tick += new EventHandler(ScanTimer_Tick);
            ScanTimer.Interval = new TimeSpan(0, 0, 0, 0, (SAMPLETIME*SAMPLESREAD));//  250ms interval
            ScanTimer.Start();//t
            mbus = new Modbus();
            COM.Content = MB.ser.serial.PortName;
            Modbus.MBrec += new Modbus.rec(RewP);
            Modbus.INPREGS[0].w = 0x22;//t
            StarTime = DateTime.Now;
        }

        private void Save_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Configure save file dialog box
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Shot"; // Default file name
                dlg.Filter = " documents(png)|*.png|documents(pdf)|*.pdf|svg documents(svg)| *.svg "; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    if (dlg.FilterIndex == 2)
                    {
                        using (var fs = File.Create(dlg.FileName + dlg.DefaultExt))
                        {
                            var pdfExporter = new OxyPlot.PdfExporter { Width = 600, Height = 400 };
                            pdfExporter.Export(MyModel, fs);
                        }

                    }
                    else
                        // Save document               
                        using (var fs = new System.IO.FileStream(dlg.FileName + dlg.DefaultExt, FileMode.OpenOrCreate))
                        {
                            switch (dlg.FilterIndex)
                            {
                                case 1:
                                    var pngexp = new PngExporter();
                                    
                                    pngexp.Export(MyModel, fs);
                                    break;
                                case 3:
                                    var svgexp = new OxyPlot.SvgExporter();
                                    svgexp.Export(MyModel, fs);
                                    break;
                                default:
                                    break;
                            }
                        }
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Save file except:\n" + ex.ToString());
            }

        }

        private void SetPar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(float.TryParse(Space.Text,out SpacePar))
                 Modbus.HOLDREGS[0].w = (UInt16)(SpacePar*KD);
            if( float.TryParse(Moment.Text, out MomentPar))
                 Modbus.HOLDREGS[1].w = (UInt16) (MomentPar*KM);
            if (GreasEna.IsChecked == true)
                 Modbus.HOLDREGS[2].w |= 1;
            else
                 Modbus.HOLDREGS[2].w &= 0xfffe;
            if(UInt16.TryParse(Force.Text, out ForcePar))
                 Modbus.HOLDREGS[3].w =ForcePar;
            if(UInt16.TryParse(Noforce.Text, out NoforcePar))
                 Modbus.HOLDREGS[4].w = NoforcePar;
            if(float.TryParse(Closemm.Text, out ClosingPar))
                 Modbus.HOLDREGS[5].w = (UInt16)(ClosingPar*KD);
            if (UInt16.TryParse(Cycles.Text, out CyclePar))
                Modbus.HOLDREGS[6].w = CyclePar;
            if (UInt16.TryParse(Gredist.Text, out CyclePar))
                Modbus.HOLDREGS[6].w = CyclePar;
            if (float.TryParse(Gredist.Text, out GredisPar))
                Modbus.HOLDREGS[7].w = (UInt16)(GredisPar * KD);
            if (float.TryParse(Curt.Text, out CurtPar))
                Modbus.HOLDREGS[8].w = (UInt16)(CurtPar *20 );

            Modbus.ReqStruc.unitId = 0x1;
            Modbus.ReqStruc.funCode = 0x10;
            Modbus.ReqStruc.startAdr.w = Modbus.HOLDADR0;
            Modbus.ReqStruc.quantity.w = 12;
            SetPar.Background = Brushes.LightPink;
            COMMAND = true;
        }
       
        private void GetPar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Modbus.ReqStruc.unitId = 0x1;
            Modbus.ReqStruc.funCode = 0x3;
            Modbus.ReqStruc.startAdr.w = Modbus.HOLDADR0;
            Modbus.ReqStruc.quantity.w = 12;
            GetPar.Background = Brushes.LightPink;
            COMMAND = true;
        }
 
        public void ScanTimer_Tick(object sender, EventArgs et)
        {
            try
            {
                if (B.CRCERR || !ser.serial.IsOpen)
                    errorsrec++;
                else errorsrec = 0;
                if(errorsrec>10)
                {
                    errorsrec = 0;
                    //ser.tryport();
                    //          ser.serial.BaudRate = 115200;
                    //          ser.serial.Open();

                    //          ser.serial.DataReceived += ser.Read;
                    //          ser.BOPEN = true;
                    ser.OpenCOM();
                    COM.Content = MB.ser.serial.PortName;
                }
                else if (COMMAND)
                {
                  //  Space.Background = Brushes.LightPink;
                }
                else
                {
                    time = DateTime.Now - StarTime;
                    t = time.TotalMilliseconds;
                    dt = (t - ta) / SAMPLETIME;
                    dtn = (int)Math.Round(dt);
                    if (dtn > (SAMPLESREAD*3)) dtn = SAMPLESREAD * 3;
                    ta = t;
                    Modbus.ReqStruc.unitId = 0x1;
                    Modbus.ReqStruc.funCode = 0x4;
                    Modbus.ReqStruc.startAdr.w = (ushort)(Modbus.INPADR0);
                    Modbus.ReqStruc.quantity.w = (ushort)((dtn+1)*WORDSREAD) ;              
                }
                Modbus.Require(Modbus.ReqStruc);
            }            
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Timer tick except:\n" + e.ToString());

            }
        }

        public void RewP()
        {
            if (B.CRCERR)
            {
                B.CRCERR = false;
                COM.Background = Brushes.LightPink;
            }
            else if (COMMAND)
            {
                COMMAND = false;
                if (Modbus.RespStruc.funCode == 0x3)
                {
                    GetPar.Background = Brushes.LightGreen;
                    Space.Text = ((float)Modbus.HOLDREGS[0].w / KD).ToString("0.0");
                    Moment.Text = ((float)Modbus.HOLDREGS[1].w / KM).ToString("0.0");
                    GreasEna.IsChecked = (bool)((Modbus.HOLDREGS[2].w & 1) != 0);
                    Force.Text = (Modbus.HOLDREGS[3].w).ToString();
                    Noforce.Text = (Modbus.HOLDREGS[4].w).ToString();
                    Closemm.Text = ((float)Modbus.HOLDREGS[5].w / KD).ToString("0.0");
                    Cycles.Text = (Modbus.HOLDREGS[6].w).ToString();
                    Gredist.Text = ((float)Modbus.HOLDREGS[7].w / KD).ToString("0.0");
                    CurtPar = (float)Modbus.HOLDREGS[8].w / 20;
                    Curt.Text = CurtPar.ToString("0.0");

                }
                else
                if (Modbus.RespStruc.funCode == 0x10)
                {

                    if (Modbus.RespStruc.errorCode != 0)
                        SetPar.Background = Brushes.LightPink;
                    else
                        SetPar.Background = Brushes.LightGreen;
                }

            }
            else
            {
                COM.Background = Brushes.LightGreen;
                if (GraphEn.IsChecked == true)
                {
                    ts = time.TotalSeconds;
                    if (ts - tsa >= 60)
                    {
                        tsa = ts;
                        MyModel.Axes[0].Minimum = tsa;
                        MyModel.Axes[0].AbsoluteMinimum = tsa - 1;
                        MyModel.Axes[0].Maximum = tsa + 60;
                        MyModel.Axes[0].AbsoluteMaximum = tsa + 61;
                        I.Points.Clear();
                        D.Points.Clear();
                        V.Points.Clear();
                        U.Points.Clear();
                        CurtMem = 0;
                    }
                    if (CurtMem != CurtPar)
                    {
                        CurtMem = CurtPar;
                        J.Points.Clear();
                        if (Imotor.IsChecked == true)
                        {
                            J.Points.Add(new DataPoint(MyModel.Axes[0].Minimum, CurtPar * 10));
                            J.Points.Add(new DataPoint(MyModel.Axes[0].Maximum, CurtPar * 10));
                        }
                    }
                    int jk = WORDSREAD;
                    for (int ik = 1; ik <= dtn; ik++)
                    {
                        current = (Int16)Modbus.INPREGS[jk].w;
                        jk++;
                        distance = (Int16)Modbus.INPREGS[jk].w;
                        jk++;
                        revolinv = -Modbus.ConvRegsToInt32(Modbus.INPREGS, jk);
                        if (revolinv != 0)
                            revolution = ((3800000) / (float)revolinv);
                        jk++;
                        jk++;
                        voltage = (Int16)Modbus.INPREGS[jk].w;
                        jk++;
                        jk++;
                        if (Imotor.IsChecked == true) I.Points.Add(new DataPoint(ts, (float)current / 2));//t
                        if (Distance.IsChecked == true) D.Points.Add(new DataPoint(ts, -(float)distance * 10 / KD));
                        if (Revolution.IsChecked == true) V.Points.Add(new DataPoint(ts, revolution));
                        if (Voltage.IsChecked == true) U.Points.Add(new DataPoint(ts, voltage * 0.21));
                        ts += 0.050;
                    }
                    this.MyModel.InvalidatePlot(true);
                }
            }

        }

    }
}
