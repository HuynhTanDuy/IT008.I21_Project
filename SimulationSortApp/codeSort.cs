using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulationSortApp
{
    class CodeSort
    {
        public static ListBox code;
        public static RichTextBox idea;

        private static string[] Convert(string textCode)
        {
            string[] textCodes;
            textCodes = textCode.Split('\n');
            return textCodes;
        }

        //Hiển thị idea+ code của các thuật toán
        public static void BubbleSort(bool asc)
        {
            string textIdea = @"Xuất phát từ cuối dãy,đổi chỗ các cặp phần tử kế cận để đưa phần tử nhỏ hơn hoặc lớn hơn trong cặp phần tử đó về vị trí đúng đầu dãy hiện hành, sau đó sẽ không xét đến nó ở bước tiếp theo,do vậy ở lần xử lý thứ i sẽ có vị trí đầu dãy là i. Lặp lại xử lý trên cho đến khi không còn cặp phần tử nào để xét";
            string[] textCode=Convert(
@"Sắp tăng                
void BubbleSort(int a[], int N)
{
   int i,j;
   for(i = 0; i < N - 1; i++)
      for(j = N - 1; j > i; j--)
        if(a[j] < a[j - 1])
            Swap(a[j], a[j - 1]);
}");

            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach(string item in textCode)
            {
                code.Items.Add(item);
            }
            if (asc == false) 
            {
                code.Items[0] = "Sắp giảm";
                code.Items[6] = "       if(a[j] > a[j - 1])";
            }
        }

        public static void SelectionSort(bool asc)
        {
            string textIdea = @"Chọn phần tử nhỏ nhất hoặc lớn nhất trong N phần tử trong dãy hiện hành. Đưa phần tử này về vị trí đầu dãy hiện hành. Xem dãy hiện hành chỉ còn N-1 phần tử của dãy hiện hành ban đầu. Bắt đầu từ vị trí thứ 2. Lặp lại quá trình trên cho dãy hiện hành... đến khi dãy hiện hành chỉ còn 1 phần tử";
            string[] textCode = Convert(
@"Sắp tăng:
void SelecttionSort(int arr[], int N)
{
    int min, i, j;
    for (i=0; i < N-1; i++)
    {
        min = i;
        for (j=i+1; j <N; j++)
            if (a[j] < a[min])
                    min=j;
        Swap(a[min], a[i]);   
    } 
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            // nếu là giảm thì sửa lại
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[8] = "            if (a[j] > a[min])";
            }
        }

        public static void InsertionSort(bool asc)
        {
            string textIdea = @"Giả sử có một dãy a(0),a(1),...,a(n-1) trong đó i phần tử đầu tiên a(0),a(1),...,a(i-1) đã có thứ tự. Tìm cách chèn phần tử a(i) vào vị trí thích hợp của đoạn đã được sắp để có dãy mới a(0),a(1),...,a(i) trở nên có thứ tự. Vị trí này chính là vị trí giữa hai phần tử a(k-1) và a(k) thỏa a(k-1)<a(i)<a(k) (1<=k<=i)";
            string[] textCode = Convert(
@"Sắp tăng
                  
void InsertionSort(int a[], int N)
{
    int pos, i;
    int x;
    for(i = 1; i < N; i++)
    {
        x = a[i]; pos = i - 1;
        while((pos >= 0) && (x < a[pos]))
        {
            a[pos + 1] = a[pos];
            pos--;
        }
        a[pos + 1] = x;
    }
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[9] = "        while((pos >= 0) && (x > a[pos]))";
            }
        }

        public static void BinaryInsertionSort(bool asc)
        {
            string textIdea = @"Cải tiến của giải thuật InsertionSort. Giải thuật chèn nhị phân cho phép giảm số lần so sánh, nhưng không làm giảm số lần dời chỗ ";
            string[] textCode = Convert(
@"Sắp tăng
void BinaryInsertionSort(int a[], int N)
{
   int left, right, m, i , pos;
   int x;
   for(int i = 1; i < N ; i++)
   {
      x = a[i]; left = 0; right = i - 1;
      while(left <= right)
      {
         m = (left + right)/2;
         if(x < a[m]) right = m - 1;
         else left = m + 1;                    
      }
      for(pos = i - 1; pos >= left; pos--)
         a[pos+1] = a[pos];
      a[left] = x;
    }
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[11] = "         if(x > a[m]) right = m - 1;";
            }
        }

        public static void InterchangeSort(bool asc)
        {
            string textIdea = @"Xuất phát từ đầu dãy,tìm tất cả các cặp nghịch thế chứa phần tử này, triệt tiêu chúng bằng cách đổi phần tử này với phần tử tương ứng trong cặp nghịch thế .Lặp lại xử lý trên với các phần tử tiếp theo";
            string[] textCode = Convert(
@"Sắp tăng
void InterchangeSort( int a[], int N)
{
    int i, j;
    for(i = 0; i < N - 1; i++)
        for(j = i + 1; j < N; j++)
            if( a[j] < a[i] )
                Swap( a[i], a[j]);
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[6] = "            if( a[j] > a[i] )";
            }

        }

        public static void ShakerSort(bool asc)
        {
            string textIdea =
@"Trong mỗi lần sắp xếp, duyệt mảng theo 2 lượt từ 2 phía khác nhau:
-Lượt đi : đẩy phần tử nhỏ(lớn) về đầu mảng.
-Lượt về : đẩy phần tử lớn(nhỏ) về cuối mảng.
Ghi nhận lại những đoạn đã sắp xếp nhằm tiết kiệm các phép so sánh ";
            string[] textCode = Convert(
@"Sắp tăng
void ShakerSort(int a[], int N)
{
    int j, left, right, k;
    left = 0; right = N - 1, k = N - 1;
    while(left < right)
    {
    for(j = right; j > left; j--)
        if(a[j] < a[j - 1])
        {
            Swap(a[j], a[j - 1]);
            k = j;
        }
    left = k;
    for(j = left; j < right; j++)
        if(a[j + 1] < a[j])
        {
            Swap(a[j], a[j + 1]);
            k = j;
        }
    right = k;
    }
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }

            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[8] = "        if(a[j] > a[j - 1])";
                code.Items[15] = "        if(a[j + 1] > a[j])";
            }

        }

        public static void HeapSort(bool asc)
        {
            string textIdea = @"Khi tìm phần tử nhỏ nhất(lớn nhất) ở bước i, phương pháp SelectionSort không tận dụng được các thông tin đã có được do các phép so sánh ở bước i. Giải thuật HeapSort khắc phục nhược điểm này bằng cách chọn ra được một cấu trúc dữ liệu cho phép tích lũy các thông tin về sự so sánh giá trị các phần tử trong quá trình sắp xếp";
            string[] textCode = Convert(
@"Sắp tăng    
void HeapSort(int a[], int N)
{
    CreateHeap(a,N - 1);
    int r;
    r = N - 1;
    while(r >= 0)
    {
        Swap(a[0], a[r]);
        r--;
        if(r > 0 )
            Shift(a,0,r);
    }
}

void CreateHeap(int a[], int N)
{
    int l;
    l = N/2 - 1;
    while(l >= 0)
    {
        Shift(a,l,N - 1);
        l--;
    }
}

void Shift(int a[], int l, int r)
{
    int i = l;
    int j = 2*i + 1;
    while(j <= r)
    {
        if(j < r && a[j] < a[j+1]) 
            j++;
        if(a[i] < a[j])
        {
            Swap(a[i], a[j]);
            i = j; 
            j = 2*i + 1;
        }
        else return;
    }
}
");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[32] = "        if(j < r && a[j] > a[j+1]) ";
                code.Items[34] = "        if(a[i] > a[j])";
            }

        }

        public static void QuickSort(bool asc)
        {
            string textIdea = @"Sắp xếp dãy a(1),a(2),...,a(n) dựa trên việc phân hoạch dãy ban đầu thành 3 phần :
-Phần 1 : Gồm các phần tử có giá trị bé hơn x.
-Phần 2 : Gồm các phần tử có giá trị bằng x.
-Phần 3 : Gồm các phần tử có giá trị lớn hơn x
(Với x là giá trị của một phần tử tùy ý trong dãy ban đầu)";

            string[] textCode = Convert(

@"Sắp tăng   
void QuickSort(int a[], int left, int right)
{
    int i, j, x;
    x = a[(left + night)/2];
    i = left, j = right;
    do
    {
        while(a[i] < x)
            i++;
        while(x < a[j])
            j--;
        if(i <= j)
        {
            Swap(a[i], a[j]);
            i++, j--;
        }            
    }while(i <= j);
    if(left < j)
        QuickSort(a, left, j);
    if(i < right)
        QuickSort(a, i, right);
}
");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[8] = "        while(a[i] > x)";
                code.Items[10] = "        while(x > a[j])";
            }

        }

        public static void MergeSort(bool asc)
        {
            string textIdea = @"Sắp xếp dãy a(1),a(2),...,a(n) dựa trên nhận xét sau :
-Mỗi dãy a(1),a(2),...,a(n) bất kỳ là một tập hợp các dãy con liên tiếp mà mỗi dãy con đều đã có thứ tự. 
-Dãy đã có thứ tự coi như có 1 dãy con.           
Hướng tiếp cận : tìm cách làm giảm số dãy con không giảm của dãy ban đầu";

            string[] textCode = Convert(
  @"Sắp tăng
int b[MAX], c[MAX], nb, nc;
int Min(int a, int b)
{
    if(a > b) return b;
    else return a;
}
void Distribute(int a[], int N, int &nb, int &nc, int k)
{
    int i, pa, pb, pc;
    pa = pb = pc = 0 ;
    while(pa < N)
    {
        for(i = 0; (pa < N) && (i < k); i++, pa++, pb++)
            b[pb] = a[pa];
        for(i = 0; (pa < N) && (i < k); i++, pa++, pc++)
            c[pc] = a[pa];
    }
    nb = pb; nc = pc;
}
void Merge(int a[], int nb, int nc, int k)
{
    int p, pb, pc, ib, ic, kb, kc;
    p = pb = pc = 0; ib = ic = 0;
    while((nb > 0) && (nc > 0))
    {
        kb = Min(k, nb); kc = Min(k, nc);
        if(c[pc + ic] < b[pb + ib] == false)
        {
            a[p++] = b[pb + ib]; ib++;
            if(ib == kb)
            {
                for(;ic < kc; ic++)
                    a[p++] = c[pc + ic];
                pb += kb; pc += kc; ib = ic = 0;
                nb -= kb; nc -= kc;
            }
        }
        else
        {
            a[p++] = c[pc + ic]; ic++;
            if(ic == kc)
            {
                for(;ib < kb; ib++)
                    a[p++] = b[pb + ib];
                pb += kb; pc += kc; ib = ib = 0;
                nb -= kb; nc -= kc;
            }
        }
    }
}
void MergeSort(int a[], int N)
{
        int k;
        for(k = 1; k < N; k*= 2)
        {
            Distribute(a, N, nb, nc, k);
            Merge(a, nb, nc, k);
        }
}");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if (!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[27] = "        if(c[pc + ic] > b[pb + ib] == false)";
            }
        }

        public static void ShellSort(bool asc)
        {
            string textIdea = @"Cải tiến phương pháp InsertionSort. Phân hoạch dãy thành các dãy con. Sắp xếp các dãy con theo phương pháp InsertionSort. Dùng phương pháp InsertionSort sắp xếp lại cả dãy. Tìm k bước với các khoảng cách chọn theo công thức h(i) = (h(i-1) - 1)/2 và h(k) = 1, k = lg(n)/lg(2) - 1.";
            string[] textCode = Convert(
@"Sắp tăng
void ShellSort (int a[], int N)
{
    for (int gap = N / 2; gap > 0; gap /= 2)
    {
        for (int i = gap; i < N; i++)
        {
            for (int j = i; j >= gap && a[j] < a[j - gap]; j -= gap)
            {
                Swap(a[j], a[j - gap]);
            }
        } 
    }
}             
 ");
            idea.Clear();
            idea.Text = textIdea;
            code.Items.Clear();
            foreach (string item in textCode)
            {
                code.Items.Add(item);
            }
            if(!asc)
            {
                code.Items[0] = "Sắp giảm";
                code.Items[7] = "for (int j = i; j >= gap && a[j] > a[j - gap]; j -= gap)";
            }

        }
    }
}
