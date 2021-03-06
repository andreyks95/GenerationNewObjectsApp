Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class floatingbox
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Floating Box Chart"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' Sample data for the Box-Whisker chart. Represents the minimum, 1st quartile, medium, 3rd
        ' quartile and maximum values of some quantities
        Dim Q0Data() As Double = {40, 45, 40, 30, 20, 50, 25, 44}
        Dim Q1Data() As Double = {55, 60, 50, 40, 38, 60, 51, 60}
        Dim Q2Data() As Double = {62, 70, 60, 50, 48, 70, 62, 70}
        Dim Q3Data() As Double = {70, 80, 65, 60, 53, 78, 69, 76}
        Dim Q4Data() As Double = {80, 90, 75, 70, 60, 85, 80, 84}

        ' The labels for the chart
        Dim labels() As String = {"Group A", "Group B", "Group C", "Group D", "Group E", _
            "Group F", "Group G", "Group H"}

        ' Create a XYChart object of size 550 x 250 pixels
        Dim c As XYChart = New XYChart(550, 275)

        ' Set the plotarea at (50, 25) and of size 450 x 200 pixels. Enable both horizontal and
        ' vertical grids by setting their colors to grey (0xc0c0c0)
        c.setPlotArea(50, 50, 450, 200).setGridColor(&Hc0c0c0, &Hc0c0c0)

        ' Add a title to the chart
        c.addTitle("Computer Vision Test Scores")

        ' Set the labels on the x axis and the font to Arial Bold
        c.xAxis().setLabels(labels).setFontStyle("Arial Bold")

        ' Set the font for the y axis labels to Arial Bold
        c.yAxis().setLabelStyle("Arial Bold")

        ' Add a Box Whisker layer using light blue 0x9999ff as the fill color and blue (0xcc) as the
        ' line color. Set the line width to 2 pixels
        c.addBoxLayer(Q4Data, Q3Data, &H00ff00, "Top 25%")
        c.addBoxLayer(Q3Data, Q2Data, &H9999ff, "25% - 50%")
        c.addBoxLayer(Q2Data, Q1Data, &Hffff00, "50% - 75%")
        c.addBoxLayer(Q1Data, Q0Data, &Hff0000, "Bottom 25%")

        ' Add legend box at top center above the plot area using 10pt Arial Bold Font
        Dim b As LegendBox = c.addLegend(50 + 225, 22, False, "Arial Bold", 10)
        b.setAlignment(Chart.TopCenter)
        b.setBackground(Chart.Transparent)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='{xLabel} ({dataSetName}): {bottom} to {top} points'")

    End Sub

End Class

