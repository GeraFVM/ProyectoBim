using Microsoft.Data.SqlClient;
using System.Data;

using Domain;
using Application;

namespace Infrastructure;

public class UsuariosDbContext
{
    private readonly string _connectionString;

    public UsuariosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Usuario> List()
    {
        var data = new List<Usuario>();

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT [Id], [Nombre], [Direccion], [Telefono], [Correo] FROM [Usuario]", con))
        {
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Usuario
                    {
                        Id = (Guid)dr["Id"],
                        Nombre = dr["Nombre"] as string ?? string.Empty,
                        Direccion = dr["Direccion"] as string ?? string.Empty,
                        Telefono = dr["Telefono"] as string ?? string.Empty,
                        Correo = dr["Correo"] as string ?? string.Empty
                    });
                }
                dr.Close();
                return data;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al listar usuarios.", ex);
            }
        }
    }

    public Usuario Details(Guid id)
    {
        Usuario data = null;

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("SELECT [Id], [Nombre], [Direccion], [Telefono], [Correo] FROM [Usuario] WHERE [Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data = new Usuario
                    {
                        Id = (Guid)dr["Id"],
                        Nombre = dr["Nombre"] as string ?? string.Empty,
                        Direccion = dr["Direccion"] as string ?? string.Empty,
                        Telefono = dr["Telefono"] as string ?? string.Empty,
                        Correo = dr["Correo"] as string ?? string.Empty
                    };
                }
                dr.Close();

                if (data == null)
                {
                    // No encontrado
                    throw new Exception($"Usuario con Id {id} no encontrado.");
                }

                return data;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al obtener detalles del usuario.", ex);
            }
        }
    }

    public void Create(Usuario data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        data.Id = Guid.NewGuid();

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("INSERT INTO [Usuario] ([Id], [Nombre], [Direccion], [Telefono], [Correo]) VALUES (@id, @nombre, @direccion, @telefono, @correo)", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 128).Value = data.Nombre ?? string.Empty;
            cmd.Parameters.Add("@direccion", SqlDbType.NVarChar, 256).Value = data.Direccion ?? string.Empty;
            cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50).Value = data.Telefono ?? string.Empty;
            cmd.Parameters.Add("@correo", SqlDbType.NVarChar, 128).Value = data.Correo ?? string.Empty;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al crear el usuario.", ex);
            }
        }
    }

    public void Edit(Usuario data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("UPDATE [Usuario] SET [Nombre] = @nombre, [Direccion] = @direccion, [Telefono] = @telefono, [Correo] = @correo WHERE [Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 128).Value = data.Nombre ?? string.Empty;
            cmd.Parameters.Add("@direccion", SqlDbType.NVarChar, 256).Value = data.Direccion ?? string.Empty;
            cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50).Value = data.Telefono ?? string.Empty;
            cmd.Parameters.Add("@correo", SqlDbType.NVarChar, 128).Value = data.Correo ?? string.Empty;

            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"No se encontró usuario con Id {data.Id} para actualizar.");
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al editar el usuario.", ex);
            }
        }
    }

    public void Delete(Guid id)
    {
        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("DELETE FROM [Usuario] WHERE [Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"No se encontró usuario con Id {id} para eliminar.");
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al eliminar el usuario.", ex);
            }
        }
    }
}
