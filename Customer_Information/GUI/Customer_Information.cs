using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Customer_Information.DAL;
using Customer_Information.BLL;
using Customer_Information.Validation;
using System.IO;

namespace Customer_Information
{
    public partial class Customer_Information : Form
    {
        List<Customer> listC = new List<Customer>();

        public Customer_Information()
        {
            InitializeComponent();
            if ((File.Exists(@"\Customers.dat")))
            {
                buttonListCustomers.Enabled = false;
                buttonUpdate.Enabled = false;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show
                (
                "Are you sure you want to exit?",
                 "Question", MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question
                 );

            if (DialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void buttonAddToList_Click(object sender, EventArgs e)
        {
            Customer aCustomer = new Customer();

            if (Validator.IsValidId(textBoxCustomerid) &&
                Validator.isValidName(textBoxFirstName) &&
                Validator.isValidName(textBoxLastName) &&
                Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerid.Text))
                && Validator.IsEmpty(textBoxFirstName.Text)
                && Validator.IsEmpty(textBoxLastName.Text)
                && Validator.IsEmpty(maskedTextBoxPhoneNumber)
                )
            {
                aCustomer.CustomerId = Convert.ToInt32(textBoxCustomerid.Text);
                aCustomer.FirstName = textBoxFirstName.Text;
                aCustomer.LastName = textBoxLastName.Text;
                aCustomer.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                //add to the list
                listC.Add(aCustomer);

                MessageBox.Show("Customer info has been added to the list",
                    "Confirmation", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (buttonListCustomers.Enabled == false && buttonUpdate.Enabled == false)
                {
                    buttonListCustomers.Enabled = true;
                    buttonUpdate.Enabled = true;
                }
                ClearAll();
            }
        }

        private void buttonListCustomers_Click(object sender, EventArgs e)
        {
            CustomerDA.ListCustomers(listViewCustomer);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Validator.IsValidId(textBoxCustomerid) &&
                Validator.isValidName(textBoxFirstName) &&
                Validator.isValidName(textBoxLastName) &&
                Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerid.Text))
                && Validator.IsEmpty(textBoxFirstName.Text)
                && Validator.IsEmpty(textBoxLastName.Text)
                && Validator.IsEmpty(maskedTextBoxPhoneNumber)
            )
            {
                //save list of customer
                List<Customer> listC = CustomerDA.ListCustomers();

                //save from an Object
                Customer aCustomer = new Customer();
                aCustomer.CustomerId = Convert.ToInt32(textBoxCustomerid.Text);
                aCustomer.FirstName = textBoxFirstName.Text;
                aCustomer.LastName = textBoxLastName.Text;
                aCustomer.PhoneNumber = maskedTextBoxPhoneNumber.Text;

                CustomerDA.Save(aCustomer);

                if (buttonListCustomers.Enabled == false && buttonUpdate.Enabled == false)
                {
                    buttonListCustomers.Enabled = true;
                    buttonUpdate.Enabled = true;
                }
                ClearAll();
            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int choice = comboBoxChoice.SelectedIndex;

            if(Validator.IsEmpty(textBoxInput.Text))
            {
                switch (choice)
                {
                    case -1: //-1 if the user don't select any search option
                        MessageBox.Show("Select one search option", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 0:
                        Customer cust = CustomerDA.Search(Convert.ToInt32(textBoxInput.Text));
                        if (cust != null)
                        {
                            textBoxCustomerid.Text = cust.CustomerId.ToString();
                            textBoxFirstName.Text = cust.FirstName.ToString();
                            textBoxLastName.Text = cust.LastName.ToString();
                            maskedTextBoxPhoneNumber.Text = cust.PhoneNumber.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Customer not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    default: //if the user don't select any search option on the search button
                        break;
                }
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (Validator.IsValidId(textBoxCustomerid) &&
                Validator.isValidName(textBoxFirstName) &&
                Validator.isValidName(textBoxLastName) &&
                Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerid.Text))
                && Validator.IsEmpty(textBoxFirstName.Text)
                && Validator.IsEmpty(textBoxLastName.Text)
                && Validator.IsEmpty(maskedTextBoxPhoneNumber)
)
            {
                CustomerDA.Delete(Convert.ToInt32(textBoxCustomerid.Text));

                MessageBox.Show("Customer record deleted sucessfully", "Delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (Validator.IsValidId(textBoxCustomerid) &&
                Validator.isValidName(textBoxFirstName) &&
                Validator.isValidName(textBoxLastName) &&
                Validator.IsUniqueID(listC, Convert.ToInt32(textBoxCustomerid.Text))
                && Validator.IsEmpty(textBoxFirstName.Text)
                && Validator.IsEmpty(textBoxLastName.Text)
                && Validator.IsEmpty(maskedTextBoxPhoneNumber)
            )
            {
                Customer cust = new Customer();
                cust.CustomerId = Convert.ToInt32(textBoxCustomerid.Text);
                cust.FirstName = textBoxFirstName.Text;
                cust.LastName = textBoxLastName.Text;
                cust.LastName = maskedTextBoxPhoneNumber.Text;

                DialogResult answer = MessageBox.Show("Are you sure you want to update this customer?", "Customer Update",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }
        }

        private void Customer_Information_Load(object sender, EventArgs e)
        {
                
        }

        private void ClearAll()
        {
            textBoxCustomerid.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            maskedTextBoxPhoneNumber.Clear();
            textBoxCustomerid.Focus();
        }

        private void comboBoxChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            int choice = comboBoxChoice.SelectedIndex;

            switch (choice)
            {
                case 0:
                    labelInfo.Text = "Enter the Customer ID";
                    textBoxInput.Focus();
                    break;
                case 1:
                    labelInfo.Text = "Enter the First Name";
                    textBoxInput.Focus();
                    break;
                case 2:
                    labelInfo.Text = "Enter the Last Name";
                    textBoxInput.Focus();
                    break;
                default:
                    break;

            }
        }
    }
}
