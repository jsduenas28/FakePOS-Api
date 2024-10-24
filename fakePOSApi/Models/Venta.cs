using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakePOSApi.Models
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDVenta { get; set; }
        public string Factura { get; set; }
        public DateOnly Fecha { get; set; }
        public string MetodoPago { get; set; }
        public double TotalVenta { get; set; }
        public bool IsContable { get; set; }
        public int IDUser { get; set; }

        [ForeignKey("IDUser")]
        public Usuario Usuario { get; set; }
    }
}
