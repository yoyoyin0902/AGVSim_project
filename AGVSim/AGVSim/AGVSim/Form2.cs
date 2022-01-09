using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AGVSim
{
    public partial class Form2 : Form
    {
        List<COrder> Order_List = new List<COrder>();
        int read_Product_ID;
        int read_Quantity;
        int read_Priority;
        int read_Due;
        int read_Done;
        int read_Status;
        int item;
   


        //public List<COrder> ReceiveOrder = new List<COrder>();
        public Form2()
        {
            InitializeComponent();
        }



        private void myGroup1_Enter(object sender, EventArgs e)
        {

        }

        private void Done_TextChanged(object sender, EventArgs e)
        {

        }

        private void myGroup2_Enter(object sender, EventArgs e)
        {
            
        }

        private void MessageShow_TextChanged(object sender, EventArgs e)
        {
            textBox_Product_ID.Show();
        }

        private void Product_ID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlserver.Delete_Order(item);
            int selectedIndex = dataGridView1.SelectedRows[0].Index;
            dataGridView1.Rows.RemoveAt(selectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //sqlserver.Add_Order();
            label1.Text = "Your Order is ........ \n" +
                          "Product_ID :" + textBox_Product_ID.Text + " " + "Quantity :" + textBox_Quantity.Text + " " +
                          "Priority :" + comboBox1.Text + " " + "Due :" + textBox_Due.Text + " " +
                          "Done :" + textBox_Done.Text + " " + "Status :" + textBox_Status.Text + "\n";

            //SQL
            int Product_ID = int.Parse(textBox_Product_ID.Text);
            int Quantity = int.Parse(textBox_Quantity.Text);
            int Priority = int.Parse(comboBox1.Text);
            int Due = int.Parse(textBox_Due.Text);
            int Done = int.Parse(textBox_Done.Text);
            int Status = int.Parse(textBox_Status.Text);
            COrder newOrder = new COrder(Product_ID, Quantity, Priority, Due, Done, Status);

            sqlserver.Add_Order(ref newOrder);
            //sqlserver.Update_Order();
            dataGridView1.Rows.Clear();
            sqlserver.Read_Order(ref Order_List);
            for (int i = 0; i < Order_List.Count; i++)
            {
                read_Product_ID = Order_List[i].m_Product_ID;
                read_Quantity = Order_List[i].m_Quantity;
                read_Priority = Order_List[i].m_Priority;
                read_Due = Order_List[i].m_Due;
                read_Done = Order_List[i].m_Done;
                read_Status = Order_List[i].m_OdrStatus;
                //label2.Text = "ID: "+read_Product_ID + "Qty: " + read_Quantity + "Prty: " + read_Priority + "Due: " + read_Due + "Done: " + read_Done + "Status: " + read_Status + "\n";
                //listBox2.Items.Add(String.Format(stdDetails_number, read_Product_ID, read_Quantity, read_Priority, read_Due, read_Done, read_Status));
                dataGridView1.Rows.Add(read_Product_ID, read_Quantity, read_Priority, read_Due, read_Done, read_Status);
                //listBox2.Items.Add( read_Product_ID + " " + read_Quantity + " " + read_Priority + " " + read_Due + " " + read_Done + " " + read_Status);
            }
            Order_List.Clear();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox_Product_ID.Clear();
            textBox_Quantity.Clear();
            comboBox1.Text = null;
            textBox_Due.Clear();
            textBox_Status.Clear();
            textBox_Done.Clear();
            label1.Text = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Form1 OrderPage1 = new Form1();
            //OrderPage1.Show();
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

        }



        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            sqlserver.Read_Order(ref Order_List);
            for (int i = 0; i < Order_List.Count; i++)
            {
                read_Product_ID = Order_List[i].m_Product_ID;
                read_Quantity = Order_List[i].m_Quantity;
                read_Priority = Order_List[i].m_Priority;
                read_Due = Order_List[i].m_Due;
                read_Done = Order_List[i].m_Done;
                read_Status = Order_List[i].m_OdrStatus;
                //label2.Text = "ID: "+read_Product_ID + "Qty: " + read_Quantity + "Prty: " + read_Priority + "Due: " + read_Due + "Done: " + read_Done + "Status: " + read_Status + "\n";
                //listBox2.Items.Add(String.Format(stdDetails_number, read_Product_ID, read_Quantity, read_Priority, read_Due, read_Done, read_Status));
                dataGridView1.Rows.Add(read_Product_ID, read_Quantity, read_Priority, read_Due, read_Done, read_Status);
                //listBox2.Items.Add( read_Product_ID + " " + read_Quantity + " " + read_Priority + " " + read_Due + " " + read_Done + " " + read_Status);
            }
            Order_List.Clear();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            item = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            COrder delOrder = new COrder();
            delOrder.delet_Product_ID = item;
            Console.WriteLine(item);

            

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
