   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   RESHOTEL_APP    RESHOTEL_APP.View.hbWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6sys&clr-namespace:System;assembly=mscorlib     -     �Name$  	hbWindow1�� � Title$ Tableau de bord��$	��650��$	��900�� � Icon$7 1/RESHOTEL_APP;component/Images/reshotel_Icone.ico��.���  � RowDefinitions �� $��6* ��� $��4* ��� $��1* ��� $	��41* ��� $��5* �����  �� ImageSource$2 ,/RESHOTEL_APP;component/Images/reception.jpg�� �� Opacity$	 0.3Q�.+�� -   $��	gr_header��$��0�� � ColumnDefinitions �� $
��160* ��� $
��170* ��� $	��67* �.+�� $��0��$
��Left=�$��Center=�$	��0,2q�.+��� $��55��$��50��$1��+/RESHOTEL_APP;component/Images/reshotel.png��$
��10,0q�$��Center=�$��Center=��� $��
Bienvenue ��$��20(�$
��Bold"�$��Center=�$��Center=��� -   $��tbl_username��$��20(�$��Center=�$��Center=�� $��1��$
��Left=�$��Center=�$	��0,2q��� -   $��
btn_logout��$��2��
�������$��Se déconnecter��$
��Bold"�$��13(�$��10,12q�
���  ��
���  ��� -   $��
gr_toolbar��$��1��.+�c� .@��� -   $��btn_dashboard��$��Tableau de Bord��$
��35.2��$��Center=�$��Center=�$
��10,0q�$	��8,2q��� -   $��btn_planning��$ ��Planning des réservations��$
��35.2��$��Center=�$��Center=�$
��10,0q�$	��8,2q��� -   $��btn_Promotion��$��
Promotions��$y�	Collapsed=�$
��35.2��$��Center=�$��Center=�$
��10,0q�$	��8,2q��� -	   $��btn_customer��$��Gérer les clients��$
��35.2��$��Center=�$��Center=�$
��10,0q�$	��8,2q�� -
   $��
gr_manager��$��2��� -   $��gr_dashboard��$��3��$y�Visible=�$��10,0,9.6,54q�$��2�� �� $��4* ��� $	��12* �.+��� -   $��
wp_booking��$��Center=�$y�Visible=�$��0��.+��� $��Date d'arrivée :��(  "System.Windows.Controls.DatePicker  -   $��
dp_checkin��   Text$ Date d'arrivée��$��20,20q�
 �� Now	   DisplayDateStart#	 Z �� $��Date de départ :��  -   $��dp_checkout��$ Date de départ��$��20,20q�#	 Z �� $��Type de chambre :���� -   $��cb_roomType��$��20,20q�
 �� Text$
 Type de chambre��.@��� 
 � Tag$ 1��.��Simple�� $ 2��.��Double�� $ 3��.��Triple�� $ 4��.��4 personnes�� $ 5��.��6 personnes�� $ 6��.��Avec lit bébé�� $ 7��.��Avec vue sur le CESI� $��1��.+��� -   $��lb_rooms��
������ $��Disabled=����� .R�� ���� .R��  � Orientation$ 
Horizontal=�.+�� $��0,5,0,0q� �� $
��auto ��� .+��� $��80��$��80��$��0������ *
imageUrl+�� $��1�� �� Orientation$ Vertical=�$��Center=�.+��� $ 
Horizontal=�.+��� $��Chambre N°��$��5,1,0,1q��� $��3,1,0,1q����� *number+�� $ 
Horizontal=�.+��� $��Capacité :��$��5,1,0,1q��� $��5,1,0,1q����� *
capacity+�� $ 
Horizontal=�.+��� $��	Etage N°��$��5,1,0,1q��� $��5,1,0,1q����� *floor+�� $ 
Horizontal=�.+��� $��Prix :��$��5,1,0,1q��� $��3,1,0,1q����� *	price1n+�� $	��€��$��5,1,0,1q�� -   $��gr_planning��$��3��$y�Hidden=�$��10,0,9.6,54q�$��2�� �� $��4* ��� $	��12* �.+��� -   $��wp_planning��$��0��$��Center=�$y�Visible=�.+��� $��Du��  -   $��dp_begin��$��20,10q��� $��Au��  -   $��dp_end��$��20,10q��� $��Type de chambre ���� -   $��cb_bookRoomType��$��20,10q�$
 Type de chambre��.@��� $ 1��.��Simple�� $ 2��.��Double�� $ 3��.��Triple�� $ 4��.��4 personnes�� $ 5��.��6 personnes�� $ 6��.��Avec lit bébé�� $ 7��.��Avec vue sur le CESI�� -   $��btn_searchByDate��$��
Rechercher��$��20,10q��� $��Recherche par client ���� -   $��tb_searchByClient��$��20,10q��� -   $��btn_searchByClient��$��
Rechercher��$��20,10q��� -   $��lb_bookings��
������ $��1��$��Disabled=����� .R�� ���� .R��� $��Center=�.+�� $��0,5,0,0q�.+��� $	��100��.+��� $ 
Horizontal=�.+��� $��Réservation N° : ��$��5,1,0,1q��� $��	tb_bookId��$y�Visible=����� *bookId+�� $ 
Horizontal=�.+��� $��Date d'arrivée : ��$��5,1,0,1q��� $��5,1,0,1q����� *	checkIn+�� $ 
Horizontal=�.+��� $��Date de départ : ��$��5,1,0,1q��� $��5,1,0,1q����� *
checkOut+�� $ 
Horizontal=�.+��� $��	Client : ��$��5,1,0,1q��� $��3,1,0,1q����� *
lastname+�� $��3,1,0,1q����� *	firstname+�� $ 
Horizontal=�.+��� $��Numéro chambre : ��$��5,1,0,1q��� $��3,1,0,1q����� *roomNb+� -   $��
gr_newResa��$��3��$y�Hidden=� �� ��  �� �� .+�� -   $��gr_customer��$��0��$��0�� �� $	��16* ��� $	��89* � �� �� .+��� -   $��sp_srchCustomer��$��0��$��0��$��Center=�.+��� $��Rechercher un client��$
��20,0q�� .+��� -   $��tb_searchClient��$	��150��$��20,0,0,5q��� -   $��btn_searchClient��$��29������ $5 //RESHOTEL_APP;component/Images/Search_16x16.png�� j� Stretch$ Uniform=��� $��0��$��1��$��Center=�.+��� -   $��chk_newCustomer��$��	20,0,20,5q�.��Nouveau client�� -    $��sp_customersList��$��1��$��0��$��Center=�.+��� -!    ��Name$ lv_customers��$	��150��$
��Auto=�$ �� IsSynchronizedWithCurrentItem$
 TrueY����� *
Customer+ �� View �� .P��� $	��Nom�� �� Width$ 90�� �� DisplayMemberBinding �� *
lastname+�� $��Prénom��$	 100�� �� *	firstname+�� -"   $��sp_newCustomer��$��1��$��1��$��Center=�{�.  .+��� $	��Nom��$��	20,0,20,5q��� -#   $��tb_lastname��$
��20,0q��� $��Prénom��$��	20,0,20,5q��� -$   $��tb_firstname��$
��20,0q��� $��Email��$��	20,0,20,5q��� -%   $��tb_email��$
��20,0q��� $��Téléphone��$��	20,0,20,5q��� -&   $��tb_phone��$
��20,0q�� -'   $��	gr_option��$��0��$��1�� �� �� .+��� $��0��$��Center=�$
��Auto=�.���� $��10q�.+��� $��Options supplémentaires��$
��Bold"�$��	20,0,20,5q��� -(   $��
chk_cardio��$ 1��.��Espace Cardio (15€)�� -)   $��chk_jacuzzi��$ 2��.��Espace Jacuzzi/Hammam (15€)�� -*   $��chk_wifi��$ 3��.��Wi-Fi (20€)�� -+   $��chk_tv��$ 4��.��Télévision (10€)�� -,   $��
chk_tennis��$ 5��.��Cours de Tennis (25€)�� $��1��$��Center=�.+��� $��Type de paiement��$��
20,0,20,10q��� --   $��
cb_payment��$
��20,5q� �� DisplayMemberPath$ label��$ id������ *	Payment+�� $��Petit déjeuner��$��
20,0,20,10q��� -.   $��cb_breakfast��$
��20,5q�.@��� $ 0��.��0�� $ 1��.��1�� $ 2��.��2�� $ 3��.��3�� $ 4��.��4�� $ 5��.��5�� $ 6��.��6�� $��Dîner��$��
20,0,20,10q��� -/   $��	cb_dinner��$
��20,5q�.@��� $ 0��.��0�� $ 1��.��1�� $ 2��.��2�� $ 3��.��3�� $ 4��.��4�� $ 5��.��5�� $ 6��.��6� $��1��$��0�� �� �� .+�� -0   $��gr_mealDemand��$��0��$y�Hidden=�.+��� $��10q�.+��� $&�� Demande de repas supplémentaire��$
��Bold"�$
��0,10q��� $��Date du repas��$
��10,0q�  -1   $��dp_dinnerDate��$��10,10q��� $��Nombre de personne(s)��$
��10,0q��� -2   $��cb_nbDinner��$
��10,5q�.@��� $ 1��.��1�� $ 2��.��2�� $ 3��.��3�� $ 4��.��4�� $ 5��.��5�� $ 6��.��6�� -3   $��btn_addMealValidate��$��Ajouter repas��$��10q�� -4   $��gr_room��$��1��$��1�� �� �� .+��� $��0��$��Center=�.+�� .+��� $��
Arrivée :��$��10,0,5,5q��� -5   $��
tb_checkIn��$��0,0,5,5q�  -6   $��dp_modifCheckin��$y�	Collapsed=�� .+��� $��	Départ :��$��10,0,5,5q��� -7   $��tb_checkOut��$��0,0,5,5q�  -8   $��dp_modifCheckout��$y�	Collapsed=�� $ 
Horizontal=�.+��� $��Chambre n°:��$��10,0,5,5q��� -9   $��
lbl_roomNb��$��0,0,5,5q�� $ 
Horizontal=�.+��� $��Capacité :��$��10,0,5,5q��� -:   $��lbl_roomCapacity��$��0,0,5,5q�� $ 
Horizontal=�.+��� $��
Etage n°:��$��10,0,5,5q��� -;   $��lbl_roomFloor��$��0,0,5,5q�� $ 
Horizontal=�.+��� $��Prix :��$��10,0,5,5q��� -<   $��lbl_roomPrice��$��0,0,5,5q�� $ 
Horizontal=�.+��� $��
Nuitées :��$��10,0,5,5q��� -=   $��lbl_nbNights��$��0,0,5,5q��� $��1��$	��Top=�.+��� ->   $��btn_genRate��$��Génerer les tarifs��$
��10,0q��� -?   $��sp_allPrices��$y�Hidden=�.+�� .+��� $��Prix total nuitées��$��10,5,5,5q��� -@   $��tbl_totalNightsPrice��$
��Bold"�$��0,5,5,5q��� $	��€��$��0,5,5,5q�� .+��� $��Petit déjeuner��$��10,5,5,5q��� -A   $��tbl_totalBFPrice��$
��Bold"�$��0,5,5,5q��� $	��€��$��0,5,5,5q�� .+��� $��Dîner��$��10,5,5,5q��� -B   $��tbl_totalDnrPrice��$
��Bold"�$��0,5,5,5q��� $	��€��$��0,5,5,5q�� .+��� $��Total des options��$��10,5,5,5q��� -C   $��tbl_totalOptPrice��$
��Bold"�$��0,5,5,5q��� $	��€��$��0,5,5,5q�� -D   $��wp_discount��$y�	Collapsed=�.+��� $��Réduction fidélité��$��10,5,5,5q��� -E   $��tbl_discount��$
��-5 %��$
��Bold"�$��0,5,5,5q�� -F   $��wp_tmpDiscount��$y�	Collapsed=�.+��� $��Promotion temporaire��$��10,5,5,5q��� -G   $��tbl_tmpDiscount��$
��Bold"�$��0,5,5,5q��� $��%��$��0,5,5,5q�� .+��� $��Total à payer��$��	10,10,5,5q��� -H   $��tbl_totalPrice��$
��Bold"�$��0,10,5,5q��� $	��€��$��0,10,5,5q�� -I   $��gr_promo��$��3��$y�Hidden=� �� $
��190* ��� $
��449* ��� $
��254* �.+�� -J   $��gr_promoSearch��$��0��.+��� $
��20,0q�.+��� $��Critères de recherche��$
��Bold"�$
��0,20q��� $��Date de début��  -K   $��dp_startDate���� $��Date de fin��  -L   $��
dp_endDate���� $��Type de chambre :���� -M   $��cb_promoRmType��$
 Type de chambre��.@��� $ 1��.��Simple�� $ 2��.��Double�� $ 3��.��Triple�� $ 4��.��4 personnes�� $ 5��.��6 personnes�� $ 6��.��Avec lit bébé�� $ 7��.��Avec vue sur le CESI�� -N   $��btn_searchPromo��$��
Rechercher��$
��0,20q�� -O   $��gr_promoList��$��1�� �� $��8* ��� .+��� -P   $��lb_promotions��$��0��
������ $��Disabled=����� .R�� ���� .R��� $��Center=�.+�� $��0,5,0,0q�.+��� $	��100��.+��� $ 
Horizontal=�.+��� $��Promotion N° : ��$��5,1,0,1q��� $��
tb_promoId��$y�Visible=����� *id+�� $ 
Horizontal=�.+��� $��Date de début : ��$��5,1,0,1q��� $��5,1,0,1q����� *	startDate+�� $ 
Horizontal=�.+��� $��Date de fin : ��$��5,1,0,1q��� $��5,1,0,1q����� *	endDate+�� $ 
Horizontal=�.+��� $��Réduction : ��$��5,1,0,1q��� $��3,1,0,1q����� *
discount+�� $��%���� $ 
Horizontal=�.+��� $��Code promo : ��$��5,1,0,1q��� $��3,1,0,1q����� *code+� $��1��$��Center=�$��Center=�.+��� -Q   $��btn_cancelPromo��$��Annuler��$
��20,0q��� -R   $��btn_modifPromo��$��Modifier��$
��20,0q�� -S   $��gr_newPromo��$��2�� �� $��8* ��� .+�� .+��� $-��'Formulaire d'ajout et de modification :��$
��Bold"�$��20,20q��� $��Center=�$
��20,0q�.+��� $��Date de début��  -T   $��dp_addStartDate��#	 Z �� $��Date de fin��  -U   $��dp_addEndDate��#	 Z �� $��
Réduction���� -V   $��tb_addDiscount���� $��
Code promo���� -W   $��
tb_addCode���� $��Type de chambre���� -X   $��cb_addRmType��$
 Type de chambre��.@��� $ 1��.��Simple�� $ 2��.��Double�� $ 3��.��Triple�� $ 4��.��4 personnes�� $ 5��.��6 personnes�� $ 6��.��Avec lit bébé�� $ 7��.��Avec vue sur le CESI� $��1��$��Center=�$��Center=�.+��� -Y   $��btn_editPromo��$��Modifier��$
��20,0q�{�.  �� -Z   $��btn_addPromo��$��Ajouter��$
��20,0q�� -[   $��gr_custs��$��3��$y�Hidden=� �� $	��90* ��� $
��240* ��� $
��117* �.+�� -\   $��gr_custSearch��$��0��.+��� $
��20,0q�.+��� $��Critères de recherche��$
��Bold"�$
��0,20q��� $��Nom ou prénom client���� -]   $��tb_custName���� -^   $��btn_srchCust��$��
Rechercher��$
��0,20q�� -_   $��gr_custList��$��1�� �� $��8* ��� .+�� -`   $��sp_customersList2��$
��Auto=�$��0��$��0��.+��� -a   $ lv_customers2������ *
Customer+ �� .P��� $��id��$ 40�� �� *id+�� $	��Nom��$	 100�� �� *
lastname+�� $��Prénom��$	 100�� �� *	firstname+�� $��Email��$	 100�� �� *email+�� $��Téléphone��$	 100�� �� *phone+�� $��Nb. réservations��$ 50�� �� *nbReservation+� $��1��$��Center=�$��Center=�.+��� -b   $��btn_modifCust��$��Modifier��$
��20,0q�� -c   $��
gr_newCust��$��2�� �� $��8* ��� .+�� .+��� $-��'Formulaire d'ajout et de modification :��$
��Bold"�$��20,20q��� $��Center=�$
��20,0q�.+��� $	��Nom��$��	20,0,20,5q��� -d   $��tb_lastname2��$
��20,0q��� $��Prénom��$��	20,0,20,5q��� -e   $��tb_firstname2��$
��20,0q��� $��Email��$��	20,0,20,5q��� -f   $��	tb_email2��$
��20,0q��� $��Téléphone��$��	20,0,20,5q��� -g   $��	tb_phone2��$
��20,0q�� $��1��$��Center=�$��Center=�.+��� -h   $��btn_editCust��$��Modifier��$
��20,0q�{�.  �� -i   $��btn_addCust��$��Ajouter��$
��20,0q�� -j   $��
gr_actions��$��4��.+�� 
������ $��Center=�$��Right=�.+��� -k   $��btn_addMeal��$��Demande de repas ��$
��35.2��$��Center=�$��Center=�
���   �$��15(�
������ $y�	Collapsed=�$	��5,0q�$	��8,2q��� -l   $��btn_pdfInvoice��$��Génerer Facture��$
��35.2��$��Center=�$��Center=�
���   �$��15(�
������ $y�	Collapsed=�$	��5,0q�$	��8,2q��� -m   $��btn_cancelChoice��$��Retour��$
��35.2��$��Center=�$��Center=�
���   �$��15(�
������ $y�	Collapsed=�$	��5,0q�$	��8,2q��� -n   $��btn_selectRoom��$��Valider le choix��$y�	Collapsed=�
��� ���$
��35.2��$��15(�$��Center=�$��Center=�$	��5,0q�$	��8,2q�
������ �� -o   $��btn_changeRoom��$��Changer la chambre��$y�	Collapsed=�
��� ���$��	30,0,30,0q�
������ $��Center=�$��40���� -p   $��btn_cancelBook��$��Annuler une réservation��$
��35.2��$��Center=�$��Center=�
���   �$��15(�
������ $y�	Collapsed=�$	��5,0q�$	��8,2q��� -q   $��btn_modifBook��$��Modifier une réservation��$
��35.2��$��Center=�$��Center=�$	��5,0q�$	��8,2q�$y�	Collapsed=�
������ $��15(��� -r   $��btn_editBook��$��Modifier��$y�	Collapsed=�$��Center=�$��Center=�$
��35.2��
���   ����#33FF00$��15(�$	��5,0q�$	��8,2q�
������ �� -s   $��btn_book��$��	Réserver��$y�	Collapsed=�$��Center=�$��Center=�$
��35.2��
���   ����#33FF00$��15(�$	��5,0q�$	��8,2q�
������ �� -t   $��btn_toolBook��$ ��Effectuer une réservation�����#33FF00$��15(�$
��35.2��$��Center=�$��Center=�
������ $	��5,0q�$	��8,2q�