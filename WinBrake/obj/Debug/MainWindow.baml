   M S B A M L   `   `   `  ????C  ?WinBrake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WinBrakeWinBrake  5   	       WinBrake.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   
   $localclr-namespace:WinBrake   5   	   -   5          ?Name$  MainWin??5   	    ? Title$ MOTOR??5	   	   $	??592??6   $	??800??6$    ? Icon$ /imagebrake.ico??60    ? ShowInTaskbar .  .??5      ? $	??574??6   $	??Top=?6   .+?5   
     OxyPlot.Wpf.PlotView  -   5          Name$ plotview??5      $??
0,136,0,10q?6D    ? ToolTip$" Scale on axes with the mouse??6X   $??2??6     OxyPlot.Wpf.PlotViewBase   Model 6*   ?? *	MyModel+6   ?? 5   
   -   5         ??Name$	 COM??5      $	??COM??6   $
??Left=?6,   $??	19,15,0,0q?6G   $	??Top=?6Z   $??75??6r   
???  ??$??20??6?    J? RenderTransformOrigin$ 0.511,-0.365&?6?   ?? 5   
   -   5        $ SetPar??5      $??
Set Params??6!   $
??Left=?66   $??
664,36,0,0q?6Q   $	??Top=?6e   $??75??6}   $V PDouble-click the mouse to write values  to the drive. Double-click GetPar first!??6?   	 ? UseLayoutRounding	 . $??1??6'  $??21??67  ?? 5   
   -   5        
 ??Name$
 Space??5      $
??Left=?6"   $??23??6=   $??
573,57,0,0q?6I    ?? TextWrapping$
 Wrap=?6]   $??0??6q   $	??Top=?6z   $??64??6?   $??1??6?   
???  ??6?   ?? 5   
   -   5         ??Name$ 
SpaceLabel??5      $??SPACING[mm]:??6$   $
??Left=?6;   $??
569,36,0,0q?6V   $	??Top=?6j   $ 1.022,0.423&?6?   $??1??6?   $??26??6?   $??88??6?   
???  ??6?   ?? 5   
   -   5        $ GetPar??5      $??
Get Params??6!   $
??Left=?66   $??
197,40,0,0q?6Q   $	??Top=?6e   $??75??6}   $7 1Double-click the mouse to read values  from drive??6?   $??1??6?   $??20??6?   ?? 5   
   -   5        $
 Moment??5      $
??Left=?6"   $??23??6=   $??
389,15,0,0q?6I   $
 Wrap=?6]   $??0??6q   $	??Top=?6z   $??72??6?   $??1??6?   
???  ??6?   ?? 5   
   -	   5        $ MomentLabel??5      $??FORCEMOMENT[kN]:??6%   $
??Left=?6@   $??
359,-5,0,0q?6[   $	??Top=?6o   $ 0.237,0.308&?6?   $??1??6?   $??26??6?   $	??122??6?   
???  ??6?   ?? 5   
   -
   5         ??Name$ GreasEna??5      $??Grease
 Enable ??6%   $
??Left=?6H   $??	210,4,0,0q?6c   $	??Top=?6v   $??34??6?   $ 0.032,0.531&?6?   $??1??6?   $??62??6?   ?? 5   
   -   5        $ Imotor??5      $??Imotor[10A= 100%]??6#   $
??Left=?6?   $??
134,99,0,0q?6Z   $	??Top=?6n    d? 	IsChecked$
 TrueY?6?   
???  ??
???  ??
???  ??$??15??6?   $	??124??6?   ?? 5   
   -   5        $ Distance??5      $??Distance[10mm=100%]??6%   $
??Left=?6C   $??
272,99,0,0q?6^   $	??Top=?6r   $	??148??6?   $
 TrueY?6?   
??? ? ????	#FF21B721???	#9900FF00$??15??6?   ?? 5   
   -   5        $ 
Revolution??5      $??Revolution[60rpm=100%]??6'   $
??Left=?6H   $??436,100,0,0q?6c   $	??Top=?6x   $	??168??6?   ???	#990000FF
????  ?
????  ?$
 TrueY?6?   $??15??6?   ?? 5   
   -   5        $ Voltage??5      $??Voltage[24V=100%]??6$   $
??Left=?6@   $??620,100,0,0q?6[   $	??Top=?6p   $	??174??6?   ???	#99FF8000???	#FFFF8000???	#FFFF8000$
 TrueY?6?   $??15??6?   ?? 5   
   -   5        $
 Force??5      $
??Left=?6!   $??
389,57,0,0q?6<   $
 Wrap=?6P   $??0??6d   $	??Top=?6m   $??63??6?   $??22??6?   $??1??6?   ?? 5   
   -   5        $ 
ForceLabel??5      $??FORCETIME[s]:??6$   $
??Left=?6<   $??
378,38,0,0q?6W   $	??Top=?6k   $ 0.474,-0.153&?6?   $??1??6?   $??26??6?   $??87??6?   ?? 5   
   -   5        $ NoforceLabel??5      $??NOFORCE[s]:??6&   $
??Left=?6<   $??
283,39,0,0q?6W   $	??Top=?6k   $ 0.474,-0.153&?6?   $??1??6?   $??26??6?   $??78??6?   ?? 5   
   -   5        $
 Noforce??5      $
??Left=?6#   $??
291,58,0,0q?6>   $
 Wrap=?6R   $??0??6f   $	??Top=?6o   $??63??6?   $??21??6?   $??1??6?   ?? 5   
   -   5        $ ClosingLabel??5      $??CLOSING[mm]:??6&   $
??Left=?6=   $??
575,-5,0,0q?6X   $	??Top=?6l   $ 0.474,-0.153&?6?   $??1??6?   $??26??6?   $??89??6?   ?? 5   
   -   5        $
 Closemm??5      $
??Left=?6#   $??
575,15,0,0q?6>   $
 Wrap=?6R   $??0??6f   $	??Top=?6o   $??62??6?   $??21??6?   $??1??6?   ?? 5    
   -   5        $
 Cycles??5       $
??Left=?6"   $??
482,58,0,0q?6=   $
 Wrap=?6Q   $??0??6e   $	??Top=?6n   $??65??6?   $??20??6?   $??1??6?   ?? 5!   
   -   5        $ CyclesLabel??5!      $??CYCLESbGREASE:??6%   $
??Left=?6>   $??
466,38,0,0q?6Y   $	??Top=?6m   $	??102??6?   $??1??6?   $??26??6?   ?? 5"   
   -   5        $
 Gredist??5"      $
??Left=?6#   $??
482,16,0,0q?6>   $
 Wrap=?6R   $??0??6f   $	??Top=?6o   $??65??6?   $??22??6?   $??1??6?   ?? 5#   
   $??GREASEDIST[mm]:??6   $
??Left=?6*   $??
472,-5,0,0q?6E   $	??Top=?6Y   $ -0.711,0.348&?6q   $??1??6?   $??26??6?   $	??106??6?   ?? 5$   
   -   5        $

 Curt??5$      $??1??6    $
??Left=?60   $??
291,17,0,0q?6K   $
 Wrap=?6_   $??0??6s   $	??Top=?6|   $??63??6?   $??21??6?   
???  ??6?   ?? 5%   
   -   5        $ 	CurtLabel??5%      $??Isw[A]:-----??6#   $
??Left=?6:   $??
291,-5,0,0q?6U   $	??Top=?6i   $??26??6?   $??70??6?   $ 
0.185,0.63&?6?   
???  ??6?   ?? 5&   
   -   5        $ GraphEn??5&      $??Graph
Enable??6$   $
??Left=?6D   $??	10,99,0,0q?6_   $	??Top=?6r   $
 TrueY?6?   ?? 5'   
   -   5        $
 Save??5'      $??
SAVE Graph??6   $
??Left=?64   $??	92,54,0,0q?6O   $	??Top=?6b   $??67??6z   6?   5)      5*      