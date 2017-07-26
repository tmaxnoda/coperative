namespace CyberCooperative_Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ELMAH_Error
    {
        [Key]
        [Column(Order = 0)]
        public Guid ErrorId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(60)]
        public string Application { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Host { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(60)]
        public string Source { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(500)]
        public string Message { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string User { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusCode { get; set; }

        [Key]
        [Column(Order = 8)]
        public DateTime TimeUtc { get; set; }

        [Key]
        [Column(Order = 9)]
        public int Sequence { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "ntext")]
        public string AllXml { get; set; }
    }
}
