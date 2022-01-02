using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGVSim
{
    public class COrder
    {
        public int m_Order_ID;
        public int m_workPieceID;
        public int m_Product_ID;
        public int m_Quantity; //數量
        public int m_Due; //到期日
        public int m_Priority; //優先權
        public int m_Done;
        public int m_OdrStatus; //0: idle, 1: Machine processing, 2: AGV, 3: finished
        // Run Time
        public int cur_unique_id;
        public int cur_process_id;
        public int cur_stop_id;
        public int max_process_id;

        public List<int> all_machines = new List<int>(); // Format: 001;002;007;
        public List<int> all_types = new List<int>(); // Format: 001;002;007;
        public List<int> run_machines = new List<int>(); // Format: 001;002;007;, Event: 1 start; 2: end 
        public List<int> run_events = new List<int>(); // Format: 001;002;007;, Event: 1 start; 2: end 
        public List<long> run_sim_clock = new List<long>(); // Format: 001;002;007;, Event: 1 start; 2: end

        public List<int> ReceiveOrder = new List<int>();

        public CRoute pRoute;

        public int delet_Product_ID;
        



        public COrder()//int product,int quanttity,int priority, int due, int done,int status
        {
            //this.m_Product_ID = product;
            //this.m_Quantity = quanttity;
            //this.m_Priority = priority;
            //this.m_Due = due;
            //this.m_Done = done;
            //this.m_OdrStatus = status;
            all_machines.Clear();
            all_types.Clear();
            //all_machines.Remove();
            //all_types.RemoveAll();
        }

        public COrder(int product, int quanttity, int priority, int due, int done, int status)
        {
            this.m_Product_ID = product;
            this.m_Quantity = quanttity;
            this.m_Priority = priority;
            this.m_Due = due;
            this.m_Done = done;
            this.m_OdrStatus = status;
        }



        public void UpdateOrderInfo(int cur_proc, int cur_stop, int mach, int type)
        {
            cur_process_id = cur_proc;
            cur_stop_id = cur_stop;
            all_machines.Add(mach);
            all_types.Add(type);
        }

        public void AddRoute(CRoute  pRut)
        {
            CRoute pRoute = new CRoute();
            pRoute.ID = pRut.ID;
            pRoute.max_process_id = pRut.max_process_id;
            for(int i = 0; i < pRut.process_ids.Count;i++)
            {
                pRoute.process_ids.Add(pRut.process_ids[i]);
                pRoute.sub_process_ids.Add(pRut.sub_process_ids[i]);
                pRoute.type_ids.Add(pRut.type_ids[i]);
                pRoute.machine_ids.Add(pRut.machine_ids[i]);
                pRoute.code_ids.Add(pRut.code_ids[i]);
                pRoute.oper_times.Add(pRut.oper_times[i]);
                pRoute.unique_ids.Add(pRut.unique_ids[i]);
                pRoute.process_alternative_machines.Add(pRut.process_alternative_machines[i]);
            }
            max_process_id = pRoute.max_process_id;
        }

        public void ReleaseRoute()
        {
            pRoute = null;

        }

        // Format: 001;002;007;, Event: 1 start; 2: end 
        public void StartAProcess(int m_id, long t) //start
        {
            run_machines.Add(m_id);
            run_events.Add(1);
            run_sim_clock.Add(t);
        }

        public void FinishAProcess(int m_id, long t) //finish
        {
            run_machines.Add(m_id);
            run_events.Add(2);
            run_sim_clock.Add(t);
        }
        public void StockerOuts(int m_id, long t) //取料
        {
            run_machines.Add(m_id);
            run_events.Add(3);
            run_sim_clock.Add(t);
        }

        public void StockerIn(int m_id, long t) //放料
        {
            run_machines.Add(m_id);
            run_events.Add(4);
            run_sim_clock.Add(t);
        }

    }

    


}
