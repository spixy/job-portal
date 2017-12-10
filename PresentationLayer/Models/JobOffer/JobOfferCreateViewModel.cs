using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BusinessLayer.DTOs;

namespace PresentationLayer.Models.JobOffer
{
    public class JobOfferCreateViewModel
    {
        [Range(1,20)]
        public int NumberOfQuestions { get; set; }

        public int NumberOfSkills { get; set; }

        public IEnumerable<SelectListItem> Offices { get; set; }

        public JobOfferCreateDto JobOfferCreateDto { get; set; }
    }
}
