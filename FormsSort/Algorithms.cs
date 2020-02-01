using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormsSort
{
    public static class Algorithms
    {
        public static int checking_index;
        public static void beep()
        {
            /*
             * TODO: find better way to do this because default c# way is rubbish
             * or maybe it just needs to be implemented better. threading?
             */
            /*int freq = 2000 + (elements[checking_index] * 10);
              int duration = 10;
              System.Console.Beep(freq, duration);*/
            Thread.Sleep(1000 / 60);
        }

        /* sorting algorithms */
        public static void bogosort(int[] arr)
        {
            int n = arr.Length;
            while (!is_sorted(arr))
            {
                Random rng = new Random();
                scramble(arr, rng);
            }
        }

        public static void bubblesort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; i++)
            {
                update_algo(i);
                for (int j = 0; j < n - i - 1; j++)
                {
                    update_algo(j);
                    if (arr[j] > arr[j + 1])
                    {
                        //swap them
                        swap(arr, j, j + 1);
                    }
                }
            }
        }

        public static void quicksort(int[] arr, int low, int high)
        {
            int pivot;
            if (low < high)
            {
                pivot = partition(arr, low, high);

                quicksort(arr, low, pivot - 1);
                quicksort(arr, pivot + 1, high);
            }
        }

        public static void radixsort(int[] arr)
        {
            int m = get_max(arr);

            //countsort for every digit. exp is 10^i where i is current digit
            for (int exp = 1; m / exp > 0; exp *= 10)
            {
                update_algo(m);
                countingsort(arr, exp);
            }
        }

        private static void countingsort(int[] arr, int exp)
        {
            //sort array based on least significant digit, working up
            int n = arr.Length;
            int i;
            int[] output = new int[n];
            int[] count = new int[10];
            for (i = 0; i < 10; i++)
            {
                count[i] = 0;
            }

            //count occurrences
            for (i = 0; i < n; i++)
            {
                update_algo(i);
                count[(arr[i] / exp) % 10]++;
            }

            //count[i] to contain position of digit in output
            for (i = 1; i < 10; i++)
            {
                count[i] += count[i - 1];
            }

            //build output
            for (i = n - 1; i >= 0; i--)
            {
                update_algo(i);
                output[count[(arr[i] / exp) % 10] - 1] = arr[i];
                count[(arr[i] / exp) % 10]--;
            }

            //copy into arr
            for (i = 0; i < n; i++)
            {
                update_algo(i);
                arr[i] = output[i];
            }
        }

        /* utility functions */

        private static void update_algo(int index)
        {
            checking_index = index;
            beep();
        }

        private static bool is_sorted(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                update_algo(i);
                if (arr[i] > arr[i + 1])
                {
                    return false;
                }
            }
            return true;
        }

        private static void swap(int[] arr, int a, int b)
        {
            int tmp = arr[b];
            arr[b] = arr[a];
            arr[a] = tmp;
        }

        private static int partition(int[] arr, int low, int high)
        {
            int i = low - 1;
            int pivot = arr[high];
            for (int j = low; j < high; j++)
            {
                update_algo(j);
                if (arr[j] < pivot)
                {
                    i++;
                    swap(arr, i, j);
                }
            }
            swap(arr, i + 1, high);
            return i + 1;
        }


        static int get_max(int[] arr)
        {
            int max = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }

        public static void scramble<T>(T[] arr, Random rng)
        {
            int n = arr.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                update_algo(k);
                T temp = arr[n];
                arr[n] = arr[k];
                arr[k] = temp;
            }
        }
    }
}
