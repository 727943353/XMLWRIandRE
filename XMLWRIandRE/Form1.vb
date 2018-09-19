Imports System.Xml
Public Class Form1

    Private Sub btnCreate_Click(sender As System.Object, e As System.EventArgs) Handles btnCreate.Click
        Dim xmlWS As New XmlWriterSettings
        '设置文本格式
        xmlWS.Indent = True
        xmlWS.NewLineOnAttributes = True

        Using xmlW As XmlWriter = XmlWriter.Create(Application.StartupPath & "\2.xml", xmlWS)
            xmlW.WriteComment("Same as generate by serializing,FilmOrder") '写入注释 
            xmlW.WriteStartElement("FilmOrder")
            xmlW.WriteAttributeString("FilmId", "101")
            xmlW.WriteAttributeString("Quantity", "10")
            xmlW.WriteElementString("Title", "Grease")
            xmlW.WriteStartElement("Test")
            xmlW.WriteElementString("clock", "ok")
            xmlW.WriteStartElement("A")
            xmlW.WriteElementString("B", "b")
            xmlW.WriteEndElement()
            xmlW.WriteEndElement()
            xmlW.WriteEndElement()
            xmlW.Flush()
        End Using
        MessageBox.Show("OK")
    End Sub

    Private Sub btnRead_Click(sender As System.Object, e As System.EventArgs) Handles btnRead.Click
        Dim xmlRS As New XmlReaderSettings
        Dim strXml As String = ""
        'xmlRS.IgnoreComments = True      '忽略注释
        'xmlRS.IgnoreWhitespace = True    '忽略空白符
        'xmlRS.IgnoreProcessingInstructions = True '忽略处理指令
        Using xmlR As XmlReader = XmlReader.Create(Application.StartupPath & "\2.xml", xmlRS)
            While xmlR.Read
                strXml &= GetNodeInfo(xmlR) & vbCrLf
                While xmlR.MoveToNextAttribute
                    strXml &= GetNodeInfo(xmlR) & vbCrLf
                End While
            End While
        End Using
        TextBox1.Text = strXml
    End Sub

    Private Function GetNodeInfo(ByVal obj As XmlReader ) As String
        Dim strTemp As String = ""
        If obj.Depth > 0 Then '为了便于显示识别，每增一级下级节点，则前导加4个空格
            For i As Integer = 1 To obj.Depth
                strTemp &= "    "
            Next i
        End If

        If obj.NodeType = XmlNodeType.Whitespace Then
            Return strTemp & obj.NodeType
        ElseIf obj.NodeType = XmlNodeType.Text Then   '文本节点无name
            Return strTemp & obj.NodeType & ":" & obj.Value '获取当前节点上文本值
        Else
            Return strTemp & obj.Name & ":" & obj.Value & "==" & obj.AttributeCount '获取当前节点上的属性数
        End If

    End Function
    Dim rawData = <multiFilmOrders>
                      <FilmOrder>
                          <name>Grease</name>
                          <filmld>101</filmld>
                          <quantity>10</quantity></FilmOrder>
                      <FilmOrder>
                          <name>Lawrence of Arabia</name>
                          <filmld>102</filmld>
                          <quantity>10</quantity>
                      </FilmOrder>
                  </multiFilmOrders>
End Class
