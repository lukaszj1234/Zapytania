   M S B A M L   `   `   `  ���� ?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3ffCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.MetroMahApps.Metro.Controls.Dialogs )    #View.InquiryViews.CompareOffersView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:View.InquiryViews     VDialogCclr-namespace:MahApps.Metro.Controls.Dialogs;assembly=MahApps.Metro     8  2MahApps.Metro.Controls.Dialogs.DialogParticipation    Register  �� .���  � ColumnDefinitions �� $	��300 � � RowDefinitions �� $��40 ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��Porównanie ofert�����#0d88fc$��DemiBold"�$��
10,0, 0, 0q�$��18(��� $��0�� �� Orientation$ 
Horizontal=��� $��1��$ 
Horizontal=�$��36��$��Bottom=��� $
��Wrap=�$��10,10,10,10q�$��1�� J� RenderTransformOrigin$ 
0.5,-0.213&�$��DemiBold"�ywWprowadź po przecinku numery kolumn, które mają zostać sprawdzone. Kolumna "A" oznaczona jest numerem 1. Np. 1,3,4.�� $��10,10q�$��2��$��0��$��DemiBold"����� *
Collumns+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��3��$��0��$ 
Horizontal=�.+��� $��Porównaj oferty��$	��150��$��10,5,5,5q����� *CompareOffersCommand+�� $��10,0,0,0q�	 �� X1$ 0��	 �� X2$	 200��		 �� Y1$	 30��	
 �� Y2$
 30��   underTitleLine#��[  