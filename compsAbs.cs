using System;
using System.Collections.Generic;

namespace OOP_in_Csharp
{

    //Abstract class
    public abstract class Device
    {
        public Device(string name, string make)
        {
            _name = name;
            _make = make;
        }

        protected string _name;
        protected string _make;
        public abstract void StartComputer();
        public abstract void ShutDown();

    }

    public class Computer : Device
    {
        public Computer(string name, string make, string operatingSystem, bool switchedOn) : base(name, make)
        {
            //this.Name = name;
            //this.Ipadress = ipadress;
            //this.Make = make;
            this.OperatingSystem = operatingSystem;
            this.SwitchedOn = switchedOn;

            if (switchedOn)
            {
                switchedOnCounter++;
            }

            compCounter++;
        }

        //public Computer()
        //{
        //    compCounter++;
        //}

        // private member variables
        //protected string _name;
        protected string _ipadress;
        protected bool _switchedOn = false;
        //protected string _make;
        protected string _operatingSystem;

        protected static int CompInNet = 10; //To generate the IpAddress

        protected static int compCounter = 0; //Counter for computers in the network (switched on or not).
        protected static int switchedOnCounter = 0; //Counter for computers that are switched on

        public static int CountComps()
        {
            return compCounter;
        }

        public static int switchedComps()
        {
            return switchedOnCounter;
        }


        public string GetIpAddress()
        {
            ++CompInNet; //Increase the ip in 1. Initial value is 10.
            string address = "10.0.0." + CompInNet.ToString();
            return address;
        }

        //Virtual because this method can be override in the other classes
        public override void StartComputer()
        {
            if (this.SwitchedOn == false)
            {
                this.SwitchedOn = true;
                switchedOnCounter++;
                this.Ipadress = GetIpAddress();
            }
            Console.WriteLine("The comp {0} is starting...", this.Ipadress);
        }

        public override void ShutDown()
        {
            if (this.SwitchedOn == true)
            {
                this.SwitchedOn = false;
                switchedOnCounter--;
            }

            Console.WriteLine("{0} is shutting Down...", this.Ipadress);
        }

        public string IsSwitchedOn()
        {
            if (this.SwitchedOn)
            {
                return "Switched ON";
            }
            else
            {
                return "Switched OFF";
            }
        }


        public static void PingToComputer(List<Computer> net, Computer pingFrom, string pingTo)
        {
            Random rnd = new Random();
            float answer;
            Computer myComp = pingFrom;
            bool found = false;
            Console.WriteLine("\nTrying to ping from the machine {0}({1}) to {2}.", myComp.Name, myComp.Ipadress, pingTo);
            Console.WriteLine("--------------------------------------------------------------------");
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
        } //end of PingToComputer method


        // public properties
        public string Ipadress
        {
            get { return _ipadress; }
            set { _ipadress = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string OperatingSystem
        {
            get { return _operatingSystem; }
            set { _operatingSystem = value; }
        }

        public bool SwitchedOn
        {
            get { return _switchedOn; }
            set { _switchedOn = value; }
        }




        public static void ShowComputers(List<Computer> net)
        {

            foreach (Computer comp in net)
            {
                if (comp.SwitchedOn)
                {
                    Type type = comp.GetType();
                    string stringType = type.ToString();
                    Console.Write("\n{0}\t{1}\t{2}\t {3}\t",
                                  comp.Name, comp.Ipadress, comp.OperatingSystem, comp.Make, comp.IsSwitchedOn());
                    int pos = stringType.IndexOf('.');

                    if (stringType.Substring(pos + 1, 4) == "Serv")
                    {
                        Server serv = (Server)comp;
                        Console.WriteLine("\t{0}", serv.Destination);
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                }
            }
        }//End showcomputers



    }


    public class Server : Computer
    {
        public Server(string name, string make, string osystem, bool switched, string dest, string ip) : base(name, make, osystem, switched)
        {
            this.Destination = dest;
            this.Ipadress = ip;

        }

        // private member variables
        protected string _destination;

        // public properties
        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        public override void StartComputer()
        {
            if (SwitchedOn == false)
            {
                this.SwitchedOn = true;
                switchedOnCounter++;
                //this.Ipadress = GetIpAddress();
                Console.WriteLine("The SERVER {0} is starting...", this.Ipadress);
            }
        }

        public override void ShutDown()
        {
            if (this.SwitchedOn)
            {
                Console.WriteLine("***** You are tryingg to shut down this server.\nAre you sure? y/n *****");
                string confirm = Console.ReadLine();
                if (confirm == "y")
                {
                    this.SwitchedOn = false;
                    switchedOnCounter--;
                    Console.WriteLine("{0} is shutting Down...", this.Ipadress);
                }
            }
            else
            {
                Console.WriteLine("******* You are trying to shut down a server that is already shutted Down **********");
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {

            List<Computer> network = new List<Computer>();

            //Creating new "server"
            Server s1 = new Server("s1", "Dell", "Linux", false, "WEB Server", "10.0.0.1");
            Server s2 = new Server("s2", "IBM", "Linux", false, "DHCP", "10.0.0.2");

            //Creating the object myComp using the default constructer
            Computer myComp = new Computer("comp1", "Dell", "Windows", false);

            //Creating the object mySecondComp using the defined constructer
            Computer mySecondComp = new Computer("comp2", "Asus", "Linux", false);

            //Creating the object myThirdComputer using the defined constructer
            Computer myThirdComp = new Computer("comp3", "Samsung", "Android", false);

            //Insert computers into List
            network.Add(myComp);
            network.Add(mySecondComp);
            network.Add(myThirdComp);
            network.Add(s1);
            network.Add(s2);

            Console.WriteLine("\n---------------------------------------\n");


            for (int i = 0; i < network.Count; i++)
            {
                network[i].StartComputer();
            }

            //Call to showComputer method
            Computer.ShowComputers(network);

            Console.WriteLine("\n---------------------------------------\n");

            //string ip0 = "10.0.0.0";

            ////Ping with mySecondComputer
            //network[0].PingNetwork(network[1].Ipadress, network);
            ////Ping with my ThirdComputer
            //network[0].PingNetwork(myThirdComp.Ipadress, network);
            ////Pingg with ip0, define upside.
            //network[0].PingNetwork(ip0, network);

            Computer.PingToComputer(network, myComp, "10.0.0.13");
            Computer.PingToComputer(network, myComp, "10.0.0.0");

            Console.WriteLine("\n---------------------------------------\n");

            network[1].ShutDown();
            network[2].ShutDown();
            s1.ShutDown();

            Console.WriteLine("\n---------------------------------------");

            //To see after shutdown/switch on the state of our computers
            Console.WriteLine("\n{0} is {1}" +
"\n{2} is {3}" +
"\n{4} is {5}\n",
myComp.Ipadress, myComp.IsSwitchedOn(), mySecondComp.Ipadress, mySecondComp.IsSwitchedOn(), myThirdComp.Ipadress, myThirdComp.IsSwitchedOn());


            Console.WriteLine("---------------------------------------");

            int compsInNetwork = Computer.CountComps();
            int compsSwitchedOn = Computer.switchedComps();

            Computer.ShowComputers(network);

            Console.WriteLine("\nWe have {0} computers in our network.",
            compsInNetwork);

            Console.WriteLine("We have {0} computers Switched On.",
            compsSwitchedOn);

        }
    }

}