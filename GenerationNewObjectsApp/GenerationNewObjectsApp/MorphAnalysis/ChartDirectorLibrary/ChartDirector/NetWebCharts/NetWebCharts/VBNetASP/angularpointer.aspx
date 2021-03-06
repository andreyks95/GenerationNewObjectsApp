<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' Create an AngularMeter object of size 300 x 300 pixels with transparent background
    Dim m As AngularMeter = New AngularMeter(300, 300, Chart.Transparent)

    ' Set the default text and line colors to white (0xffffff)
    m.setColor(Chart.TextColor, &Hffffff)
    m.setColor(Chart.LineColor, &Hffffff)

    ' Center at (150, 150), scale radius = 128 pixels, scale angle 0 to 360 degrees
    m.setMeter(150, 150, 128, 0, 360)

    ' Add a black (0x000000) circle with radius 148 pixels as background
    m.addRing(0, 148, &H000000)

    ' Add a ring between radii 139 and 147 pixels using the silver color with a light grey
    ' (0xcccccc) edge as border
    m.addRing(139, 147, Chart.silverColor(), &Hcccccc)

    ' Meter scale is 0 - 100, with major/minor/micro ticks every 10/5/1 units
    m.setScale(0, 100, 10, 5, 1)

    ' Set the scale label style to 16pt Arial Italic. Set the major/minor/micro tick lengths to
    ' 13/10/7 pixels pointing inwards, and their widths to 2/1/1 pixels.
    m.setLabelStyle("Arial Italic", 16)
    m.setTickLength(-13, -10, -7)
    m.setLineWidth(0, 2, 1, 1)

    ' Add a semi-transparent blue (0x7f6666ff) pointer using the default shape
    m.addPointer(25, &H7f6666ff, &H6666ff)

    ' Add a semi-transparent red (0x7fff6666) pointer using the arrow shape
    m.addPointer(9, &H7fff6666, &Hff6666).setShape(Chart.ArrowPointer2)

    ' Add a semi-transparent yellow (0x7fffff66) pointer using another arrow shape
    m.addPointer(51, &H7fffff66, &Hffff66).setShape(Chart.ArrowPointer)

    ' Add a semi-transparent green (0x7f66ff66) pointer using the line shape
    m.addPointer(72, &H7f66ff66, &H66ff66).setShape(Chart.LinePointer)

    ' Add a semi-transparent grey (0x7fcccccc) pointer using the pencil shape
    m.addPointer(85, &H7fcccccc, &Hcccccc).setShape(Chart.PencilPointer)

    ' Output the chart
    WebChartViewer1.Image = m.makeWebImage(Chart.PNG)

End Sub

</script>

<html>
<head>
    <title>Angular Meter Pointers (1)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Angular Meter Pointers (1)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

