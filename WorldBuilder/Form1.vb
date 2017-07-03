﻿Imports System.Drawing.Drawing2D
Imports System.Text

Public Class FrmMain

    Dim GblAryIntMap(0, 0) As TileType
    Dim GblIntSizeX As Integer = 0
    Dim GblIntSizeY As Integer = 0
    Dim GblColTiles(12) As Color
    Dim GblIntZoom As Integer = 12

    Public Enum TileType As Integer
        Null = 0
        Water = 1
        Ice = 2
        Grass = 3
        Snow = 4
        Desert = 5
        Forest = 6
        Jungle = 7
        SnowForest = 8
        Mountain = 9
        SnowMountain = 10
        Swamp = 11
    End Enum
    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GblColTiles(0) = Color.Black
        GblColTiles(1) = Color.Blue
        GblColTiles(2) = Color.LightCyan
        GblColTiles(3) = Color.LawnGreen
        GblColTiles(4) = Color.White
        GblColTiles(5) = Color.SandyBrown
        GblColTiles(6) = Color.Green
        GblColTiles(7) = Color.Olive
        GblColTiles(8) = Color.PaleGreen
        GblColTiles(9) = Color.Brown
        GblColTiles(10) = Color.Tan
        GblColTiles(11) = Color.Fuchsia

    End Sub

    Private Sub BtnGenerate_Click(sender As Object, e As EventArgs) Handles BtnGenerate.Click
        'UPDATE VARIABLES
        GblIntSizeX = NumTilesX.Value
        GblIntSizeY = NumTilesY.Value
        ReDim GblAryIntMap(GblIntSizeX, GblIntSizeY)

        'CREATE TILEMAP
        CreateTileMap() '<--- Do your work here

        'THIS SHOWS THE RESILTS 
        TxtTextView.Text = GenerateTextMap()
        PicReference.Image = GeneratePixelMap()
    End Sub

    Private Function GeneratePixelMap() As Bitmap
        Dim BmpReturn As New Bitmap(GblIntSizeX, GblIntSizeY)
        Dim IntColorIndex As Integer

        For IntTileY = 0 To GblIntSizeY - 1 Step 1
            For IntTileX = 0 To GblIntSizeX - 1 Step 1
                IntColorIndex = GblAryIntMap(IntTileX, IntTileY)
                BmpReturn.SetPixel(IntTileX, IntTileY, GblColTiles(IntColorIndex))
            Next IntTileX
        Next IntTileY

        Return BmpReturn
    End Function

    Private Sub CreateTileMap()
        'DECLARE VARIABLES
        Dim IntTileX As Integer
        Dim IntTileY As Integer

        'I CREATED RANDOM TILES
        'YOUR CODE SHOLD CREATE STRUCTURES THAT CONTINENTS, OCEANS, ISLANDS, ETC

        For IntTileY = 0 To GblIntSizeY - 1 Step 1
            For IntTileX = 0 To GblIntSizeX - 1 Step 1
                GblAryIntMap(IntTileX, IntTileY) = Choose({1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11})
            Next IntTileX
        Next IntTileY
    End Sub

    Function GenerateTextMap() As String
        'DECLARE VARIABLES
        Dim IntTileX As Integer
        Dim IntTileY As Integer
        Dim StrReturn As New StringBuilder
        Dim IntTileType As Integer
        Dim StrTileType As String



        PgsText.Value = 0
        PgsText.Visible = True
        PgsText.Maximum = (GblIntSizeX) * (GblIntSizeY)


        'LOOP THROUGH ARRAY AND WRITE EACH INDEX TO STRING
        For IntTileY = 0 To GblIntSizeY - 1 Step 1
            For IntTileX = 0 To GblIntSizeX - 1 Step 1
                IntTileType = GblAryIntMap(IntTileX, IntTileY)
                StrTileType = IntTileType.ToString.PadLeft(2, "0") + " "
                StrReturn.Append(StrTileType)
                PgsText.Value += 1
            Next IntTileX
            StrReturn.Append(vbNewLine)
            Me.Invalidate()
        Next IntTileY

        PgsText.Visible = False
        Return StrReturn.ToString
    End Function

    Function Choose(ObjAryChoices() As Object) As Object
        Dim IntMaxIndex As Integer
        Dim IntSelectedIndex = 0

        'CHOOSE A VALUE FROM AN ARRAY AT RANDOM
        IntMaxIndex = ObjAryChoices.Length - 1
        IntSelectedIndex = CInt(Rnd() * IntMaxIndex)

        Return ObjAryChoices(IntSelectedIndex)
    End Function




    Private Sub PicReference_Click(sender As Object, e As MouseEventArgs) Handles PicReference.Click
        If e.Button = MouseButtons.Left Then GblIntZoom += 1
        If e.Button = MouseButtons.Right Then GblIntZoom -= 1
        If e.Button = MouseButtons.Middle Then GblIntZoom = 1

        PicReference.Width = GblIntSizeX * GblIntZoom
        PicReference.Height = GblIntSizeY * GblIntZoom
        PicReference.Invalidate()

    End Sub

    Private Sub PicReference_Paint(sender As Object, e As PaintEventArgs) Handles PicReference.Paint
        Dim GrpTemp As Graphics
        GrpTemp = e.Graphics
        GrpTemp.InterpolationMode = InterpolationMode.NearestNeighbor
        GrpTemp.PixelOffsetMode = PixelOffsetMode.Half

        If PicReference.Image Is Nothing = False Then
            GrpTemp.DrawImage(PicReference.Image, 0, 0, PicReference.Width, PicReference.Height)
        End If
    End Sub

End Class
