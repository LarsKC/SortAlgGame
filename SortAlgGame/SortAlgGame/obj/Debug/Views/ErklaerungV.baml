   M S B A M L   `   `   `  ����F  BSortAlgGame, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SortAlgGameSortAlgGame  5      5clr-namespace:SortAlgGame.ViewsSortAlgGame.Views  5      #    SortAlgGame.Views.ErklaerungV   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008  5      $rootclr-namespace:SortAlgGame  5      'vclr-namespace:SortAlgGame.Views  5      .��5      �   � ColumnDefinitions  5   
   �� 5      $
��0.5* �6   �� 5      $
��5.5* �6   �� 5      $
��3.5* �6   �� 5      $
��0.5* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $��1* �6   .+�5   
   �� $��4��6   $��4��6$   $5��//SortAlgGame;component/Resources/Background.png��65   �� 5   
   $��1��6   $��2��6%   $��0��66   $��Bottom=�6C   $
��Left=�6^    �� Orientation$ 
Horizontal=�6y    � DataContext 6�   ��  �� Path$ AnimationVM�.+�5      �� $ Vertical=�6   $��Center=�60   $��Center=�6K   $��
100,0,50,0q�6h   .+�5      ;  5Microsoft.Surface.Presentation.Controls.SurfaceButton     BtnBackward#��[  6"     5        
BtnForward#��[ 6"     5        BtnPlay#��[ 6"     5        BtnPause#��[ 6"   5       �� 5!      .+�5"      ��   AnimationDiagramm#��[ 6   �� 5#        AnimationDataValues#��[ 6   5$      5%      �� 5'   
   $��1��6   $��2��6%   $��Stretch=�62   $��Center=�6P   .+�5(      ��  	 Label#��[ 66   $��30(�6a   ��6   �� $ SortName�6a   �� 5)        InfoText#��[ 6   5*      <  6Microsoft.Surface.Presentation.Controls.SurfaceListBox  5,   
   $��2��6   $��1��6+   $��2��68   $��	10,0,0,10q�6I     CodeList#��[ 6�    	 ListItem#��[	 6�    6\   �� $ AnimationVM�6�   �� 5-   
   $��2��6   $��4��6%   $��Center=�62   $��Center=�6O   $ 
Horizontal=�6j   .+�5.        -   5        $��btnHauptmenue��5.   ^   $��
Hauptmenü��6    

 Button#��[
 62   ��6s   �� * DataContext.changeToHauptmenue+ �� RelativeSource ��  �� AncestorType    SortAlgGame.SurfaceWindow1"   �� Mode$ OneWay=�5/      50      51      