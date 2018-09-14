namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLogin")]
    public partial class UserLogin
    {
        public int id { get; set; }

        [StringLength(50)]
        public string loginname { get; set; }

        [StringLength(20)]
        public string loginpwd { get; set; }

        [StringLength(50)]
        public string pid { get; set; }

        [StringLength(50)]
        public string mobileid { get; set; }
        [DisplayName("ÊÇ·ñ½ûÑÔ")]

        public string md5pass { get; set; }

        public int roleid { get; set; }

        public int reseauStatus { get; set; }

        public string reseauTown { get; set; }
    }
}
