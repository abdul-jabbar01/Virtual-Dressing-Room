   M S B A M L   `   `   `  ����N  JVirtualDressingRoom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VirtualDressingRoomVirtualDressingRoom  5   	   )    #VirtualDressingRoom.genderDetection   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VirtualDressingRoom     5   	   -   5          � Title$
  Home��5   	   $	��768��6   $
��1366��6#    � WindowStartupLocation$ CenterScreen=�61    � WindowState$ 	Maximized=�5	   	   .��5      � ���	#FFE5E6EB$��	0,0,2,-21q�6"   .+�5   
   � $
��Left=�6   $	��114��6*   ���#38A1F3$	��Top=�6L   $
��1366��6d   .+�5      �� -   5         ��Name$ TitleLbl��5      $��Virtual Dressing Room��6&   $
��Left=�6F   $��
439,20,0,0q�6a   $��94��6u   $	��Top=�6�   $	��517��6�   $��48(�6�   
�������$
��Bold"�6�   5      �� 5   
   -   5         ��Name$ image��5      $
��Left=�6   $	��200��6:   $	��Top=�6G   $	��200��6_   $��
61,216,0,0q�6k   $��/Resource/swipeleft.png��6   �� 5   
   -   5        $ image1��5      $
��Left=�6    $	��200��6;   $	��Top=�6H   $	��200��6`   $��1106,216,0,0q�6l   $��/Resource/swiperight.png��6�   �� 5   
   -   5        $ label2��5      $��Swipe Left for Female��6    $
��Left=�6@   $	��Top=�6[   $��
89,416,0,0q�6s   �� 5   
   -   5        $ label3��5      $��Swipe Right for Male��6    $
��Left=�6?   $��1156,416,0,0q�6Z   $	��Top=�6p   �� 5   
   -   5         ��Name$ button��5      $��For Male��6!   $
��Left=�64   $	��Top=�6O   $��93��6g   $��1160,487,0,0q�6r   
�������$��27��6�    J� OpacityMask �	#FFC240406�   �� 5   
   -   5        $ buttonFemale��5      $��
For Female��6'   $
��Left=�6<   $	��Top=�6W   $��93��6o   $��103,488,0,0q�6z   
�������$��27��6�   �� 5   
   -	   5         ��Name$ canvas1��5      $
��Left=�6"   $	��346��6=   $��364,164,0,0q�6J   $	��Top=�6_   $	��482��6w   .+�5      �� -
   5        $
 feed��5      $	��404��6"   $	��704��6/    �� Left$	 -25��6;   5      �� 5   
   -   5        $ label��5      $��Label��6   $
��Left=�6/   $��205,675,0,0q�6J   $	��Top=�6_   $	��606��6w   �� 5   
   -   5        $ label1��5      $�� ��6    $
��Left=�6+   $��570,585,0,0q�6F   $	��Top=�6[   $	��196��6s   $��60��6   $��30(�6�   �� 5   
   -   5        $ button1��5      $
��Left=�6"   $��589,663,0,0q�6=   $	��Top=�6R   $��Detect Gender��6j   $	��196��6�   $��58��6�   $��20(�6�   
�������5      �� 5   
   -   5        $ label4��5      $��Label��6    $
��Left=�60   $��876,585,0,0q�6K   $	��Top=�6`   5       5!      