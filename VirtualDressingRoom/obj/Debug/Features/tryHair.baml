   M S B A M L   `   `   `  ����N  JVirtualDressingRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VirtualDressingRoomVirtualDressingRoom  5   	   !    VirtualDressingRoom.tryHair   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VirtualDressingRoom     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5       � Title$ Try Hair Color��5   
   $	��768��6!   $
��1366��6.    � WindowState$ 	Maximized=�6i   .��5
      �� $��10q�6    � 	Resources 5   
      MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%�   &	        �� 5      " ��6*   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      � #��z��6#   .+�5   "   �� $��Center=�63   $��Center=�5      5      5      5      5      5      .+�5   
   � .+�5      �� .+�5      � .+�5      (� $��Center=�6   $��Right=�69   $	��546��6U   $��0,157,685,308q�6b   $	��592��6y   .��5       � $	��329��6   $	��308��6+   .+�5!      �� -   5        $��camera��5!   W   $	��Top=�6$   $
��Left=�6<   6W   �� 5"      -   5        $��canvas��5"   %   $	��Top=�63   $
��Left=�6K   5#      5$      � 5&      $
��Left=�6   $	��114��66   $	��Top=�6C   $
��1366��6[   ���#38A1F3.+�5'      �� -   5         ��Name$ TitleLbl��5'       $��Virtual Dressing Room��62   $
��Left=�6R   $��
439,20,0,0q�6m   $��94��6�   $	��Top=�6�   $	��517��6�   $��48(�6�   
�������$
��Bold"�6�   �� 5)      -   5        	 ��Name$	 border��5)   H   #��[  6!   $��70��6X   $��70��6d   $
��Left=�6o   $��1270,25,0,0q�6�   $	��Top=�6�   ���#38A1F3���	#FF3B5998.��5*      �� $6��0/VirtualDressingRoom;component/Resource/home.png��6$   6`   5+      �� 5,      -   5        $	 gender1��5,   ^   #��[  6!   $
��Left=�6o   $��0,10,0,0q�6�   $	��Top=�6�   $��94��6�   $��94��6�   ���#38A1F3���	#FF3B5998.��5.      �� -   5        
 ��Name$
 image4��5.   $   $��70��64   $��70��6@   $
��Left=�6K   $	��Top=�6g   $��13,0,0,0q�6�   $
��Fill=�6�   $8��2/VirtualDressingRoom;component/Resource/female.png��6�   5/      50      � 51      $
��Left=�6   ���#f7f7f7$	��621��6K   $	��Top=�6X   $	��202��6p   $��1163,119,0,0q�6|    � ColumnDefinitions 52      �� 53      $��1697* �6/   �� 54      $
��150* �6/   .+�56      �� -   5        $��hair1��56   ;   $
��Left=�6    $	��130��6H   $	��Top=�6U   $	��130��6m   $��	34,30,0,0q�6y   $7��1/VirtualDressingRoom;component/Resource/1Hair.png��6�    J� Effect 57      3  -System.Windows.Media.Effects.DropShadowEffect  58   "   -	   5        firstEffect��   Color$ 	#FF868686��58   H      	Direction$ 0Q�6Z      ShadowDepth$ 0Q�6h      
BlurRadius$ 10Q�6x   5:      �� 5;      -
   5        $��hair2��5;   ;   $
��Left=�6    $	��130��6H   $	��Top=�6U   $	��130��6m   $��
38,205,0,0q�6y   $7��1/VirtualDressingRoom;component/Resource/2Hair.png��6�    5<        5=   "   -   5        secondEffect��$ 	#FF868686��5=   I   $ 0Q�6[   $ 0Q�6i   $ 3Q�6y   5?      �� 5@      -   5        $��hair3��5@   ;   $
��Left=�6    $	��130��6H   $	��Top=�6U   $	��130��6m   $��
38,373,0,0q�6y   $7��1/VirtualDressingRoom;component/Resource/3Hair.png��6�    5A        5B   "   -   5        thirdEffect��$ 	#FF868686��5B   H   $ 0Q�6Z   $ 0Q�6h   $ 3Q�6x   5D      5F      �� 5H      -   5        $	 button��5H      $��Capture��6-   $
��Left=�6?   $��875,431,0,0q�6Z   $	��Top=�6o   $	��128��6�   $��50��6�   $��17(�6�   6�   �� 5I      -   5        $	 button_Copy1��5I      $��Browse��63   $
��Left=�6D   $��875,347,0,0q�6_   $	��Top=�6t   $	��128��6�   $��50��6�   $��17(�6�    J� RenderTransformOrigin$ -1.133,0.512&�6�   6�   �� 5K      -   5        $	 upButton��5K      $
��Left=�6/   $��1080,125,0,0q�6J   $	��Top=�6`   $��75��6x   
�������$��0q�6�   .��5L      �� -   5        $
 upImage��5L       $:��4/VirtualDressingRoom;component/Resource/up_arrow.png��61   5N      �� 5P      -   5        $	 
downButton��5P      $
��Left=�61   $��1080,652,0,0q�6L   $	��Top=�6b   $��75��6z   $��75��6�   
�������$��0q�6�   .��5Q      �� -   5        $
 	downImage��5Q       $<��6/VirtualDressingRoom;component/Resource/down_arrow.png��63   $ 0.534,-0.329&�6s   5R      �� 5S      -   5        $ label1��5S      $��Label��6,   $
��Left=�6<   $��
49,135,0,0q�6W   $	��Top=�6k   $	��364��6�   5U      5V      5W      5X      5Y      