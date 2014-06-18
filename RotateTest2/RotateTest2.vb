Public Class RotateTest2

    Const RATIO As Double = 0.25

    Dim srcImageName As String = String.Empty

    Private Sub btnLoadSrcImage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadSrcImage.Click
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Bitmap files (*.bmp)|*.bmp|GIF files (*.gif)|*.gif|JPEG files (*.jpg)|*.jpg|PNG files (*.png)|*.png|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 1
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.CheckPathExists = True
        OpenFileDialog1.Title = "Open File"
        OpenFileDialog1.RestoreDirectory = True

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            srcImageName = OpenFileDialog1.FileName

            btnResizeRotate.Enabled = True
            btnRotateOnly.Enabled = True
        End If
    End Sub

    Private Sub btnResizeRotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResizeRotate.Click
        Dim imageOrig As Bitmap
        Dim imageResized As Bitmap
        Dim g As Graphics
        Dim p As Pen

        imageOrig = Bitmap.FromFile(srcImageName)

        ' now resize the image...
        Dim newWidth As Integer
        Dim newHeight As Integer
        newWidth = imageOrig.Width * RATIO
        newHeight = imageOrig.Height * RATIO

        imageResized = New Bitmap(newWidth, newHeight, Imaging.PixelFormat.Format32bppArgb)

        g = Graphics.FromImage(imageResized)
        g.DrawImage(imageOrig, New Rectangle(0, 0, imageResized.Width, imageResized.Height), _
                    New Rectangle(0, 0, imageOrig.Width, imageOrig.Height), _
                    GraphicsUnit.Pixel)
        p = New Pen(Color.White, 3)
        g.DrawLine(p, 0, 0, imageResized.Width, imageResized.Height)
        p.Dispose()

        p = New Pen(Color.Yellow, 3)
        g.DrawLine(p, imageResized.Width, 0, 0, imageResized.Height)
        p.Dispose()

        g.Dispose()
        g = Nothing
        imageResized.Save("C:\TestOut1.jpg", Imaging.ImageFormat.Jpeg)

        ' now rotate...
        imageResized.RotateFlip(RotateFlipType.Rotate270FlipNone)
        g = Graphics.FromImage(imageResized)
        p = New Pen(Color.Blue, 3)
        g.DrawLine(p, 0, 0, imageResized.Width, imageResized.Height)
        p.Dispose()

        p = New Pen(Color.Red, 3)
        g.DrawLine(p, imageResized.Width, 0, 0, imageResized.Height)
        p.Dispose()
        p = Nothing

        g.Dispose()
        g = Nothing

        imageResized.Save("C:\TestOut2.jpg", Imaging.ImageFormat.Jpeg)

        PictureBox1.Image = Image.FromFile("C:\TestOut1.jpg")
        PictureBox2.Image = Image.FromFile("C:\TestOut2.jpg")

        imageOrig.Dispose()
        imageOrig = Nothing

        imageResized.Dispose()
        imageResized = Nothing
    End Sub

    Private Sub btnRotateOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotateOnly.Click
        Dim imageOrig As Bitmap
        Dim g As Graphics
        Dim p As Pen

        imageOrig = Bitmap.FromFile(srcImageName)

        ' now rotate...
        imageOrig.RotateFlip(RotateFlipType.Rotate270FlipNone)
        g = Graphics.FromImage(imageOrig)
        p = New Pen(Color.Blue, 3)
        g.DrawLine(p, 0, 0, imageOrig.Width, imageOrig.Height)
        p.Dispose()

        p = New Pen(Color.Red, 3)
        g.DrawLine(p, imageOrig.Width, 0, 0, imageOrig.Height)
        p.Dispose()
        p = Nothing

        g.Dispose()
        g = Nothing

        imageOrig.Save("C:\TestOut3.jpg", Imaging.ImageFormat.Jpeg)

        PictureBox1.Image = Nothing
        PictureBox2.Image = Image.FromFile("C:\TestOut3.jpg")

        imageOrig.Dispose()
        imageOrig = Nothing
    End Sub
End Class
