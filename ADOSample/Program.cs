using Microsoft.Data.SqlClient;
using System.Data;

internal class Program
{
    static SqlConnection conn;
    private static void Main(string[] args)
    {
        while(true)
        {
            Console.WriteLine("ADO.NET Sample");
            Console.WriteLine("1.Create Table");
            Console.WriteLine("2.Insert Record");
            Console.WriteLine("3.Display Records");
            Console.WriteLine("4.Update Record");
            Console.WriteLine("5.Delete Record");
            Console.WriteLine("6.Exit");
            Console.WriteLine("Enter your choice:");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                CreateTable();
            }
            else if (choice == 2)
            {
                INsert();
            }
            else if (choice == 3)
            {
                display();
            }
            else if (choice == 4)
            {
                Update();
            }
            else if (choice == 5)
            {
                Delete();
            }
            else if (choice == 6)
            {
                break;
            }
            else
            {
                Console.WriteLine("Exiting...");
                return;
            }
        }
        
    }

    static void GetConn()
    {
        //1.Connection Establishment
        conn = new SqlConnection("server=DESKTOP-00GRLI4;" +
            "database=kanini;Integrated Security=true;trustservercertificate=true;");

        //2.Open the connection
        conn.Open();
    }

    static void CreateTable()
    {
        //1.Connection Establishment
        GetConn();
        //3.Prepare the SqlCommand
        SqlCommand cmd = new SqlCommand("create table Product1 (Proid int primary key,ProName varchar(30),Price int)", conn);
        //4.Execute the command
        cmd.ExecuteNonQuery();
        //5.Display the result
        Console.WriteLine("Table created");
        //6.Close the connection
        
    }
    static void INsert()
    {
        //1.Connection Establishment

        //2.Open the connection
        GetConn();

        //3.Prepare the SqlCommand
        Console.WriteLine("Enter the Product details(id,name,price):");
        int id = int.Parse(Console.ReadLine());
        string nm = Console.ReadLine();
        int price = Convert.ToInt32(Console.ReadLine());

        SqlCommand cmd = new SqlCommand("insert into Product1 values (@pid,@name,@price)", conn);
        cmd.Parameters.AddWithValue("@pid", id); //mapping c# variable with db var
        cmd.Parameters.AddWithValue("@name", nm);
        cmd.Parameters.AddWithValue("@price", price);

        //4.Execute the command
        cmd.ExecuteNonQuery();

        //5.Display the result
        Console.WriteLine("Record Inserted");

    }

    static void display()
    {
        //1.Connection Establishment
        GetConn();
        //2.Prepare the SqlCommand
        SqlCommand cmd = new SqlCommand("select * from Product1", conn);
        //3.Execute the command
        SqlDataReader reader = cmd.ExecuteReader();
        //4.Display the result
        while (reader.Read())
        {
            Console.WriteLine($"Id:{reader[0]}, Name:{reader[1]}, Price:{reader[2]}");
        }
        //5.Close the connection
        conn.Close();
    }

    static void Update()
    {
        //1.Connection Establishment
        GetConn();
        //2.Prepare the SqlCommand
        Console.WriteLine("Enter the Product id to update:");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the new price:");
        int price = Convert.ToInt32(Console.ReadLine());
        SqlCommand cmd = new SqlCommand("update Product1 set Price=@price where Proid=@pid", conn);
        cmd.Parameters.AddWithValue("@pid", id);
        cmd.Parameters.AddWithValue("@price", price);
        //3.Execute the command
        cmd.ExecuteNonQuery();
        //4.Display the result
        Console.WriteLine("Record Updated");
        //5.Close the connection
    }

    static void Delete()
    {
        //1.Connection Establishment
        GetConn();
        //2.Prepare the SqlCommand
        Console.WriteLine("Enter the Product id to delete:");
        int id = int.Parse(Console.ReadLine());
        SqlCommand cmd = new SqlCommand("delete from Product1 where Proid=@pid", conn);
        cmd.Parameters.AddWithValue("@pid", id);
        //3.Execute the command
        cmd.ExecuteNonQuery();
        //4.Display the result
        Console.WriteLine("Record Deleted");
        //5.Close the connection
    }
}
