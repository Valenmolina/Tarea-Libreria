public interface ILibreria
{
    Usuario LoguearUsuario(string nombre, string password);
    List<string> MostrarRubros();
    List<Libro> MostrarCatalogo(string rubro);
    bool VerificarStock(Libro libro, int cantidad);
    void DescontarStock(Libro libro, int cantidad);
    void ProcesarCompra(Compra compra);
}
