   M S B A M L   `   `   `  ����   RESHOTEL_APP%    RESHOTEL_APP.View.cleanPlanning   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��cpWindow��  � Icon$7  1/RESHOTEL_APP;component/Images/reshotel_Icone.ico�� � Title$ Planning nettoyage��$	��600��$	��800��.��� ����  �� ImageSource$8 2/RESHOTEL_APP;component/Images/Hotel-cleaning2.jpg�� �� Opacity$	 0.4Q� j� Stretch$ UniformToFill=� � RowDefinitions �� $��8* ��� $	��49* �.+�� -   $��	gr_header��$��0��$��
0,0,-0.4,1q� � ColumnDefinitions �� $
��160* ��� $
��170* ��� $	��67* �.+�� $��0��$
��Left=�$��Center=�$	��0,2q�.+��� $��55��$��50��$1��+/RESHOTEL_APP;component/Images/reshotel.png��$
��10,0q�$��Center=�$��Center=��� $��
Bienvenue ��$��20(�$
��Bold"�$��Center=�$��Center=��� -   $��tbl_username��$��20(�$��Center=�$��Center=�� $��1��$
��Left=�$��Center=�$	��0,2q��� -   $��
btn_logout��$��2��
�������$��Se déconnecter��$
��Bold"�$��13(�$��10,20q�
���  ��
���  ��� $��1�� �� $
��320* ��� $	��77* �.+�� -   $��gr_listView��$��0��.+��� -   $��lb_clean��
������ $��Disabled=����� .R�� ���� .R��� $��Center=�.+�� $��0,5,0,0q�.+��� $��Center=�.+���  �� Orientation$ 
Horizontal=�.+��� $��Chambre N° : ��$��5,1,0,1q��� $��	tb_bookId��$��5,1,0,1q����� *number+�� $ 
Horizontal=�.+��� $��Étage N° : ��$��5,1,0,1q��� $��3,1,0,1q����� *floor+�� $ 
Horizontal=�.+��� $��Date d'arrivée : ��$��5,1,0,1q��� $��5,1,0,1q����� *	startDate+�� $ 
Horizontal=�.+��� $��Date de départ : ��$��5,1,0,1q��� $��5,1,0,1q����� *	endDate+�� $ 
Horizontal=�.+��� $��Nettoyé le : ��$��5,1,0,1q��� $��3,1,0,1q����� *	cleaned+� -   $��gr_tools��$��1��.+��� $	��Top=�$��20,20q�.+��� $��Type de chambre���� -   $��cb_roomType�� �� Text$ Type de chambre��.@��� 
	 � Tag$	 1��.��Simple�� $	 2��.��Double�� $	 3��.��Triple�� $	 4��.��4 personnes�� $	 5��.��6 personnes�� $	 6��.��Avec lit bébé�� $	 7��.��Avec vue sur le CESI�� $��Bottom=�$��20,20q�.+��� -	   $��btn_cleaned��$��Chambre nettoyé��