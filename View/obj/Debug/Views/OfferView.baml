   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:View.Views
View.Views  5          View.Views.OfferView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:View.Views     5      .��5	      �� .+�5
      �   � RowDefinitions  5      �� 5   
   $��40 �6   �� 5   
   $
��Auto �6   �� 5   
   $��50 �6   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6   �� 5   
   $	��150 �6   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6   �� 5   
   $
��Auto �6    � ColumnDefinitions 5      �� 5      $	��300 �6#   .+�5      �� $��0��6   $��Oferty��6!   ���#0d88fc$��DemiBold"�6H   $��
10,0, 0, 0q�6^   $��18(�6r   �� 5      $��1��6   $��Oferta wzorcowa��6"   $��
10 10 10 0q�6<   $��DemiBold"�6P   � 5      $��2��6   $��0��6   .+�5   
   ��  �� DisplayMemberPath$ DisplayReferenceOffer��5      $��10q�6=   $��DemiBold"�6I   ��5      �� *ReferenceOffers+��5      �� 5        �� 
TargetType" ��6   .�5!      ��  �� Property � |� �� Value .  62   5"      5$      5%      �� 5&      $��4��6   $��0��6    �� Orientation$ 
Horizontal=�6.   .+�5'   
   �� $��Dodaj wzorzec��6   $	��120��6)   $��10q�65   ��6A   �� *AddReferenceOfferCommand+�� 5(   
   $��Usuń wzorzec��6   $	��120��6(   $��10q�64   ��6@   �� *DeleteReferenceOfferCommand+5)      �� 5*      $��7��6   $��0��6   $ 
Horizontal=�6.   .+�5+   
   �� $��Dodaj ofertę��6   $	��100��6(   $��10q�64   ��6@   �� *AddOfferCommand+�� 5,   
   $��Usuń ofertę��6   $	��100��6'   $��10q�63   ��6?   �� *DeleteOfferCommand+5-      �� 5/      $��5��6   $��0��6!   $��Dodane oferty��63   $��	10 0 10 0q�6L   $��DemiBold"�6_   � 50      $��6��6   $��0��6    51   
   �� 52      $
��177* �6   �� 53      $
��623* �6   .+�55   
   �� $��DemiBold"�56   G   $ DisplayOffer��57      $��2��64   $��	10,5,10,5q�6H   ��55      �� *Offers+ �� SelectedItem 56      �� *SelectedOffer+ �� Mode$ TwoWay=�57   H   58      �� 59      $��10,0,0,0q�6   		 �� X1$	 0��5:      	
 �� X2$	
 200��5;      	 �� Y1$ 30��5<      	 �� Y2$ 30��5=      ���#3cb615$��3��5?      5@      5A      5B      