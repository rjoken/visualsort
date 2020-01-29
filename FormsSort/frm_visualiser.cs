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
            bogosort,
            radixsort
        }
        int num_elements;
        int[] elements;
        int checking_index;
        Random rng = new Random();
        algorithm algo;
        algorithm[] algos;
        Thread sort_thread;
        public frm_visualiser(int n)
        {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            refresh.Interval = 1000 / 60;
            refresh.Enabled = true;
            algo = algorithm.bubblesort;
            algos = new algorithm[5] { algorithm.bubblesort, algorithm.mergesort, algorithm.quicksort, algorithm.bogosort, algorithm.radixsort };
            num_elements = n;
            elements = new int[num_elements];
            for(int i = 0; i < elements.Length; i++)
            {
                elements[i] = i;
            }
            scramble(elements);
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
                    //need to make it fit exactly
                    /*double diff = ((w * elements.Length) - Width);
                    w = w * w % diff;*/
                }
                else w = 1;
                element_box.Width = (int)Math.Ceiling(w);
                element_box.Height = Height;
                element_box.X = i * element_box.Width;
                element_box.Y = Height + (int)h;
                if (checking_index == i)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Red), element_box);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), element_box);
                }
            }
        }
        private void beep()
        {
            int freq = 4000 + (elements[checking_index] * 10);
            int duration = 1000 / 60;
            System.Console.Beep(freq, duration);
        }

        private void bogosort(int[] arr)
        {
            int n = arr.Length;
            while(!is_sorted(arr))
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
                beep();
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
                beep();
                for (int j = 0; j < n - i - 1; j++)
                {
                    checking_index = j;
                    beep();
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
            //kind of broke
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
                beep();
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

        private void countingsort(int[] arr, int exp)
        {
            int n = arr.Length;
            int i;
            int[] output = new int[n];
            int[] count = new int[10];
            for (i = 0; i < 10; i++)
            {
                count[i] = 0;
            }

            //count occurrences
            for(i = 0; i < n; i++)
            {
                checking_index = i;
                beep();
                count[(arr[i] / exp) % 10]++;
            }

            //count[i] to contain position of digit in output
            for(i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            //build output
            for(i = n - 1; i >= 0; i--)
            {
                checking_index = i;
                beep();
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            //copy into arr
            for(i = 0; i < n; i++)
            {
                checking_index = i;
                beep();
                arr[i] = output[i];
            }
        }

        int get_max(int[] arr)
        {
            int max = arr[0];
            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }

        void radixsort(int[] arr)
        {
            int m = get_max(arr);

            //countsort for every digit. exp is 10^i where i is current digit
            for(int exp = 1; m/exp > 0; exp *= 10)
            {
                countingsort(arr, exp);
            }
        }

        private void scramble<T> (T[] arr)
        {
            int n = arr.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                checking_index = k;
                beep();
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
                if (sort_thread != null)
                {
                    sort_thread.Abort();
                }
                if (algo == algorithm.bubblesort)
                {
                    sort_thread = new Thread(() => bubblesort(elements));
                    sort_thread.Start();
                }
                else if(algo == algorithm.mergesort)
                {
                    sort_thread = new Thread(() => mergesort(elements));
                    sort_thread.Start();
                }
                else if (algo == algorithm.quicksort)
                {
                    sort_thread = new Thread(() => quicksort(elements, 0, elements.Length-1));
                    sort_thread.Start();
                }
                else if (algo == algorithm.bogosort)
                {
                    sort_thread = new Thread(() => bogosort(elements));
                    sort_thread.Start();
                }
                else if (algo == algorithm.radixsort)
                {
                    sort_thread = new Thread(() => radixsort(elements));
                    sort_thread.Start();
                }
            }
            if(e.KeyCode == Keys.Back)
            {
                if (sort_thread != null)
                {
                    sort_thread.Abort();
                }
                sort_thread = new Thread(() => scramble(elements));
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
    }
}
