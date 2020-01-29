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
            mergesort,
            quicksort,
            bogosort
        }
        int num_elements;
        int[] elements;
        int rect_width;
        int checking_index;
        Random rng = new Random();
        algorithm algo;
        algorithm[] algos;
        public frm_visualiser(int n)
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            refresh.Interval = 1000 / 60;
            refresh.Enabled = true;
            algo = algorithm.bubblesort;
            algos = new algorithm[4] { algorithm.bubblesort, algorithm.mergesort, algorithm.quicksort, algorithm.bogosort };
            num_elements = n;
            elements = new int[num_elements];
            for(int i = 0; i < elements.Length; i++)
            {
                elements[i] = i;
            }
            rect_width = Width / elements.Length;
            scramble(elements);
        }
        
        private void frm_visualiser_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawString(algo.ToString(), new Font("Verdana", 12.0f), new SolidBrush(Color.AliceBlue), 0, 0);
            for(int i = 0; i < elements.Length; i++)
            {
                if (checking_index == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), i * rect_width, Height - elements[i] * 10, rect_width, elements[i] * 10);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), i * rect_width, Height - elements[i] * 10, rect_width, elements[i] * 10);
                }
                e.Graphics.DrawString(elements[i].ToString(), new Font("Verdana", 6.0f), new SolidBrush(Color.Black), i * rect_width, Height - elements[i] * 10);
            }
        }

        private void bogosort(int[] arr)
        {
            int n = arr.Length;
            while(is_sorted(arr) == false)
            {
                scramble(arr);
            }
        }

        private bool is_sorted(int[] arr)
        {
            int n = arr.Length;
            for(int i = 0; i < n - 1; i++)
            {
                checking_index = i;
                Thread.Sleep(10);
                if (arr[i] > arr[i+1])
                {
                    return false;
                }
            }
            return true;
        }

        private void bubblesort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                checking_index = i;
                for (int j = 0; j < n - i - 1; j++)
                {
                    Thread.Sleep(10);
                    if (arr[j] > arr[j + 1])
                    {
                        //swap them
                        int tmp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = tmp;
                    }
                }
            }
        }

        private void mergesort(int[] arr)
        {
            if(arr.Length > 1)
            {
                int n = arr.Length;
                int mid = n / 2;
                int[] l = new int[mid];
                int[] r = new int[n - mid];
                for (int i = 0; i < mid; i++)
                {
                    
                    l[i] = arr[i];
                }
                for(int i = 0; i < n - mid; i++)
                {
                    checking_index = i;
                    Thread.Sleep(10);
                    r[i] = arr[mid + i];
                }

                mergesort(l);
                mergesort(r);

                int x, y, z;
                x = y = z = 0;
                while(x < l.Length && y < r.Length)
                {
                    if (l[x] < r[y])
                    {
                        arr[z] = l[x];
                        x++;
                    }
                    else
                    {
                        arr[z] = r[y];
                        y++;
                    }
                    z++;
                }
                while(x < l.Length)
                {
                    arr[z] = l[x];
                    x++;
                    z++;
                }
                while(y < r.Length)
                {
                    arr[z] = r[y];
                    y++;
                    z++;
                }
            }
        }

        private int partition(int[] arr, int low, int high)
        {
            int i = low - 1;
            int pivot = arr[high];
            for(int j = low; j < high; j++)
            {
                checking_index = j;
                Thread.Sleep(10);
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                }
            }
            int tmp = arr[high];
            arr[high] = arr[i+1];
            arr[i+1] = tmp;
            return i + 1;
        }

        private void quicksort(int[] arr, int low, int high)
        {
            int pivot;
            if(low < high)
            {
                pivot = partition(arr, low, high);

                quicksort(arr, low, pivot - 1);
                quicksort(arr, pivot + 1, high);
            }
        }

        private void scramble<T> (T[] arr)
        {
            int n = arr.Length;
            while (n > 1)
            {
                Thread.Sleep(10);
                int k = rng.Next(n--);
                checking_index = k;
                T temp = arr[n];
                arr[n] = arr[k];
                arr[k] = temp;
            }
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void frm_visualiser_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (algo == algorithm.bubblesort)
                {
                    Thread thread = new Thread(() => bubblesort(elements));
                    thread.Start();
                }
                else if(algo == algorithm.mergesort)
                {
                    Thread thread = new Thread(() => mergesort(elements));
                    thread.Start();
                }
                else if (algo == algorithm.quicksort)
                {
                    Thread thread = new Thread(() => quicksort(elements, 0, elements.Length-1));
                    thread.Start();
                }
                else if (algo == algorithm.bogosort)
                {
                    Thread thread = new Thread(() => bogosort(elements));
                    thread.Start();
                }
            }
            if(e.KeyCode == Keys.Back)
            {
                Thread thread = new Thread(() => scramble(elements));
                thread.Start();
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
    }
}
