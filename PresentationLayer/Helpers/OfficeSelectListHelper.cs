using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DTOs;
using BusinessLayer.Facades.Offices;

namespace PresentationLayer.Helpers
{
    public class OfficeSelectListHelper
    {
        public OfficeFacade OfficeFacade { get; set; }

		// todo: mali byt tam byt iba offices aktualneho employera
        public async Task<IList<SelectListItem>> Get()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (OfficeDto office in (await OfficeFacade.GetAllOffices()).Items)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = string.Format("{0} {1} {2}", office.Address, office.City, office.Country),
                    Value = office.Id.ToString()
                };
                listSelectListItems.Add(selectList);
            }

            return listSelectListItems;
        }
    }
}