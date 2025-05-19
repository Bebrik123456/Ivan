using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace Ivan;

public partial class AdminWin : Window
{
    public AdminWin()
    {
        InitializeComponent();
        LoadOrders();
    }
    
    
    private void LoadOrders()
    {
        EmployeeListBox.Items.Clear();

        var orders = GetOrdersFromDatabase();

        string a;
        string b = "test";
       

     
        foreach (var order in orders)
        {
           
           a = order.Role;
           switch (a)
           {
               case "0":
               b = "Администратор";
               break;
               case "1":
                   b = "Диспетчер";
                   break;
               case "2":
                   b = "Монтажник";
                   break;
           }
             string orderInfo = $"{order.Id} | {order.Login} | {order.Adress} | {b} ";  
             EmployeeListBox.Items.Add(orderInfo); 
        }
        LoadSchedule();
    }

    private void LoadSchedule()
    {
        ScheduleListBox.Items.Clear();
        var orders = GetOrdersFromDatabase();
        foreach (var order in orders)
        {
            string orderInfo = $"{order.Id} | {order.Login} | {order.Adress} | {order.Schedule} ";  
            ScheduleListBox.Items.Add(orderInfo); 
        }
    }



    private List<Order> GetOrdersFromDatabase()
    {
        var orders = new List<Order>();

        var conn = new MySqlConnection("Server=localhost;Database=provider;User Id=root;Password=;");
        {
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM provider.User", conn);
            var reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    orders.Add(new Order
                    {
                        Id = reader.GetInt32("ID"),
                        Login = reader.GetString("Login"),
                        Adress = reader.GetString("Location"),
                        Role = reader.GetString("Role"),
                        Schedule = reader.GetString("Schedule")
                    });
                }
            }
        }
        
        return orders;
    }
    public class Order
    {
        public int Id { get; set; }
        
        public string Login { get; set; }
    
        public string Role { get; set; }
    
        public string Adress { get; set; }

        public string Schedule { get; set; }



    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        CreateTaskWIn create = new CreateTaskWIn();
      Hide();
      create.Show();
      this.Close();
    }
}