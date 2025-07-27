   using System;
   using System.Collections.Generic;
   using System.Data;
   using Microsoft.Data.SqlClient; // Asegúrate de que este es el correcto
   using Domain.Entities;

namespace Infrastructure.Data
{
    public class LibrosDbContext
    {
        private readonly string _connectionString;

        public LibrosDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<IM253E01Libro> List()
        {
            var data = new List<IM253E01Libro>();

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT [Id],[Autor],[Editorial],[ISBN],[Foto] FROM [IM253E01Libro]", con))
            {
                try
                {
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            data.Add(new IM253E01Libro
                            {
                                Id = (Guid)dr["Id"],
                                Autor = dr["Autor"].ToString(),
                                Editorial = dr["Editorial"]?.ToString(),
                                ISBN = dr["ISBN"].ToString(),
                                Foto = dr["Foto"]?.ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al listar libros", ex);
                }
            }
            return data;
        }

        public IM253E01Libro Details(Guid id)
        {
            var libro = new IM253E01Libro();

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT [Id],[Autor],[Editorial],[ISBN],[Foto] FROM [IM253E01Libro] WHERE [Id] = @id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                
                try
                {
                    con.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            libro.Id = (Guid)dr["Id"];
                            libro.Autor = dr["Autor"].ToString();
                            libro.Editorial = dr["Editorial"]?.ToString();
                            libro.ISBN = dr["ISBN"].ToString();
                            libro.Foto = dr["Foto"]?.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al obtener detalles del libro con ID: {id}", ex);
                }
            }
            return libro;
        }

        public void Create(IM253E01Libro libro)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "INSERT INTO [IM253E01Libro] ([Id],[Autor],[Editorial],[ISBN],[Foto]) " +
                "VALUES (@id,@autor,@editorial,@isbn,@foto)", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
                cmd.Parameters.Add("@autor", SqlDbType.NVarChar).Value = libro.Autor;
                cmd.Parameters.Add("@editorial", SqlDbType.NVarChar).Value = (object)libro.Editorial ?? DBNull.Value;
                cmd.Parameters.Add("@isbn", SqlDbType.NVarChar).Value = libro.ISBN;
                cmd.Parameters.Add("@foto", SqlDbType.NVarChar).Value = (object)libro.Foto ?? DBNull.Value;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al crear libro", ex);
                }
            }
        }

        public void Update(IM253E01Libro libro)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "UPDATE [IM253E01Libro] SET [Autor] = @autor, [Editorial] = @editorial, " +
                "[ISBN] = @isbn, [Foto] = @foto WHERE [Id] = @id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = libro.Id;
                cmd.Parameters.Add("@autor", SqlDbType.NVarChar).Value = libro.Autor;
                cmd.Parameters.Add("@editorial", SqlDbType.NVarChar).Value = (object)libro.Editorial ?? DBNull.Value;
                cmd.Parameters.Add("@isbn", SqlDbType.NVarChar).Value = libro.ISBN;
                cmd.Parameters.Add("@foto", SqlDbType.NVarChar).Value = (object)libro.Foto ?? DBNull.Value;

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al actualizar libro con ID: {libro.Id}", ex);
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM [IM253E01Libro] WHERE [Id] = @id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
                
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error al eliminar libro con ID: {id}", ex);
                }
            }
        }
    }
}
