using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Avalonia.Interactivity;

namespace Ivan;

public partial class DispatcherWin : Window
{
    public DispatcherWin()
    {
        InitializeComponent();
        LoadOrders();
    }
    
    
      private void LoadOrders()
    {
        // 1. Очищаем список
        OrdersListBox.Items.Clear();

        // 2. Получаем данные из БД
        var orders = GetOrdersFromDatabase();

        // 3. Заполняем ListBox вручную
        foreach (var order in orders)
        {
            // Формируем строку для отображения
            string a;

         
           string orderInfo = $"{order.Id} | {order.Name} | {order.Adress} ";
            OrdersListBox.Items.Add(orderInfo); 
        }
    }
    private List<Her> GetOrdersFromDatabase()
    {
        var orders = new List<Her>();

        var conn = new MySqlConnection("Server=localhost;Database=Provider;User Id=root;Password=;");
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Provider.Task", conn);
            var reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    orders.Add(new Her
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        Adress = reader.GetString("Adress"),
                        Accountable = reader.GetInt32("Accountable")
                    });
                }
            }
        }

        return orders;
    }


   private void UpdateButton(object sender, RoutedEventArgs e)
    {
        LoadOrders();
    }


    public class Her
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Adress { get; set; }
        
        public int Accountable { get; set; }

    }
    
    
    
    
}