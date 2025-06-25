/*
CREATE TABLE [IM253E00Usuario] (
    [Id] [uniqueidentifier] NOT NULL,
    [Nombre] [nvarchar](256) NOT NULL,
    [Direccion] [nvarchar] NULL,
    [Telefono] [nvarchar] NOT NULL,
    [Correo] [nvarchar] NOT NULL,

    CONSTRAINT PK_IM253E00Usuario PRIMARY KEY ([Id])
);
*/

namespace Domain.Entities
{
   public class IM253E01Usuario
   {
       public Guid Id { get; set; }
       public string Nombre { get; set; }
       public string Direccion { get; set; }
       public string Telefono { get; set; }
       public string Correo { get; set; }
       public string? Foto { get; set; } // Asegúrate de que esta propiedad esté presente
   }
   
}