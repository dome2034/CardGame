Imports System.IO

Module Module1
    Dim d1, d2 As String()
    Class Card
        Private Score As Integer
        Private Face As Integer

        Sub New(Name As String)
            Dim S As String = Name.Substring(0, 1)
            Dim F As String = Name.Substring(1, 1)

            Select Case S.ToUpper()
                Case "9"
                    Score = 1
                Case "T"
                    Score = 2
                Case "J"
                    Score = 3
                Case "Q"
                    Score = 4
                Case "K"
                    Score = 5
                Case Else 'A
                    Score = 6
            End Select
            Select Case F.ToLower()
                Case "c"
                    Face = 1
                Case "d"
                    Face = 2
                Case "h"
                    Face = 3
                Case Else 's
                    Face = 4
            End Select
        End Sub

        Function getScore() As Integer
            Return Score
        End Function
        Function getFace() As Integer
            Return Face
        End Function
    End Class

    Sub Main()
        Try
            Dim Deck1, Deck2 As New LinkedList(Of Card)
            readFile("E:\งาน ป.โท\Algor\CardGame\cardgame\Data7.txt")
            Deck1 = createDeck(d1)
            Deck2 = createDeck(d2)
            'process(Deck1, Deck2)
            process2(Deck1, Deck2)
            Console.WriteLine("End")
            Console.ReadLine()
        Catch ex As Exception
            Console.WriteLine("error")
            Console.ReadLine()
        End Try
    End Sub
    Sub readFile(Path As String)
        Dim sr As StreamReader = New StreamReader(Path)
        Dim i As Integer = 0
        Do While sr.Peek() >= 0
            If i = 0 Then
                d1 = sr.ReadLine.Split(" ")
            Else
                d2 = sr.ReadLine.Split(" ")
            End If
            i = i + 1
        Loop
        sr.Close()
    End Sub
    Function createDeck(SourceDeck As String()) As LinkedList(Of Card)
        Dim DeckResult As New LinkedList(Of Card)
        For Each i In SourceDeck
            DeckResult.AddLast(New Card(i))
        Next
        Return DeckResult
    End Function
    Sub process(Deck1 As LinkedList(Of Card), Deck2 As LinkedList(Of Card))
        Dim c1, c2 As Card
        Dim Turn As Integer = 0
        While Deck1.Count <> 0 And Deck2.Count <> 0 And Turn <= 99
            c1 = Deck1.First.Value
            c2 = Deck2.First.Value
            If c1.getScore > c2.getScore Then
                Deck1.AddLast(c1)
                Deck1.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
            ElseIf c1.getScore < c2.getScore Then
                Deck2.AddLast(c1)
                Deck2.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
            Else
                If c1.getFace > c2.getFace Then
                    Deck1.AddLast(c1)
                    Deck1.AddLast(c2)
                    Deck1.RemoveFirst()
                    Deck2.RemoveFirst()
                Else
                    Deck2.AddLast(c1)
                    Deck2.AddLast(c2)
                    Deck1.RemoveFirst()
                    Deck2.RemoveFirst()
                End If
            End If
            Turn = Turn + 1
        End While

        If Deck1.Count = 0 Then
            Console.WriteLine("deck2 wins in " + Turn.ToString() + " steps")
        ElseIf Deck2.Count = 0 Then
            Console.WriteLine("deck1 wins in " + Turn.ToString() + " steps")
        Else
            Console.WriteLine("game ties after " + Turn.ToString() + " steps")
        End If
    End Sub

    Sub process2(Deck1 As LinkedList(Of Card), Deck2 As LinkedList(Of Card))
        Dim c1, c2, t As Card
        Dim DeckTemp As New LinkedList(Of Card)
        Dim Turn As Integer = 0
        While Deck1.Count <> 0 And Deck2.Count <> 0 And Turn <= 99
            c1 = Deck1.First.Value
            c2 = Deck2.First.Value
            If c1.getScore > c2.getScore Then
                Deck1.AddLast(c1)
                Deck1.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
            ElseIf c1.getScore < c2.getScore Then
                Deck2.AddLast(c1)
                Deck2.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
            Else
                DeckTemp.AddLast(c1) 'C
                DeckTemp.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
                c1 = Deck1.First.Value
                c2 = Deck2.First.Value

                DeckTemp.AddLast(c1) 'M
                DeckTemp.AddLast(c2)
                Deck1.RemoveFirst()
                Deck2.RemoveFirst()
                c1 = Deck1.First.Value
                c2 = Deck2.First.Value

                If c1.getScore > c2.getScore Then
                    Deck1.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck1.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck1.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck1.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck1.AddLast(c1)
                    Deck1.AddLast(c2)
                    Deck1.RemoveFirst()
                    Deck2.RemoveFirst()
                ElseIf c1.getScore < c2.getScore Then
                    Deck2.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck2.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck2.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck2.AddLast(DeckTemp.First.Value)
                    DeckTemp.RemoveFirst()
                    Deck2.AddLast(c1)
                    Deck2.AddLast(c2)
                    Deck2.RemoveFirst()
                    Deck1.RemoveFirst()
                End If

            End If
            Turn = Turn + 1
        End While

        If Deck1.Count = 0 Then
            Console.WriteLine("deck2 wins in " + Turn.ToString() + " steps")
        ElseIf Deck2.Count = 0 Then
            Console.WriteLine("deck1 wins in " + Turn.ToString() + " steps")
        Else
            Console.WriteLine("game ties after " + Turn.ToString() + " steps")
        End If
    End Sub

End Module
