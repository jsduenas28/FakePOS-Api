using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakePOSApi.Models
{
    public class DetalleCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDetalleCompra { get; set; }
        public int IDCompra { get; set; }
        public int IDProducto { get ; set; }
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }

        [ForeignKey("IDCompra")]
        public Compra Compra { get; set; }

        [ForeignKey("IDProducto")]
        public Producto Producto { get; set; }
    }
}
