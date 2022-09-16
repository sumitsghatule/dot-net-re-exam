using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TestPaper.Models
{
    public class Products
    {

        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Enter product name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Enter product rate")]
        public decimal BookRate { get; set; }

        [Required(ErrorMessage = "Enter product description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter Category name")]
        public string CategoryName { get; set; }
        

        public static List<Products> AllProducts()
        {
            List<Products> prods = new List<Products>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True";
            try
            {
                con.Open();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = con;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "Select * from Products";

                SqlDataReader dr = cmdSelect.ExecuteReader();
                while (dr.Read())
                {
                    prods.Add(new Products { BookId = (int)dr["BookId"], ProductName = (string)dr["Product Name"], BookRate = (decimal)dr["BookRate"], Description = (string)dr["Description"].ToString(), CategoryName = (string)dr["CategoryName"] });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return (prods);
        }

        public static void UpdateProduct(int id, Products obj)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Products set ProductName=@ProductName, Rate=@Rate ,Description=@Description, CategoryName=@CategoryName where ProductId=@ProductId ";
                // update product set productid=@productid ,
                /*cmd.CommandText = "UpdateProduct";*/
                cmd.Parameters.AddWithValue("@ProductId", obj.BookId);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@Rate", obj.BookRate);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);

                /*object ret = cmd.ExecuteScalar();
                if (ret == null)
                    return false;
                else
                    return true;*/
                cmd.ExecuteNonQuery();
            }
            catch
            {
                
            }
            finally
            {
                con.Close();
            }
        }

        public static Products getproductDetails(int id)
        {
            Products prod = null;
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=JKJuly2022;Integrated Security=True";
            try
            {
                con.Open();
                SqlCommand cmdSelect = new SqlCommand();
                cmdSelect.Connection = con;
                cmdSelect.CommandType = CommandType.Text;
                cmdSelect.CommandText = "Select * from Products where ProductId=@Productid";
                cmdSelect.Parameters.AddWithValue("@Productid", id);

                SqlDataReader dr = cmdSelect.ExecuteReader();
                while (dr.Read())
                {
                    prod = new Products { BookId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], BookRate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategoryName = (string)dr["CategoryName"] };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return prod;
        }
    }
}