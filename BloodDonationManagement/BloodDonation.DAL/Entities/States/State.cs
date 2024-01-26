using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.DAL.Entities.States
{
    public class State
    {
        public int  id {  get; set; }
        public string state { get; set; }
    }
}
