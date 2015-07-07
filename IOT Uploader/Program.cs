using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using System.Threading;
using System.IO;

namespace IOT_Uploader
{
    class Program
    {
        private static StringBuilder tString = new StringBuilder();

     

        private static SerialPort mySerialPort;


        static string eventHubName = "ospicon-eh";
        static string connectionString = "Endpoint=sb://ospicon-eh-ns.servicebus.windows.net/;SharedAccessKeyName=androidSender;SharedAccessKey=enter your key";

         static  void Main(string[] args)
        {

            Random random = new Random();

            while (true) {

      
                tString.Clear();
                tString.Append("{");

                tString.Append("\"MatAccID\":");
                tString.Append("\"12345\"");
                tString.Append(",");

                tString.Append("\"displayname\":");
                tString.Append("\"ospicon sensor\"");
                tString.Append(",");

                tString.Append("\"location\":");
                tString.Append("\"consumer\"");
                tString.Append(",");

                tString.Append("\"organization\":");
                tString.Append("\"ospicon\"");
                tString.Append(",");

                tString.Append("\"guid\":");
                tString.Append("\"62X74059-A444-4797-8A7E-526C3EF9D64B\"");
                tString.Append(",");

                tString.Append("\"measurename\":");
                tString.Append("\"heartrate\"");
                tString.Append(",");

                tString.Append("\"unitofmeasure\":");
                tString.Append("\"beats per second\"");
                tString.Append(",");

                tString.Append("\"timecreated\":");
                tString.Append("\""+ DateTime.UtcNow.ToString("o") + "\"");
                tString.Append(",");

                tString.Append("\"value\":");
                tString.Append(random.Next(70,190));

                tString.Append("}");
                
                upload(tString.ToString());
                Thread.Sleep(2000);
                tString.Clear();
                tString.Append("{");

                tString.Append("\"MatAccID\":");
                tString.Append("\"12345\"");
                tString.Append(","); 

                tString.Append("\"displayname\":");
                tString.Append("\"ospicon sensor\"");
                tString.Append(",");

                tString.Append("\"location\":");
                tString.Append("\"consumer\"");
                tString.Append(",");

                tString.Append("\"organization\":");
                tString.Append("\"ospicon\"");
                tString.Append(",");

                tString.Append("\"guid\":");
                tString.Append("\"62X74059-A444-4797-8A7E-526C3EF9D64B\"");
                tString.Append(",");

                tString.Append("\"measurename\":");
                tString.Append("\"room temp\"");
                tString.Append(",");

                tString.Append("\"unitofmeasure\":");
                tString.Append("\"C\"");
                tString.Append(",");

                tString.Append("\"timecreated\":");
                tString.Append("\"" + DateTime.UtcNow.ToString("o") + "\"");
                tString.Append(",");

                tString.Append("\"value\":");
                tString.Append(random.Next(20, 30));

                tString.Append("}");

                upload(tString.ToString());
                Thread.Sleep(2000);
                tString.Clear();
                tString.Append("{");

                tString.Append("\"MatAccID\":");
                tString.Append("\"12345\"");
                tString.Append(",");

                tString.Append("\"displayname\":");
                tString.Append("\"ospicon sensor\"");
                tString.Append(",");

                tString.Append("\"location\":");
                tString.Append("\"consumer\"");
                tString.Append(",");

                tString.Append("\"organization\":");
                tString.Append("\"ospicon\"");
                tString.Append(",");

                tString.Append("\"guid\":");
                tString.Append("\"62X74059-A444-4797-8A7E-526C3EF9D64B\"");
                tString.Append(",");

                tString.Append("\"measurename\":");
                tString.Append("\"breathrate\"");
                tString.Append(",");

                tString.Append("\"unitofmeasure\":");
                tString.Append("\"breath per second\"");
                tString.Append(",");

                tString.Append("\"timecreated\":");
                tString.Append("\"" + DateTime.UtcNow.ToString("o") + "\"");
                tString.Append(",");

                tString.Append("\"value\":");
                tString.Append(random.Next(25, 40));

                tString.Append("}");

                upload(tString.ToString());
                Thread.Sleep(2000);
                tString.Clear();
                tString.Append("{");

                tString.Append("\"MatAccID\":");
                tString.Append("\"12345\"");
                tString.Append(",");

                tString.Append("\"displayname\":");
                tString.Append("\"ospicon sensor\"");
                tString.Append(",");

                tString.Append("\"location\":");
                tString.Append("\"consumer\"");
                tString.Append(",");

                tString.Append("\"organization\":");
                tString.Append("\"ospicon\"");
                tString.Append(",");

                tString.Append("\"guid\":");
                tString.Append("\"62X74059-A444-4797-8A7E-526C3EF9D64B\"");
                tString.Append(",");

                tString.Append("\"measurename\":");
                tString.Append("\"baby status\"");
                tString.Append(",");

                tString.Append("\"unitofmeasure\":");
                tString.Append("\"status\"");
                tString.Append(",");

                tString.Append("\"timecreated\":");
                tString.Append("\"" + DateTime.UtcNow.ToString("o") + "\"");
                tString.Append(",");

                tString.Append("\"value\":");
                tString.Append(random.Next(1, 10));

                tString.Append("}");

                upload(tString.ToString());
                Thread.Sleep(2000);
            }
        }

        static void savetotext(String message)
        {


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!File.Exists(path + @"\sample.txt"))
            {
                Console.WriteLine("{0} > Saving message: {1}", DateTime.Now, message);
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path + @"\sample.txt"))
                {
                    sw.WriteLine(message);
                }
            }
            else
            {

                using (StreamWriter sw = new StreamWriter(path + @"\sample.txt", true))
                {
                    Console.WriteLine("{0} > Saving message: {1}", DateTime.Now, message);
                    sw.WriteLine(message);
                }
            }

        }
        static async Task upload(String message)
        {
            savetotext(message);
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);
          
                try
                {
                   
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now.ToString(), message);
                    await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now.ToString(), exception.Message);
                    Console.ResetColor();
                }

               
          
        }
    }
}
