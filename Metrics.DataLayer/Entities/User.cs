using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Metrics.DataLayer.Entities
{
    [Table("users")]
    public class User : IMetricsEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }
        
        [Column("last_activity_date")]
        public DateTime LastActivityDate { get; set; }
    }
}