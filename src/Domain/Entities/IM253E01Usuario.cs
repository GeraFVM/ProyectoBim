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

public class IM253E01Usuario
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Direccion { get; set; }
    public string Telefono { get; set; } = string.Empty;
    public string? Correo { get; set; } = string.Empty;
}