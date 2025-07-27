/*CREATE TABLE [IM253E01Libro] (
    [Id] [uniqueidentifier] NOT NULL,
    [Autor] [nvarchar] NOT NULL,
    [Editorial] [nvarchar] NULL,
    [ISBN] [nvarchar] NOT NULL,
    [Foto] [nvarchar](max) NULL,

    CONSTRAINT PK_IM253E01Libro PRIMARY KEY ([Id])
);
*/

   namespace Domain.Entities
   {
       public class IM253E01Libro
       {
           public Guid Id { get; set; }
           public string Autor { get; set; } = string.Empty;
           public string? Editorial { get; set; }
           public string ISBN { get; set; } = string.Empty;
           public string? Foto { get; set; } 
           public string Titulo { get; set; } = string.Empty; // Agregar propiedad Titulo
           public DateTime? Inicio { get; set; } // Agregar propiedad Inicio
           public DateTime FechaPublicacion { get; set; }
       }
   }
   
   