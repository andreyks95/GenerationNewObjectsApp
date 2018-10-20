Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class barlabel
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Bar Labels"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The data for the bar chart
        Dim data() As Double = {85, 156, 179, 211, 123, 189, 166}

        ' The labels for the bar chart
        Dim labels() As String = {"Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"}

        ' Create a XYChart object of size 600 x 360 pixels
        Dim c As XYChart = New XYChart(600, 360)

        ' Set default text color to dark grey (0x333333)
        c.setColor(Chart.TextColor, &H333333)

        ' Set the plotarea at (70, 20) and of size 500 x 300 pixels, with transparent background and
        ' border and light grey (0xcccccc) horizontal grid lines
        c.setPlotArea(70, 20, 500, 300, Chart.Transparent, -1, Chart.Transparent, &Hcccccc)

        ' Set the x and y axis stems to transparent and the label font to 12pt Arial
        c.xAxis().setColors(Chart.Transparent)
        c.yAxis().setColors(Chart.Transparent)
        c.xAxis().setLabelStyle("Arial", 12)
        c.yAxis().setLabelStyle("Arial", 12)

        ' Add a blue (0x6699bb) bar chart layer using the given data
        Dim layer As BarLayer = c.addBarLayer(data, &H6699bb)

        ' Use bar gradient lighting with the light intensity from 0.8 to 1.3
        layer.setBorderColor(Chart.Transparent, Chart.barLighting(0.8, 1.3))

        ' Set rounded corners for bars
        layer.setRoundedCorners()

        ' Display labela on top of bars using 12pt Arial font
        layer.setAggregateLabelStyle("Arial", 12)

        ' Set the labels on the x axis.
        c.xAxis().setLabels(labels)

        ' For the automatic y-axis labels, set the minimum spacing to 40 pixels.
        c.yAxis().setTickDensity(40)

        ' Add a title to the y axis using dark grey (0x555555) 14pt Arial Bold font
        c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial Bold", 14, &H555555)

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", "title='{xLabel}: ${value}M'")

    End Sub

End Class

