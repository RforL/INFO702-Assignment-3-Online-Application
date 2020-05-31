using AtleX.HaveIBeenPwned;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFO702_Assignment3
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        ///Form Elements
        private void Form1_Load(object sender, EventArgs e)
        { }

        //Variables
        private bool mouseDown;
        private Point lastLocation;

        //Mouse Down Event
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        //Mouse Move Event
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        //Mouse Up Event
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            resultsBox.Clear();

            if (emailTextBox.Text == null)
            {
                resultsBox.Text = "Please enter an email";
            }

            // Create the client
            var settings = new HaveIBeenPwnedClientSettings()
            {
                ApiKey = "00b9d0e69f9345799eda0ea03cdd9709", // Get one from https://haveibeenpwned.com/API/Key. Necessary for getting the breaches and pastes an account is in.
                ApplicationName = "HaveIBeenHacked",
            };

            using (var client = new HaveIBeenPwnedClient(settings))
            {
                
                // Get the breaches for an account. This returns a collection of breaches with their 
                // name. Use the response from GetAllBreachesAsync() to find the corresponding details 
                // by name
                var breaches = await client.GetBreachesAsync(emailTextBox.Text); //Rquires an API key

                foreach (var breach in breaches)
                {
                    resultsBox.AppendText("Breach Name: " + breach.Name + "\r\n");
                }


            }
        }

        ///Buttons
    }
}
