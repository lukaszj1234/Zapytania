   M S B A M L   `   `   `  ���� ?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:View.InquiryViewsView.InquiryViews  !    View.InquiryViews.OfferView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:View.InquiryViews     .���� .+��   � RowDefinitions  �� $��40 ��� $
��Auto ��� $��50 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��150 ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $��* �.+��� $��0��$��Oferty�����#0d88fc$��DemiBold"�$��
10,0, 0, 0q�$��18(��� $��1��$��Oferta wzorcowa��$��
10 10 10 0q�$��DemiBold"�� $��2��$��0��.+���  �� DisplayMemberPath$ DisplayReferenceOffer��$��10q�$��DemiBold"����� *ReferenceOffers+����  �� 
TargetType" ��.���  �� Property � |� �� Value .  �� $��4��$��0�� �� Orientation$ 
Horizontal=�.+��� $��Dodaj wzorzec��$	��120��$��10q����� *AddReferenceOfferCommand+�� $��Usuń wzorzec��$	��120��$��10q����� *DeleteReferenceOfferCommand+�� $��7��$��0��$ 
Horizontal=�.+��� $��Dodaj ofertę��$	��100��$��10q����� *AddOfferCommand+�� $��Usuń ofertę��$	��100��$��10q����� *DeleteOfferCommand+�� $��5��$��0��$��Dodane oferty��$��	10 0 10 0q�$��DemiBold"�� $��6��$��0�� �� $
��177* ��� $
��623* �.+��� $��DemiBold"�$ DisplayOffer��$��2��$��	10,5,10,5q����� *Offers+ �� SelectedItem �� *SelectedOffer+ �� Mode$ TwoWay=��� $��10,0,0,0q�		 �� X1$	 0��	
 �� X2$	
 200��	 �� Y1$ 30��	 �� Y2$ 30��   underTitleLine#��[  