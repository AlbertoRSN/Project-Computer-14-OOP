using System;
using System.Collections.Generic;

namespace OOP_in_Csharp
{

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

