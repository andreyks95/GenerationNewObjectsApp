<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

//
// Create chart
//
protected void createChart(WebChartViewer viewer, int chartIndex)
{
    // The x and y coordinates of the grid
    double[] dataX = {-10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10};
    double[] dataY = {-10, -8, -6, -4, -2, 0, 2, 4, 6, 8, 10};

    // The values at the grid points. In this example, we will compute the values using the formula
    // z = x * sin(y) + y * sin(x).
    double[] dataZ = new double[dataX.Length * dataY.Length];
    for(int yIndex = 0; yIndex < dataY.Length; ++yIndex) {
        double y = dataY[yIndex];
        for(int xIndex = 0; xIndex < dataX.Length; ++xIndex) {
            double x = dataX[xIndex];
            dataZ[yIndex * dataX.Length + xIndex] = x * Math.Sin(y) + y * Math.Sin(x);
        }
    }

    // Create a SurfaceChart object of size 380 x 400 pixels, with white (ffffff) background and
    // grey (888888) border.
    SurfaceChart c = new SurfaceChart(380, 400, 0xffffff, 0x888888);

    // Demonstrate various shading methods
    if (chartIndex == 0) {
        c.addTitle("11 x 11 Data Points\nSmooth Shading");
    } else if (chartIndex == 1) {
        c.addTitle("11 x 11 Points - Spline Fitted to 50 x 50\nSmooth Shading");
        c.setInterpolation(50, 50);
    } else if (chartIndex == 2) {
        c.addTitle("11 x 11 Data Points\nRectangular Shading");
        c.setShadingMode(Chart.RectangularShading);
    } else {
        c.addTitle("11 x 11 Data Points\nTriangular Shading");
        c.setShadingMode(Chart.TriangularShading);
    }

    // Set the center of the plot region at (175, 200), and set width x depth x height to 200 x 200
    // x 160 pixels
    c.setPlotRegion(175, 200, 200, 200, 160);

    // Set the plot region wall thichness to 5 pixels
    c.setWallThickness(5);

    // Set the elevation and rotation angles to 45 and 60 degrees
    c.setViewAngle(45, 60);

    // Set the perspective level to 35
    c.setPerspective(35);

    // Set the data to use to plot the chart
    c.setData(dataX, dataY, dataZ);

    // Set contour lines to semi-transparent black (c0000000)
    c.setContourColor(unchecked((int)0xc0000000));

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.JPG);
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    createChart(WebChartViewer0, 0);
    createChart(WebChartViewer1, 1);
    createChart(WebChartViewer2, 2);
    createChart(WebChartViewer3, 3);
}

</script>

<html>
<head>
    <title>Surface Shading</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Surface Shading
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request["SCRIPT_NAME"]%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

