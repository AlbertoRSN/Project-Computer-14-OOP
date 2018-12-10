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
            if (button4.BackColor == Color.Red)
            {
                button4.BackColor = SystemColors.Control;
                button3.BackColor = Color.Green;
            }

            textBox6.Text = net[1].Name + " " + net[1].Ipadress;
            net[1].StartComputer();

        }

        //------- BUTTON OFF COMPUTER 2 -------
        private void button4_Click(object sender, EventArgs e)
        {
            if (button3.BackColor == Color.Green)
            {
                button4.BackColor = Color.Red;
                button3.BackColor = SystemColors.Control;
            }

            net[1].ShutDown();
            textBox6.Clear();
        }
        //------- BUTTON ON COMPUTER 3 -------
        private void button8_Click(object sender, EventArgs e)
        {
            if (button11.BackColor == Color.Red)
            {
                button11.BackColor = SystemColors.Control;
                button8.BackColor = Color.Green;
            }
            textBox5.Text = net[2].Name + " " + net[2].Ipadress;
            net[2].StartComputer();
        }

        //------- BUTTON OFF COMPUTER 3 -------
        private void button11_Click(object sender, EventArgs e)
        {
            if (button8.BackColor == Color.Green)
            {
                button11.BackColor = Color.Red;
                button8.BackColor = SystemColors.Control;
            }
            net[2].ShutDown();
            textBox5.Clear();
        }

        //------- BUTTON ON COMPUTER 4 -------
        private void button9_Click(object sender, EventArgs e)
        {
            if (button12.BackColor == Color.Red)
            {
                button12.BackColor = SystemColors.Control;
                button9.BackColor = Color.Green;
            }
            textBox8.Text = net[3].Name + " " + net[3].Ipadress;
            net[3].StartComputer();
        }

        //------- BUTTON OFF COMPUTER 4 -------
        private void button12_Click(object sender, EventArgs e)
        {
            if (button9.BackColor == Color.Green)
            {
                button12.BackColor = Color.Red;
                button9.BackColor = SystemColors.Control;
            }
            net[3].ShutDown();
            textBox8.Clear();
        }


        //------- BUTTON ON SERVER 1 -------
        private void button7_Click(object sender, EventArgs e)
        {
            if (button10.BackColor == Color.Red)
            {
                button10.BackColor = SystemColors.Control;
                button7.BackColor = Color.Green;
            }
            textBox4.Text = net[4].Name + " " + net[4].Ipadress;
            net[4].StartComputer();
        }

        //------- BUTTON OFF SERVER 1 -------
        private void button10_Click(object sender, EventArgs e)
        {
            if (button7.BackColor == Color.Green)
            {
                button10.BackColor = Color.Red;
                button7.BackColor = SystemColors.Control;
            }
            textBox4.Clear();
            net[4].ShutDown();
        }

        //------- BUTTON ON SERVER 2 -------
        private void button5_Click(object sender, EventArgs e)
        {
            if (button6.BackColor == Color.Red)
            {
                button6.BackColor = SystemColors.Control;
                button5.BackColor = Color.Green;
            }
            textBox7.Text = net[5].Name + " " + net[5].Ipadress;
            net[5].StartComputer();
        }

        //------- BUTTON OFF SERVER 2 -------
        private void button6_Click(object sender, EventArgs e)
        {
            if (button5.BackColor == Color.Green)
            {
                button6.BackColor = Color.Red;
                button5.BackColor = SystemColors.Control;
            }
            textBox7.Clear();
            net[5].ShutDown();
        }

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
                    string s = comp.Name + "\t" + comp.Ipadress + "\t    " + serv.Destination;// + "   " + comp.IsSwitchedOn();
                        listBox.Items.Add(s);
                        comboBox2.Items.Add(comp.Name);

                        listBox.DrawItem += new DrawItemEventHandler(listBox1_DrawItem);
                    }
                    else //IF TYPE IS COMPUTER PRINT NAME AND IP
                    {
                        listBox.Items.Add(comp.Name + "    " + comp.Ipadress + "    " + comp.IsSwitchedOn());//comp.GetIpAddress().ToString());
                        comboBox1.Items.Add(comp.Name);// + "    " + comp.Ipadress);
                        comboBox2.Items.Add(comp.Name);
                }
            }
            
            //i++;
        }

        //------- BUTTON TO SIMULATE PING -------
        private void button13_Click_1(object sender, EventArgs e)
        {
            Random rnd = new Random();
            float answer;
            //Computer myComp = pingFrom;
            bool found = false;
            string name1 = "";
            string name2 = "";

            pingConsole.Clear();

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please, first list computers and then, choose one of both lists");
                return;
            }
            else
            {
                //name of comp box 1
                name1 = comboBox1.SelectedItem.ToString();
                //name of comp box 2 (compX)
                name2 = comboBox2.SelectedItem.ToString();
            }

            pingConsole.Text += "\nTrying to ping from " + name1.ToUpper() +" to " + name2.ToUpper(); //myComp.Name, myComp.Ipadress, pingTo;

            Computer c1 = new Computer();
            Computer c2 = new Computer(); ;

            foreach(Computer comp in net)
            {
                if(comp.Name == name1)
                {
                    c1 = comp;
                }
                else if(comp.Name == name2)
                {
                    c2 = comp;
                }
            }

            if(c1.SwitchedOn == true && c2.SwitchedOn == true)
            {
                pingConsole.Text += "\n64 bytes from " + c1.Ipadress + " to " + c2.Ipadress + " icmp_seq=1 ttl=64 time=0.1 ms\n";
            }

            else
            {
                pingConsole.Text += "from " + c1.Ipadress + " to " + c2.Ipadress + ": icmp_seq13 Destination Host Unreachable";
            }

            /*
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
                   // Console.WriteLine("64 bytes from {0} icmp_seq={1} ttl=64 time={2} ms", myComp.Ipadress, i.ToString(), answer.ToString());
                }
            }
            else
            {
              
                //Console.WriteLine("from {0}: icmp_seq13 Destination Host Unreachable", pingTo);
            }
            */
            //-----------------------------
            /*
            string name1 = " ";
            string ip1 = "-";
            bool found = false;

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please, first list computers and then, choose one of both lists");
                return;
            }
            else
            {
                name1 = comboBox1.SelectedItem.ToString();
            }
           
            //Buscar por el nombre la ip
            foreach(Computer comp in net)
            {
                if(name1 == comp.Name && comp.SwitchedOn == true)
                {
                    ip1 = comp.Ipadress;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                pingConsole.Text += "\n64 bytes from " + ip1 + " icmp_seq=1 ttl=64 time=0.1 ms\n";
            }
            else
            {
                pingConsole.Text += "\n UNABLE TO FIND THE COMPUTER";
            }

            */
            //MessageBox.Show("Selected Item Text: " + myCom.Name + "\n";
        }

        //------- METHOD TO PAINT THE SERVERS -------
        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            e.DrawBackground();
            Brush brush;

            if (e.Index == lb.Items.Count - 1)//El último elemento
                brush = Brushes.Red;
            else if (e.Index == lb.Items.Count - 2)//El penúltimo elemento
                brush = Brushes.Red;
            else brush = Brushes.Black;

            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, brush, e.Bounds);
        }

        private void Simulation_Load(object sender, EventArgs e)
        {
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

        //--------------- MORE BUTTONS/PANELS ---------------------
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

    }
}



//ALTERNATIVE TO METHOD PING

/*
private void button13_Click_1(object sender, EventArgs e)
{
    //pingConsole consola donde voy a mostrar el mensaje
    //textBox1 texttbox ping from
    //textBox2 textBox ping to
    string pingFrom = textBox1.Text;
    string pingTo = textBox2.Text;

    //pingConsole.Text += pingTo;

    //Check if computer pingFrom is on the ListBox
    bool found = false;

    foreach(Computer comp in net)
    {
        /*
        if(pingTo == comp.Ipadress)// && (comp.SwitchedOn == true))
        {
            found = true;
            break;
        }*/

//pingConsole.Text += pingTo.Length.ToString() + " ";
//pingConsole.Text += comp.Ipadress + " ";
//pingConsole.Text += comp.Ipadress.Length.ToString();
//pingConsole.Text += found;

/*
if (pingTo.Substring(0, 8) == comp.Ipadress.Substring(0, 8) && pingTo == comp.Ipadress)
{
    found = true;
    break;
}
else
{
    pingConsole.Text += "\ndifferent";
}


if (found)
{
    pingConsole.Text += "64 bytes from" + pingFrom + "  icmp_seq=1 ttl=64 time=0.1 ms";
}
}

}*/
