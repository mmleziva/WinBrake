   M S B A M L   `   `   `  ???? C  ?WinBrake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WinBrakeWinBrake      WinBrake.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   $localclr-namespace:WinBrake   -     ?Name$  MainWin?? ? Title$ MOTOR??$	??592??$	??800?? ? Icon$ /imagebrake.ico?? ? ShowInTaskbar .  .??? $	??574??$	??Top=?.+?  OxyPlot.Wpf.PlotView  -     Name$ plotview??$??
0,136,0,10q? ? ToolTip$" Scale on axes with the mouse??$??2??  OxyPlot.Wpf.PlotViewBase   Model ?? *	MyModel+?? -    ??Name$	 COM??$	??COM??$
??Left=?$??	19,15,0,0q?$	??Top=?$??75??
???  ??$??20?? J? RenderTransformOrigin$ 0.511,-0.365&??? -   $ SetPar??$??
Set Params??$
??Left=?$??
664,36,0,0q?$	??Top=?$??75??$V PDouble-click the mouse to write values  to the drive. Double-click GetPar first!??	 ? UseLayoutRounding	 . $??1??$??21???? -   
 ??Name$
 Space??$
??Left=?$??23??$??
573,57,0,0q? ?? TextWrapping$
 Wrap=?$??0??$	??Top=?$??64??$??1??
???  ???? -    ??Name$ 
SpaceLabel??$??SPACING[mm]:??$
??Left=?$??
569,36,0,0q?$	??Top=?$ 1.022,0.423&?$??1??$??26??$??88??
???  ???? -   $ GetPar??$??
Get Params??$
??Left=?$??
197,40,0,0q?$	??Top=?$??75??$7 1Double-click the mouse to read values  from drive??$??1??$??20???? -   $
 Moment??$
??Left=?$??23??$??
389,15,0,0q?$
 Wrap=?$??0??$	??Top=?$??72??$??1??
???  ???? -	   $ MomentLabel??$??FORCEMOMENT[kN]:??$
??Left=?$??
359,-5,0,0q?$	??Top=?$ 0.237,0.308&?$??1??$??26??$	??122??
???  ???? -
    ??Name$ GreasEna??$??Grease
 Enable ??$
??Left=?$??	210,4,0,0q?$	??Top=?$??34??$ 0.032,0.531&?$??1??$??62???? -   $ Imotor??$??Imotor[10A= 100%]??$
??Left=?$??
134,99,0,0q?$	??Top=? d? 	IsChecked$
 TrueY?
???  ??
???  ??
???  ??$??15??$	??124???? -   $ Distance??$??Distance[10mm=100%]??$
??Left=?$??
272,99,0,0q?$	??Top=?$	??148??$
 TrueY?
??? ? ????	#FF21B721???	#9900FF00$??15???? -   $ 
Revolution??$??Revolution[60rpm=100%]??$
??Left=?$??436,100,0,0q?$	??Top=?$	??168?????	#990000FF
????  ?
????  ?$
 TrueY?$??15???? -   $ Voltage??$??Voltage[24V=100%]??$
??Left=?$??620,100,0,0q?$	??Top=?$	??174?????	#99FF8000???	#FFFF8000???	#FFFF8000$
 TrueY?$??15???? -   $
 Force??$
??Left=?$??
389,57,0,0q?$
 Wrap=?$??0??$	??Top=?$??63??$??22??$??1???? -   $ 
ForceLabel??$??FORCETIME[s]:??$
??Left=?$??
378,38,0,0q?$	??Top=?$ 0.474,-0.153&?$??1??$??26??$??87???? -   $ NoforceLabel??$??NOFORCE[s]:??$
??Left=?$??
283,39,0,0q?$	??Top=?$ 0.474,-0.153&?$??1??$??26??$??78???? -   $
 Noforce??$
??Left=?$??
291,58,0,0q?$
 Wrap=?$??0??$	??Top=?$??63??$??21??$??1???? -   $ ClosingLabel??$??CLOSING[mm]:??$
??Left=?$??
575,-5,0,0q?$	??Top=?$ 0.474,-0.153&?$??1??$??26??$??89???? -   $
 Closemm??$
??Left=?$??
575,15,0,0q?$
 Wrap=?$??0??$	??Top=?$??62??$??21??$??1???? -   $
 Cycles??$
??Left=?$??
482,58,0,0q?$
 Wrap=?$??0??$	??Top=?$??65??$??20??$??1???? -   $ CyclesLabel??$??CYCLESbGREASE:??$
??Left=?$??
466,38,0,0q?$	??Top=?$	??102??$??1??$??26???? -   $
 Gredist??$
??Left=?$??
482,16,0,0q?$
 Wrap=?$??0??$	??Top=?$??65??$??22??$??1???? $??GREASEDIST[mm]:??$
??Left=?$??
472,-5,0,0q?$	??Top=?$ -0.711,0.348&?$??1??$??26??$	??106???? -   $

 Curt??$??1??$
??Left=?$??
291,17,0,0q?$
 Wrap=?$??0??$	??Top=?$??63??$??21??
???  ???? -   $ 	CurtLabel??$??Isw[A]:-----??$
??Left=?$??
291,-5,0,0q?$	??Top=?$??26??$??70??$ 
0.185,0.63&?
???  ???? -   $ GraphEn??$??Graph
Enable??$
??Left=?$??	10,99,0,0q?$	??Top=?$
 TrueY??? -   $
 Save??$??
SAVE Graph??$
??Left=?$??	92,54,0,0q?$	??Top=?$??67??