   M S B A M L   `   `   `  ����G  CkioskProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:kioskProjectkioskProject  5   	   5clr-namespace:kioskProject.ViewkioskProject.View  5   	   ?$clr-namespace:kioskProject.ViewModelkioskProject.ViewModel  5   	       kioskProject.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:kioskProject     5   	   0Viewclr-namespace:kioskProject.View     5   	   :	ViewModel$clr-namespace:kioskProject.ViewModel     5   	     � Title$  
MainWindow��5
   	   $	��880��6   $	��670��6)    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType��%,  '��       �� 5   
   " ��6   .�5      ��  � �� �	#FFFF57576+   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $��2q�6K   
�������._�5      �� $��Center=�6/   $��Center=�6J   $��Header��6g   5      5      5      5      5      .��5#      �  � ColumnDefinitions 5$   
   �� 5%      $
��471* �6   �� 5&      $
��210* �6    � RowDefinitions 5(   
   �� 5)      $��0* �6   �� 5*      $
��697* �6   �� 5+      $
��147* �6   .+�5.   
   � ���	#FFFF5757$��0��6&   $��2��66   $��0��6J   $��2��6W   .+�5/      �� $
��Wrap=�6   $��Kiosk��6,   $��Center=�69   $��Center=�6T   $��30(�6q   !  System.Windows.SystemColors   HighlightTextBrushKey#���  6   $��	0,0,0,650q�6�   50      �� 52   
   $
��Auto=�6   $��0��6:   $��2��6J   $��0��6^   $��2��6k   .��53      �� $��2��6   $��0��6-   $��1��6=   $��0,43,0,0q�6J   .@�54      �� $��음식��6   ���	#FFFF5757$��	0,0,-90,0q�6>   .��55      �� $��8,0,0,0q�6K   ��6#   �� *page1ItemModels+��56      �� 57      .R�58   "   � 	 � Orientation$	 
Horizontal=�6,   59      ��5;      �� 5<      .R�5=   "      kioskProject.View.Menu  
 � DataContext
 6,   �� 5>      5@      5A      �� 5B      $��음식2��6   $��90,0,-202,0q�6'   .��5C      �� $��8,0,0,0q�6K   ��6#   �� *page2ItemModels+��5D      �� 5E      .R�5F   "   � $	 
Horizontal=�6,   5G      ��5I      �� 5J      .R�5K   "     
 6,   �� 5L      5N      5O      �� 5P      $��음식3��6   $��202,0,-314,0q�6'   5R      5S      5T      �� 5U   
   $
��Auto=�6   $��0��6=   $��2��6M   $��1��6Z   $��0,2,0,0q�6n   .��5V      �� $��0��6C   $��2��6S   $	��448��6d   ��6   �� *orderItemModels+��5W      �� 5X      .R�5Y      � $	 
Horizontal=�6$   5Z      ��5\      �� 5]      .R�5^      !   kioskProject.View.OrderItem  $��Stretch=�6A   $��Stretch=�6_   
 6)   �� 6_   5_      5a      5b      "   kioskProject.View.TotalPrice  5c   
   -   5          Name$ TotalPriceControl��5c      $��2��65   $��1��6Z   $��1��6j   
 6B   �� 6j   �� 5d   
   $��	관리자��6   $��1��6   $��1��6,   $��Bottom=�6<   $��Right=�6W   $��30��6s   $��66��6   ��6�   �� *
adminClick+5e      5f      