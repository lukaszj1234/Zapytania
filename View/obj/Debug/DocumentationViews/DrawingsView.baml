   M S B A M L   `   `   `  ����D  @ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null~Mclr-namespace:ViewModel.ViewModels.DocumentationViewModels;assembly=ViewModel,ViewModel.ViewModels.DocumentationViewModels  5       View*   $View.DocumentationViews.DrawingsView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _localMclr-namespace:ViewModel.ViewModels.DocumentationViewModels;assembly=ViewModel     5      .��5
      �   � RowDefinitions  5   
   �� 5      $��60 �6   �� 5      $��85 �6   �� 5      $��85 �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��0��6K   .+�5      �� $��Rysunki��6   ���#0d88fc$��DemiBold"�6<   $��	0,10,10,0q�6R   $��24(�6e   5      �� 5   
   $ Vertical=�6   $��1��6,   .+�5      �� $ 
Horizontal=�6   $��Center=�62   $��1��6O   .+�5      �� $��Dodaj��6   $
��Left=�6)   $��80��6E   $��20��6P   $��10q�6\   $��DemiBold"�6h   ��6~   �� *AddDrawingCommand+�� 5      $��Usuń��6   $
��Left=�6(   $��80��6D   $��20��6O   $��10q�6[   $��DemiBold"�6g   ��6}   �� *DeleteDrawingCommand+5      �� 5      $��
Aktualizuj��6   $��Center=�6*   $	��160��6H   $��20��6T   $��10q�6`   $��DemiBold"�6l   ��6�   �� *UpdateCommand+5      �� 5   
   $��2��6    �� DisplayMemberPath$
 Name��5      $��DemiBold"�6-   $��10q�6C   ��5       �� *	Drawing+ �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem 5      �� *SelectedDrawing+ �� Mode$ TwoWay=� J� InputBindings 5      d� 5       d� Gesture$ LeftDoubleClicka�6    �� Command 6<   �� 	 �� RelativeSource	 �� 
 �� AncestorType$
 UserControlL� �� Path$ DoubleClickCommand�5!      5"      5#      