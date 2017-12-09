using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer.DTOs;

namespace PresentationLayer.Models.JobOffer
{
    public class JobOfferCreateViewModel
    {
        public IEnumerable<SelectListItem> Offices { get; set; }

        public JobOfferCreateDto JobOfferCreateDto { get; set; }
    }
}
