   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  5      '    !View.InquiryViews.SendedInquiries   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:View.InquiryViews     5      -   5        $��asdadd��5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $��* �6   $	��100��6)   �� 5      $��* �6   $	��250��6)    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��30 �6   �� 5      $��40 �6   �� 5      �� 5      $��70 �6   .+�5   
   � $��2��6   $��0��6   $��
10 0 10 10q�6,   $��3��6@   .+�5      ��  �� DisplayMemberPath$ DisplaySendedInquiry��5      $��DemiBold"�6<   ��5      �� *SendedInquiries+ �� SelectedItem 5      �� *SelectedSendedInquiry+ �� Mode$ TwoWay=�5   <   5      �� 5   
   $��0��6   $��Wysłane zapytania��6   ���#0d88fc$��DemiBold"�6O   $��2��6e   $��
10,0, 0, 0q�6{   $��18(�6�   �� 5   
   $��Nazwa��6   $��1��6    $��
10 10 10 0q�6-   $��1��6A   $��DemiBold"�6Q   �� 5   
   $��2��6   $��1��6   $��
10,0,50,10q�5    D   $��DemiBold"�6X   ��5   /   �� *,*SelectedSendedInquiry.DisplaySendedInquiry+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5    X   �� 5!   
   $��3��6   $��1��6   $��Notatki��6-   $��	10 0 10 0q�6?   $��DemiBold"�6R   �� 5"   
   $��4��6   $��1��6    �� TextWrapping$
 Wrap=�6/   $��
10,0,50,10q�5#   D   $��DemiBold"�6X   ��5"   C   �� **(SelectedSendedInquiry.DisplayDescription+$ TwoWay=�$ PropertyChanged=�5#   X   �� 5$   
   $��5��6   $��1��6"    �� Orientation$ 
Horizontal=�62   .+�5%      �� $��Zapisz��6   $��70��6&   $��10,5,5,5q�61   $��20��6C   ��6O   �� *SaveCommand+�� 5&      $��Usuń��6   $��70��6$   $��10,5,5,5q�6/   $��20��6B   ��6O   �� *DeleteCommand+5'      �� 5(   
   $��Dodaj��6   $��70��6!   $��10,5,5,5q�6,   $��20��5)      $��5��6   ��5(   >   �� *AddSendedInquiry+5)      �� 5*   
   $��10,0,0,0q�6   	 �� X1$ 0��6!   		 �� X2$		 200��6(   	
 �� Y1$
 30��61   	 �� Y2$ 30��6:      underTitleLine#��[  6B   $��2��6j   5+      5,      