using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ColorRunning
{
    class MakePictureBox
    {
        static Random rand = new Random();
        static KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
        static string[] colorArray;
        static string[] knownArray;
        static ComboBox sComboBox;
        public static int pairCount;
        public static int notPairCount;
        public static int totalColumns;
        public static int totalRows;
        public static int totalCount;

        public int TotalColumns 
        { 
            get
            {
                return totalColumns;
            }
        }
        public int TotalRows
        {
            get
            {
                return totalRows;
            }
        }
        public int Totalcount
        {
            get
            {
                return totalCount;
            }
        }
        public static void makePicBox(int count,Panel screen,ComboBox BoxColor)
        {
            int boxWidth, boxHeight;
            int colorArrayNum = 0;

            pairCount = 0;
            notPairCount = 0;

            sComboBox = BoxColor;
            colorArray=new string[count];

            int columns = (int)Math.Sqrt(count);
            int rows = Math.Pow(columns, 2) >= count ? columns : (columns+1)*columns >= count ? columns+1:columns+2;

            totalColumns = columns;
            totalRows = rows;
            totalCount = count;


            
            boxWidth = screen.Width / columns;
            boxHeight = screen.Height / rows;

            bool b2nd = false;
            for (int i = 0; i < rows; i++)
            {

                if (count !=columns*rows && count-(i*columns)<columns)
                {
                    columns = count-(i * columns);
                    boxWidth = (int)screen.Width / columns;
                }

                for (int j = 0; j < columns; j++)
                {
                    Panel inBox = new Panel();
                    inBox.Size = new System.Drawing.Size(boxWidth,boxHeight);
                    inBox.Location = new System.Drawing.Point(boxWidth*j,boxHeight*i);

                    if (b2nd)
                    {
                        inBox.Name = "2" + i + j;
                        b2nd = false;
                        pairCount++;
                    }
                    else
                    {
                        inBox.Name = "1" + i + j;
                        b2nd = true;
                        notPairCount++;
                    }

                    Color randomColor = Color.FromKnownColor(names[rand.Next(names.Length)]);
                    inBox.BackColor = randomColor;
                    colorArray[colorArrayNum] = Convert.ToString(randomColor);
                    
                    
                    screen.Controls.Add(inBox);
                    inBox.Click += inBox_Click;
                }

            }

            knownArray = new string[names.Length];
            foreach (KnownColor number in names)
            {
                knownArray[(int)number - 1] = Convert.ToString(names[(int)number - 1]);
            }
            BoxColor.Items.AddRange(knownArray);
        }
        
        public static void inBox_Click(object sender, EventArgs e)
        {
            (sender as Panel).BackColor = Color.FromName(sComboBox.Text);
        }
        static bool bactive = false;
        public static void glowing(bool pair, Panel screen)
        {
            #region pair
            if (pair)
            {
                if (!bactive)
	            {
		            for (int i = 0; i < screen.Controls.Count; i++)
                    {
                        if(screen.Controls[i].Name[0]=='2')
                        {
                            screen.Controls[i].Visible = false;
                        }
                    }
                    bactive = true;
	            }
                else
                {
                    for (int i = 0; i < screen.Controls.Count; i++)
                    {
                        if (screen.Controls[i].Name[0] == '2')
                        {
                            screen.Controls[i].Visible = true;
                        }
                    }
                    bactive = false;
                }
            }
            #endregion
            #region notPair
            else if (!pair)
                {
                    if (!bactive)
                    {
                        for (int i = 0; i < screen.Controls.Count; i++)
                        {
                            if (screen.Controls[i].Name[0] == '1')
                            {
                                screen.Controls[i].Visible = false;
                            }
                        }
                        bactive = true;
                    }
                    else
                    {
                        for (int i = 0; i < screen.Controls.Count; i++)
                        {
                            if (screen.Controls[i].Name[0] == '1')
                            {
                                screen.Controls[i].Visible = true;
                            }
                        }
                        bactive = false;
                    }
                }
            #endregion

        }
        }
    
}
