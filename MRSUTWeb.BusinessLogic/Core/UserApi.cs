using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MRSUTWeb.Domain.Entities.User;
using MRSUTWeb.Helpers;
using System.Net.Mail;
using System.Net;


namespace MRSUTWeb.BusinessLogic.Core
{
    public class UserApi
    {
        string salt = HashSaltGenerate.GenerateSalt();
        string code = HashSaltGenerate.GeneraterRandomCode();

        internal URegister RegisterUserAction(URegister register)
        {


            string hashPassword = HashSaltGenerate.HashPasswordWithSalt(register.Password, salt);
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MRSUTWeb;Integrated Security=True";
            string query = "INSERT INTO Userr (Name, Surname, Username, Email, Hash_Password, Salt, Code, ID_Type_user) VALUES (@Name, @Surname, @Username, @Email, @Hash_Password, @Salt, @Code, @ID_Type_user)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_User", register.Id);
                    command.Parameters.AddWithValue("@ID_Type_user", 1);
                    command.Parameters.AddWithValue("@Name", register.Name);
                    command.Parameters.AddWithValue("@Surname", register.Surname);
                    command.Parameters.AddWithValue("@Username", register.Username);
                    command.Parameters.AddWithValue("@Email", register.Email);
                    command.Parameters.AddWithValue("@Hash_Password", hashPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@Code", code);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return register;
        }
        internal void SendEmail(URegister register)
        {
            // Send email to the user
            MailMessage mail = new MailMessage("madarableach55@gmail.com", register.Email);
            mail.Subject = "Confirm your email";
            mail.Body = "Your confirmation code is: " + code;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("madarableach55@gmail.com", "nhrjcgocqzznmove");//ascundeti parola
            smtp.EnableSsl = true;
            smtp.Send(mail);
            
        }
    }
}
