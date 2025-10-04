namespace EcommerceASPNETCoreWebAPI.Entities
{
    public class Producto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public char Estado { get; set; }
        public bool Habilitado { get; set; }
        public bool Eliminado { get; set; }        
    }
}
