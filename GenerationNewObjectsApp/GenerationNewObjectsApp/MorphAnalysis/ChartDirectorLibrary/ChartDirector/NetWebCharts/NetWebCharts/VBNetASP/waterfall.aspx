<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' 4 data points to represent the cash flow for the Q1 - Q4
    Dim data() As Double = {230, 140, 220, 330, 150}

    ' We want to plot a waterfall chart showing the 4 quarters as well as the total
    Dim labels() As String = {"Product 1", "Product 2", "Product 3", "Product 4", "Product 5", _
        "Total"}

    ' The top side of the bars in a waterfall chart is the accumulated data. We use the
    ' ChartDirector ArrayMath utility to accumulate the data. The "total" is handled by inserting a
    ' zero point at the end before accumulation (after accumulation it will become the total).
    Dim boxTop() As Double = New ArrayMath(data).insert2(0, 1).acc().result()

    ' The botom side of the bars is just the top side of the previous bar. So we shifted the top
    ' side data to obtain the bottom side data.
    Dim boxBottom() As Double = New ArrayMath(boxTop).shift(1, 0).result()

    ' The last point (total) is different. Its bottom side is always 0.
    boxBottom(UBound(boxBottom)) = 0

    ' Create a XYChart object of size 500 x 280 pixels. Set background color to light blue (ccccff),
    ' with 1 pixel 3D border effect.
    Dim c As XYChart = New XYChart(500, 290, &Hccccff, &H000000, 1)

    ' Add a title to the chart using 13 points Arial Bold Itatic font, with white (ffffff) text on a
    ' deep blue (0x80) background
    c.addTitle("Product Revenue - Year 2004", "Arial Bold Italic", 13, &Hffffff).setBackground( _
        &H000080)

    ' Set the plotarea at (55, 50) and of size 430 x 215 pixels. Use alternative white/grey
    ' background.
    c.setPlotArea(55, 45, 430, 215, &Hffffff, &Heeeeee)

    ' Set the labels on the x axis using Arial Bold font
    c.xAxis().setLabels(labels).setFontStyle("Arial Bold")

    ' Set the x-axis ticks and grid lines to be between the bars
    c.xAxis().setTickOffset(0.5)

    ' Use Arial Bold as the y axis label font
    c.yAxis().setLabelStyle("Arial Bold")

    ' Add a title to the y axis
    c.yAxis().setTitle("USD (in millions)")

    ' Add a multi-color box-whisker layer to represent the waterfall bars
    Dim layer As BoxWhiskerLayer = c.addBoxWhiskerLayer2(boxTop, boxBottom)

    ' Put data labels on the bars to show the cash flow using Arial Bold font
    layer.setDataLabelFormat("{={top}-{bottom}}M")
    layer.setDataLabelStyle("Arial Bold").setAlignment(Chart.Center)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{xLabel}: {={top}-{bottom}} millions'")

End Sub

</script>

<html>
<head>
    <title>Waterfall Chart</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Waterfall Chart
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

