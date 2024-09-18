#region Documentación
/****************************************************************************************************
* WEBAPI                                                    
****************************************************************************************************
* Unidad        : <.NET/C# modelo para el mapeo de las propiedades de Orders>                                                                      
* DescripciÓn   : <Logica de negocio para los servicios de Orders>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación
namespace SalesDatePrediction.Models
.Models
{
    public class Orders
    {
        /// <summary>
        /// Propiedad para alamacenar el Id de la orden
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el nombre del cliente
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el Id del empleado que genero la orden
        /// </summary>
        public int EmpId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el nombre del transportista
        /// </summary>
        public string ShipName { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la direccion del transportista
        /// </summary>
        public string ShipAddress { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la ciudad del envio
        /// </summary>
        public string ShipCity { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la fecha del envio
        /// </summary>
        public DateTime OrderDate { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la fecha requerida
        /// </summary>
        public DateTime? RequireDdate { get; set; }
        /// <summary>
        /// Propiedad para alamacenar la fecha de envio
        /// </summary>
        public DateTime? ShippedDate { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el flete
        /// </summary>
        public Decimal Freight { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el pais de envio
        /// </summary>
        public string ShipCountry { get; set; }

    }

}
