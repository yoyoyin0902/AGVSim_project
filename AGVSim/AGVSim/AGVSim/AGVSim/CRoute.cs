using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVSim
{
    public class CRoute
    {
        public int ID;
        public int max_process_id;
        public List<int> process_ids = new List<int>();
        public List<int> sub_process_ids = new List<int>();
        public List<int> type_ids = new List<int>();
        public List<int> machine_ids = new List<int>();
        public List<int> code_ids = new List<int>();
        public List<int> oper_times = new List<int>();

        public List<int> unique_ids = new List<int>();
        public List<int> process_alternative_machines = new List<int>();
    }
}
