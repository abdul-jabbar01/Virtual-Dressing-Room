   M S B A M L   `   `   `  ����N  JVirtualDressingRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VirtualDressingRoomVirtualDressingRoom  5   	   %    VirtualDressingRoom.tryLipstick   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VirtualDressingRoom     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5       � Title$ Try Lipstick��5   	   $	��768��6   $
��1366��6+    � WindowState$ 	Maximized=�6O   .��5
      �� $��10q�6    � 	Resources 5   
      MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%�   &	        �� 5      " ��6*   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      � #��z��6#   .+�5   "   �� $��Center=�63   $��Center=�5      5      5      5      5      5      .+�5   
   � .+�5      �� .+�5      � $
��1366��6   $	��768��6$   .+�5      (� $��Center=�6   $��Right=�69   $	��500��6U   $��0,222,202,289q�6b   � 5       $
��Left=�6   $	��114��66   $	��Top=�6C   $
��1366��6[   ���#38A1F3.+�5!      �� -   5         ��Name$ TitleLbl��5!       $��Virtual Dressing Room��62   $
��Left=�6R   $��
439,20,0,0q�6m   $��94��6�   $	��Top=�6�   $	��517��6�   $��48(�6�   
�������$
��Bold"�6�   �� 5"      -   5        	 ��Name$	 border��5"   _   #��[  6!   $��70��6H   $��70��6T   $
��Left=�6o   $��1270,25,0,0q�6�   $	��Top=�6�   ���#38A1F3���	#FF3B5998.��5#      �� $6��0/VirtualDressingRoom;component/Resource/home.png��6$   6`   5$      �� 5%      -   5        $	 gender1��5%   I   #��[  6!   $
��Left=�6[   $��0,10,0,0q�6v   $	��Top=�6�   $��94��6�   $��94��6�   ���#38A1F3���	#FF3B5998.��5'      �� -   5        
 ��Name$
 image4��5'   $   $
��Left=�64   $��70��6O   $��70��6[   $	��Top=�6f   $��13,0,0,0q�6   $
��Fill=�6�   $8��2/VirtualDressingRoom;component/Resource/female.png��6�   5(      �� 5)      -   5        $
 image1��5)       $
��Left=�60   $	��100��6K   $��488,41,0,-27q�6X   $	��Top=�6n   $	��100��6�   5+      � 5,      $
��Left=�6   ���#f7f7f7$	��617��6K   $	��Top=�6X   $	��202��6p   $��1158,114,0,0q�6|   .+�5-      �� -   5        $
��lip1��5-   ;   $
��Left=�6    $	��130��6G   $	��Top=�6T   $	��130��6l   $��	27,19,0,0q�6x   $6��0/VirtualDressingRoom;component/Resource/lip1.png��6�    J� Effect 5.      3  -System.Windows.Media.Effects.DropShadowEffect  5/   "   -   5        firstEffect��   Color$ 	#FF868686��5/   H      	Direction$ 0Q�6Z      ShadowDepth$ 0Q�6h      
BlurRadius$ 10Q�6x   51      �� 52      -	   5        $
��lip3��52   ;   $
��Left=�6    $	��130��6G   $	��Top=�6T   $	��130��6l   $��
27,327,0,0q�6x   $6��0/VirtualDressingRoom;component/Resource/lip2.png��6�    53        54   "   -
   5        thirdEffect��$ 	#FF868686��54   H   $ 0Q�6Z   $ 0Q�6h   $ 3Q�6x   56      �� 57      -   5        $
��lip4��57   ;   $
��Left=�6    $	��130��6G   $	��Top=�6T   $	��130��6l   $��
27,478,0,0q�6x   $6��0/VirtualDressingRoom;component/Resource/lip6.png��6�    58        59   "   -   5        fourthEffect��$ 	#FF868686��59   I   $ 0Q�6[   $ 0Q�6i   $ 3Q�6y   5;      �� 5<      -   5        $
��lip2��5<   ;   $
��Left=�6    $	��130��6G   $	��Top=�6T   $	��130��6l   $��
27,170,0,0q�6x   $6��0/VirtualDressingRoom;component/Resource/lip5.png��6�    5=        5>   "   -   5        secondEffect��$ 	#FF868686��5>   I   $ 0Q�6[   $ 0Q�6i   $ 3Q�6y   5@      5A      � 5C      $��209,170,582,73q�6    J� RenderTransformOrigin$ -0.578,0.506&�63   .+�5D      �� -   5        $
 camera��5D   S   $	��Top=�6    $
��Left=�68    5E        5F   "   -   5        
effect��$ 	#FF868686��5F   C   $ 0Q�6U   $ 0Q�6c   $ 3Q�6s   5H      �� 5I      -   5         ��Name$ canvas��5I   !   $	��Top=�61   $
��Left=�6I   �� 5J      -   5        $
 image��5J       $
��Left=�6/   $	��100��6J   $��129,117,0,0q�6W   $	��Top=�6l   $	��100��6�   5K      �� 5M      -   5        $	 upButton��5M      $
��Left=�6/   $��1080,125,0,0q�6J   $	��Top=�6`   $��75��6x   
�������$��0q�6�   .��5N      �� -   5        $
 upImage��5N       $:��4/VirtualDressingRoom;component/Resource/up_arrow.png��61   5P      �� 5R      -   5        $	 
downButton��5R      $
��Left=�61   $��1080,652,0,0q�6L   $	��Top=�6b   $��75��6z   $��75��6�   
�������$��0q�6�   .��5S      �� -   5        $
 	downImage��5S       $<��6/VirtualDressingRoom;component/Resource/down_arrow.png��63   $ 0.5,0.5&�6s   5T      �� 5U      -   5        $ label��5U      $��Label��6+   $
��Left=�6;   $��
49,139,0,0q�6V   $	��Top=�6j   $	��397��6�   5X      5Y      5Z      5[      5\      