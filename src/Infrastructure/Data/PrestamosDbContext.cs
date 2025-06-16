using System.Data.SqlClient;
using System.Data;

using Domain;
using Application;

namespace Infrastructure;

public class PrestamosDbContext
{
    private readonly string _connectionString;

    public PrestamosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<IM253E01Prestamo> List()
    {
        var data = new List<IM253E01Prestamo>();

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand(@"
            SELECT p.[Id], p.[UsuarioId], p.[LibroId], p.[FechaPrestamo], p.[FechaDevolucion],
                   u.[Id] AS UsuarioId, u.[Nombre], 
                   l.[Id] AS LibroId, l.[Titulo]
            FROM [IM253E00Prestamos] p
            JOIN [IM253E00Usuario] u ON p.[UsuarioId] = u.[Id]
            JOIN [IM253E00Libro] l ON p.[LibroId] = l.[Id]", con))
        {
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var prestamo = new IM253E01Prestamo
                    {
                        Id = (Guid)dr["Id"],
                        UsuarioId = (Guid)dr["UsuarioId"],
                        LibroId = (Guid)dr["LibroId"],
                        FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                        FechaDevolucion = dr["FechaDevolucion"] as DateTime?,
                        Usuario = new IM253E01Usuario
                        {
                            Id = (Guid)dr["UsuarioId"],
                            Nombre = dr["Nombre"] as string ?? string.Empty
                        },
                        Libro = new IM253E01Libro
                        {
                            Id = (Guid)dr["LibroId"],
                            Titulo = dr["Titulo"] as string ?? string.Empty
                        }
                    };
                    data.Add(prestamo);
                }
                return data;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al listar préstamos.", ex);
            }
        }
    }

    public IM253E01Prestamo Details(Guid id)
    {
        IM253E01Prestamo data = null;

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand(@"
            SELECT p.[Id], p.[UsuarioId], p.[LibroId], p.[FechaPrestamo], p.[FechaDevolucion],
                   u.[Id] AS UsuarioId, u.[Nombre], 
                   l.[Id] AS LibroId, l.[Titulo]
            FROM [IM253E00Prestamos] p
            JOIN [IM253E00Usuario] u ON p.[UsuarioId] = u.[Id]
            JOIN [IM253E00Libro] l ON p.[LibroId] = l.[Id]
            WHERE p.[Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data = new IM253E01Prestamo
                    {
                        Id = (Guid)dr["Id"],
                        UsuarioId = (Guid)dr["UsuarioId"],
                        LibroId = (Guid)dr["LibroId"],
                        FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                        FechaDevolucion = dr["FechaDevolucion"] as DateTime?,
                        Usuario = new IM253E01Usuario
                        {
                            Id = (Guid)dr["UsuarioId"],
                            Nombre = dr["Nombre"] as string ?? string.Empty
                        },
                        Libro = new IM253E01Libro
                        {
                            Id = (Guid)dr["LibroId"],
                            Titulo = dr["Titulo"] as string ?? string.Empty
                        }
                    };
                }
                return data;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al obtener detalles del préstamo.", ex);
            }
        }
    }

    public void Create(IM253E01Prestamo data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        data.Id = Guid.NewGuid();

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("INSERT INTO [IM253E00Prestamos] ([Id],[UsuarioId],[LibroId],[FechaPrestamo],[FechaDevolucion]) VALUES (@id,@usuarioId,@libroId,@fechaPrestamo,@fechaDevolucion)", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@usuarioId", SqlDbType.UniqueIdentifier).Value = data.UsuarioId;
            cmd.Parameters.Add("@libroId", SqlDbType.UniqueIdentifier).Value = data.LibroId;
            cmd.Parameters.Add("@fechaPrestamo", SqlDbType.SmallDateTime).Value = data.FechaPrestamo;
            cmd.Parameters.Add("@fechaDevolucion", SqlDbType.SmallDateTime).Value = (object)data.FechaDevolucion ?? DBNull.Value;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al crear el préstamo.", ex);
            }
        }
    }

    public void Edit(IM253E01Prestamo data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("UPDATE [IM253E00Prestamos] SET [UsuarioId] = @usuarioId, [LibroId] = @libroId, [FechaPrestamo] = @fechaPrestamo, [FechaDevolucion] = @fechaDevolucion WHERE [Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@usuarioId", SqlDbType.UniqueIdentifier).Value = data.UsuarioId;
            cmd.Parameters.Add("@libroId", SqlDbType.UniqueIdentifier).Value = data.LibroId;
            cmd.Parameters.Add("@fechaPrestamo", SqlDbType.SmallDateTime).Value = data.FechaPrestamo;
            cmd.Parameters.Add("@fechaDevolucion", SqlDbType.SmallDateTime).Value = (object)data.FechaDevolucion ?? DBNull.Value;

            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"No se encontró préstamo con Id {data.Id} para actualizar.");
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al editar el préstamo.", ex);
            }
        }
    }

    public void Delete(Guid id)
    {
        using (var con = new SqlConnection(_connectionString))
        using (var cmd = new SqlCommand("DELETE FROM [IM253E00Prestamos] WHERE [Id] = @id", con))
        {
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"No se encontró préstamo con Id {id} para eliminar.");
                }
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new Exception("Error al eliminar el préstamo.", ex);
            }
        }
    }
}
