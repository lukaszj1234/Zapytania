   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  5      (    "View.InquiryViews.InquiryFilesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:View.InquiryViews     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  5      $	��600��5	      $
��Auto��6     � 	Resources  5
       �� 
TargetType��%E   '��       �� 5      " ��6   .�5   
   -   5        5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��40 �6   �� 5      $��10 �6   �� 5      $��26 �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��50 �6   �� 5      $	��230 �6   �� 5      $��70 �6   �� 5      $
��Auto �6   .+�5!   
   �� $��0��6   $��	Zapytanie��6   ���#0d88fc$��DemiBold"�6G   $��
10,0, 0, 0q�6]   $��18(�6q   �� 5"   
   $��8��6   $��Pliki��6   ���#0d88fc$��DemiBold"�6C   $��10,10, 0, 0q�6Y   $��18(�6n   �� 5#   
   $��Nazwa��6   $
��10,0q�6    $��2��6.   $��DemiBold"�6;   �� 5$   
   $��3��6   $��0��6   $��
10 0 10 10q�6   $��DemiBold"�6�   ��6/   �� *Inquiry.Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5%   
   $��4��6   $��0��6   $��Branża��6-   $��	10 0 10 0q�6>   $��DemiBold"�6Q   �� 5&   
   -   5         ��Name$ industryComboBox��5&   S   $��5��6   $��0��6     �� DisplayMemberPath$ DisplayIndustry��5'       �� SelectedValuePath$ Id��5(      $��
10 0 10 10q�5*      $��DemiBold"�6'   ��5&   0   �� *
Industries+	 �� SelectedValue	 5)      �� *Inquiry.IndustryId+$ TwoWay=�5*   '   �� 5+   
   $��6��6   $��0��6"   
 �� Orientation$
 
Horizontal=�62   .+�5,      �� $��Dodaj branżę��6   $	��100��6,   $��10q�68   ��6D   �� *AddIndustryCommand+�� 5-      $��Usuń branżę��6   $	��100��6+   $��10q�67   ��6C   �� *DeleteIndustryCommand+5.      � 5/   
   $��9��6   $��0��6   .+�50      �� -   5        $ DisplayFile��52      $��DemiBold"�64   $��10q�6J   ��50      �� *Files+ �� SelectedItem 51      �� *SelectedFile+$ TwoWay=�52   V   53      �� 54   
   $��7��6   $��0��6"   $
 
Horizontal=�62   .+�55      �� $��Zapisz��6   $	��100��6&   $��10q�62   ��6>   �� *SaveCommand+�� 56      $��Usuń��6   $	��100��6$   $��10q�60   ��6<   �� *DeleteCommand+57      �� 58   
   $��10��6   $��0��6#   $
 
Horizontal=�63   $��0,0,0,20q�6L   .+�59      �� $��
Dodaj Plik��6   $��90��6*   $��15,5,5,5q�65   $��20��6G   ��6S   �� *AddFileCommand+�� 5:      $��
Usuń Plik��6   $��90��6)   $��5q�64   $��20��6?   ��6K   �� *DeleteFileCommand+�� 5;      $	��Zip��6   $��7��6#   $��50��60   $��10q�6;   $��20��6H   ��6U   �� *SendInquiryCommand+5<      �� 5=   
   $��10,0,0,0q�6   	 �� X1$ 0��6!   	 �� X2$	 200��6(   	 �� Y1$ 30��61   	 �� Y2$ 30��6:      underTitleLine#��[  6B   �� 5>   
   $��8��6   $��10,0,0,0q�6   $ 0��6.   $	 200��65   $ 40��6>   $ 40��6G   #��[  6O   5?      5@      