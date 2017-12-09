using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessLayer.DTOs.Enums;

namespace PresentationLayer.Helpers
{
	public class EducationSelectListHelper
	{
		// cache
		private readonly IList<SelectListItem> educationList;

		public EducationSelectListHelper()
		{
			this.educationList = this.CreateList();
		}

		public IList<SelectListItem> Get()
		{
			return this.educationList;
		}

		private IList<SelectListItem> CreateList()
		{
			List<SelectListItem> listSelectListItems = new List<SelectListItem>();

			var allItems = Enum.GetValues(typeof(Education));

			foreach (Education item in allItems)
			{
				SelectListItem selectList = new SelectListItem
				{
					Text = item.ToString(),
					Value = ((int)item).ToString()
				};
				listSelectListItems.Add(selectList);
			}

			return listSelectListItems;
		}
	}
}