   M S B A M L   `   `   `  ����G  CkioskProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:kioskProject.ViewkioskProject.View  5   	       kioskProject.View.Login   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:kioskProject.View     5   	     � Title$  Login��5   	   $	��164��6   $	��217��6$   .��5      � .+�5   
   �� $
��Left=�6   $��	10,15,0,0q�6/   $
��Wrap=�6B   $��	아이디��6V   $	��Top=�6a   �� 5   
   $
��Left=�6   $��	10,63,0,0q�6/   $
��Wrap=�6B   $��비밀번호��6V   $	��Top=�6b    J� RenderTransformOrigin$ 0.587,-0.479&�6z   �� 5   
   $
��Left=�6   $��	74,13,0,0q�6-    �� TextWrapping$
 Wrap=�6@   $	��Top=�6s   $	��120��6�   ��6T   �� *loginModel.ID+6�   �� 5   
   $
��Left=�6   $��	74,61,0,0q�6-   $
 Wrap=�6@   $	��Top=�6y   $	��120��6�   ��6T   �� *loginModel.Password+6�   �� 5   
   $��회원가입��6   $
��Left=�6    $��
126,90,0,0q�6;   $	��Top=�6O   $��42��6g   $��71��6s   ��6~   �� *adminRegisterClick+�� 5   
   $��	로그인��6   $
��Left=�6   $��	10,90,0,0q�6:   $	��Top=�6M   $��42��6e   $��71��6q   ��6|   �� *adminLoginClick+��6�   ��  �� RelativeSource ��  �� AncestorType
 Window5      5      