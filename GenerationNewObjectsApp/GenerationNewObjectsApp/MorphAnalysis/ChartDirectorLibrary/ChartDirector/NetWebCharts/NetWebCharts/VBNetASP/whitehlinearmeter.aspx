<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Create chart
'
Protected Sub createChart(viewer As WebChartViewer, chartIndex As Integer)

    ' The value to display on the meter
    Dim value As Double = 74.25

    ' Create a LinearMeter object of size 250 x 75 pixels with very light grey (0xeeeeee) backgruond
    ' and a light grey (0xccccccc) 3-pixel thick rounded frame
    Dim m As LinearMeter = New LinearMeter(250, 75, &Heeeeee, &Hcccccc)
    m.setRoundedFrame(Chart.Transparent)
    m.setThickFrame(3)

    ' Set the scale region top-left corner at (14, 23), with size of 218 x 20 pixels. The scale
    ' labels are located on the top (implies horizontal meter)
    m.setMeter(14, 23, 218, 20, Chart.Top)

    ' Set meter scale from 0 - 100, with a tick every 10 units
    m.setScale(0, 100, 10)

    ' Demostrate different types of color scales and putting them at different positions
    Dim smoothColorScale() As Double = {0, &H6666ff, 25, &H00bbbb, 50, &H00ff00, 75, &Hffff00, _
        100, &Hff0000}
    Dim stepColorScale() As Double = {0, &H33ff33, 50, &Hffff33, 80, &Hff3333, 100}
    Dim highLowColorScale() As Double = {0, &H6666ff, 70, Chart.Transparent, 100, &Hff0000}

    If chartIndex = 0 Then
        ' Add the smooth color scale at the default position
        m.addColorScale(smoothColorScale)
    ElseIf chartIndex = 1 Then
        ' Add the high low scale at the default position
        m.addColorScale(highLowColorScale)
    ElseIf chartIndex = 2 Then
        ' Add the smooth color scale starting at y = 43 (bottom of scale) with zero width and ending
        ' at y = 23 with 20 pixels width
        m.addColorScale(smoothColorScale, 43, 0, 23, 20)
    Else
        ' Add the step color scale at the default position
        m.addColorScale(stepColorScale)
    End If

    ' Add a blue (0x0000cc) pointer at the specified value
    m.addPointer(value, &H0000cc)

    ' Add a label left aligned to (10, 61) using 8pt Arial Bold font
    m.addText(10, 61, "Temperature C", "Arial Bold", 8, Chart.TextColor, Chart.Left)

    ' Add a text box right aligned to (235, 61). Display the value using white (0xffffff) 8pt Arial
    ' Bold font on a black (0x000000) background with depressed rounded border.
    Dim t As ChartDirector.TextBox = m.addText(235, 61, m.formatValue(value, "2"), "Arial Bold", _
        8, &Hffffff, Chart.Right)
    t.setBackground(&H000000, &H000000, -1)
    t.setRoundedCorners(3)

    ' Output the chart
    viewer.Image = m.makeWebImage(Chart.PNG)

End Sub

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    createChart(WebChartViewer0, 0)
    createChart(WebChartViewer1, 1)
    createChart(WebChartViewer2, 2)
    createChart(WebChartViewer3, 3)

End Sub

</script>

<html>
<head>
    <title>White Horizontal Linear Meters</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        White Horizontal Linear Meters
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer0" runat="server" />
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
    <chart:WebChartViewer id="WebChartViewer2" runat="server" />
    <chart:WebChartViewer id="WebChartViewer3" runat="server" />
</body>
</html>

