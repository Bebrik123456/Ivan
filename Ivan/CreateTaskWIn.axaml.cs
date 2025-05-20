using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using System;
namespace Ivan;

public partial class CreateTaskWIn : Window
{
    public CreateTaskWIn()
    {
        InitializeComponent();
    }


    private void CreateOrderButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string stringconnection = "Server=localhost;Database=Provider;User Id=root;Password=;";
            var NameTask = NameTextBox.Text;
            var Description = DescriptionTextBox.Text;
            var Aderess = AderessTextBox.Text;

            MySqlConnection conn = new(stringconnection);
            conn.Open();
            string query = "INSERT INTO Task (Name,Description,Adress) VALUES (@name,@description,@adress)";
            MySqlCommand cmd = new(query, conn);
            cmd.Parameters.AddWithValue("@name", NameTask);
            cmd.Parameters.AddWithValue("description",Description);
            cmd.Parameters.AddWithValue("@adress", Aderess);
            cmd.ExecuteNonQuery();
            TestLabel.Content = "Заказ успешно создан";
     
        }
        catch (Exception ex)
        {
            TestLabel.Content = ex.Message;    
            
        }
    }
}