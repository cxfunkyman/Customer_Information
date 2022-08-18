using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer_Information.BLL;
using System.Windows.Forms;
using System.IO;

namespace Customer_Information.DAL
{

    //STATIC WILL AVOID TO CREATE INS
   public static  class CustomerDA
    {
        //Create the path for two files one for save another for a temporary save
        private static string filePath = Application.StartupPath + @"\Customers.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";

        //Save action on filePath
        public static void Save(Customer cust)
        { 
            StreamWriter sWriter = new StreamWriter(filePath,true);
            sWriter.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Custormer Data has been saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //List all the entries in the listbox
        public static void ListCustomers(ListView listViewCustomers)
        {
            StreamReader sreader = new StreamReader(filePath);
            listViewCustomers.Items.Clear();
            string line = sreader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);

                listViewCustomers.Items.Add(item);
                line = sreader.ReadLine();
            }
            sreader.Close();
        }
        public static List  <Customer> ListCustomers()
        {
            List<Customer> listC = new List<Customer>();
            
            // Step 1: Create an object of type StreamReader
            StreamReader sReader = new StreamReader(filePath);
            
            //Step2:
            //1- Read the file until the end of the file
            string line = sReader.ReadLine();

            //2- Read line by lie
            while (line != null)
            {
                //3- Split the line into array of string based on separator
                string[] fields = line.Split(',');
                
                //4- Create an object of type Customer
                Customer cust = new Customer();
                
                //5- Store data in the Objhect Customer
                cust.CustomerId = Convert.ToInt32(fields[0]);
                cust.FirstName = fields[1];
                cust.LastName = fields[2];
                cust.PhoneNumber = fields[3];
               
                //6- Add the Object to the listC
                listC.Add(cust);
                
                //7- Read next line
                line = sReader.ReadLine();
            }
            //8- Close the file!! VERY IMPORTANT or else is gonna crash!!
            sReader.Close();
            return listC;
        }
        // Search by Id in the document
        public static Customer Search(int custId)
        {
            Customer cust = new Customer();
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');                
               
                if (custId == Convert.ToInt32(fields[0]))
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];

                    sReader.Close();
                    return cust;
                }
                line = sReader.ReadLine();
            }
            sReader.Close();
            MessageBox.Show("Id not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
        // Search and delete by Id an entry in the document
        public static void Delete(int custId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);

            while(line != null)
            {
                string[] fields = line.Split(',');

                if((custId) != (Convert.ToInt32(fields[0])))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                    //sWriter.WriteLine(line);
                    //sWriter.WriteLine(fileds.ToString());
                }
                line=sReader.ReadLine();
            }
            sWriter.Close();
            sReader.Close();

            //Delete the "old file" = Customer.dat
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
        }
        public static void Update(Customer cust)
        {
            StreamReader sReader = new StreamReader(filePath);            
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');

                if (Convert.ToInt32(fields[0]) != cust.CustomerId)
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                    //sWriter.WriteLine(line);
                    //sWriter.WriteLine(fileds.ToString());
                }
                line = sReader.ReadLine();
            }
            sWriter.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
           
            sWriter.Close();
            sReader.Close();

            //Delete the "old file" = Customer.dat
            File.Delete(filePath);
            File.Move(fileTemp, filePath);
            MessageBox.Show("Custormer Data has been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


    }
}
