#region Documentación
/****************************************************************************************************
* WEBAPI                                                    
****************************************************************************************************
* Unidad        : <.NET/C# modelo para el mapeo de las propiedades de Customers>                                                                      
* DescripciÓn   : <Logica de negocio para los servicios de Customers>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación

namespace SalesDatePrediction.Models
{
    public class Customers
    {
        /// <summary>
        /// Propiedad para alamacenar el nombre del cliente
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el nombre del cliente
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la fecha de la ultima orden
        /// </summary>
        public DateTime? LastOrderDate { get; set; }

        /// <summary>
        /// Propiedad para alamacenar la fecha predictiva de la siguiente orden
        /// </summary>
        public DateTime? NextPredictedOrder { get; set; }
    }
}
