using System;
using ChartDirector;

namespace CSharpWPFDemo
{
    public class threedpie : DemoModule
    {
        //Name of demo module
        public string getName() { return "3D Pie Chart"; }

        //Number of charts produced in this demo module
        public int getNoOfCharts() { return 1; }

        //Main code for creating chart.
        //Note: the argument chartIndex is unused because this demo only has 1 chart.
        public void createChart(WPFChartViewer viewer, int chartIndex)
        {
            // The data for the pie chart
            double[] data = {25, 18, 15, 12, 8, 30, 35};

            // The labels for the pie chart
            string[] labels = {"Labor", "Licenses", "Taxes", "Legal", "Insurance", "Facilities",
                "Production"};

            // Create a PieChart object of size 360 x 300 pixels
            PieChart c = new PieChart(360, 300);

            // Set the center of the pie at (180, 140) and the radius to 100 pixels
            c.setPieSize(180, 140, 100);

            // Add a title to the pie chart
            c.addTitle("Project Cost Breakdown");

            // Draw the pie in 3D
            c.set3D();

            // Set the pie data and the pie labels
            c.setData(data, labels);

            // Explode the 1st sector (index = 0)
            c.setExplode(0);

            // Output the chart
            viewer.Chart = c;

            //include tool tip for the chart
            viewer.ImageMap = c.getHTMLImageMap("clickable", "",
                "title='{label}: US${value}K ({percent}%)'");
        }
    }
}

