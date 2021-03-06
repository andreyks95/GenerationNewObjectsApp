<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Sample data for the Box-Whisker chart. Represents the minimum, 1st quartile, medium, 3rd
    ' quartile and maximum values of some quantities
    Dim Q0Data() As Double = {40, 45, 40, 30, 20, 50, 25, 44}
    Dim Q1Data() As Double = {55, 60, 50, 40, 38, 60, 51, 60}
    Dim Q2Data() As Double = {62, 70, 60, 50, 48, 70, 62, 70}
    Dim Q3Data() As Double = {70, 80, 65, 60, 53, 78, 69, 76}
    Dim Q4Data() As Double = {80, 90, 75, 70, 60, 85, 80, 84}

    ' The labels for the chart
    Dim labels() As String = {"Group A", "Group B", "Group C", "Group D", "Group E", "Group F", _
        "Group G", "Group H"}

    ' Create a XYChart object of size 550 x 250 pixels
    Dim c As XYChart = New XYChart(550, 250)

    ' Set the plotarea at (50, 25) and of size 450 x 200 pixels. Enable both horizontal and vertical
    ' grids by setting their colors to grey (0xc0c0c0)
    c.setPlotArea(50, 25, 450, 200).setGridColor(&Hc0c0c0, &Hc0c0c0)

    ' Add a title to the chart
    c.addTitle("Computer Vision Test Scores")

    ' Set the labels on the x axis and the font to Arial Bold
    c.xAxis().setLabels(labels).setFontStyle("Arial Bold")

    ' Set the font for the y axis labels to Arial Bold
    c.yAxis().setLabelStyle("Arial Bold")

    ' Add a Box Whisker layer using light blue 0x9999ff as the fill color and blue (0xcc) as the
    ' line color. Set the line width to 2 pixels
    c.addBoxWhiskerLayer(Q3Data, Q1Data, Q4Data, Q0Data, Q2Data, &H9999ff, &H0000cc).setLineWidth(2)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{xLabel}: min/med/max = {min}/{med}/{max}" & vbLf & _
        "Inter-quartile range: {bottom} to {top}'")

End Sub

</script>

<html>
<head>
    <title>Box-Whisker Chart (1)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Box-Whisker Chart (1)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

