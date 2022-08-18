using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Customer_Information.BLL;

namespace Customer_Information.Validation
{
    public static class Validator
    {
        // Validation
        public static bool IsValidId(string input)
        {
            int tempID;

            if (input.Length != 5)
            {
                MessageBox.Show("Invalid Customer ID, it must be a 5 digit number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            else if (!(Int32.TryParse(input, out tempID)))
            {
                MessageBox.Show("Invalid Customer ID, it must be numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }
        // is the same as IsValidId(string input) but instead of the data itself, we're taking the component
        public static bool IsValidId(TextBox text)
        {
            int tempID;

            if (text.TextLength != 5)
            {
                MessageBox.Show("Invalid Customer ID, it must be a 5 digit number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                text.Clear();
                text.Focus();
                return false;
            }
            else if (!(Int32.TryParse(text.Text, out tempID)))
            {
                MessageBox.Show("Invalid Customer ID, it must be numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                text.Clear();
                text.Focus();
                return false;
            }
            return true;
        }
        public static bool isValidName(TextBox text)
        {
            for(int i = 0; i < text.TextLength; i++)
            {
                if (char.IsDigit(text.Text[i]) || (char.IsWhiteSpace(text.Text, i)))
                {
                    MessageBox.Show("Invalid name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    text.Clear();
                    text.Focus();
                    return false;
                }
            }
            return true;
        }
        public static bool IsUniqueID(List<Customer> listC, int id)
        {
            foreach(Customer c in listC)
            {
                if(c.CustomerId == id)
                {
                    MessageBox.Show("Duplicate ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        public static bool IsEmpty(string text)
        {
            if(text == "")
            {
                    MessageBox.Show("Missing Info, blank data not accepted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
            }
            return true;
        }
        public static bool IsEmpty(MaskedTextBox text)
        {
            if ((!text.MaskCompleted))
            {
                MessageBox.Show("Wrong phone number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
