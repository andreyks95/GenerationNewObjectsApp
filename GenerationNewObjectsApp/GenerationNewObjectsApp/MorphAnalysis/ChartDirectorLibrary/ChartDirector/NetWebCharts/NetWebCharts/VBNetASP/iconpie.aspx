<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<!DOCTYPE html>

<script runat="server">

'
' Page Load event handler
'
Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' The data for the pie chart
    Dim data() As Double = {72, 18, 15, 12}

    ' The depths for the sectors
    Dim depths() As Double = {30, 20, 10, 10}

    ' The labels for the pie chart
    Dim labels() As String = {"Sunny", "Cloudy", "Rainy", "Snowy"}

    ' The icons for the sectors
    Dim icons() As String = {"sun.png", "cloud.png", "rain.png", "snowy.png"}

    ' Create a PieChart object of size 400 x 310 pixels, with a blue (CCCCFF) vertical metal
    ' gradient background, black border, 1 pixel 3D border effect and rounded corners
    Dim c As PieChart = New PieChart(400, 310, Chart.metalColor(&Hccccff, 0), &H000000, 1)
    c.setRoundedFrame()

    ' Set default directory for loading images from current script directory
    Call c.setSearchPath(Server.MapPath("."))

    ' Set the center of the pie at (200, 180) and the radius to 100 pixels
    c.setPieSize(200, 180, 100)

    ' Add a title box using 15pt Times Bold Italic font, on a blue (CCCCFF) background with glass
    ' effect
    c.addTitle("Weather Profile in Wonderland", "Times New Roman Bold Italic", 15).setBackground( _
        &Hccccff, &H000000, Chart.glassEffect())

    ' Set the pie data and the pie labels
    c.setData(data, labels)

    ' Add icons to the chart as a custom field
    c.addExtraField(icons)

    ' Configure the sector labels using CDML to include the icon images
    c.setLabelFormat( _
        "<*block,valign=absmiddle*><*img={field0}*> <*block*>{label}<*br*>{percent}%<*/*><*/*>")

    ' Draw the pie in 3D with variable 3D depths
    c.set3D2(depths)

    ' Set the start angle to 225 degrees may improve layout when the depths of the sector are sorted
    ' in descending order, because it ensures the tallest sector is at the back.
    c.setStartAngle(225)

    ' Output the chart
    WebChartViewer1.Image = c.makeWebImage(Chart.PNG)

    ' Include tool tip for the chart
    WebChartViewer1.ImageMap = c.getHTMLImageMap("", "", _
        "title='{label}: {value} days ({percent}%)'")

End Sub

</script>

<html>
<head>
    <title>Icon Pie Chart (1)</title>
</head>
<body style="margin:5px 0px 0px 5px">
    <div style="font:bold 18pt verdana">
        Icon Pie Chart (1)
    </div>
    <hr style="border:solid 1px #000080" />
    <div style="font:10pt verdana; margin-bottom:1.5em">
        <a href='viewsource.aspx?file=<%=Request("SCRIPT_NAME")%>'>View Source Code</a>
    </div>
    <chart:WebChartViewer id="WebChartViewer1" runat="server" />
</body>
</html>

