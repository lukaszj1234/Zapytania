   M S B A M L   `   `   `  ���� ?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  (    "View.InquiryViews.InquiryFilesView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:View.InquiryViews     T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f6mah,http://metro.mahapps.com/winfx/xaml/controls  $	��600��$
��Auto��  � 	Resources   �� 
TargetType��%   '��       �� " ��.�-   .���  � ColumnDefinitions �� $��* � � RowDefinitions �� $��40 ��� $��10 ��� $��26 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��50 ��� $	��230 ��� $��70 ��� $
��Auto �.+��� $��0��$��	Zapytanie�����#0d88fc$��DemiBold"�$��
10,0, 0, 0q�$��18(��� $��8��$��Pliki�����#0d88fc$��DemiBold"�$��10,10, 0, 0q�$��18(��� $��Nazwa��$
��10,0q�$��2��$��DemiBold"��� $��3��$��0��$��
10 0 10 10q�$��DemiBold"����� *Inquiry.Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��4��$��0��$��Branża��$��	10 0 10 0q�$��DemiBold"��� -    ��Name$ industryComboBox��$��5��$��0�� �� DisplayMemberPath$ DisplayIndustry�� �� SelectedValuePath$ Id��$��
10 0 10 10q�$��DemiBold"����� *
Industries+	 �� SelectedValue	 �� *Inquiry.IndustryId+$ TwoWay=��� $��6��$��0��
 �� Orientation$
 
Horizontal=�.+��� $��Dodaj branżę��$	��100��$��10q����� *AddIndustryCommand+�� $��Usuń branżę��$	��100��$��10q����� *DeleteIndustryCommand+� $��9��$��0��.+��� -   $ DisplayFile��$��DemiBold"�$��10q����� *Files+ �� SelectedItem �� *SelectedFile+$ TwoWay=��� $��7��$��0��$
 
Horizontal=�.+��� $��Zapisz��$	��100��$��10q����� *SaveCommand+�� $��Usuń��$	��100��$��10q����� *DeleteCommand+�� $��10��$��0��$
 
Horizontal=�$��0,0,0,20q�.+��� $��
Dodaj Plik��$��90��$��15,5,5,5q�$��20������ *AddFileCommand+�� $��
Usuń Plik��$��90��$��5q�$��20������ *DeleteFileCommand+�� $	��Zip��$��7��$��50��$��10q�$��20������ *SendInquiryCommand+�� $��10,0,0,0q�	 �� X1$ 0��	 �� X2$	 200��	 �� Y1$ 30��	 �� Y2$ 30��   underTitleLine#��[  �� $��8��$��10,0,0,0q�$ 0��$	 200��$ 40��$ 40��#��[  