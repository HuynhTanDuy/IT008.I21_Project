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
using static SimulationSortApp.Movement;


namespace SimulationSortApp
{
    public partial class Form1 : Form
    {
        private List<ButtonNode> nodeArr = new List<ButtonNode>(); //mảng chứa các buttonnode
        int[] M; 
        public static ManualResetEvent pauseStatus = new ManualResetEvent(true);
        public static bool IsPause = false;
        int step;
        Boolean flag;
        int x, y;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AscRadioButton.Checked = true;
            RandomGenerateBtn.Enabled = true;
            ManualGenerateBtn.Enabled = true;
        }

        #region Khởi tạo mảng
        private void RandomGenerateButton(object sender, EventArgs e)
        {
            if (int.Parse(NumberOfElementTxt.Text) > 10) NumberOfElementTxt.Text = "10";
            RandomGenerate(int.Parse(NumberOfElementTxt.Text));
        }

        private void RandomGenerate(int numberofelement)
        {
            deletebuttonnode();
            Random rd = new Random();
            M = new int[numberofelement];

            for (int i = 0; i < numberofelement; i++)
            {
                int giaTri = rd.Next(0, 99);
                ButtonNode temp = new ButtonNode(i, giaTri);
                this.ViewPanel.Controls.Add(temp);
                nodeArr.Add(temp);
                temp.Location = new Point(ViewPanel.Location.X + i * 80 - 20*int.Parse(NumberOfElementTxt.Text), ViewPanel.Location.Y - 40 / 2);
                M[i] = giaTri;
            }
        }

        private void ManualGenerateBtn_Click(object sender, EventArgs e)
        {
            if (int.Parse(NumberOfElementTxt.Text) > 10) NumberOfElementTxt.Text = "10";
            ManualGenerate(int.Parse(NumberOfElementTxt.Text));
        }

        private void ManualGenerate(int numberofelement)
        {
            deletebuttonnode();
            M = new int[numberofelement];
            for (int i = 0; i < numberofelement; i++)
            {
                int giaTri = 0;
                ButtonNode temp = new ButtonNode(i, giaTri);
                this.ViewPanel.Controls.Add(temp);
                nodeArr.Add(temp);
                temp.Location = new Point(ViewPanel.Location.X + i * 80 - 20 * int.Parse(NumberOfElementTxt.Text), ViewPanel.Location.Y - 40 / 2);
                M[i] = giaTri;
            }
            nodeArr[0].Focus();
        }
        #endregion

        #region Bảng điều khiển
        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (nodeArr.Count == 0) { MessageBox.Show("Please generate array"); return; }
            for (int i = 0; i < int.Parse(NumberOfElementTxt.Text); i++)
            {
                M[i] = nodeArr[i].giaTri;
            }
            RandomGenerateBtn.Enabled = false;
            ManualGenerateBtn.Enabled = false;
            StartBtn.Enabled = false;
            saveQuaTrinh.Clear();
            backgroundWorker1.RunWorkerAsync();

            disableTexbox(true); //khi bắt đầu mô phỏng, không cho phép người dùng thay đổi giá trị textbox
        }
        public void disableTexbox(bool e) 
        {
            for (int i = 0; i < nodeArr.Count; i++) nodeArr[i].nhapTayTexbox.ReadOnly = e; 
        }
        private void PauseBtn_Click(object sender, EventArgs e)
        {
            Pause();
        }

        public void Pause()
        {
            if (IsPause)
            {
                pauseStatus.Set();     // hàm để resume
                IsPause = false;
                PauseBtn.Text = "Pause";
                timer1.Start();
                bunifuFlatButton2.Enabled = true;
            }
            else
            {
                pauseStatus.Reset();    // hàm để pause
                IsPause = true;
                PauseBtn.Text = "Continue";
                timer1.Stop();
                StartBtn.Enabled = false;
                bunifuFlatButton2.Enabled = false;
            }
        }

        private void DeleteArrayBtn_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy) { backgroundWorker1.CancelAsync(); }
            RandomGenerateBtn.Enabled = true;
            ManualGenerateBtn.Enabled = true;
            StartBtn.Enabled = true;
        }
        #endregion

        #region code sort
        private void Swap(int vt1, int vt2)
        {
            if (backgroundWorker1.CancellationPending) return;
            Status st = new Status();
            st.Vt1 = vt1;
            st.Vt2 = vt2;
            st.Type = LoaiDiChuyen.DI_LEN_DI_XUONG;
            for (int x = 0; x < 100; x++)
            {
                backgroundWorker1.ReportProgress(0, st); //gọi hàm ProgressChanged để cập nhật giao diện
                pauseStatus.WaitOne(Timeout.Infinite);
                System.Threading.Thread.Sleep(15);
            }
            st.Type = LoaiDiChuyen.QUA_PHAI_QUA_TRAI;
            int WIDTH = Math.Abs(vt1 - vt2) * 80;

            for (int x = 0; x < WIDTH; x++)
            {
                backgroundWorker1.ReportProgress(0, st);//gọi hàm ProgressChanged để cập nhật giao diện
                pauseStatus.WaitOne(Timeout.Infinite);
                System.Threading.Thread.Sleep(15);
            }//ok làm tiếp cho nút di chuyển ngược lại đi xuống đi lên để vào 1 hàng
            st.Type = LoaiDiChuyen.DI_XUONG_DI_LEN;
            for (int x = 0; x < 100; x++)
            {
                backgroundWorker1.ReportProgress(0, st);//gọi hàm ProgressChanged để cập nhật giao diện
                pauseStatus.WaitOne(Timeout.Infinite);
                System.Threading.Thread.Sleep(15);
            }//làm tiếp trường hợp dừng
            st.Type = LoaiDiChuyen.DUNG;
            backgroundWorker1.ReportProgress(0, st);
        }
        private void SwapInsertion(int vt1, int vt2)
        {
            if (backgroundWorker1.CancellationPending) return;
            Status st = new Status();
            st.Vt1 = vt1;
            st.Vt2 = vt2;
            //st.Type = LoaiDiChuyen.DI_LEN_DI_XUONG;
            //for (int x = 0; x < 100; x++)
            //{
            //    backgroundWorker1.ReportProgress(0, st); //gọi hàm ProgressChanged để cập nhật giao diện
            //    pauseStatus.WaitOne(Timeout.Infinite);
            //    System.Threading.Thread.Sleep(15);
            //}
            st.Type = LoaiDiChuyen.QUA_PHAI_QUA_TRAI;
            int WIDTH = Math.Abs(vt1 - vt2) * 80;

            for (int x = 0; x < WIDTH; x++)
            {
                backgroundWorker1.ReportProgress(0, st);//gọi hàm ProgressChanged để cập nhật giao diện
                pauseStatus.WaitOne(Timeout.Infinite);
                System.Threading.Thread.Sleep(15);
            }//ok làm tiếp cho nút di chuyển ngược lại đi xuống đi lên để vào 1 hàng
            //st.Type = LoaiDiChuyen.DI_XUONG_DI_LEN;
            //for (int x = 0; x < 100; x++)
            //{
            //    backgroundWorker1.ReportProgress(0, st);//gọi hàm ProgressChanged để cập nhật giao diện
            //    pauseStatus.WaitOne(Timeout.Infinite);
            //    System.Threading.Thread.Sleep(15);
            //}//làm tiếp trường hợp dừng
            st.Type = LoaiDiChuyen.DUNG;
            backgroundWorker1.ReportProgress(0, st);
        }
        private void ShellSort(int[] M)
        {
            CodeSort.code = showCode;
            CodeSort.ShellSort(AscRadioButton.Checked);
            int i, n, gap, temp;
            int j;
            n = M.Length;
            step = 0;
            string str = " Dãy chưa sắp : ";
            for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
            saveQuaTrinh.Text += str;
            Status st = new Status();
            for (gap = n / 2; gap > 0; gap = gap / 2)
            {
                step++;
                for (i = 0; i < n; i = i + gap)
                {
                  
                    if (backgroundWorker1.CancellationPending) return;
                   
                    temp = M[i];
                  
                    for (j = i; j > 0 && M[j - gap] > temp; j = j - gap)

                    {
                       
                        M[j] = M[j - gap];
                        Swap(j, j - gap);
                        str = " Bước " + step.ToString() + " : ";

                    }
                   
                    M[j] = temp;

                    


                }
                for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                saveQuaTrinh.Text += "\n" + str;



            }
        }
        private void MergeSort1(int[] M)
        {
        
            int i, n, gap, temp;
            int j;
            n = M.Length;
            step = 0;
            string str = " Dãy chưa sắp : ";
            for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
            saveQuaTrinh.Text += str;
            Status st = new Status();
            for (gap = n / 2; gap > 0; gap = gap / 2)
            {
                step++;
                for (i = 0; i < n; i = i + gap)
                {

                    if (backgroundWorker1.CancellationPending) return;

                    temp = M[i];

                    for (j = i; j > 0 && M[j - gap] > temp; j = j - gap)

                    {

                        M[j] = M[j - gap];
                        Swap(j, j - gap);
                        str = " Bước " + step.ToString() + " : ";

                    }

                    M[j] = temp;




                }
                for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                saveQuaTrinh.Text += "\n" + str;



            }
        }
        private void BubbleSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.BubbleSort(AscRadioButton.Checked);

            int i, j;
            step = 0;
            string str = " Dãy chưa sắp : ";
            for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
            saveQuaTrinh.Text +=str;
            int n = M.Length;
            Status st = new Status();
            for (i = 0; i < n; i++)
            {
                for (j = n - 1; j > i; j--)
                {
                    if (backgroundWorker1.CancellationPending) return;
                    if (((AscRadioButton.Checked == true) && (M[j] < M[j - 1])) || ((DescRadioButton.Checked == true) && (M[j] > M[j - 1])))
                    {
                        int tam = M[j];
                        M[j] = M[j - 1];
                        M[j - 1] = tam;
                        System.Threading.Thread.Sleep(15);
                        Swap(j, j - 1);

                        step++;
                        str = " Bước "+step.ToString()+" : ";
                        for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                        saveQuaTrinh.Text += "\n" + str;
                    }
                   
                }
            }
        }

        private void InterchangeSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.InterchangeSort(AscRadioButton.Checked);
            int i, j;
            int n = M.Length;
            step = 0;
            string str = " Dãy chưa sắp : ";
            for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
            saveQuaTrinh.Text +=str;
            Status st = new Status();
            for (i = 0; i < n - 1; i++)
                for (j = i + 1; j < n; j++)
                {
                    if (backgroundWorker1.CancellationPending) return;
                    if (((AscRadioButton.Checked == true) && (M[i] > M[j])) || ((DescRadioButton.Checked == true) && (M[i] < M[j])))
                    {
                        int tam = M[i];
                        M[i] = M[j];
                        M[j] = tam;
                        System.Threading.Thread.Sleep(15);
                        Swap(j, i);
                        step++;
                        str = " Bước " + step.ToString() + " : ";
                        for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                        saveQuaTrinh.Text += "\n" + str;

                    }
                }
        }
        private void InsertionSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.InsertionSort(AscRadioButton.Checked);
            int n = M.Length;
            int x, temp;
            Status st = new Status();
            step = 0;
            string str = " Dãy chưa sắp : ", strNew = "", strOld = "";
            for (int u = 0; u < M.Length; u++) strOld += M[u].ToString() + " ";
            str += strOld;
            saveQuaTrinh.Text +=str;
            for (int i = 1; i < n; i++)
            {
                x = i - 1;
                temp = M[i];
                if (backgroundWorker1.CancellationPending) return;
                while ((x >= 0) && (((AscRadioButton.Checked == true) && (M[x] > temp)) || ((DescRadioButton.Checked == true) && (M[x] < temp))))
                {
                    M[x + 1] = M[x];
                    System.Threading.Thread.Sleep(15);
                    SwapInsertion(x + 1, x);
                    x--;
                }

                M[x + 1] = temp;
                step++;
                str = " Bước " + step.ToString() + " : ";
                for (int u = 0; u < M.Length; u++) strNew += M[u].ToString() + " ";
                str += strNew;
                if (strNew != strOld) saveQuaTrinh.Text += "\n" + str;
                else step--;
                strOld = strNew;
                strNew = "";
            }
        }
        private void SelectionSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.SelectionSort(AscRadioButton.Checked);
            step = 0;
            string str = " Dãy chưa sắp : ", strNew = "", strOld = "";
            for (int u = 0; u < M.Length; u++) strOld += M[u].ToString() + " ";
            str += strOld;
            saveQuaTrinh.Text += str;

            int n = M.Length;
            Status st = new Status();
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (((AscRadioButton.Checked == true) && (M[min] > M[j])) || ((DescRadioButton.Checked == true) && (M[min] < M[j])))
                    {
                        min = j;
                    }
                }

                if (backgroundWorker1.CancellationPending) return;
                if (min != i)
                {
                    int temp = M[i];
                    M[i] = M[min];
                    M[min] = temp;
                    System.Threading.Thread.Sleep(15);
                    Swap(min, i);
                    step++;
                    str = " Bước " + step.ToString() + " : ";
                    for (int u = 0; u < M.Length; u++) strNew += M[u].ToString() + " ";
                    str += strNew;
                    if (strNew != strOld) saveQuaTrinh.Text += "\n" + str;
                    else step--;
                    strOld = strNew;
                    strNew = "";
                }
            }
        }
        private void BinaryInsertionSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.BinaryInsertionSort(AscRadioButton.Checked);
            step = 0;
            string str = " Dãy chưa sắp : ", strNew = "", strOld = "";
            for (int u = 0; u < M.Length; u++) strOld += M[u].ToString() + " ";
            str += strOld;
            saveQuaTrinh.Text += str;

            int n = M.Length;
            int x, left, right, m;
            Status st = new Status();
            for (int i = 1; i < n; i++)
            {
                x = M[i];
                left = 0;
                right = i - 1;
                while (left <= right)
                {
                    m = (left + right) / 2;
                    if ((((AscRadioButton.Checked == true) && (x < M[m])) || ((DescRadioButton.Checked == true) && (x > M[m]))))
                        right = m - 1;
                    else left = m + 1;
                }

                for (int j = i - 1; j >= left; j--)
                {
                    if (backgroundWorker1.CancellationPending) return;
                    M[j + 1] = M[j];
                    System.Threading.Thread.Sleep(15);
                    SwapInsertion(j + 1, j);
                }
                M[left] = x;
                step++;
                str = " Bước " + step.ToString() + " : ";
                for (int u = 0; u < M.Length; u++) strNew += M[u].ToString() + " ";
                str += strNew;
                if (strNew != strOld) saveQuaTrinh.Text += "\n" + str;
                else step--;
                strOld = strNew;
                strNew = "";
            }
        }
        private void ShakerSort(int[] M)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.ShakerSort(AscRadioButton.Checked);
            step = 0;
            string str = " Dãy chưa sắp : ", strNew = "", strOld = "";
            for (int u = 0; u < M.Length; u++) strOld += M[u].ToString() + " ";
            str += strOld;
            saveQuaTrinh.Text += str;
            int j, left, right, k;
            int n = M.Length;
            left = 0;
            right = n - 1;
            k = n - 1;
            Status st = new Status();
            while (left < right)
            {
                for (j = right; j > left; j--)
                {
                    if (backgroundWorker1.CancellationPending) return;
                    if (((AscRadioButton.Checked == true) && (M[j] < M[j - 1])) || ((DescRadioButton.Checked == true) && (M[j] > M[j - 1])))
                    {
                        int temp = M[j];
                        M[j] = M[j - 1];
                        M[j - 1] = temp;
                        System.Threading.Thread.Sleep(15);
                        Swap(j, j - 1);
                        k = j;

                        step++;
                        str = " Bước " + step.ToString() + " : ";
                        for (int u = 0; u < M.Length; u++) strNew += M[u].ToString() + " ";
                        str += strNew;
                        if (strNew != strOld) saveQuaTrinh.Text += "\n" + str;
                        else step--;
                        strOld = strNew;
                        strNew = "";
                    }
                }
                left = k;
                for (j = left; j < right; j++)
                {
                    if (backgroundWorker1.CancellationPending) return;
                    if (((AscRadioButton.Checked == true) && (M[j + 1] < M[j])) || ((DescRadioButton.Checked == true) && (M[j + 1] > M[j])))
                    {
                        int temp = M[j];
                        M[j] = M[j + 1];
                        M[j + 1] = temp;
                        System.Threading.Thread.Sleep(15);
                        Swap(j + 1, j);
                        k = j;

                        step++;
                        str = " Bước " + step.ToString() + " : ";
                        for (int u = 0; u < M.Length; u++) strNew += M[u].ToString() + " ";
                        str += strNew;
                        if (strNew != strOld) saveQuaTrinh.Text += "\n" + str;
                        else step--;
                        strOld = strNew;
                        strNew = "";
                    }
                }
                right = k;
            }
        }
        #region HeapSort
        void HeapSort(int[] M, int N)
        {
            CodeSort.idea = ideaSort;
            CodeSort.code = showCode;
            CodeSort.HeapSort(AscRadioButton.Checked);
            step = 0;
            string str = " Dãy chưa sắp : ";
            for (int u = 0; u < M.Length; u++) str+= M[u].ToString() + " ";
            saveQuaTrinh.Text +=str;

            CreateHeap(M, N);
            int r;
            r = N - 1;
            if (backgroundWorker1.CancellationPending) return;
            while (r > 0)
            {
                int temp = M[0];
                M[0] = M[r];
                M[r] = temp;

                System.Threading.Thread.Sleep(15);
                Swap(r, 0);
                step++;
                str = " Bước " + step.ToString() + " : ";
                for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                saveQuaTrinh.Text += "\n" + str;
                r--;
                if (r > 0)
                    Shift(M, 0, r);
            }
        }
        void CreateHeap(int[] M, int N)
        {
            int l;
            l = N / 2 - 1;
            while (l >= 0)
            {
                Shift(M, l, N - 1);
                l--;
            }
        }
        void Shift(int[] M, int l, int r)
        {
            int i = l;
            int j = 2 * i + 1;
            int x = M[i];
            while (j <= r)
            {
                if (j < r)
                    if (((AscRadioButton.Checked == true) && (M[j] < M[j + 1])) || ((DescRadioButton.Checked == true) && (M[j] > M[j + 1]))) 
                        j++;
                if (backgroundWorker1.CancellationPending) return;
                if (((AscRadioButton.Checked == true) && (M[i] < M[j])) || ((DescRadioButton.Checked == true) && (M[i] > M[j])))
                {
                    
                    M[i] = M[j];
                    M[j] = x;

                    System.Threading.Thread.Sleep(15);
                    Swap(j, i); 
                    i = j;
                    j = 2 * i + 1;
                    x = M[i];

                    step++;
                    string str = " Bước " + step.ToString() + " : ";
                    for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                    saveQuaTrinh.Text += "\n" + str;
                }
                else return;
            }
        }
        #endregion
        #region QuickSort
        private void Quick_Sort(int[] M, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(M, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(M, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(M, pivot + 1, right);
                }
            }

        }

        private int Partition(int[] M, int left, int right)
        {
            int pivot = M[left];
            while (true)
            {
                while (((AscRadioButton.Checked == true) && (M[left] < pivot)) || ((DescRadioButton.Checked == true) && (M[left] > pivot)))     
                {
                    left++;
                }

                while (((AscRadioButton.Checked == true) && (M[right] > pivot)) || ((DescRadioButton.Checked == true) && (M[right] < pivot))) 
                {
                    right--;
                }

                if (left < right)
                {
                    if (M[left] == M[right]) return right;

                    int temp = M[left];
                    M[left] = M[right];
                    M[right] = temp;

                    System.Threading.Thread.Sleep(15);

                    Swap(right, left);
                    step++;
                    string str = " Bước " + step.ToString() + " : ";
                    for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                    saveQuaTrinh.Text += "\n" + str;
                }
                else
                {
                    return right;
                }
            }
        }
        #endregion
        #region MergeSort


        void merge(int[] M, int l, int m, int r)
        {
           
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;


            int[] R = new int[n2];
            int[] L = new int[n1];

            /* Copy data to temp arrays L[] and R[] */
            for (i = 0; i < n1; i++)
            { L[i] = M[l + i];
             
            }
            for (j = 0; j < n2; j++)
            { R[j] = M[m + 1 + j];
               
            }

            /* Merge the temp arrays back into arr[l..r]*/
            i = 0; // Initial index of first subarray
            j = 0; // Initial index of second subarray
            k = l; // Initial index of merged subarray
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                {
                    M[k] = L[i];
                    
                    i++;
                    


                }
                else
                {
                    M[k] = R[j];
                    
                    j++;
                  

                }
                k++;
            }

            /* Copy the remaining elements of L[], if there
               are any */
            while (i < n1)
            {

                Swap(k, i);
                M[k] = L[i];
               
                i++;
              
                k++;
               
            }
            
            /* Copy the remaining elements of R[], if there
               are any */
            while (j < n2)
            {



                Swap(k, j);
                M[k] = R[j];

                
                j++;
                
                k++;
               
            }
           
            string str = "";
            step++;
            str = " Bước " + step.ToString() + " : ";
            for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
            saveQuaTrinh.Text += "\n" + str;

        }

        /* l is for left index and r is right index of the
           sub-array of arr to be sorted */
        void mergeSort(int[] M, int l, int r)
        {
           
            
            if (l < r)
            {
                // Same as (l+r)/2, but avoids overflow for
                // large l and h
                int m = l + (r - l) / 2;

                // Sort first and second halves
                mergeSort(M, l, m);

                
                mergeSort(M, m + 1, r);
               
                merge(M, l, m, r);
            }
        }
        #endregion
        #endregion

        #region quản lý tiểu trình
        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e) //được thực thi khi gọi hàm RunWorkerSync() 
        {

            if (listSort.selectedValue == "Bubble Sort") BubbleSort(M);
            else
                if (listSort.selectedValue == "Interchange Sort") InterchangeSort(M);
            else
                if (listSort.selectedValue == "Insertion Sort") InsertionSort(M);
            else
                if (listSort.selectedValue == "Selection Sort") SelectionSort(M);
            else
                if (listSort.selectedValue == "Binary Insertion Sort") BinaryInsertionSort(M);
            else
                if (listSort.selectedValue == "Shaker Sort") ShakerSort(M);
            else
                if (listSort.selectedValue == "Heap Sort") HeapSort(M, M.Length);
            else
                if (listSort.selectedValue == "Quick Sort")
            {
                CodeSort.idea = ideaSort;
                CodeSort.code = showCode;
                CodeSort.QuickSort(AscRadioButton.Checked);
                step = 0;
                string str = " Dãy chưa sắp : ";
                for (int u = 0; u < M.Length; u++) str += M[u].ToString() + " ";
                saveQuaTrinh.Text += str;
                Quick_Sort(M, 0, M.Length - 1);
            }
            else
                if (listSort.selectedValue == "Shell Sort") ShellSort(M);
            else
                if (listSort.selectedValue == "Merge Sort")
            {
                CodeSort.idea = ideaSort;
                CodeSort.code = showCode;
                CodeSort.MergeSort(AscRadioButton.Checked);
                
                MergeSort1(M);
            }
            else
            {
                MessageBox.Show("Please Choose Sort !");
                return;
            }
        }
        
        
        private void backgroundWorker1_ProgressChanged_1(object sender, ProgressChangedEventArgs e) //được thực thi khi gọi hàm ReportProgess()
        {
            //Cập nhật giao diện thời gian thực xong chuyển đến hàm dowork
            Status st = e.UserState as Status;
            if (st == null) return;//không làm gì cả
            //dừng đã làm rồi 
            if (st.Type == LoaiDiChuyen.DUNG)//nếu dừng tiến trình thì thay đổi giá trị của 2 nút trong mảng
            {
                ButtonNode tam = nodeArr[st.Vt2];
                nodeArr[st.Vt2] = nodeArr[st.Vt1];
                nodeArr[st.Vt1] = tam;
                return;
            }
            Button btn1 = nodeArr[st.Vt1];
            Button btn2 = nodeArr[st.Vt2];
            if (st.Type == LoaiDiChuyen.DI_LEN_DI_XUONG)
            {
                btn1.Top = btn1.Top + 1;//Nút 1 đi lên
                btn2.Top = btn2.Top - 1;//Nút 2 đi xuống
            }
            else if (st.Type == LoaiDiChuyen.QUA_PHAI_QUA_TRAI)
            {
                btn1.Left = btn1.Left - 1;//nút 1 qa phải
                btn2.Left = btn2.Left + 1;//Nút 2 di chuyển qua trái
            }
            else if (st.Type == LoaiDiChuyen.DI_XUONG_DI_LEN)
            {
                btn1.Top = btn1.Top - 1;//Nút 1 đi xuống
                btn2.Top = btn2.Top + 1;//Nút 2 đi lên
            }

        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e) //hàm kết thúc tiểu trình, được thực thi sau khi hàm DoWork thực thi xong, hoặc người dùng chọn hủy tiến trình 
        {
            MessageBox.Show("Mô phỏng kết thúc");
            deletebuttonnode();
            RandomGenerateBtn.Enabled = true;
            ManualGenerateBtn.Enabled = true;
            StartBtn.Enabled = true;

        }
    

        private void deletebuttonnode()
        {
            foreach (Control node in nodeArr)
            {
                node.Dispose();
            }
            nodeArr.Clear();
            if (IsPause) { IsPause = false; }
        }
        #endregion

        //Thông tin phần mềm
        private void AboutBtn_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        //Hướng dẫn sử dụng
        private void InstructionBtn_Click(object sender, EventArgs e)
        {
            InstrucForm instrucForm = new InstrucForm();
            instrucForm.Show();
        }



        #region các sự kiện taskbar
        private void TaskBar_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void TaskBar_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
            x = e.X;
            y = e.Y;
        }

        private void TaskBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.SetDesktopLocation(Cursor.Position.X - x, Cursor.Position.Y - y);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                Application.Exit();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        #region bỏ
        private void listSort_onItemSelected(object sender, EventArgs e)
        {
            
        }

        private void showCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TaskBar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #endregion


    }
}

