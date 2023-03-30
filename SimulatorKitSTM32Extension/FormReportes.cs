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
    public partial class FormReportes : Form
    {
        int tickStart = 0; // khởi động timer
        int totalCount = 0;
        int ramUsed = 0;
        int tickTime = 0;

        public FormReportes()
        {
            InitializeComponent();

            GraphT1();
        }

        private void GraphT1()
        {
            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // Dat ten cho do thi
            GraphPane myPane = zedGraphControl1.GraphPane;
            zedGraphControl1.GraphPane.Title.Text = "SỐ LƯỢNG RAM TIÊU THỤ";

            // Fill the axis background with a gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, Color.White), 45.0F);
            myPane.XAxis.MajorGrid.IsVisible = true; // Tao luoi theo truc doc
            myPane.YAxis.MajorGrid.IsVisible = true; // Tao luoi theo truc ngang

            // Tao khung thong bao va dinh nghia cho list
            RollingPointPairList list_count_RTOS = new RollingPointPairList(60000);

            LineItem curve_totalcount = myPane.AddCurve("TotalRam", list_count_RTOS, Color.Red, SymbolType.None); // 3 đường để vẽ

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
            myPane.YAxis.Title.Text = "TotalRam"; // Ten truc Y
            myPane.YAxis.Scale.Min = 0;
            myPane.YAxis.Scale.Max = 7000;
            myPane.YAxis.Scale.MinorStep = 100;
            myPane.YAxis.Scale.MajorStep = 500;

            // Ham xac dinh co truc 
            zedGraphControl1.AxisChange();
            // Force a redraw
            zedGraphControl1.Invalidate();
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

        public void DrawT1(string a, string b)
        {
            // Tao 1 curve
            double int_a;
            double int_b;
            double.TryParse(a, out int_a); // chuyển đổi dữ liệu kiểu string sang kiểu double
            double.TryParse(b, out int_b);

            if (zedGraphControl1.GraphPane.CurveList.Count <= 0)
                return;

            // Tao item curve tren do thi
            LineItem curve_countTotal = zedGraphControl1.GraphPane.CurveList[0] as LineItem;

            if (curve_countTotal == null) // kiểm tra đường curve đã được khởi tạo chưa,nếu chưa trả về chương trình chính
                return;


            // Lay pointpairlist
            IPointListEdit list_countTotal = curve_countTotal.Points as IPointListEdit; // kiểm tra tất cả các điểm của đường cong

            if (list_countTotal == null)
                return;

            list_countTotal.Add(int_a, int_b);

            // Vẽ đồ thị
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void SaveToExcel(ListView listview)
        {
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult resultDialog;
            resultDialog = MessageBox.Show("Do you save data?", "Save", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (resultDialog == DialogResult.OK)
            {
                //SaveToExcel(listViewT2);
            }
        }
    }
}
