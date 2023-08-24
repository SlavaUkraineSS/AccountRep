using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Account_Project.Model
{
    static class MsSqlDataProvider
    {
        private static SqlConnection connection;

        static MsSqlDataProvider()
        {
            new Action(() =>
           {
               try
               {

                   connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lenovo\source\repos\Account_Project\UsersDataWPF.mdf;Integrated Security=True");
                   connection.Open();
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }

           })();

        }


        public static bool IsValidLogin(string name, string password)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM [User] WHERE [Name] = N'{name}' AND [Password] = N'{password}'", connection))
            {
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }



        }

        public static User GetUser(string name, string password)
        {

            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM [User] WHERE [Name] = N'{name}' AND [Password] = N'{password}'", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    try
                    {
                        User user = new User();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                                user.id = reader.GetString(0);
                            if (!reader.IsDBNull(1))
                                user.name = reader.GetString(1);
                            if (!reader.IsDBNull(2))
                                user.password = reader.GetString(2);
                            if (!reader.IsDBNull(3))
                                user.birthday = reader.GetDateTime(3);
                            if (!reader.IsDBNull(4))
                                user.description = reader.GetString(4);
                            if (!reader.IsDBNull(5))
                                user.Image = (byte[])reader.GetValue(5);

                        }
                        return user;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + $"{nameof(GetUser)}");
                        return null;
                    }
                    finally
                    {
                        // if (reader != null)
                        reader.Close();

                    }

                }
            }


        }



        public async static void AddNewUser(User user)
        {
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO [User]([Id], [Name], [Password]) VALUES('{user.id}','{user.name}', '{user.password}')", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public async static void AddDate(DateTime date, User user)
        {
            await Task.Run(() =>
            {

                string convertDate = date.ToString("yyyy-MM-dd");

                using (SqlCommand cmd = new SqlCommand($"UPDATE [User] SET [Date of Birth] = '{convertDate}' WHERE [Id] = '{user.id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public async static void AddImage(byte[] imageData, User user)
        {
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE [User] SET [Image] = (@ImageData) WHERE [Id] = '{user.id}'", connection))
                {
                    SqlParameter imageParam = new SqlParameter("@ImageData", SqlDbType.VarBinary);
                    imageParam.Value = imageData;
                    cmd.Parameters.Add(imageParam);
                    cmd.ExecuteNonQuery();

                }
            });
        }

        public async static void AddDescription(string description, User user)
        {
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE [User] SET [Status] = N'{description}' WHERE [Id] = '{user.id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public static async void AddName(string name, User user)
        {
            await Task.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE [User] SET [Name] = '{name}' WHERE [Id] = '{user.id}'", connection))
                {
                    cmd.ExecuteNonQuery();
                }
            });
        }




        public static bool NameIsTaken(string name)
        {
            using (SqlCommand cmd = new SqlCommand($"SELECT COUNT([Name]) FROM [User] WHERE [Name] = '{name}'", connection))
            {
                return (int)cmd.ExecuteScalar() > 0;
            }

        }

        public async static Task<string> GetUserName(User user)
        {
            return await Task<string>.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [Name] FROM [User] WHERE [Id] = '{user.id}'", connection))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        string userName = null;
                        while (reader.Read())
                        {
                            userName = reader.GetString(0);
                        }
                        return userName;
                    }
                }


            });

        }

        public async static Task<string> GetUserDiscription(User user)
        {
            return await Task<string>.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [Status] FROM [User] WHERE [Id] = '{user.id}'", connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.GetString(0);
                }

            });

        }

        public async static Task<DateTime> GetUserBirthday(User user)
        {
            return await Task<DateTime>.Run(() =>
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT [Date of Birth] FROM [User] WHERE [Id] = '{user.id}'", connection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    return reader.GetDateTime(0);
                }


            });

        }
    }
}
