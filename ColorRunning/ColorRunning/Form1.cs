using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorRunning
{
    public partial class ColorEffect : Form
    {
        KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
        static ComboBox sComboBox;
        string[] knownArray;
        public ColorEffect()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(timer1.Interval);

            knownArray = new string[names.Length];
            foreach (KnownColor number in names)
            {
                knownArray[(int)number - 1] = Convert.ToString(names[(int)number - 1]);
            }
        }

        private void NumberOfPictureBox_TextChanged(object sender, EventArgs e)
        {
            
    
                if (NumberOfPictureBox.Text!="")
                {
                    try
                    {
                        int numberPic = int.Parse(NumberOfPictureBox.Text);
                        
                        if (numberPic != 0&& numberPic<1000)
                        {
                            field.Controls.Clear();
                            sComboBox = BoxColor;
                            MakePictureBox.makePicBox(numberPic, field, BoxColor);
                        }

                    }
                    catch (Exception)
                    {
                    }
                    

                } 
                
            
        }
        bool bPair=false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            MakePictureBox.glowing(bPair,field);
        }
        bool bactive = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!bactive)
            {
                timer1.Start();
                bPair = true;
                bactive = true;
            }
            else
            {
                timer1.Stop();
                bPair = false;
                bactive = false;
                for (int i = 0; i < field.Controls.Count; i++)
                {
                    field.Controls[i].Visible = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = int.Parse(textBox1.Text);
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!bactive)
            {
                timer1.Start();
                bPair = false;
                bactive = true;
            }
            else
            {
                timer1.Stop();
                bPair = false;
                bactive = false;
                for (int i = 0; i < field.Controls.Count; i++)
                {
                    field.Controls[i].Visible = true;
                }
            }
        }
        int timerMode;
        int timerTick = 0;
        int colorTick = 1;
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string selectedRadio = ((RadioButton)sender).Name;
            char choice = selectedRadio[11];
            if (choice == '1')
            {
                timerMode = 1;
                timerTick = 0;
                timer2.Start();
            }
            else if (choice == '2')
            {
                timerMode = 2;
                timerTick = 0;
                timer2.Start();
            }
            else if (choice == '3')
            {
                timerMode = 3;
                timerTick = 0;
                timer2.Start();
            }
            else if (choice == '4')
            {
                timerMode = 4;
                timerTick = 0;
                timer2.Start();
            }
            else if (choice == '5')
            {   
                if (NumberOfPictureBox.Text != "")
                {
                    try
                    {
                        field.Controls.Clear();
                        int numberPic = int.Parse(NumberOfPictureBox.Text);

                        if (numberPic != 0 && numberPic < 1000)
                        {
                            sComboBox = BoxColor;
                            MakePictureBox.makePicBox(numberPic, field, BoxColor);
                        }

                    }
                    catch (Exception)
                    {
                    }
                } 
                timerMode = 0;
                timerTick = 0;
                timer2.Stop(); 
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            int controller = (((int)(MakePictureBox.totalColumns / 2)) * 2 == MakePictureBox.totalColumns ? MakePictureBox.totalColumns / 2 : (MakePictureBox.totalColumns / 2) + 1);
            if (timerMode == 1)//toLeft
            {
                if (timerTick == field.Controls.Count)
                {
                    timerTick = 0;
                    colorTick++;
                    if (colorTick > names.Length - 1)
                    {
                        colorTick = 1;
                    }
                }
                field.Controls[timerTick].BackColor = Color.FromName(knownArray[colorTick]);
                timerTick++;

            }
            else if (timerMode == 2)//toRight
            {
                if (timerTick == field.Controls.Count)
                {
                    timerTick = 0;
                    colorTick++;
                    if (colorTick > names.Length - 1)
                    {
                        colorTick = 1;
                    }
                }
                field.Controls[MakePictureBox.totalCount-1-timerTick].BackColor = Color.FromName(knownArray[colorTick]);
                timerTick++;
            }
            else if (timerMode == 3)//toCenter
            {
                if (timerTick > (((int)(MakePictureBox.totalColumns / 2)) * 2 == MakePictureBox.totalColumns ? MakePictureBox.totalColumns / 2 : (MakePictureBox.totalColumns / 2)+1))//tocenter
                {
                    timerTick = 0;
                    colorTick++;
                    if (colorTick > names.Length - 1)
                    {
                        colorTick = 1;
                    }
                }
                for (int i = 0; i < timerTick; i++)
                {
                    for (int j = 0; j < MakePictureBox.totalRows; j++)
                    {
                        field.Controls[j*MakePictureBox.totalColumns+i].BackColor = Color.FromName(knownArray[colorTick]);
                    } 
                }
                for (int k = 0; k < timerTick; k++)
                {
                    for (int l = 0; l < MakePictureBox.totalRows; l++)
                    {
                        field.Controls[MakePictureBox.totalCount-1-k - (l*MakePictureBox.totalColumns)].BackColor = Color.FromName(knownArray[colorTick]);
                    }
                }
                timerTick++;
            }
            else//fromCenter
            {
                if (timerTick > (((int)(MakePictureBox.totalColumns / 2)) * 2 == MakePictureBox.totalColumns ? MakePictureBox.totalColumns / 2 : (MakePictureBox.totalColumns / 2) + 1))//fromcentre
                {
                    timerTick = 0;
                    colorTick++;
                    
                    if (colorTick > knownArray.Length - 1)
                    {
                        colorTick = 1;
                    }
                }
                for (int i = 0; i < timerTick; i++)
                {
                    for (int j = 0; j < MakePictureBox.totalRows; j++)
                    {
                        if (!(MakePictureBox.totalCount % 2 == 0) && MakePictureBox.totalCount == (j * MakePictureBox.totalColumns) + controller + i)
                        {

                        }
                        else
                        {
                            field.Controls[(j * MakePictureBox.totalColumns) + controller - 1 - i].BackColor = Color.FromName(knownArray[colorTick]);
                        }
                    }
                }
                for (int k = 0; k < timerTick; k++)
                {
                    for (int l = 0; l < MakePictureBox.totalRows; l++)
                    {
                        if (!(MakePictureBox.totalCount % 2 == 0) && MakePictureBox.totalCount==(l*MakePictureBox.totalColumns)+controller+k)
                        {
                            
                        }
                        else
                        {
                            field.Controls[(l*MakePictureBox.totalColumns)+controller+k-1].BackColor = Color.FromName(knownArray[colorTick]);
                        }
                        
                    }
                }
                timerTick++;
            }
        }

    }
}
