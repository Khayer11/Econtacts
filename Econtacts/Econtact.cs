using Econtacts.eContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Econtacts
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();

        private void Econtact_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields.
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;

            //inserting data into database using the method created
            bool success = c.Insert(c);
            if (success == true)
            {
                // Successfully Inserted
                MessageBox.Show("New Contact Successfully Inserted");
                //call the clear method here
                Clear();
            }
            else
            {
                //failed to add contact
                MessageBox.Show("Failed to add New Contact. Try Again");
            }
            // Load data in data gridview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // Method to Clear Fields

        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtboxContactNumber.Text = "";
            txtboxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            // Get the data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtboxContactNumber.Text;
            c.Address = txtboxAddress.Text;
            c.Gender = cmbGender.Text;
            // Update in Data in Database
            bool success = c.Update(c);
            if (success == true)
            {
                //Update Successfully
                MessageBox.Show("Contact has been successfully updated");
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // call clear method 
                Clear();
            }
            else
            {
                //FAILED TO UPDATE
                MessageBox.Show("Failed to Update Contact. Try Again");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it to the textboxes respectively
            // identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtboxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtboxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();


        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            // call clear method 
            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if (success == true)
            {
                // successfully deleted
                MessageBox.Show("Contact has been deleted successfully.");

            }
            else
            {
                //failed to delete
                MessageBox.Show("Failed to Delete Contact. Please try again");
                //refresh data grid view
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                // clear method
                Clear();


            }
        }
    
            
        
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the value from text box
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "' OR Address LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource=dt;

        }
    }
}
