using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot.Modal
{
    public class ControlModal
    {
        public UserStepNextStep step { get; set; }
        public string message { get; set; }
        public bool isBackBtnClicked { get; set; }
    }

    public class BackBtnHelper
    {
        public int SelectedCountryId { get; set; }
        public int SelectedCityId { get; set; }
    }

}
