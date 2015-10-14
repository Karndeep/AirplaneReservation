/*
 * Program Name: Assigment1 for Airplane Seat Reservation
 *
 * Purpose:Is to make a airplane reservation program that allows
 * user to enter their name and seating but if full will be sent to
 * waiting list and when a seat opens up the first person waiting in the
 * waiting list will be add to the reservation list. The user should be 
 * able to check if seats are available and be able to remove reservation. 
 *
 * Created: Karndeep Kahlon Sept 2015
 * 
 * Revision history:
 * Karndeep Kahlon Sept 2015
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Declaration of queue array
        Queue<string> waitingPassengers = new Queue<string>();

        //Declaration of multidimensional array
        string[,] passengersList = new string[5,3] {

            {"A1: ", "A2: ", "A3: "},
            {"B1: ", "B2: ", "B3: "},
            {"C1: ", "C2: ", "C3: "},
            {"D1: ", "D2: ", "D3: "},
            {"E1: ", "E2: ", "E3: "}, };

        public Form1()
        {
            InitializeComponent();
        }

        //A function to book reservation. 
        private void btnBook_Click(object sender, EventArgs e)
        {
            //Initialize.
            string name = txtName.Text;
            int seat = lstAlpha.SelectedIndex;
            int row = lstNumeric.SelectedIndex;

            //checking if name length is 0 and listbox are selected.
            if (name.Length > 0 && (lstAlpha.SelectedIndex >= 0) && (lstNumeric.SelectedIndex >= 0))
            {
                //checks if array is full by default length as 4.
                if (passengersList[seat, row].Length != 4)
                {
                    MessageBox.Show("Sorry seat is reserved");
                }
                //check if array not full default length 4 if yes adds to array.
                if (passengersList[seat, row].Length == 4)
                {
                    passengersList[seat, row] += name;
                    MessageBox.Show("You have been entered in the reservation: " + name);
                }
                //If passenger count is 15 and name is not blank then ask to add to waiting list.
                if (Counter() >= 15 & name != "")
                {
                    DialogResult dialogResult = MessageBox.Show("Sorry reservation are full, would you like to enter the Waiting list?"
                     , "Full Reservation", MessageBoxButtons.YesNoCancel);
                    //Checks if says yes and if waiting list is not full.
                    if (dialogResult == DialogResult.Yes && waitingPassengers.Count < 10)
                    {
                        waitingPassengers.Enqueue(name);
                    }
                }
            }
            else
            {  
                MessageBox.Show("Please enter a name/seat/row");

            }

            }
    
        // A function to show the passenger list.
        private void btnShow_Click(object sender, EventArgs e)
        {
            rtxtAllBookings.Clear();
            //Loops for each passenger and adds to rich textbox.
            foreach (string person in passengersList)
            {
                rtxtAllBookings.Text += person + "\n";
            }

        }

        //A function to keep track of number of passenger.
        public int Counter()
        {
            int result = 0;
            int arraySeat = 5;
            int arrayRow = 3;

            //Loops 15 times but only adds to result if value is in passenger list.
            for (int i = 0; i < arraySeat; i++)
            {
                for (int k = 0; k < arrayRow; k++)
                {
                    if (passengersList[i,k].Length > 4)
                    {
                        result++;
                    }
                }
                
            }

            return result;
        }

        //A function to Add to waiting list.
        private void btnAddWait_Click(object sender, EventArgs e)
        {
            //Initialize.
            string name = txtName.Text;

                //Checks if name is empty and reservation are full.
                if (name != "" && Counter() >= 15 && waitingPassengers.Count < 10)
                {
                    waitingPassengers.Enqueue(name);
                    MessageBox.Show("You have been entered in the waiting list: " + name);
                }
                //Check if seats are still available.
                if (Counter() < 15 )
                {
                    MessageBox.Show("Sorry there are seats available still");
                }
                //Checks if waiting list is full and passenger list.
                if(waitingPassengers.Count > 10 && Counter() < 15)
                {

                    MessageBox.Show("Sorry waiting list is full! Try Again Soon.");

                }
                //Check if name is blank.
                else if (name == "")
                {
                      MessageBox.Show("Please enter a name");
                }
        }
        //A function that cancel reservation.
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Initialize.
            int seat = lstAlpha.SelectedIndex;
            int row = lstNumeric.SelectedIndex;

            string newName = "";

            //Checks if seat is full.
            if (passengersList[seat, row].Length != 4)
            {   
                //clear the seat
                passengersList[seat, row] = passengersList[seat, row].Remove(4);
                MessageBox.Show("Your reservation has been cancelled");

                //Check if there is seats available.
                if (Counter() <= 15 && waitingPassengers.Count > 0)
                {
                    //Deletes from passenger list and returns value.
                    newName = waitingPassengers.Dequeue();
                    passengersList[seat, row] += newName;

                    MessageBox.Show("Spot has just open up for " + newName);
                }
            }
            else
            {
                MessageBox.Show("Sorry seat is empty");
            }
        }
   
        //A function to Show Waiting List
        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            rtxtWaitList.Clear();
            //Loops for each passenger and adds to rich textbox.
            foreach (string person in waitingPassengers)
            {
                rtxtWaitList.Text += person + "\n";
            }
        }

        //A function a to fill all passenger list 
        private void btnFillAll_Click(object sender, EventArgs e)
        {
            //Initialize.
            string fakeText = "BOB";
            int arraySeat = 5;
            int arrayRow = 3;

            //Loops 15 times and fills passenger array with bob.
            for (int i = 0; i != arraySeat; i++)
            {
                for (int k = 0; k < arrayRow; k++)
                  {
                      //removes any other entries
                      if (passengersList[i, k].Length != 4)
                      {
                          passengersList[i, k] = passengersList[i, k].Remove(4);
                      }

                        passengersList[i, k] += fakeText;
                  }
                }   
            }
    
        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       //A function to check the status of a seat.
        private void btnStatus_Click(object sender, EventArgs e)
        {
            //Initialize.
            int seat = lstAlpha.SelectedIndex;
            int row = lstNumeric.SelectedIndex;

            //checks if seats and row is selected.
            if (lstAlpha.SelectedIndex == -1 || lstNumeric.SelectedIndex == -1)
            {

                MessageBox.Show("Please select a seat");
            }
            else
            {   //If default length then Available
                if (passengersList[seat, row].Length == 4)
                {
                    txtStatus.Text = "Available";
                }
                //If not default length then Occupied
                if (passengersList[seat, row].Length != 4)
                {
                    txtStatus.Text = "Occupied";
                }
            }
        }

    }
}