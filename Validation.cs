using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelExpertWeb.App_Code
{
    public class Validation
    {
        private static string title = "Entry Error";

        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public static bool IsPresent(TextBox textBox)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(textBox.Tag + " is a required field.", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsDecimal(TextBox textBox)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be a decimal value.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(TextBox textBox)
        {
            int number = 0;
            if (Int32.TryParse(textBox.Text, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox.Tag + " must be an integer.", Title);
                textBox.Focus();
                return false;
            }
        }

        public static bool IsWithinRange(TextBox textBox, decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(textBox.Tag + " must be between " + min
                    + " and " + max + ".", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }
        public static bool IsPositiveDecimal(TextBox textBox, string name)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number <= 0)
            {
                MessageBox.Show(name + " must be positive");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsPositiveInt(TextBox textBox, string name)
        {
            int number = Convert.ToInt32(textBox.Text);
            if (number <= 0)
            {
                MessageBox.Show(name + " must be positive");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsPhoneNumber(string number)
        {
            string phone = Convert.ToString(textBox.Text);
            if (IsPresent(TextBox textBox))
            {
                MessageBox.Show(phone, @"^\s*(?:\+? (\d{ 1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})\s*$ ").Success;
            }
            else
            {
                MessageBox.Show(textBoxName + " must be in the format of +xx (xxx)xxx-xxxx");
                textBox.Focus();
                return false;

            }//^\s*                 Line start, match any whitespaces at the beginning if any.
             //(?:\+? (\d{ 1,3}))?  GROUP 1: The country code. Optional.
             //[-. (]*              Allow certain non numeric characters that may appear between the Country Code and the Area Code.
             //(\d{3})              GROUP 2: The Area Code. Required.
             //[-. )]*              Allow certain non numeric characters that may appear between the Area Code and the Exchange number.
             //(\d{3})              GROUP 3: The Exchange number. Required.
             //[-. ]*               Allow certain non numeric characters that may appear between the Exchange number and the Subscriber number.
             //(\d{4})              Group 4: The Subscriber Number. Required.
             //\s*$                 Match any ending whitespaces if any and the end of string.
        }
        public static bool IsEmail(string number)
        {
            string email = Convert.ToString(textBox.Text);
            if (IsPresent(TextBox textBox))
            {
                MessageBox.Show(email, @"/^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/").Success;
            }
            else
            {
                MessageBox.Show(textBoxName + " must be in the format of xxxxx@xxxxx.xxx or @xxxxx.xxx.xx");
                textBox.Focus();
                return false;
            }
        }

            public static bool IsPostalCode(string number)
            {
                string code = Convert.ToString(textBox.Text);
                if (IsPresent(TextBox textBox))
                {
                    MessageBox.Show(code, @"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} *\d{1}[A-Z]{1}\d{1}$)").Success;
                }
                else
                {
                    MessageBox.Show(textBoxName + " must be in the format of xNx xNx or xxxxx");
                    textBox.Focus();
                    return false;
                }
            }


        //THESE ARE METHODS WITH THE SAME NAME BUT WITH TRY/CATCH AND IF /ELSE

        private static string title = "Entry Error";

        /// <summary>
        /// The title that will appear in dialog boxes.
        /// </summary>
        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// Checks whether the user entered data into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered data.</returns>
        public static bool IsPresent(Control control)
        {
            if (control.GetType().ToString() == "System.Windows.Forms.TextBox")
            {
                TextBox textBox = (TextBox)control;
                if (textBox.Text == "")
                {
                    MessageBox.Show(textBox.Tag + " is a required field.", Title);
                    textBox.Focus();
                    return false;
                }
            }
            else if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                ComboBox comboBox = (ComboBox)control;
                if (comboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(comboBox.Tag + " is a required field.", "Entry Error");
                    comboBox.Focus();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Checks whether the user entered a decimal value into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered a decimal value.</returns>
        public static bool IsDecimal(TextBox textBox)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be a decimal number.", Title);
                textBox.Focus();
                return false;
            }
        }

        /// <summary>
        /// Checks whether the user entered an int value into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <returns>True if the user has entered an int value.</returns>
        public static bool IsInt32(TextBox textBox)
        {
            try
            {
                Convert.ToInt32(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(textBox.Tag + " must be an integer.", Title);
                textBox.Focus();
                return false;
            }
        }

        /// <summary>
        /// Checks whether the user entered a value within a specified range into a text box.
        /// </summary>
        /// <param name="textBox">The text box control to be validated.</param>
        /// <param name="min">The minimum value for the range.</param>
        /// <param name="max">The maximum value for the range.</param>
        /// <returns>True if the user has entered a value within the specified range.</returns>
        public static bool IsWithinRange(TextBox textBox, decimal min, decimal max)
        {
            decimal number = Convert.ToDecimal(textBox.Text);
            if (number < min || number > max)
            {
                MessageBox.Show(textBox.Tag + " must be between " + min
                    + " and " + max + ".", Title);
                textBox.Focus();
                return false;
            }
            return true;
        }
    }
}