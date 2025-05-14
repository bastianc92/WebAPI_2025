using System.ComponentModel.DataAnnotations;

namespace WebApi_2025_jueves.DAL.Entities
{
    public class AuditBase{

        [Key] // PK
        [Required] // Campo obligatorio
        public virtual Guid Id { get; set; } // Este sera la PK de todas las tablas

        public virtual DateTime CreatedDate { get; set; } //Guarda registro nuevo con su fecha

        public virtual DateTime ModifiedDate { get; set; } // Guarda todo registro que se modifica 

    }
}
 
// Profe a mi el .NET 6 no me dejo instalar solo me dejaba instalar el 7 y 8 y me paso eso es porque yo para el visualStudiocode uso el .NET 8