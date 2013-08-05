﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using BusinessObjects;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для PositionWindow.xaml
    /// </summary>
    public partial class PositionWindow : Window
    {
        int num;
        public PositionWindow()
        {
            InitializeComponent();

            num = 0;

            refreshTable();
        }

        private void refreshTable()
        {
            DataSet dsPositions = Position.getDataSetByQuery("1 = 1");
            dataGrid1.DataContext = dsPositions.Tables[0].DefaultView;
        }

        // Добавление
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            String value = tBAdd.Text;
            tBAdd.Text = "";
            if (value != "")
            {
                Position p = new Position();
                p.Name = value;
                p.insertIntoDB();
                refreshTable();
            }
        }

        // Выход
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Изменение
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Position.updateDB(num, tBAdd.Text);
            num = 0;
            tBAdd.Clear();
            refreshTable();
        }

        // Удаление
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid1.SelectedValue as DataRowView;
            Position.delDataSetByPositionId(Convert.ToInt32(rowView[0]));
            refreshTable();
        }

        // Выбор пункта меню
        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView rowView = dataGrid1.SelectedValue as DataRowView;
            tBAdd.Text = rowView[1].ToString();
            num = Convert.ToInt32(rowView[0]);
        }
    }
}
