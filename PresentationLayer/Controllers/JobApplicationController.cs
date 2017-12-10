using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.JobApplication;
using BusinessLayer.Facades.JobOffer;
using BusinessLayer.Facades.RegisteredUsers;
using Castle.Core.Internal;
using PresentationLayer.Helpers;
using PresentationLayer.Models.JobApplication;

namespace PresentationLayer.Controllers
{
    /// <summary>
    /// Vytvara a vybavuje job applications
    /// </summary>
    public class JobApplicationController : Controller
    {
	    public JobApplicationFacade JobApplicationFacade { get; set; }
		public RegisteredUserFacade RegisteredUserFacade { get; set; }
		public JobOfferFacade JobOfferFacade { get; set; }
		public EducationSelectListHelper EducationSelectListHelper { get; set; }

		// GET: JobApplication/Details/5
		public async Task<ActionResult> Details(int id)
		{
			JobApplicationDto application = await this.JobApplicationFacade.Get(id);
			return View(application);
        }

        // GET: JobApplication/Create/5
        public async Task<ActionResult> Create(int id)
        {
            JobOfferDto jobOffer = await JobOfferFacade.Get(id, false);
            var candidate = User.Identity.Name.IsNullOrEmpty()
                ? new JobCandidateDto()
                : await RegisteredUserFacade.GetByUsername(User.Identity.Name);

            JobApplicationCreateDto applicationCreateDto = new JobApplicationCreateDto
            {
                JobOfferId = jobOffer.Id,
                JobOfferName = jobOffer.Name,
                JobOffer = jobOffer,
                Answers = CreateEmptyAnswers(jobOffer),
                CandidateDto = candidate
            };

            JobApplicationCreateViewModel model = new JobApplicationCreateViewModel
            {
                JobApplicationCreateDto = applicationCreateDto,
                Educations = EducationSelectListHelper.Get()
            };

            return View(model);
        }

        // POST: JobApplication/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobApplicationCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var jobOffer = await JobOfferFacade.Get(model.JobApplicationCreateDto.JobOfferId);
                model.JobApplicationCreateDto.JobOfferName = jobOffer.Name;
                model.JobApplicationCreateDto.JobOffer = jobOffer;

                var candidate = User.Identity.Name.IsNullOrEmpty()
                    ? new JobCandidateDto()
                    : await RegisteredUserFacade.GetByUsername(User.Identity.Name);

                model.JobApplicationCreateDto.CandidateDto = candidate;

                model.Educations = EducationSelectListHelper.Get();
                return View(model);
            }

            try
            {
                this.JobApplicationFacade.Create(model.JobApplicationCreateDto);
                return RedirectToAction("Details", "Jobs", new { id = model.JobApplicationCreateDto.JobOfferId });
            }
            catch
            {
                return View();
            }
        }

        // HELPERS //

        private static List<AnswerDto> CreateEmptyAnswers(JobOfferDto jobOffer)
		{
			int questionsCount = jobOffer.Questions.Count;
			var list = new List<AnswerDto>(questionsCount);
		    for (int i = 0; i < questionsCount; i++)
		    {
			    QuestionDto question = jobOffer.Questions[i];
			    list.Add(new AnswerDto
			    {
				    QuestionId = question.Id,
				    Question = question
			    });
		    }
		    return list;
	    }
	}
}
