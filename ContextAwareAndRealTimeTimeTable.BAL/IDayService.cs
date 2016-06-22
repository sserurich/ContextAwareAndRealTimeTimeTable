using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface IDayService
    {

        IEnumerable<Models.Day> GetAllDays();
    }
}
