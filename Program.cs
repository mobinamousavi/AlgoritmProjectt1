using using System;
using using System.Collections.Generic;

namespace AlgoProject1
{
    internal class MaxHeap    //کلاس هیپ
    {
        private int[] array;    //از آرایه استفاده میکنیم
        private int size;       //آرایه سایز داره
        public MaxHeap(int n)    //ایجاد کانستراکتور
        {
            array = new int[n];
            size = 0;
        }

        private static int Left(int n)   //اندیس می گیرد و فرزند چپ را می دهد
        {
            return n * 2 + 1;
        }
        private static int Right(int n)    //اندیس می گیرد و فرزند راست را می دهد
        {
            return n * 2 + 2;
        }
        private static int Parent(int n)   //اندیس می گیرد و والد را می دهد
        {
            return (n - 1) / 2;
        }

        public void Insert(int n)   //اضافه کردن به ماکس هیپ 
        {
            if (size == array.Length)  //سایز آرایه برابر است با طولش
                throw new InvalidOperationException("Heap is full");   //درخت پر است

            array[size] = n;    //در غیر این صورت  یکی به سایز آرایه اضافه می شود
            shiftUp(size);
            size++;
        }
        private void shiftUp(int n)   //زمانی که یک نود جدید وارد کردیم به ماکس هیپ
        {
            int P = Parent(n);   
            if (P >= 0 && array[n] > array[P])   //اگر مقدار نود جدید بزرگتر از  والدش باشد جاش درست نیست و باید بره بالاتر
            {
                //swap P and n
                array[P] += array[n];
                array[n] = array[P] - array[n];
                array[P] -= array[n];

                shiftUp(P);
            }
        }
        public string ToString()  //نمایش بده
        {
            string s = "[";
            foreach (int n in array)
            {
                s += n.ToString() + ", ";
            }
            return s + "]";
        }
    }

    internal class Node   //کلاس نود
    {
        public int value;    //مقدار

        public Node? left;    //فرزند چپ
        public Node? right;   //فرزند راست
        public Node? parent;   //والد

        public Node(int value)    //ایجاد کانستراکتور برای فیلدها 
        {
            this.value = value;
            left = null;
            right = null;
            parent = null;
        }
    }

    internal class BST   //کلاس درخت باینری
    {
        public Node? root;   //ریشه یعنی بالاترین نود را نگه می دارد
        public BST()    //ایجاد کانستراکتور 
        {
            root = null;    //ریشه خالیه 
        }

        static Node Minimum(Node node)   //یک نود میگیرد و نودی که گرفته ریشه ی زیردرخت مینیمم است
        {
            while (node.left != null) node = node.left;    //در مینیمم چپ ترین نود مینیمم است
            return node;//یعنی در واقع انقدر زیر درخت های چپ میای پایین که دیگه زیر درخت چپی وجود نداشته باشه که اون گره کوچک ترینه
        }
        static Node Maximum(Node node)    //یک نود میگیرد و نودی که گرفته ریشه ی زیردرخت ماکسیمم است
        {
            while (node.right != null) node = node.right;//در ماکزیمم راست ترین نود ماکزیمم است
            return node;//یعنی در واقع انقدر زیر درخت های راست میای پایین که دیگه زیر درخت راستی وجود نداشته باشه که اون گره بزرگترینه
        }
        private Node? Search(int n)   //برای تابع حذف کار میکنن یه مقدار ورودی می گیرد
        {
            Node? node = root;  //از ریشه شروع می شود
            while (node != null && n != node.value)      //تا زمانی که ریشه و مقدار نود ورودی داشته باشیم
            {
                if (n < node.value) node = node.left;   //مقدار ورودی کوچک تر از ریشه باشد باید فرزند چپ بگردد
                else node = node.right;     //مقدار ورودی بزرگتر از ریشه باشد باید فرزند راست بگردد
            }
            return node;
        }
        private void Transplant(Node old, Node @new)  //این تابع دو تا نود می گیرد و نود قبلی با نود جدید جایگزین می کند
        {
            if (old.parent == null)   //نود قبلی اگر مقدار نداشته باشه 
                root = @new;   //ریشه میشه مقدار جدید
            else if (old == old.parent.left) old.parent.left = @new;  //اگر والد داشته باشه یعنی یا فرزند چپ والده یا فرزند راست است.اگر نود قبلی فرزند چپ والد باشد نود جدید میگذارد جای فرزند چپ والد نود قبلی
            else old.parent.right = @new;//اگر فرزند راست والد باشد نود جدید می گذارد جای فرزند راست والد نود قبلی 
            if (@new != null)  //اگر نود جدید مقدار نداشت
                @new.parent = old.parent; //والد نود قبلی میزاره جای والد نود جدید
        }
        private void Delete(Node z)//یک نود می گیرد. برای حذف کردن یک نود از درخت چهار تا حالت داریم که به فرزنطدان بستگی دارد
        {      //اگر فرزند نداشته باشه خودش مساوی null قرار می دهیم
            if (z.left == null)   //اگر فقط فرزند راست داشته باشد فرزند چپ نداشته باشه
                Transplant(z, z.right);  //فرزند  راست میاد جای این
            else if (z.right == null)   //اگر فرزند راست نداشته باشد و فرزند چپ داشته باشد
                Transplant(z, z.left);    //فرزند چپ میزاریم جای خودش از همون تابع ترنزپلنت استفاده میکنیم
            else    // هر دو تا  فرزند را داشته باشد
            {
                Node y = Minimum(z.right); //مینیمم  پیدا کنیم
                if (y.parent != z)   //باز دو حالت داره 
                {
                    Transplant(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                Transplant(z, y);
                y.left = z.left;
                y.left.parent = y;
            }
        }

        public void Insert(int n)  //یه مقدار صحیح می گیرد و از آن یک نود درست می کند  و در درخت می گذارد
        { //برای اضافه کردن نود جدید باید والد هم داشته باشیم
            Node? z = new Node(n);   //نود ایجاد شده که در داخل درخت است
            Node? y = null;      //ye pele qabltar az x ast y
            Node? x = root;     //  استzدرخت را پیمایش می کند تا به جایی برسد که نود 
            while (x != null)   //پیمایش می کند درخت تا جای مناسب برای نود جدید پیدا کند
            {
                y = x;   //مقدار قبلی x را نگه دارد
                if (z.value < x.value)  // مقدار جدید است z
                    x = x.left;   // xباید بره سمت چپ 
                else
                    x = x.right;    // xباید بره سمت راست 
            }  //enqadr mire payin ta x null shavad va nude jadid onja qarar begire
            z.parent = y; //valed z ra y qarar midahim chon ke y ye pele aqabtar negah midasht ta bebine valed nude jadid ki bashad
            if (y == null) root = z;  //درخت خالی است و نود جدید که وارد شد باید ریشه باشد
            else if (z.value < y.value) y.left = z;  //agar meqdar y bozorgtar az z bashadz bayad farzand chap y bashad
            else y.right = z;  //agar meqdar y kochaktar az z bashadz bayad farzand y bashad farzand rast bashad
        }
        static Node Next(Node node)//یک نود می گیرد و بعدی اشو نشون میده . دو تا هست  ازش یکی نود می گیرد و بعدی نشون میده اون یکی عدد صحیح می گیرد
        {
            if (node.right != null) return Minimum(node.right);//تا زمانی که فرزند راست داشته باشیم کوچک ترین مقدار توش پیدا می کند
            Node? y = node.parent;  //اگر نداشته باشه یه متغیر میگیره که از جنس نود که میشه والد نود فعلی
            while (y != null && node == y.right)//ta zamani ke null nist va nude farzand rast y bashad yaani enadr mire bala ta in nude farzand chap y bashad
            {
                node = y;
                y = y.parent;
            }
            if (y == null) throw new Exception("out of bound"); //agar enqadr bala raft ta be rishe resid va natonest peida konad exeption mide
            return y;
        }
        public Node Next(int n)  //مقدار می گیرد 
        {
            return Next(Search(n));   //نود مربوط به اون مقدار را پیدا کند و به تابع اصلی می دهد
        }
        static Node Previous(Node node)  //نود قبلی پیدا کند 
        {
            if (node.left != null) return Maximum(node.left);   //تا زمانی که فرزند چپ داشته باشیم بیشترین مقدار نود چپ بر می گرداند
            Node? y = node.parent; //اگر نداشته باشه یه متغیر میگیره که از جنس نود که میشه والد نود فعلی
            while (y != null && node == y.left)//ta zamani ke null nist va nude farzand chap y bashad yaani enadr mire bala ta in nude farzand rast y bashad
            {
                node = y;
                y = y.parent;
            }
            if (y == null) throw new Exception("out of bound");//agar enqadr bala raft ta be rishe resid va natonest peida konad exeption mide
            return y;
        }
        public Node Previous(int n)   //مقدار داخل گره را بگیرد و با استفاده از تابع سرچ پیدا کند و به تابع اصلی بدهد
        {
            return Previous(Search(n));
        }
        public void Delete(int n)
        {
            Delete(Search(n));
        }
        public MaxHeap kLargestToHeap(int k)   //یه متغیر میگیره برای بزرگ ترین اعداد درخت دودویی 
        {
            Node current = Maximum(root);  // نود فعلی  برابر می شود با بزرگ ترین عدد درخت  و وارد هیپ می شود 
            MaxHeap heap = new MaxHeap(k);  //k بار عددهای قبلی هم وارد هیپ میکنیم
            while (k > 0)   //k  بار انجام می شود
            {
                heap.Insert(current.value);    //نود فعلی را وارد می کند
                current = Previous(current);   //نود قبلی قرار می دهد
                k--;    //بار که این کارو تکرار میکنه  k
            }
            return heap;
        }

        public void PrintInorder()   //پرینت کردن  درخت به صورت indorder
        {
            if (root == null) Console.WriteLine("BST is empty");  //اگر ریشه نداشتیم درخت خالی است
            else
            {
                PrintInorder(root);
                Console.WriteLine();
            }
        }
        public static void PrintInorder(Node n)   // پرینت کردن اینجا یه نود میگیره
        {
            if (n == null) return;
            PrintInorder(n.left);  //زیر درخت چپ بعد ریشه زیر درخت راست به ترتیب چاپ کند
            Console.Write("{0} ", n.value);
            PrintInorder(n.right);
        }
    }
    class program
    {
        static void Main(string[] args)
        {
            BST bST = new BST();
            while (true)
            {
                Console.WriteLine("1-insert new node\n2-show next node\n3-show previous node\n4-delete node\n5-turn k largest nodes to max heap\n6-show BST\nenter a number:");
                int input = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (input)
                {
                    case 1:
                        {
                            Console.Write("enter new nodes value: ");
                            int i = int.Parse(Console.ReadLine());
                            bST.Insert(i);
                            Console.WriteLine("{0} added to the BST", i);
                            break;
                        }
                    case 2:
                        {
                            Console.Write("enter node's value: ");
                            int i = int.Parse(Console.ReadLine());
                            Console.WriteLine("next value is: {0}", bST.Next(i).value);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("enter node's value: ");
                            int i = int.Parse(Console.ReadLine());
                            Console.WriteLine("previous value is: {0}", bST.Previous(i).value);
                            break;
                            break;
                        }
                    case 4:
                        {
                            Console.Write("enter node's value: ");
                            int i = int.Parse(Console.ReadLine());
                            bST.Delete(i);
                            Console.WriteLine("{0} deleted from the BST", i);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("enter k: ");
                            int k = int.Parse(Console.ReadLine());
                            Console.WriteLine(bST.kLargestToHeap(k).ToString());
                            break;
                        }
                    case 6:
                        {
                            bST.PrintInorder();
                            Console.WriteLine();
                            break;
                        }
                }
            }

        }
    }
}