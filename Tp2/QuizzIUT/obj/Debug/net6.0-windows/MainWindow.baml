   M S B A M L   `   `   `  ����C  ?QuizzIUT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:QuizzIUTQuizzIUT  5   	       QuizzIUT.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:QuizzIUT     5   	   -   5          �Name$  WNDFenetrePrincipale��5   	    � Title$ 
MainWindow��5   	   $	��412��6   $	��800��6)   .��5	      � -   5         �Name$ WNDFenetrePrincipale1��5	       � ColumnDefinitions 5
   
   �� 5      $	��99* �6   �� 5       � RowDefinitions 5   
   �� 5      $��5* �6   �� 5      $	��39* �6   .+�5   
   �� -   5         ��Name$ LBLTitre��5      $��Questions pour un BUT��6"   $��30(�6B   $��	0,0,472,0q�6P   �� 5   
   -   5        $ LBLQuestion��5      $��Insérez la questions ici��6%   $
��Left=�6H   $��	46,38,0,0q�6c   $��1��6v   $	��Top=�6�   �� 5   
   -   5         ��Name$ 
TBXReponse��5      $
��Left=�6&   $��160,101,0,0q�6A   $��1��6V    �� TextWrapping$
 Wrap=�6c   $��TextBox��6w   $	��Top=�6�   $	��120��6�   6�   �� 5   
   -   5        $ LBLBonnesReponses��5      $��Bonne réponses:��6+   $
��Left=�6E   $��
59,176,0,0q�6`   $��1��6t   $	��Top=�6�   �� 5   
   -   5        $ LBLMauvaisesReponses��5      $��Mauvaise réponses:��6.   $
��Left=�6K   $��
59,227,0,0q�6f   $��1��6z   $	��Top=�6�   �� 5   
   -   5         ��Name$ SDRModeNuit��5      $��615,23,114,0q�6&   	 � Maximum$	 1Q�6<   
 � SmallChange$
 0Q�6H   6X   �� 5   
   $��587,12,146,311q�6   $��1��6(   �� 5   
   -	   5         ��Name$ IMGMoon��5      $��678,16,82,0q�6!   $��	/moon.png��66   $
��Fill=�6I    J� RenderTransformOrigin$ 3.344,1.001&�6X   �� 5   
   -
   5        $ IMGSun��5      $��574,14,182,0q�6    $��/sun.png��66   $
��Fill=�6H   $ 1.5,0.13&�6W   �� 5   
   -   5         ��Name$ 
BTNValider��5      $��Button��6%   $
��Left=�66   $��315,101,0,0q�6Q   $��1��6f   $	��Top=�6s   6�   �� 5   
   -   5        $  LBLMauvaisesReponsesValeur��5      $��0��64   $
��Left=�6@   $��172,227,0,0q�6[   $��1��6p   $	��Top=�6}   �� 5   
   -   5        $ LBLBonnesReponsesValeur��5      $��0��61   $
��Left=�6=   $��169,176,0,0q�6X   $��1��6m   $	��Top=�6z   $ 1.214,-0.653&�6�   �� 5   
   -   5        $ 
BTNQuitter��5      $��Quitter��6%   $
��Left=�67   $��315,169,0,0q�6R   $��1��6g   $	��Top=�6t   6�   5       5!      