using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Model;

namespace TP_DAL
{
    public class ContactServices
    {
        public static void Add(Contact contact)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Contact(FirstName, LastName, Birthday, Adress, IsFavorite, Email, [User_Id], PhoneNumber, [PhoneNumber2]) VALUES (@FirstName, @LastName, @Birthday, @Adress, @IsFavorite, @Email, @UserId, @PhoneNumber, @PhoneNumber2)";

                    cmd.Parameters.AddWithValue("FirstName", contact.FirstName);
                    cmd.Parameters.AddWithValue("LastName", contact.LastName);
                    DBUtility.HandleNullValue("Birthday", contact.Birthday, cmd);
                    DBUtility.HandleNullString("Adress", contact.Address, cmd);
                    cmd.Parameters.AddWithValue("IsFavorite", contact.IsFavorite);
                    DBUtility.HandleNullString("Email", contact.Email, cmd);
                    DBUtility.HandleNullString("PhoneNumber", contact.PhoneNumber, cmd);
                    DBUtility.HandleNullString("PhoneNumber2", contact.PhoneNumber2, cmd);
                    cmd.Parameters.AddWithValue("UserId", contact.UserID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Delete(Contact contact)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [Contact] WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("Id", contact.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void Modify(Contact contact)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Contact SET FirstName = @FirstName, LastName = @LastName, Adress = @Adress, PhoneNumber = @PhoneNumber, PhoneNumber2 = @PhoneNumber2, " +
                        "IsFavorite = @IsFavorite, Birthday = @Birthday, Email = @Email WHERE Id = @Id; ";
                    cmd.Parameters.AddWithValue("Id", contact.Id);
                    cmd.Parameters.AddWithValue("FirstName", contact.FirstName);
                    cmd.Parameters.AddWithValue("LastName", contact.LastName);
                    DBUtility.HandleNullValue("Birthday", contact.Birthday, cmd);
                    DBUtility.HandleNullString("Adress", contact.Address, cmd);
                    cmd.Parameters.AddWithValue("IsFavorite", contact.IsFavorite);
                    DBUtility.HandleNullString("Email", contact.Email, cmd);
                    DBUtility.HandleNullString("PhoneNumber", contact.PhoneNumber, cmd);
                    DBUtility.HandleNullString("PhoneNumber2", contact.PhoneNumber2, cmd);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        public static List<Contact> GetContactsBy(Guid userId)
        {
            List<Contact> contactList = new List<Contact>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString.connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandText = @"Select [Id], FirstName, LastName, Birthday, Adress, IsFavorite, Email, [User_Id], PhoneNumber, [PhoneNumber2] FROM [Contact] WHERE [User_Id] = @UserId";
                    cmd.Parameters.AddWithValue("UserId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            contactList.Add(new Contact(
                                (Guid)reader["Id"],
                                reader["FirstName"] as string,
                                reader["LastName"] as string,
                                DBUtility.HandleNullStringFromDB(reader, "Adress"),
                                DBUtility.HandleNullStringFromDB(reader, "Email"),
                                DBUtility.HandleNullStringFromDB(reader, "PhoneNumber"),
                                DBUtility.HandleNullStringFromDB(reader, "PhoneNumber2"),
                                (bool)reader["IsFavorite"],
                                reader["Birthday"] as DateTime?,
                                (Guid)reader["User_Id"]));
                        }
                    }
                }
            }
            return contactList;
        }
    }

}
