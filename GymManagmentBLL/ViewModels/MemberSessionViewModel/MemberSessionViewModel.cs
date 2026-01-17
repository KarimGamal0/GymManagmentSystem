using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberSessionViewModel
{
    public class MemberSessionViewModel
    {

        public int Id { get; set; }

        public string MemberName { get; set; } = null!;

        public DateOnly BookingDate { get; set; }

    }
}
