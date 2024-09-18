#region Documentación
/****************************************************************************************************
* WEBAPI                                                    
****************************************************************************************************
* Unidad        : <.NET/C# modelo para el mapeo de las propiedades de Product>                                                                      
* DescripciÓn   : <Logica de negocio para los servicios de Product>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación
using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Models
{
    public class OrderDetails
    {
        /// <summary>
        /// Propiedad para alamacenar el Id de la orden
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el Id del producto
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el precio unitario
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el Qtyo
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el Descuento
        /// </summary>
        public decimal Discount { get; set; }

    }

}
