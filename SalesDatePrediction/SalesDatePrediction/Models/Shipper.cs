#region Documentación
/****************************************************************************************************
* WEBAPI                                                    
****************************************************************************************************
* Unidad        : <.NET/C# modelo para el mapeo de las propiedades de Shipper>                                                                      
* DescripciÓn   : <Logica de negocio para los servicios de Shipper>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación
namespace SalesDatePrediction.Models
{
    public class Shipper
    {
        /// <summary>
        /// Propiedad para alamacenar el Id del transportista
        /// </summary>
        public int ShipperId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el nombre del transportista
        /// </summary>
        public string CompanyName { get; set; }
    }

}
