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
namespace SalesDatePrediction.Models
{
    public class Product
    {
        /// <summary>
        /// Propiedad para alamacenar el Id del producto
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el nombre del producto
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Propiedad para almacenar la cantidad de producto disponible
        /// </summary>
        public int Quantity { get; set; }
    }

}
