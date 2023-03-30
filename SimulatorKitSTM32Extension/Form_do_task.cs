using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace SimulatorKitSTM32Extension
{
    public partial class Form_do_task : Form
    {
        int tickStart = 0; // khởi động timer
        int totalCount = 0;
        int ramUsed = 0;
        int tickTime = 0;
        public Form_do_task()
        {
            InitializeComponent();
            //zedGraphControl_do_task();
            GraphT2();
        }
        private void GraphT2()
        {
            // clear old curves
            zedGraphControl_do_task.GraphPane.CurveList.Clear();

            // Dat ten cho do thi
            GraphPane myPane = zedGraphControl_do_task.GraphPane;
            zedGraphControl_do_task.GraphPane.Title.Text = "SỐ LƯỢNG TASK THỰC HIỆN";

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, Color.White), 45.0F);
            myPane.XAxis.MajorGrid.IsVisible = true; // Tao luoi theo truc doc
            myPane.YAxis.MajorGrid.IsVisible = true; // Tao luoi theo truc ngang

            // Tao khung thong bao va dinh nghia cho list
            RollingPointPairList list_count_RTOS = new RollingPointPairList(60000);

            LineItem curve_totalcount = myPane.AddCurve("TotalCount", list_count_RTOS, Color.Red, SymbolType.None); // 3 đường để vẽ

            if (curve_totalcount == null)
                return;

            // kich thuoc duong day
            curve_totalcount.Line.Width = 2.0F;

            // Thiet lap dinh dang truc X
            myPane.XAxis.Title.Text = "TickTime"; // Ten truc X
            myPane.XAxis.Scale.Min = 0;
            myPane.XAxis.Scale.Max = 2000;
            myPane.XAxis.Scale.MinorStep = 10; //Buoc nho la 1s
            myPane.XAxis.Scale.MajorStep = 50; //Buoc lon la 1 phut

            // Thiet lap dinh dang truc Y
            myPane.YAxis.Title.Text = "TotalCount"; // Ten truc Y
            myPane.YAxis.Scale.Min = 1;
            myPane.YAxis.Scale.Max = 100;
            myPane.YAxis.Scale.MinorStep = 1;
            myPane.YAxis.Scale.MajorStep = 5;

            // Ham xac dinh co truc 
            zedGraphControl_do_task.AxisChange();
            // Force a redraw
            zedGraphControl_do_task.Invalidate();
        }

        public int get_totalCount()
        {
            return totalCount;
        }

        public void set_totalCount(int value)
        {
            totalCount = value;
        }

        public int get_ramused()
        {
            return ramUsed;
        }

        public void set_ramused(int value)
        {
            ramUsed = value;
        }

        public int get_ticktime()
        {
            return tickTime;
        }

        public void set_ticktime(int value)
        {
            tickTime = value;
        }

        public void DrawT2(string a, string b)
        {
            // Tao 1 curve
            double int_a;
            double int_b;
            double.TryParse(a, out int_a); // chuyển đổi dữ liệu kiểu string sang kiểu double
            double.TryParse(b, out int_b);

            if (zedGraphControl_do_task.GraphPane.CurveList.Count <= 0)
                return;

            // Tao item curve tren do thi
            LineItem curve_countTotal = zedGraphControl_do_task.GraphPane.CurveList[0] as LineItem;

            if (curve_countTotal == null) // kiểm tra đường curve đã được khởi tạo chưa,nếu chưa trả về chương trình chính
                return;


            // Lay pointpairlist
            IPointListEdit list_countTotal = curve_countTotal.Points as IPointListEdit; // kiểm tra tất cả các điểm của đường cong

            if (list_countTotal == null)
                return;

            list_countTotal.Add(int_a, int_b);

            // Vẽ đồ thị
            zedGraphControl_do_task.AxisChange();
            zedGraphControl_do_task.Invalidate();
        }

    }
}
