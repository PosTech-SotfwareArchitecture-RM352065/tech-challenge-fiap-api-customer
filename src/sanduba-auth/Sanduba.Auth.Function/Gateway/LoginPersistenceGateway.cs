using System;
using Microsoft.Data.SqlClient;

namespace Sanduba.Auth.Api.Gateway
{
    public sealed class LoginPersistenceGateway
    {
        public Guid? GetUserId(string userName, string password)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString") ?? String.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using(SqlCommand command = new("dbo.Sp_ValidaLogin", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.Add("@Cpf", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Senha", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@Id", System.Data.SqlDbType.UniqueIdentifier).Direction = System.Data.ParameterDirection.Output;

                    command.Parameters["@Cpf"].Value = userName;
                    command.Parameters["@Senha"].Value = password;

                    command.ExecuteNonQuery();

                    var returnValue = command.Parameters["@Id"].Value.ToString();

                    Guid userId;

                    if (string.IsNullOrEmpty(returnValue) || !Guid.TryParse(returnValue, out userId))
                        return null;

                    return userId;
                }
            }
        }
    }
}
