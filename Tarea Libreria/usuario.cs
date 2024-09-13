using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea_Libreria
{
    class class1
    {
    }
}
public class libro
{
    public string Titulo { get; set; }
    public string Rubro { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }

    public libro(string titulo, string rubro, int stock, decimal precio)
    {
        Titulo = titulo;
        Rubro = rubro;
        Stock = stock;
        Precio = precio;
    }
}

public class usuario
{
    public string Nombre { get; set; }
    public string Password { get; set; }

    public usuario(string nombre, string password)
    {
        Nombre = nombre;
        Password = password;
    }
}

public class Compra
{
    public Usuario Cliente { get; set; }
    public Dictionary<Libro, int> LibrosComprados { get; set; } = new Dictionary<Libro, int>();
    public decimal Total { get; set; }

    public Compra(Usuario cliente) => Cliente = cliente;

    public void AgregarLibro(Libro libro, int cantidad)
    {
        if (LibrosComprados.ContainsKey(libro))
            LibrosComprados[libro] += cantidad;
        else
            LibrosComprados[libro] = cantidad;
    }

    public void CalcularTotal()
    {
        Total = 0;
        foreach (var item in LibrosComprados)
        {
            Total += item.Key.Precio * item.Value;
        }
    }
}

public class Sistemalibreria
{
    private List<Usuario> usuarios = new List<Usuario>();
    private List<Libro> inventario = new List<Libro>();

    public Sistemalibreria()
    {
        // Inicialización de datos
        usuarios.Add(new Usuario("cliente1", "1234"));
        usuarios.Add(new Usuario("cliente2", "abcd"));

        inventario.Add(new Libro("C# Avanzado", "programacion", 10, 25.00m));
        inventario.Add(new Libro("Harry Potter", "fantasia", 5, 15.00m));
        inventario.Add(new Libro("Historia del Arte", "arte", 7, 30.00m));
    }

    public Usuario LoguearUsuario(string nombre, string Contraseña)
    {
        foreach (var usuario in usuarios)
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
        foreach (var libro in inventario)
        {
            if (!rubros.Contains(libro.Rubro))
                rubros.Add(libro.Rubro);
        }
        return rubros;
    }

    public List<Libro> MostrarCatalogo(string rubro)
    {
        List<Libro> catalogo = new List<Libro>();
        foreach (var libro in inventario)
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

        // Simulación de ingreso de datos de tarjeta
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
}

class Program
{
    static void Main(string[] args)
    {
        sistemalibreria sistema = new sistemalibreria();

        Console.WriteLine("Bienvenido a la Libreria Web");
        Console.Write("Ingrese su nombre de usuario: ");
        string nombreUsuario = Console.ReadLine();
        Console.Write("Ingrese su contraseña: ");
        string password = Console.ReadLine();

        Usuario usuario = sistema.LoguearUsuario(nombreUsuario, password);

        if (usuario != null)
        {
            Compra compra = new Compra(usuario);
            List<string> rubros = sistema.MostrarRubros();
            Console.WriteLine("Rubros disponibles:");
            foreach (var rubro in rubros)
            {
                Console.WriteLine($"- {rubro}");
            }

            bool continuar = true;
            while (continuar)
            {
                Console.Write("Seleccione un rubro: ");
                string rubroSeleccionado = Console.ReadLine();
                List<Libro> catalogo = sistema.MostrarCatalogo(rubroSeleccionado);

                if (catalogo.Count > 0)
                {
                    Console.WriteLine("Libros disponibles:");
                    foreach (var Libro in catalogo)
                    {
                        Console.WriteLine($"- {Libro.Titulo} (${Libro.Precio}) - Stock: {Libro.Stock}");
                    }

                    Console.Write("Ingrese el título del libro a comprar: ");
                    string libroSeleccionado = Console.ReadLine();
                    Libro libro = catalogo.Find(l => l.Titulo == libroSeleccionado);

                    if (libro != null)
                    {
                        Console.Write("Ingrese la cantidad a comprar: ");
                        int cantidad = int.Parse(Console.ReadLine());

                        if (sistema.VerificarStock(libro, cantidad))
                        {
                            compra.AgregarLibro(libro, cantidad);
                            Console.WriteLine("Libro agregado al carrito.");
                        }
                        else
                        {
                            Console.WriteLine("Stock insuficiente.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Libro no encontrado.");
                    }
                }
                else
                {
                    Console.WriteLine("Rubro no disponible.");
                }

                Console.Write("¿Desea seguir comprando? (si/no): ");
                continuar = Console.ReadLine().ToUpper() == "s";
            }

            Console.Write("¿Desea confirmar la compra? (si/no): ");
            if (Console.ReadLine().ToUpper() == "si")
            {
                sistema.ProcesarCompra(compra);
            }
            else
            {
                Console.WriteLine("Compra cancelada.");
            }
        }
    }
}
