using System.Data.SqlClient;
using System.Data;

using Domain;

namespace Infrastructure;

public class LibrosDbContext
{
    private readonly string _connectionString;

    public LibrosDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Libro> List()
    {
        var data = new List<Libro>();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Autor],[Editorial], [ISBN], [Foto] FROM [Libro]", con);
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data.Add(new Libro
                {
                    Id = (Guid)dr["Id"],
                    Autor = (string)dr["Autor"],
                    Editorial = (string)dr["Editorial"],
 		    ISBN = (string)dr["ISBN"],
		    Foto = (string)dr["Foto"],
                });
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public Libro Details(Guid id)
    {
        var data = new Libro();

        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("SELECT [Id],[Autor], [Editorial], [ISBN], [Foto] FROM [Libro] WHERE [Id = @id", con);
        cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data.Id = (Guid)dr["Id"];
                data.Autor = (string)dr["Autor"];
                data.Editorial = (string)dr["Editorial"];
                data.ISBN = (string)dr["ISBN"];
		        data.Foto = (string)dr["Foto"];
		
            }
            return data;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Create(Libro data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("INSERT INTO [Libro] ([Id],[Autor],[Editorial][ISBN][Foto]) VALUES (@id,@autor,@editorial,@isbn,@foto)", con);
        cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
        cmd.Parameters.Add("autor", SqlDbType.NVarChar, 128).Value = data.Autor;
        cmd.Parameters.Add("editorial", SqlDbType.NVarChar, 128).Value = data.Editorial;
	cmd.Parameters.Add("isbn", SqlDbType.NVarChar, 128).Value = data.ISBN;
        cmd.Parameters.Add("foto", SqlDbType.NVarChar, 128).Value = data.Foto;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Edit(Libro data)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("UPDATE [Libro] SET [Autor] = @autor, [Editorial] = @editorial, [ISBN] = @isbn, [Foto] = @foto,  WHERE [Id] = @id", con);
       	    cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@autor", SqlDbType.NVarChar, 128).Value = data.Autor;
            cmd.Parameters.Add("@editorial", SqlDbType.NVarChar, 128).Value = data.Editorial;
            cmd.Parameters.Add("@isbn", SqlDbType.NVarChar, 128).Value = data.ISBN;
            cmd.Parameters.Add("@foto", SqlDbType.NVarChar, 128).Value = data.Foto;


        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
	    Console.WriteLine($"Error al actualizar el libro: {ex.Message}");

            throw;
        }
        finally
        {
            con.Close();
        }
    }

    public void Delete(Guid id)
    {
        var con = new SqlConnection(_connectionString);
        var cmd = new SqlCommand("DELETE FROM [Libro] WHERE [Id] = @id", con);
        cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
}