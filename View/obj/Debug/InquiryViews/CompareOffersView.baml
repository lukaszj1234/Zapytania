   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3ffCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs 5      )    #View.InquiryViews.CompareOffersView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:View.InquiryViews     5      VDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro     5      8  2MahApps.Metro.Controls.Dialogs.DialogParticipation    Register  5      �� .��5      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Porównanie ofert��6   ���#0d88fc$��DemiBold"�6N   $��
10,0, 0, 0q�6d   $��18(�6x   �� 5   
   $��0��6    �� Orientation$ 
Horizontal=�6"   �� 5   
   $��1��6   $ 
Horizontal=�6"   $��36��6;   $��Bottom=�6G   �� 5   
   $
��Wrap=�6   $��10,10,10,10q�6(   $��1��6=    J� RenderTransformOrigin$ 
0.5,-0.213&�6J   $��DemiBold"�6m   ywWprowadź po przecinku numery kolumn, które mają zostać sprawdzone. Kolumna "A" oznaczona jest numerem 1. Np. 1,3,4.5      �� 5   
   $��10,10q�6   $��2��6!   $��0��6.   $��DemiBold"�5   A   ��5   >   �� *
Collumns+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5   A   �� 5   
   $��3��6   $��0��6"   $ 
Horizontal=�62   .+�5      �� $��Porównaj oferty��6   $	��150��6/   $��10,5,5,5q�6;   ��6M   �� *CompareOffersCommand+5      �� 5    
   $��10,0,0,0q�6   	 �� X1$ 0��6!   	 �� X2$	 200��6(   		 �� Y1$	 30��61   	
 �� Y2$
 30��6:      underTitleLine#��[  6B   5!      5"      