#region Documentación
/****************************************************************************************************
* WEBAPI                                                    
****************************************************************************************************
* Unidad        : <.NET/C# modelo para el mapeo de las propiedades de Employee>                                                                      
* DescripciÓn   : <Logica de negocio para los servicios de Employees>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación
namespace SalesDatePrediction.Models
{
    public class Employees
    {
        /// <summary>
        /// Propiedad para alamacenar el EmpleadoId de la orden
        /// </summary>
        public int EmpId { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el primer nombre del empleado
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Propiedad para alamacenar el segundo nombre del empleado
        /// </summary>
        public string LastName { get; set; }
    }

}
