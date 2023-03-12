using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SpeedyWheelz.Migrations;

namespace SpeedyWheelz.Models
{
    public class pushSubscription
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Auth { get; set; }

        [Required]
        public string P256dh { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}