Imports System
Imports Microsoft.VisualBasic
Imports ChartDirector

Public Class paramcurve
    Implements DemoModule

    'Name of demo module
    Public Function getName() As String Implements DemoModule.getName
        Return "Parametric Curve Fitting"
    End Function

    'Number of charts produced in this demo module
    Public Function getNoOfCharts() As Integer Implements DemoModule.getNoOfCharts
        Return 1
    End Function

    'Main code for creating chart.
    'Note: the argument chartIndex is unused because this demo only has 1 chart.
    Public Sub createChart(viewer As WinChartViewer, chartIndex As Integer) _
        Implements DemoModule.createChart

        ' The XY data of the first data series
        Dim dataX0() As Double = {10, 35, 17, 4, 22, 29, 45, 52, 63, 39}
        Dim dataY0() As Double = {2.0, 3.2, 2.7, 1.2, 2.8, 2.9, 3.1, 3.0, 2.3, 3.3}

        ' The XY data of the second data series
        Dim dataX1() As Double = {30, 35, 17, 4, 22, 59, 43, 52, 63, 39}
        Dim dataY1() As Double = {1.0, 1.3, 0.7, 0.6, 0.8, 3.0, 1.8, 2.3, 3.4, 1.5}

        ' The XY data of the third data series
        Dim dataX2() As Double = {28, 35, 15, 10, 22, 60, 46, 64, 39}
        Dim dataY2() As Double = {2.0, 2.2, 1.2, 0.4, 1.8, 2.7, 2.4, 2.8, 2.4}

        ' Create a XYChart object of size 540 x 480 pixels
        Dim c As XYChart = New XYChart(540, 480)

        ' Set the plotarea at (70, 65) and of size 400 x 350 pixels, with white background and a
        ' light grey border (0xc0c0c0). Turn on both horizontal and vertical grid lines with light
        ' grey color (0xc0c0c0)
        c.setPlotArea(70, 65, 400, 350, &Hffffff, -1, &Hc0c0c0, &Hc0c0c0, -1)

        ' Add a legend box with the top center point anchored at (270, 30). Use horizontal layout.
        ' Use 10pt Arial Bold Italic font. Set the background and border color to Transparent.
        Dim legendBox As LegendBox = c.addLegend(270, 30, False, "Arial Bold Italic", 10)
        legendBox.setAlignment(Chart.TopCenter)
        legendBox.setBackground(Chart.Transparent, Chart.Transparent)

        ' Add a title to the chart using 18 point Times Bold Itatic font.
        c.addTitle("Parametric Curve Fitting", "Times New Roman Bold Italic", 18)

        ' Add titles to the axes using 12pt Arial Bold Italic font
        c.yAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)
        c.xAxis().setTitle("Axis Title Placeholder", "Arial Bold Italic", 12)

        ' Set the axes line width to 3 pixels
        c.yAxis().setWidth(3)
        c.xAxis().setWidth(3)

        ' Add a scatter layer using (dataX0, dataY0)
        c.addScatterLayer(dataX0, dataY0, "Polynomial", Chart.GlassSphere2Shape, 11, &Hff0000)

        ' Add a degree 2 polynomial trend line layer for (dataX0, dataY0)
        Dim trend0 As TrendLayer = c.addTrendLayer2(dataX0, dataY0, &Hff0000)
        trend0.setLineWidth(3)
        trend0.setRegressionType(Chart.PolynomialRegression(2))
        trend0.setHTMLImageMap("{disable}")

        ' Add a scatter layer for (dataX1, dataY1)
        c.addScatterLayer(dataX1, dataY1, "Exponential", Chart.GlassSphere2Shape, 11, &H00aa00)

        ' Add an exponential trend line layer for (dataX1, dataY1)
        Dim trend1 As TrendLayer = c.addTrendLayer2(dataX1, dataY1, &H00aa00)
        trend1.setLineWidth(3)
        trend1.setRegressionType(Chart.ExponentialRegression)
        trend1.setHTMLImageMap("{disable}")

        ' Add a scatter layer using (dataX2, dataY2)
        c.addScatterLayer(dataX2, dataY2, "Logarithmic", Chart.GlassSphere2Shape, 11, &H0000ff)

        ' Add a logarithmic trend line layer for (dataX2, dataY2)
        Dim trend2 As TrendLayer = c.addTrendLayer2(dataX2, dataY2, &H0000ff)
        trend2.setLineWidth(3)
        trend2.setRegressionType(Chart.LogarithmicRegression)
        trend2.setHTMLImageMap("{disable}")

        ' Output the chart
        viewer.Chart = c

        'include tool tip for the chart
        viewer.ImageMap = c.getHTMLImageMap("clickable", "", _
            "title='[{dataSetName}] ({x}, {value})'")

    End Sub

End Class

