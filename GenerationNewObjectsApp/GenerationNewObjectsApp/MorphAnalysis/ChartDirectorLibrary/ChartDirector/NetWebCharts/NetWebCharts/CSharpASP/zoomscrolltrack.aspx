<%@ Page Language="C#" Debug="true" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>

<script runat="server">

//
// Initialize the WebChartViewer when the page is first loaded
//
private void initViewer(WebChartViewer viewer)
{
    // The full x-axis range is from Jan 1, 2007 to Jan 1, 2012
    DateTime startDate = new DateTime(2010, 1, 1);
    DateTime endDate = new DateTime(2015, 1, 1);
    viewer.setFullRange("x", startDate, endDate);

    // Initialize the view port to show the last 366 days (out of 1826 days)
    viewer.ViewPortWidth = 366.0 / 1826;
    viewer.ViewPortLeft = 1 - viewer.ViewPortWidth;

    // Set the maximum zoom to 10 days (out of 1826 days)
    viewer.ZoomInWidthLimit = 10.0 / 1826;
}

//
// Create a random table for demo purpose.
//
private RanTable getRandomTable()
{
    RanTable r = new RanTable(127, 4, 1828);
    r.setDateCol(0, new DateTime(2010, 1, 1), 86400);
    r.setCol(1, 150, -10, 10);
    r.setCol(2, 200, -10, 10);
    r.setCol(3, 250, -8, 8);
    return r;
}

//
// Draw the chart
//
private void drawChart(WebChartViewer viewer)
{
    // Determine the visible x-axis range
    DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft));
    DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft +
        viewer.ViewPortWidth));

    // We need to get the data within the visible x-axis range. In real code, this can be by using a
    // database query or some other means as specific to the application. In this demo, we just
    // generate a random data table, and then select the data within the table.
    RanTable r = getRandomTable();

    // Select the data for the visible date range viewPortStartDate to viewPortEndDate. It is
    // possible there is no data point at exactly viewPortStartDate or viewPortEndDate. In this
    // case, we also need the data points that are just outside the visible date range to "overdraw"
    // the line a little bit (the "overdrawn" part will be clipped to the plot area) In this demo,
    // we do this by adding a one day margin to the date range when selecting the data.
    r.selectDate(0, viewPortStartDate.AddDays(-1), viewPortEndDate.AddDays(1));

    // The selected data from the random data table
    DateTime[] timeStamps = Chart.NTime(r.getCol(0));
    double[] dataSeriesA = r.getCol(1);
    double[] dataSeriesB = r.getCol(2);
    double[] dataSeriesC = r.getCol(3);

    //
    // Now we have obtained the data, we can plot the chart.
    //

    //================================================================================
    // Configure overall chart appearance.
    //================================================================================

    // Create an XYChart object of size 640 x 350 pixels
    XYChart c = new XYChart(640, 350);

    // Set the plotarea at (55, 55) with width 80 pixels less than chart width, and height 90 pixels
    // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
    // as background. Set border to transparent and grid lines to white (ffffff).
    c.setPlotArea(55, 55, c.getWidth() - 80, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
        c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

    // As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
    c.setClipping();

    // Add a title to the chart using 18pt Times New Roman Bold Italic font
    c.addTitle("    Zooming and Scrolling with Track Line", "Times New Roman Bold Italic", 18);

    // Set the axis stem to transparent
    c.xAxis().setColors(Chart.Transparent);
    c.yAxis().setColors(Chart.Transparent);

    // Add axis title using 10pt Arial Bold Italic font
    c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold Italic", 10);

    //================================================================================
    // Add data to chart
    //================================================================================

    //
    // In this example, we represent the data by lines. You may modify the code below to use other
    // layer types (areas, scatter plot, etc).
    //

    // Add a line layer for the lines, using a line width of 2 pixels
    LineLayer layer = c.addLineLayer2();
    layer.setLineWidth(2);

    // In this demo, we do not have too many data points. In real code, the chart may contain a lot
    // of data points when fully zoomed out - much more than the number of horizontal pixels in this
    // plot area. So it is a good idea to use fast line mode.
    layer.setFastLineMode();

    // Add up to 3 data series to a line layer, depending on whether the user has selected the data
    // series.
    layer.setXData(timeStamps);
    if (viewer.GetCustomAttr("data0CheckBox") != "F") {
        layer.addDataSet(dataSeriesA, 0xff3333, "Alpha Series");
    }
    if (viewer.GetCustomAttr("data1CheckBox") != "F") {
        layer.addDataSet(dataSeriesB, 0x008800, "Beta Series");
    }
    if (viewer.GetCustomAttr("data2CheckBox") != "F") {
        layer.addDataSet(dataSeriesC, 0x3333cc, "Gamma Series");
    }

    //================================================================================
    // Configure axis scale and labelling
    //================================================================================

    // Set the x-axis as a date/time axis with the scale according to the view port x range.
    viewer.syncDateAxisWithViewPort("x", c.xAxis());

    //
    // In this demo, the time range can be from a few years to a few days. We demonstrate how to set
    // up different date/time format based on the time range.
    //

    // If all ticks are yearly aligned, then we use "yyyy" as the label format.
    c.xAxis().setFormatCondition("align", 360 * 86400);
    c.xAxis().setLabelFormat("{value|yyyy}");

    // If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label of a
    // year, and "mmm" for other labels.
    c.xAxis().setFormatCondition("align", 30 * 86400);
    c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}",
        Chart.AllPassFilter(), "{value|mmm}");

    // If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
    // label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for other
    // labels.
    c.xAxis().setFormatCondition("align", 86400);
    c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
        "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(),
        "<*font=bold*>{value|mmm dd}");
    c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

    // For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a day,
    // and "hh:nn" for other labels.
    c.xAxis().setFormatCondition("else");
    c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}",
        Chart.AllPassFilter(), "{value|hh:nn}");

    //================================================================================
    // Step 5 - Output the chart
    //================================================================================

    // Output the chart
    viewer.Image = c.makeWebImage(Chart.PNG);

    // Output Javascript chart model to the browser to suppport tracking cursor
    viewer.ChartModel = c.getJsChartModel();
}

//
// Page Load event handler
//
protected void Page_Load(object sender, EventArgs e)
{
    //
    // This script handles both the full page request, as well as the subsequent partial updates
    // (AJAX chart updates). We need to determine the type of request first before we processing it.
    //
    if (WebChartViewer.IsPartialUpdateRequest(Page)) {
        // Is a partial update request.

        // The .NET platform will not restore the states of the controls before or during Page_Load,
        // so we need to restore the state ourselves
        WebChartViewer1.LoadViewerState();

        // Draw the chart in partial update mode
        drawChart(WebChartViewer1);

        // Output the chart immediately and then terminate the page life cycle (PartialUpdateChart
        // will cause Page_Load to terminate immediately without running the following code).
        WebChartViewer1.PartialUpdateChart();
    }

    //
    // If the code reaches here, it is a full page request.
    //

    // In this exapmle, we just need to initialize the WebChartViewer and draw the chart.
    initViewer(WebChartViewer1);
    drawChart(WebChartViewer1);
}

</script>

<!DOCTYPE html>
<html>
<head>
    <title>Zooming and Scrolling with Track Line</title>
    <script type="text/javascript" src="cdjcv.js"></script>
    <style type="text/css">
        .chartButton { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px; cursor:pointer;}
        .chartButtonSpacer { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px;}
        .chartButton:hover { box-shadow:inset 0px 0px 0px 2px #444488; }
        .chartButtonPressed { background-color: #CCFFCC; }
    </style>
</head>
<body style="margin:0px;">
<script type="text/javascript">

//
// Execute the following initialization code after the web page is loaded
//
JsChartViewer.addEventListener(window, 'load', function() {
    // Update the chart when the view port has changed (eg. when the user zooms in using the mouse)
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
    viewer.attachHandler("ViewPortChanged", viewer.partialUpdate);

    // The Update Chart can also trigger a view port changed event to update the chart.
    document.getElementById("SubmitButton").onclick = function() { viewer.raiseViewPortChangedEvent(); return false; };

    // Before sending the update request to the server, we include the state of the check boxes as custom
    // attributes. The server side charting code will use these attributes to decide the data sets to draw.
    viewer.attachHandler("PreUpdate", function() {
        var checkBoxes = ["data0CheckBox", "data1CheckBox", "data2CheckBox"];
        for (var i = 0; i < checkBoxes.length; ++i)
            viewer.setCustomAttr(checkBoxes[i], document.getElementById(checkBoxes[i]).checked ? "T" : "F");
    });

    // Draw track cursor when mouse is moving over plotarea or if the chart updates
    viewer.attachHandler(["MouseMovePlotArea", "TouchStartPlotArea", "TouchMovePlotArea", "PostUpdate",
        "Now", "ChartMove"], function(e) {
        this.preventDefault(e);   // Prevent the browser from using touch events for other actions
        trackLineLegend(viewer, viewer.getPlotAreaMouseX());
    });
});

//
// Draw track line with legend
//
function trackLineLegend(viewer, mouseX)
{
    // Remove all previously drawn tracking object
    viewer.hideObj("all");

    // The chart and its plot area
    var c = viewer.getChart();
    var plotArea = c.getPlotArea();

    // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
    var xValue = c.getNearestXValue(mouseX);
    var xCoor = c.getXCoor(xValue);
    if (xCoor == null)
        return;

    // Draw a vertical track line at the x-position
    viewer.drawVLine("trackLine", xCoor, plotArea.getTopY(), plotArea.getBottomY(), "black 1px dotted");

    // Array to hold the legend entries
    var legendEntries = [];

    // Iterate through all layers to build the legend array
    for (var i = 0; i < c.getLayerCount(); ++i)
    {
        var layer = c.getLayerByZ(i);

        // The data array index of the x-value
        var xIndex = layer.getXIndexOf(xValue);

        // Iterate through all the data sets in the layer
        for (var j = 0; j < layer.getDataSetCount(); ++j)
        {
            var dataSet = layer.getDataSetByZ(j);

            // We are only interested in visible data sets with names, as they are required for legend entries.
            var dataName = dataSet.getDataName();
            var color = dataSet.getDataColor();
            if ((!dataName) || (color == null))
                continue;

            // Build the legend entry, consist of a colored square box, the name and the data value.
            var dataValue = dataSet.getValue(xIndex);
            legendEntries.push("<nobr>" + viewer.htmlRect(7, 7, color) + " " + dataName + ": " +
                ((dataValue == null) ? "N/A" : dataValue.toPrecision(4)) + viewer.htmlRect(20, 0) + "</nobr> ");

            // Draw a track dot for data points within the plot area
            var yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());
            if ((yCoor != null) && (yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY()))
            {
                viewer.showTextBox("dataPoint" + i + "_" + j, xCoor, yCoor, JsChartViewer.Center,
                    viewer.htmlRect(7, 7, color));
            }
        }
    }

    // Create the legend by joining the legend entries.
    var legend = "<nobr>[" + c.xAxis().getFormattedLabel(xValue, "mm/dd/yyyy") + "]" + viewer.htmlRect(20, 0) +
        "</nobr> " + legendEntries.reverse().join("");

    // Display the legend on the top of the plot area
    viewer.showTextBox("legend", plotArea.getLeftX(), plotArea.getTopY(), JsChartViewer.BottomLeft, legend,
        "width:" + plotArea.getWidth() + "px;font:bold 11px Arial;padding:3px;-webkit-text-size-adjust:100%;");
}

//
// This method is called when the user clicks on the Pointer, Zoom In or Zoom Out buttons
//
function setMouseMode(mode)
{
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
    if (mode == viewer.getMouseUsage())
        mode = JsChartViewer.Default;

    // Set the button color based on the selected mouse mode
    document.getElementById("scrollButton").className = "chartButton" +
        ((mode  == JsChartViewer.Scroll) ? " chartButtonPressed" : "");
    document.getElementById("zoomInButton").className = "chartButton" +
        ((mode  == JsChartViewer.ZoomIn) ? " chartButtonPressed" : "");
    document.getElementById("zoomOutButton").className = "chartButton" +
        ((mode  == JsChartViewer.ZoomOut) ? " chartButtonPressed" : "");

    // Set the mouse mode
    viewer.setMouseUsage(mode);
}

//
// This method is called when the user clicks on the buttons that selects the last NN days
//
function setTimeRange(duration)
{
    var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');

    // Set the view port width to represent the required duration (as a ratio to the total x-range)
    viewer.setViewPortWidth(Math.min(1,
        duration / (viewer.getValueAtViewPort("x", 1) - viewer.getValueAtViewPort("x", 0))));

    // Set the view port left so that the view port is moved to show the latest data
    viewer.setViewPortLeft(1 - viewer.getViewPortWidth());

    // Trigger a view port change event
    viewer.raiseViewPortChangedEvent();
}

</script>
<form method="post" id="ZoomScrollTrack" runat="server">
<table cellspacing="0" cellpadding="0" style="border:black 1px solid;">
    <tr>
        <td align="right" colspan="2" style="background:#000088; color:#ffff00; padding:0px 4px 2px 0px;">
            <a style="color:#FFFF00; font:italic bold 10pt Arial; text-decoration:none" href="http://www.advsofteng.com/">
                Advanced Software Engineering
            </a>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:130px; background:#c0c0ff;">
        <div style="width:130px">
            <!-- The following table is to create 3 cells for 3 buttons to control the mouse usage mode. -->
            <table style="width:100%; padding:0px; border:0px; border-spacing:0px;">
                <tr>
                    <td class="chartButton" id="scrollButton" onclick="setMouseMode(JsChartViewer.Scroll)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="scrollew.gif" style="vertical-align:middle" alt="Drag" />&nbsp;&nbsp;Drag to Scroll
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomInButton" onclick="setMouseMode(JsChartViewer.ZoomIn)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="zoomInIcon.gif" style="vertical-align:middle" alt="Zoom In" />&nbsp;&nbsp;Zoom In
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomOutButton" onclick="setMouseMode(JsChartViewer.ZoomOut)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="zoomOutIcon.gif" style="vertical-align:middle" alt="Zoom Out" />&nbsp;&nbsp;Zoom Out
                    </td>
                </tr>
                <tr>
                    <td class="chartButtonSpacer">
                        <div style="padding:2px">&nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(30 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="goto.gif" style="vertical-align:middle" alt="Last 30 days" />&nbsp;&nbsp;Last 30 days
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(90 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="goto.gif" style="vertical-align:middle" alt="Last 90 days" />&nbsp;&nbsp;Last 90 days
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(366 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="goto.gif" style="vertical-align:middle" alt="Last Year" />&nbsp;&nbsp;Last Year
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(1E15);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="goto.gif" style="vertical-align:middle" alt="All Time" />&nbsp;&nbsp;All Time
                    </td>
                </tr>
            </table>
            <div style="font:9pt Verdana; line-height:1.5; padding-top:25px">
            <input id="data0CheckBox" type="checkbox" checked="checked" /> Alpha Series<br />
            <input id="data1CheckBox" type="checkbox" checked="checked" /> Beta Series<br />
            <input id="data2CheckBox" type="checkbox" checked="checked" /> Gamma Series<br />
            </div>
            <div style="font:9pt Verdana; margin-top:15px; text-align:center">
                <asp:button id="SubmitButton" runat="server" Text="Update Chart"></asp:button>
            </div>
        </div>
        </td>
        <td style="border-left:black 1px solid; padding:10px 5px 0px 5px;">
            <chart:WebChartViewer id="WebChartViewer1" runat="server" width="640px" height="350px" />
        </td>
    </tr>
</table>
</form>
</body>
</html>
