﻿using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Common;
using BusinessLayer.DTOs.Enums;

namespace BusinessLayer.DTOs
{
    public class OfficeDto : DtoBase
    {
        public string Address { get; set; }

        public string City { get; set; }

        public Country Country { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }
    }
}
