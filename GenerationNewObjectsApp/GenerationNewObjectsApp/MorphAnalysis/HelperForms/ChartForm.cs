using MorphAnalysis.XMLDoc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChartDirector;

namespace MorphAnalysis.HelperForms
{
    public partial class ChartForm : Form
    {
        Dictionary<string, List<BestResultGA>> _resultGAByNameProjectGroup;

        private ChartDirector.WinChartViewer _viewer;

        string[] titleAxisXYZ;

        int chooseItem;

        delegate void MethodOperation(List<BestResultGA> group, ref double[] x, ref double[] y, ref double[] z, ref int i);

        public ChartForm()
        {
            InitializeComponent();
        }

        public ChartForm(string[] titleXYZ, int chooseItem) : this()
        {
            titleAxisXYZ = titleXYZ;
            this.chooseItem = chooseItem;
        }

        private void ChartForm_Load(object sender, EventArgs e)
        {
            XMLDocReader doc = new XMLDocReader("resultGA.xml");
            _resultGAByNameProjectGroup = doc.GetProjectGroup;
            _viewer = winChartViewerResult;
            BuildChart(_viewer, titleAxisXYZ, chooseItem);
        }


        private void BuildChart(WinChartViewer winChartViewerResult, string[] titleXYZ, int chooseItem)
        {

            //Налаштування графіку

            // Create a ThreeDScatterChart object of size
            ThreeDScatterChart c = new ThreeDScatterChart(750, 550);

            // Add a title to the chart
            c.addTitle("3D Scatter Groups", "Times New Roman Italic", 14);

            // Set the center of the plot region at (350, 240), 
            //and set width x depth x height to last three parameters
            c.setPlotRegion(300, 240, 350, 350, 350);

            // Set the elevation and rotation angles to 15 and 30 degrees
            c.setViewAngle(15, 30);

            // Add a legend box at 
            c.addLegend(650, 20);


            MethodOperation methodOperation = GetDelegateOperation(chooseItem);

            double[] x;
            double[] y;
            double[] z;

            //Додавання группи для графіку по назві проекту
            foreach (KeyValuePair<string, List<BestResultGA>> group in _resultGAByNameProjectGroup)
            {
                //розмір групи
                int countElementsInGroup = group.Value.Count;
                x = new double[countElementsInGroup];
                y = new double[countElementsInGroup];
                z = new double[countElementsInGroup];
                int i = 0;
                //наповнити групу
                methodOperation(group.Value, ref x, ref y, ref z, ref i);

                //добавити групу точок
                c.addScatterGroup(x, y, z, group.Key, Chart.GlassSphere2Shape, 10);
            }

            // Set the x, y and z axis titles
            c.xAxis().setTitle(titleXYZ[0]);
            c.yAxis().setTitle(titleXYZ[1]);
            c.zAxis().setTitle(titleXYZ[2]);

            // Output the chart
            winChartViewerResult.Chart = c;   

            winChartViewerResult.ImageMap = c.getHTMLImageMap("chart");//, "", "x={x}&y={y}&z={z}&dataSet={dataSet}&dataSetName={name}");
           // winChartViewerResult.ClickHotSpot += WinChartViewerResult_ClickHotSpot;
            //Зберегти графік в зображення
            //c.makeChart("chart.png");

        }

        private MethodOperation GetDelegateOperation(int chooseItem)
        {
            MethodOperation methodOperation;
            switch (chooseItem)
            {
                case 1:
                    methodOperation = GetGroupResult_CountFuncBestEpochEstimate;
                    break;
                case 2:
                    methodOperation = GetGroupResult_CountFuncMaxPopulationEstimate;
                    break;
                case 3:
                    methodOperation = GetGroupResult_BestEpochMaxPopulationEstimate;
                    break;
                default:
                    methodOperation = null;
                    break;
            }
            return methodOperation;
        }

        private void GetGroupResult_CountFuncBestEpochEstimate(List<BestResultGA> group, ref double[] x, ref double[] y, ref double[] z, ref int i)
        {
            foreach (BestResultGA resultGA in group)
            {
                x[i] = resultGA.CountFunction; //x
                y[i] = resultGA.BestEpoch; //y
                z[i] = resultGA.Estimate; //z
                i++;
            }
        }

        private void GetGroupResult_CountFuncMaxPopulationEstimate(List<BestResultGA> group, ref double[] x, ref double[] y, ref double[] z, ref int i)
        {
            foreach (BestResultGA resultGA in group)
            {

                x[i] = resultGA.CountFunction; //x
                y[i] = resultGA.MaxPopulation; //y
                z[i] = resultGA.Estimate; //z
                i++;
            }
        }

        private void GetGroupResult_BestEpochMaxPopulationEstimate(List<BestResultGA> group, ref double[] x, ref double[] y, ref double[] z, ref int i)
        {
            foreach (BestResultGA resultGA in group)
            {
                x[i] = resultGA.BestEpoch; //x
                y[i] = resultGA.MaxPopulation; //y
                z[i] = resultGA.Estimate; //z
                i++;
            }
        }
    }
}
