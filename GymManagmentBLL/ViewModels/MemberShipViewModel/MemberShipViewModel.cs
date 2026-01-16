using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberShipViewModel
{
    public class MemberShipViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Plan { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
