   M S B A M L   `   `   `  ����G  Ckpk_schedule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:kpk_schedule.Windowskpk_schedule.Windows  5   	   )    #kpk_schedule.Windows.ScheduleWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:kpk_schedule.Windows     5   	     � Title$  Расписание��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   �� $��Button��6   $
��Left=�6"   $��1263,189,0,0q�6=   $	��Top=�6S   $��0��6k   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	GridTable��5      $��
0,59,162,1q�6&   �� 5   
   -   5        $��Добавить��5      $��645,67,10,323q�6$   6;   �� 5   
   -   5        $��Изменить��5      $��645,116,11,274q�6$   6<   �� 5   
   -   5        $��Удалить��5      $��645,165,10,225q�6#   6;   �� 5   
   -   5        $��
Назад��5      $��643,389,12,1q�6!   67   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��	DataTable��5   I   $
��Left=�6Z   $��	10,21,0,0q�6u   $	��Top=�6�   $��35��6�   $	��170��6�   �� 5   
   -   5        $��Teachers��5   p    �� DisplayMemberPath$
 Name��6@    �� SelectedValuePath$ Id��6Y   $
��Left=�6�   $��
185,21,0,0q�6�   $	��Top=�6�   $	��196��6�   $��35��6�   �� 5   
   $��Дата��6   $
��Left=�6   $��	10,-5,0,0q�6:   $	��Top=�6M   �� 5   
   $ ��Преподователь��6   $
��Left=�6(   $��
185,-5,0,0q�6C   $	��Top=�6W   �� 5   
   -   5        $��Lesson��5   q   $
 Name��6A   $ Id��6Z   $
��Left=�6   $��
387,21,0,0q�6�   $	��Top=�6�   $	��196��6�   $��35��6�   �� 5   
   $��Предмет��6   $
��Left=�6"   $��
387,-5,0,0q�6=   $	��Top=�6Q   �� 5   
   -	   5        $��Groups��5   q   $
 Name��6A   $ Id��6Z   $
��Left=�6   $��
590,21,0,0q�6�   $	��Top=�6�   $	��196��6�   $��35��6�   �� 5   
   $��Группа��6   $
��Left=�6!   $��
590,-5,0,0q�6<   $	��Top=�6P   �� 5   
   -
   5        $#��Очистить фильтр��5      $��645,214,10,176q�6+   6C   5      5      