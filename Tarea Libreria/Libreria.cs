using System.Diagnostics;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public class sistemalibreria : ILibreria
{
    private List<Usuario> Usuarios = new List<Usuario>();
    private List<Libro> Inventario = new List<Libro>();

    public sistemalibreria()
    {
        // Inicialización de datos
        Usuarios.Add(new Usuario("cliente1", "1234"));
        Usuarios.Add(new Usuario("cliente2", "abcd"));

        Inventario.Add(new Libro("C# Avanzado", "programacion", 10, 25.00m));
        Inventario.Add(new Libro("Harry Potter", "fantasia", 5, 15.00m));
        Inventario.Add(new Libro("Historia del Arte", "arte", 7, 30.00m));
    }

    public Usuario LoguearUsuario(string nombre, string Contraseña)
    {
        foreach (var usuario in Usuarios)
        {
            if (usuario.Nombre == nombre && usuario.Contraseña == Contraseña)
            {
                Console.WriteLine("Login exitoso.");
                return usuario;
            }
        }
        Console.WriteLine("Usuario o contraseña incorrectos.");
        return null;
    }

    public List<string> MostrarRubros()
    {
        List<string> rubros = new List<string>();
        foreach (var libro in Inventario)
        {
            if (!rubros.Contains(libro.Rubro))
                rubros.Add(libro.Rubro);
        }
        return rubros;
    }

    public List<Libro> MostrarCatalogo(string rubro)
    {
        List<Libro> catalogo = new List<Libro>();
        foreach (var libro in Inventario)
        {
            if (libro.Rubro == rubro)
                catalogo.Add(libro);
        }
        return catalogo;
    }

    public bool VerificarStock(Libro libro, int cantidad)
    {
        return libro.Stock >= cantidad;
    }

    public void DescontarStock(Libro libro, int cantidad)
    {
        libro.Stock -= cantidad;
    }

    public void ProcesarCompra(Compra compra)
    {
        compra.CalcularTotal();
        Console.WriteLine($"El total de la compra es: {compra.Total:C}");

        Console.WriteLine("Ingrese los datos de la tarjeta de credito:");
        Console.Write("Numero de tarjeta: ");
        string tarjeta = Console.ReadLine();
        Console.Write("Fecha de vencimiento: ");
        string vencimiento = Console.ReadLine();
        Console.Write("Codigo de seguridad: ");
        string codigo = Console.ReadLine();

        Console.WriteLine("Tarjeta verificada y pago procesado.");
        foreach (var item in compra.LibrosComprados)
        {
            DescontarStock(item.Key, item.Value);
        }
        Console.WriteLine("Compra finalizada y stock actualizado.");
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}
