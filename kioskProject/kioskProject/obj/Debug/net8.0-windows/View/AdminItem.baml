   M S B A M L   `   `   `  ����G  CkioskProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:kioskProject.ViewkioskProject.View  5   	   !    kioskProject.View.AdminItem   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:kioskProject.View     5   	   $	��269��5      $	��697��6'   .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   ��6    �� *OriginalImage+�� 5   
   $��1��6   $��0��6   ��6.   �� *#!DataContext.adminImageChangeClick+ �� RelativeSource ��  �� AncestorType
 Window��6�   �� .��5      ��  �� Orientation$ Vertical=�6   $��96��60   $	��121��6<   .+�5      �� $��이미지 변경��6   $��Center=�6*   
�������6G   �� 5      $	��120��67   $��82��6C   ��6   �� *ChangeImage+6C   5      5      �� 5   
   
���   �$��2q�6%    �� CornerRadius$ 5��69    �� Padding$ 10q�6J   $��4��6W   $��0��6k   $��2��6x   5      �� 5!   
   $��2��6   $��가격��6$   $��20(�6.   
���   � �� TextAlignment$ Center=�6O   $��Center=�6f   $	��Top=�6�   $��0,18,0,0q�6�   �� 5"   
   $��2��6   $��20(�6C   
���   �$ Center=�6d   $	��Top=�6{   $��0,63,0,0q�6�   ��6$   �� *OriginalPrice+6�   �� 5#   
   $��2��6   $��가격변경��6$   $��20(�60   
���   �$ Center=�6Q   $��Center=�6h   $	��Top=�6�   $��0,18,0,0q�6�   $��1��6�   �� 5$   
   $��2��6   $��Center=�6"   $��0,66,0,0q�6?   $��1��6Q    �� TextWrapping$
 Wrap=�6^   $	��Top=�6�   $	��125��6�   ��6r   �� *ChangePrice+6�   �� 5&   
   $��1��6   $��이름��6$   $��20(�6.   
���   �$ Center=�6O   $��Center=�6f   $	��Top=�6�   $��0,18,0,0q�6�   �� 5'   
   $��1��6   $��20(�6B   
���   �$ Center=�6c   $	��Top=�6z   $��0,63,0,0q�6�   ��6$   �� *OriginalName+6�   �� 5(   
   $��1��6   $��이름변경��6$   $��20(�60   
���   �$ Center=�6Q   $��Center=�6h   $	��Top=�6�   $��0,18,0,0q�6�   $��1��6�   �� 5)   
   $��1��6   $��Center=�6"   $��0,66,0,0q�6?   $��1��6Q   $
 Wrap=�6^   $	��Top=�6�   $	��125��6�   ��6r   �� *
ChangeName+6�   �� 5*   
   $��3��6   $��제품삭제��6!   $��Center=�60   $��Center=�6M   ��6h   �� *" DataContext.adminItemDeleteClick+ �� 
 Window��6�   �� �� 5+   
   $��3��6   $��제품변경��6!   $��Center=�60   $��1��6M   $��Center=�6Z   ��6u   �� *" DataContext.adminItemChangeClick+ �� 
 Window��6�   �� �� 5,   
   $��3��6   $��제품추가��6!   $��Center=�60   $	��Top=�6M   $��	0,125,0,0q�6�   $��2��6�   ��6e   �� *DataContext.adminItemAddClick+ �� 
 Window��6�   �� 6�   5.      5/      