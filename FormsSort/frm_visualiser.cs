using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSort
{
    public partial class frm_visualiser : Form
    {
        enum algorithm
        {
            bubblesort,
            quicksort,
            bogosort,
            radixsort
        }
        int num_elements;
        int[] elements;
        Random rng = new Random();
        algorithm algo;
        algorithm[] algos;
        Thread sort_thread;
        public frm_visualiser(int n)
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //timer refreshes the form at 60 frames per second
            timer_refresh.Interval = 1000 / 60;
            timer_refresh.Enabled = true;

            algos = new algorithm[4] { algorithm.bubblesort, algorithm.quicksort, algorithm.bogosort, algorithm.radixsort };
            algo = algos[0];
            num_elements = n;
            elements = new int[num_elements];
            for(int i = 0; i < elements.Length; i++)
            {
                elements[i] = i;
            }
            Algorithms.scramble(elements, rng);
        }
        
        private void frm_visualiser_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawString(algo.ToString(), new Font("Verdana", 12.0f), new SolidBrush(Color.AliceBlue), 0, 0);
            for(int i = 1; i < elements.Length - 1; i++)
            {
                Rectangle element_box = new Rectangle();
                double h, w;
                h = -Math.Ceiling((double)Height * (double)elements[i] / (double)elements.Length);
                if (elements.Length < Width)
                {
                    w = Width / elements.Length;
                    //TODO: need to make it fit exactly
                    /*double diff = ((w * elements.Length) - Width);
                    w = w * w % diff;*/
                }
                else w = 1;
                element_box.Width = (int)Math.Ceiling(w);
                element_box.Height = Height;
                element_box.X = i * element_box.Width;
                element_box.Y = Height + (int)h;
                if (Algorithms.checking_index == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), element_box);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), element_box);
                }
            }
        }

        private void frm_visualiser_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (sort_thread != null)
                {
                    sort_thread.Abort();
                }
                if (algo == algorithm.bubblesort)
                {
                    sort_thread = new Thread(() => Algorithms.bubblesort(elements));
                    sort_thread.Start();
                }
                /* Gone for now
                else if(algo == algorithm.mergesort)
                {
                    sort_thread = new Thread(() => Algorithms.mergesort(elements));
                    sort_thread.Start();
                }
                */
                else if (algo == algorithm.quicksort)
                {
                    sort_thread = new Thread(() => Algorithms.quicksort(elements, 0, elements.Length-1));
                    sort_thread.Start();
                }
                else if (algo == algorithm.bogosort)
                {
                    sort_thread = new Thread(() => Algorithms.bogosort(elements));
                    sort_thread.Start();
                }
                else if (algo == algorithm.radixsort)
                {
                    sort_thread = new Thread(() => Algorithms.radixsort(elements));
                    sort_thread.Start();
                }
            }
            if(e.KeyCode == Keys.Back)
            {
                if (sort_thread != null)
                {
                    sort_thread.Abort();
                }
                sort_thread = new Thread(() => Algorithms.scramble(elements, rng));
                sort_thread.Start();
            }
            if(e.KeyCode == Keys.Right)
            {
                if(algo == algos[algos.Length - 1])
                {
                    algo = algos[0];
                }
                else
                {
                    algo = algos[Array.IndexOf(algos, algo) + 1];
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                if (algo == algos[0])
                {
                    algo = algos[algos.Length - 1];
                }
                else
                {
                    algo = algos[Array.IndexOf(algos, algo) - 1];
                }
            }
        }

        private void timer_refresh_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
