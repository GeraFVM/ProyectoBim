/*CREATE TABLE [IM253E00Libro] (
    [Id] [uniqueidentifier] NOT NULL,
    [Autor] [nvarchar] NOT NULL,
    [Editorial] [nvarchar] NULL,
    [ISBN] [nvarchar] NOT NULL,
    [Foto] [nvarchar](max) NULL,

    CONSTRAINT PK_IM253E00Libro PRIMARY KEY ([Id])
);
*/

public class IM253E01Libro
{
    public Guid Id { get; set; }
    public string Autor { get; set; } = string.Empty;
    public string? Editorial { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string? Foto { get; set; } 
}