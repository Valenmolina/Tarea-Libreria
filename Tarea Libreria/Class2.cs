using System.Runtime.Serialization;

public class Usuario
{
    public string Nombre { get; set; }
    public string Contraseña { get; set; }

    public Usuario(string nombre, string contraseña)
    {
        nombre = nombre;
        contraseña = contraseña;
    }
}
