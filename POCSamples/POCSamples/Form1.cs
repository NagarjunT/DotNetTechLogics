using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POCSamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var result = ValidateInput();
                if (result.IsValid)
                {
                    bool canPlace = CanPlaceFlowers(result);
                    string message = canPlace ? "Flowers can be placed successfully." : "Cannot place flowers in the flowers bed.";
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("Please provide valid inputs.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanPlaceFlowers(FlowerData inputData)
        {
            try
            {
                int i = 0;
                int[] fbs = inputData.FlowersBed.ToArray();
                int n = inputData.FlowersToPlace;
                while (i < fbs.Length)
                {
                    if (fbs[i] == 0 &&
                        ((i + 1 <= fbs.Length - 1 && fbs[i + 1] == 0) || (fbs[i] == 0 && i == fbs.Length - 1))
                        )
                    {
                        n = n - 1;
                        i = i == fbs.Length - 1 ? i + 1 : i + 3;
                    }
                    else
                    {
                        i++;
                    }
                    if (n < 1)
                        return true;
                }
                return n == 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private FlowerData ValidateInput()
        {
            FlowerData data = new FlowerData();
            List<int> returnList = null;
            string input = textBox1.Text.Trim();
            string fCount = textBox2.Text.Trim();
            if (!string.IsNullOrEmpty(input) && !string.IsNullOrEmpty(fCount))
            {
                returnList = new List<int>();
                if (!input.Contains(','))
                    return new FlowerData() { IsValid = false };
                foreach (var val in input.Split(','))
                {
                    if (!new int[] { 0, 1 }.Contains(Convert.ToInt32(val)))
                        return new FlowerData() { IsValid = false };
                    else
                        returnList.Add(Convert.ToInt32(val));
                }
                if (Char.IsNumber(Convert.ToChar(fCount)))
                {
                    return new FlowerData()
                    {
                        IsValid = Convert.ToInt32(fCount) < 1 ? false : true,
                        FlowersBed = Convert.ToInt32(fCount) < 1 ? null : returnList,
                        FlowersToPlace = Convert.ToInt32(fCount)
                    };
                }
            }
            return new FlowerData() { IsValid = false };
        }
    }

    public class FlowerData
    {
        public bool IsValid { get; set; }
        public List<int> FlowersBed { get; set; }
        public int FlowersToPlace { get; set; }
    }
}
