   M S B A M L   `   `   `  ����N  JVirtualDressingRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VirtualDressingRoomVirtualDressingRoom  5   	        VirtualDressingRoom.tryEye   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VirtualDressingRoom     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5       � Title$ Try Eye Glasses��5   
   $	��768��6"   $
��1366��6/    � WindowState$ 	Maximized=�6j   .��5
      �� $��10q�6    � 	Resources 5   
      MyButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType��%�   &	        �� 5      " ��6*   .�5      ��  � �� 5      �� 5      " ��6*   .R�5      � #��z��6#   .+�5   "   �� $��Center=�63   $��Center=�5      5      5      5      5      5      .+�5   
   � .+�5      �� .+�5      � $	��737��6   .+�5      (� $��Center=�6   $��Right=�69   $	��500��6U   $��0,222,263,289q�6b   � 5       $
��Left=�6   $	��114��66   $	��Top=�6C   $
��1366��6[   ���#38A1F3$��0,16,0,0q�6}   .+�5!      �� -   5         ��Name$ TitleLbl��5!       $��Virtual Dressing Room��62   $
��Left=�6R   $��
439,20,0,0q�6m   $��94��6�   $	��Top=�6�   $	��517��6�   $��48(�6�   
�������$
��Bold"�6�   �� 5"      -   5        	 ��Name$	 border��5"   H   #��[  6!   $��70��6X   $��70��6d   $
��Left=�6o   $��1270,25,0,0q�6�   $	��Top=�6�   ���#38A1F3���	#FF3B5998.��5#      �� $6��0/VirtualDressingRoom;component/Resource/home.png��6$   6`   5$      �� 5%      -   5        $	 gender1��5%   ^   #��[  6!   $
��Left=�6o   $��0,10,0,0q�6�   $	��Top=�6�   $��94��6�   $��94��6�   ���#38A1F3���	#FF3B5998.��5'      �� -   5        
 ��Name$
 image4��5'   $   $��70��64   $��70��6@   $
��Left=�6K   $	��Top=�6g   $��13,0,0,0q�6�   $
��Fill=�6�   $8��2/VirtualDressingRoom;component/Resource/female.png��6�   5(      5)      � 5*      $
��Left=�6   ���#f7f7f7$	��617��6K   $	��Top=�6X   $	��202��6p   $��1158,114,0,0q�6|   .+�5,      �� -   5        $

 eye0��5,       $
��Left=�6.   $	��130��6I   $	��Top=�6V   $	��130��6n   $��	27,19,0,0q�6z   $G��A/VirtualDressingRoom;component/Resource/eyewear frames/1Glass.png��6�    J� Effect 5-      3  -System.Windows.Media.Effects.DropShadowEffect  5.   "   -   5        firstEffect��   Color$ 	#FF868686��5.   H      	Direction$ 0Q�6Z      ShadowDepth$ 0Q�6h      
BlurRadius$ 10Q�6x   50      �� 51      -   5        $

 eye1��51       $
��Left=�6.   $	��130��6I   $	��Top=�6V   $	��130��6n   $��
27,170,0,0q�6z   $G��A/VirtualDressingRoom;component/Resource/eyewear frames/3Glass.png��6�    52        53   "   -	   5        secondEffect��$ 	#FF868686��53   I   $ 0Q�6[   $ 0Q�6i   $ 3Q�6y   55      �� 56      -
   5        $

 eye2��56       $
��Left=�6.   $	��130��6I   $	��Top=�6V   $	��130��6n   $��
27,327,0,0q�6z   $G��A/VirtualDressingRoom;component/Resource/eyewear frames/4Glass.png��6�    57        58   "   -   5        thirdEffect��$ 	#FF868686��58   H   $ 0Q�6Z   $ 0Q�6h   $ 3Q�6x   5:      �� 5;      -   5        $

 eye3��5;       $
��Left=�6.   $	��130��6I   $	��Top=�6V   $	��130��6n   $��
27,478,0,0q�6z   $G��A/VirtualDressingRoom;component/Resource/eyewear frames/6Glass.png��6�    5<        5=   "   -   5        fourthEffect��$ 	#FF868686��5=   I   $ 0Q�6[   $ 0Q�6i   $ 3Q�6y   5?      5@      (� 5B      $��Center=�6   $��Right=�69   $	��546��6U   $��0,157,685,308q�6b   $	��592��6y   .��5D      � $��237,222,1118,289q�6   .+�5E      �� -   5        $
 camera��5E   W   $	��Top=�6$   $
��Left=�6<   $	��970��6g   $��634,-107,-1651,-863q�6t   $
��1017��6�   �� 5F      -   5         ��Name$ canvas��5F   %   $	��Top=�65   $
��Left=�6M   5G      5H      �� 5I      -   5        $	 upButton��5I      $
��Left=�6/   $��1080,125,0,0q�6J   $	��Top=�6`   $��75��6x   
�������$��0q�6�   .��5J      �� -   5        $
 upImage��5J       $:��4/VirtualDressingRoom;component/Resource/up_arrow.png��61   5L      �� 5N      -   5        $	 
downButton��5N      $
��Left=�61   $��1080,652,0,0q�6L   $	��Top=�6b   $��75��6z   $��75��6�   
�������$��0q�6�   .��5O      �� -   5        $
 	downImage��5O       $<��6/VirtualDressingRoom;component/Resource/down_arrow.png��63    J� RenderTransformOrigin$ 0.534,-0.329&�6s   5P      �� 5Q      -   5        $ label1��5Q      $��Label��6,   $
��Left=�6<   $��
56,138,0,0q�6W   $	��Top=�6k   $	��417��6�   5S      5T      �� 5U      -   5        $	 button��5U      $��Capture��6%   $
��Left=�67   $��170,455,0,-210q�6R   $	��Top=�6j   $	��128��6�   $��50��6�   $��17(�6�   $ -6.99,-0.437&�6�   ���	#FFF3F3F36�   �� 5V      -   5        $	 button_Copy1��5V      $��Browse��6+   $
��Left=�6<   $��170,371,0,-126q�6W   $	��128��6o   $��17(�6{   $ -1.133,0.512&�6�   ���	#FFF3F3F36�   5W      5X      5Y      