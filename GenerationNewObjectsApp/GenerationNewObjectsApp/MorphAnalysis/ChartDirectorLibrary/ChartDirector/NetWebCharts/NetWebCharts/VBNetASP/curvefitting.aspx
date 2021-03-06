<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Use random table to generate a random series. The random table is set to 1 col x 51 rows, with
    ' 9 as the seed
    Dim rantable As RanTable = New RanTable(9, 1, 51)

    ' Set the 1st column to start from 100, with changes between rows from -5 to +5
    rantable.setCol(0, 100, -5, 5)

    ' Get the 1st column of the random table as the data set
    Dim data() As Double = rantable.getCol(0)

    ' Create a XYChart object of size 600 x 300 pixels
    Dim c As XYChart = New XYChart(600, 300)

    ' Set the plotarea at (50, 35) and of size 500 x 240 pixels. Enable both the horizontal and
    ' vertical grids by setting their colors to grey (0xc0c0c0)
    c.setPlotArea(50, 35, 500, 240).setGridColor(&Hc0c0c0, &Hc0c0c0)

    ' Add a title to the chart using 18 point Times Bold Itatic font.
    c.addTitle("LOWESS Generic Curve Fitting Algorithm", "Times New Roman Bold Italic", 18)

    ' Set the y axis line width to 3 pixels
    c.yAxis().setWidth(3)

    ' Add a title to the x axis using 12pt Arial Bold Italic font
    c.xAxis().setTitle("Server Load (TPS)", "Arial Bold Italic", 12)

    ' Set the x axis line width to 3 pixels
    c.xAxis().setWidth(3)

    ' Set the x axis scale from 0 - 50, with major tick every 5 units and minor tick every 1 unit
    c.xAxis().setLinearScale(0, 50, 5, 1)

    ' Add a blue layer to the chart
    Dim layer As LineLayer = c.addLineLayer2()

    ' Add a red (0x80ff0000) data set to the chart with square symbols
    layer.addDataSet(data, &H80ff0000).setDataSymbol(Chart.SquareSymbol)

    ' Set the line width to 2 pixels
    layer.setLineWidth(2)

    ' Use lowess for curve fitting, and plot the fitted data using a spline layer with line width
    ' set to 3 pixels
    c.addSplineLayer(New ArrayMath(data).lowess().result(), &H0000ff).setLineWidth(3)

    ' Set zero affinity to 0 to make sure the line is displayed in the most detail scale
    c.yAxis().setAutoScale(0, 0, 0)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='({x}, {value|2})'")

End Sub

</script>

<html>
<head>
    <title>General Curve Fitting</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        General Curve Fitting
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

