/*CREATE TABLE [IM253E00Prestamos] (
    [Id] [uniqueidentifier] NOT NULL,
    [UsuarioId] [uniqueidentifier] NOT NULL,
    [LibroId] [uniqueidentifier] NOT NULL,
    [FechaPrestamo] [smalldatetime] NOT NULL,
    [FechaDevolucion] [smalldatetime] NULL,

    CONSTRAINT PK_IM253E00Prestamos PRIMARY KEY ([Id]),
    CONSTRAINT FK_IM253E00Prestamos_IM253E00Usuario FOREIGN KEY ([UsuarioId]) REFERENCES [IM253E00Usuario] ([Id]),
    CONSTRAINT FK_IM253E00Prestamos_IM253E00Libro FOREIGN KEY ([LibroId]) REFERENCES [IM253E00Libro] ([Id])
);
*/

namespace Domain.Entities
{
    public class IM253E01Prestamo
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid LibroId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        /*Navegation Properties*/
        public IM253E01Usuario? Usuario { get; set; }
        public IM253E01Libro? Libro { get; set; }
    }
}
