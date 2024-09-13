public class Libro
{
    public string Titulo { get; set; }
    public string Rubro { get; set; }
    public int Stock { get; set; }
    public decimal Precio { get; set; }

    public Libro(string titulo, string rubro, int stock, decimal precio)
    {
        titulo = titulo;
        rubro = rubro;
        stock = stock;
        precio = precio;
    }
}
