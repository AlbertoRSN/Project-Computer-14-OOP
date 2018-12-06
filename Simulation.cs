using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_in_Csharp
{
    public partial class Simulation : Form
    {
        public Simulation()
        {
            InitializeComponent();
        }
   
        //DECLARATION OF LIST 
        List<Computer> net = new List<Computer>();

        //------- BUTTON ON COMPUTER 1 -------
        private void button1_Click(object sender, EventArgs e)
        {
            if (button2.BackColor == Color.Red)
            {
                button2.BackColor = SystemColors.Control;
                button1.BackColor = Color.Green;
            }
            textBox3.Text = net[0].Name + " " + net[0].Ipadress;
            net[0].StartComputer();

        }

        //------- BUTTON OFF COMPUTER 1 -------
        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Green)
            {
                button2.BackColor = Color.Red;
                button1.BackColor = SystemColors.Control;
            }

            net[0].ShutDown();
            textBox3.Clear();
            

        }

        //------- BUTTON ON COMPUTER 2 -------
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.Green)
            {
                button3.BackColor = SystemColors.Control;
                button4.BackColor = Color.Red;
            }

            textBox6.Text = net[1].Name + " " + net[1].Ipadress;
            net[1].StartComputer();

        }

        //------- BUTTON OFF COMPUTER 2 -------
        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.BackColor == Color.Red)
            {
                button4.BackColor = SystemColors.Control;
                button3.BackColor = Color.Green;
            }

            net[1].ShutDown();
            textBox6.Clear();
        }
        //------- BUTTON ON COMPUTER 3 -------
        private void button8_Click(object sender, EventArgs e)
        {
            textBox5.Text = net[2].Name + " " + net[2].Ipadress;
            net[2].StartComputer();
        }

        //------- BUTTON OFF COMPUTER 3 -------
        private void button11_Click(object sender, EventArgs e)
        {
            net[2].ShutDown();
            textBox5.Clear();
        }

        //------- BUTTON ON COMPUTER 4 -------
        private void button9_Click(object sender, EventArgs e)
        {
            textBox8.Text = net[3].Name + " " + net[3].Ipadress;
            net[3].StartComputer();
        }

        //------- BUTTON OFF COMPUTER 4 -------
        private void button12_Click(object sender, EventArgs e)
        {
            net[3].ShutDown();
            textBox8.Clear();
        }


        
        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Text = net[4].Name + " " + net[4].Ipadress;
            net[4].StartComputer();
        }

        //------- BUTTON OFF SERVER 1 -------
        private void button10_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            net[4].ShutDown();
        }

        //------- BUTTON ON SERVER 2 -------
        private void button5_Click(object sender, EventArgs e)
        {
            textBox7.Text = net[5].Name + " " + net[5].Ipadress;
            net[5].StartComputer();
        }

        //------- BUTTON OFF SERVER 2 -------
        private void button6_Click(object sender, EventArgs e)
        {
            textBox7.Clear();
            net[5].ShutDown();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Counter to write only one time the list of computers
        int i = 0;

        //BUTTON TO LIST THE COMPUTERS
        private void button13_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            //listBox.DrawMode = DrawMode.OwnerDrawFixed;

            foreach (Computer comp in net)
            {
                    Type type = comp.GetType();
                    string stringType = type.ToString();

                    int pos = stringType.IndexOf('.');
                    if (stringType.Substring(pos + 1, 4) == "Serv")
                    {
                        Server serv = (Server)comp;
                        //PRINT IF TYPE IS SERVER THE NAME + IP + DESTINATION
                        string s = comp.Name + "\t" + comp.Ipadress + "\t    " + serv.Destination;
                        listBox.Items.Add(s);
                        
                        listBox.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);
                    }
                    else //IF TYPE IS COMPUTER PRINT NAME AND IP
                    {
                        listBox.Items.Add(comp.Name + "    " + comp.Ipadress);//comp.GetIpAddress().ToString());
                    }
            }
            
            i++;
        }

        //------- BUTTON TO SIMULATE PING -------
        private void button13_Click_1(object sender, EventArgs e)
        {
            //pingConsole consola donde voy a mostrar el mensaje
            //textBox1 texttbox ping from
            //textBox2 textBox ping to
            string pingFrom = textBox1.Text;
            string pingTo = textBox2.Text;

            //Check if computer pingFrom is on the ListBox
            bool found = false;

            foreach(Computer comp in net)
            {
                if(pingTo == comp.Ipadress)// && (comp.SwitchedOn == true))
                {
                    found = true;
                    break;
                }

                if (found)
                {
                    pingConsole.Text = "64 bytes from" + pingFrom + "  icmp_seq=1 ttl=64 time=0.1 ms";
                }
            }



        }

        public static void PingToComputer(List<Computer> net, Computer pingFrom, string pingTo)
        {
            Random rnd = new Random();
            float answer;
            Computer myComp = pingFrom;
            bool found = false;
            
            foreach (Computer comp in net)
            {
                if ((pingTo == comp.Ipadress) && (comp.SwitchedOn == true))
                {
                    found = true;
                    break;
                }
            } // the end of the loop
            if (found)
            {
                for (int i = 5; i < 15; i++)
                {
                    answer = (float)(rnd.Next(1, 100)) / 100;
                    Console.WriteLine("64 bytes from {0} icmp_seq={1} ttl=64 time={2} ms", myComp.Ipadress, i.ToString(), answer.ToString());
                }
            }
            else
            {
                //Console.WriteLine("Adress {0} not found !!! ", pingTo);
                Console.WriteLine("from {0}: icmp_seq13 Destination Host Unreachable", pingTo);
            }
        }



        //------- METHOD TO PAINT THE SERVERS -------
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            e.DrawBackground();
            Brush brush;

            if (e.Index == lb.Items.Count - 1)//El último elemento
                brush = Brushes.Red; 
            else if(e.Index == lb.Items.Count - 2)//El penúltimo elemento
                brush = Brushes.Red;
            else brush = Brushes.Black;

            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
        }


        //LIST OF COMPUTERS
        private void listConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

       

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void Simulation_Load(object sender, EventArgs e)
        {
            /*
            button2.BackColor = Color.Red;
            button10.BackColor = Color.Red;
            button4.BackColor = Color.Red;
            button11.BackColor = Color.Red;
            button6.BackColor = Color.Red;
            button12.BackColor = Color.Red;
            */

            //Creating new "servers"
            Server s1 = new Server("s1", "Dell", "Linux", false, "WEB Server", "10.0.0.1");
            Server s2 = new Server("s2", "IBM", "Linux", false, "DHCP", "10.0.0.2");

            //Creating the object myComp using the default constructer
            Computer myComp = new Computer("comp1", "Dell", "Windows", false);

            //Creating the object mySecondComp using the defined constructer
            Computer mySecondComp = new Computer("comp2", "Asus", "Linux", false);

            //Creating the object myThirdComputer using the defined constructer
            Computer myThirdComp = new Computer("comp3", "Samsung", "Android", false);

            //Creating the object fourComputer using the defined constructer
            Computer myFourthComputer = new Computer("comp4", "Apple", "Mac OS", false);

            //Generate every ip addresses
            myComp.GetIpAddress();
            mySecondComp.GetIpAddress();
            myThirdComp.GetIpAddress();
            myFourthComputer.GetIpAddress();

            //Insert computers into List
            net.Add(myComp);
            net.Add(mySecondComp);
            net.Add(myThirdComp);
            net.Add(myFourthComputer);
            net.Add(s1);
            net.Add(s2);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }



      





        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void pingConsole_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
