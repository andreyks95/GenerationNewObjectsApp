<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the chart
    Dim data0() As Double = {42, 49, 33, 38, 51, 46, 29, 41, 44, 57, 59, 52, 37, 34, 51, 56, 56, _
        60, 70, 76, 63, 67, 75, 64, 51}
    Dim data1() As Double = {50, 55, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, 58, _
        59, 73, 77, 84, 82, 80, 84, 98}

    ' The labels for the bottom x axis. Note the "-" means a minor tick.
    Dim labels0() As String = {"0<*br*>Jun 4", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", _
        "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", _
        "0<*br*>Jun 5"}

    ' The labels for the top x axis. Note that "-" means a minor tick.
    Dim labels1() As String = {"Jun 3<*br*>12", "13", "14", "15", "16", "17", "18", "19", "20", _
        "21", "22", "23", "Jun 4<*br*>0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", _
        "12"}

    ' Create a XYChart object of size 310 x 310 pixels
    Dim c As XYChart = New XYChart(310, 310)

    ' Set the plotarea at (50, 50) and of size 200 x 200 pixels
    c.setPlotArea(50, 50, 200, 200)

    ' Add a title to the primary (left) y axis
    c.yAxis().setTitle("US Dollars")

    ' Set the tick length to -4 pixels (-ve means ticks inside the plot area)
    c.yAxis().setTickLength(-4)

    ' Add a title to the secondary (right) y axis
    c.yAxis2().setTitle("HK Dollars (1 USD = 7.8 HKD)")

    ' Set the tick length to -4 pixels (-ve means ticks inside the plot area)
    c.yAxis2().setTickLength(-4)

    ' Synchronize the y-axis such that y2 = 7.8 x y1
    c.syncYAxis(7.8)

    ' Add a title to the bottom x axis
    c.xAxis().setTitle("Hong Kong Time")

    ' Set the labels on the x axis.
    c.xAxis().setLabels(labels0)

    ' Display 1 out of 3 labels on the x-axis. Show minor ticks for remaining labels.
    c.xAxis().setLabelStep(3, 1)

    ' Set the major tick length to -4 pixels and minor tick length to -2 pixels (-ve means ticks
    ' inside the plot area)
    c.xAxis().setTickLength2(-4, -2)

    ' Set the distance between the axis labels and the axis to 6 pixels
    c.xAxis().setLabelGap(6)

    ' Add a title to the top x-axis
    c.xAxis2().setTitle("New York Time")

    ' Set the labels on the x axis.
    c.xAxis2().setLabels(labels1)

    ' Display 1 out of 3 labels on the x-axis. Show minor ticks for remaining labels.
    c.xAxis2().setLabelStep(3, 1)

    ' Set the major tick length to -4 pixels and minor tick length to -2 pixels (-ve means ticks
    ' inside the plot area)
    c.xAxis2().setTickLength2(-4, -2)

    ' Set the distance between the axis labels and the axis to 6 pixels
    c.xAxis2().setLabelGap(6)

    ' Add a line layer to the chart with a line width of 2 pixels
    c.addLineLayer(data0, -1, "Red Transactions").setLineWidth(2)

    ' Add an area layer to the chart with no area boundary line
    c.addAreaLayer(data1, -1, "Green Transactions").setLineWidth(0)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", "title='{dataSetName}" & vbLf & _
        "HKT Jun {=3.5+{x}/24|0} {={x}%24}:00 (NYT Jun {=3+{x}/24|0} {=({x}+12)%24}:00)" & vbLf & _
        "HKD {={value}*7.8} (USD {value})'")

End Sub

</script>

<html>
<head>
    <title>Dual X-Axis</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Dual X-Axis
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

