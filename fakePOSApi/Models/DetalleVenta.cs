using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakePOSApi.Models
{
    public class DetalleVenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDetalleVenta { get; set; }
        public int IDVenta { get; set; }
        public int IDProducto { get; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }

        [ForeignKey("IDVenta")]
        public Venta Venta { get; set; }

        [ForeignKey("IDProducto")]
        public Producto Producto { get; set; }
    }
}
