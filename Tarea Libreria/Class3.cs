public class compra
{
    public Usuario Cliente { get; set; }
    public Dictionary<Libro, int> LibrosComprados { get; set; } = new Dictionary<Libro, int>();
    public decimal Total { get; set; }

    public compra(Usuario cliente) => Cliente = cliente;

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
