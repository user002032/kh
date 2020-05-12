Imports System.Windows
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq
Imports System.Text
Imports Windows.UI.Xaml.Shapes
Imports System.Xml
Imports Windows.Web.Syndication
Imports System.ServiceModel
Imports System.Xml.Linq
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Security.AccessControl
Imports Windows.Storage
Imports System.IO.TextWriter
Imports ClosedXML.Excel
Imports System.Net
Imports System.Xml.XPath
Imports System.Text.RegularExpressions
Imports System.Net.Http
Imports Windows.UI.Popups
Imports Windows.System
Imports System.Runtime.InteropServices

Public NotInheritable Class MainPage
    Inherits Page


    '↓各種用途用のカウント変数のi
    Dim i As Integer = 0

    '↓検索ボタンを押された場合、すでに表示されている記事の件数をカウントする変数。
    Dim remove_Article_Count_int As Integer = 0

    '↓実際に取得できた記事数をカウントする変数。一部のIf文に用い、配列の参照外のエラー対策に用いる予定。
    Dim actually_Article_Count_int As Integer = 0

    '↓ListBoxに登録されているRSSの情報を代入する為の動的配列。
    Dim rss_String_ArrayList As New ArrayList


    '↓RSSから抽出した各情報を代入する為の動的配列。
    Dim title_String_ArrayList As New ArrayList
    Dim url_String_ArrayList As New ArrayList
    Dim publishedDate_String_ArrayList As New ArrayList
    Dim description_String_ArrayList As New ArrayList



    '↓RSS情報を代入した動的配列の内容に準じて、UI側へ追加していくコントロールの配列。
    '  配列数は処理の中でReDimし、変更する。
    Dim Article_StackPanel_Array() As StackPanel = New StackPanel() {}
    Dim Article_Title_TextBlock_Array() As TextBlock = New TextBlock() {}
    Dim Article_Date_TextBlock_Array() As TextBlock = New TextBlock() {}
    Dim Article_Title_Bottom_Line_Array() As Line = New Line() {}
    Dim Article_Content_TextBlock_Array() As TextBlock = New TextBlock() {}
    Dim url_Button_Array() As Button = New Button() {}
    Dim Image_Array() As Image = New Image() {}



    '↓初期案でWEB画面を直接UI内に表示する案があったが、
    '  このコントロールは動的に増やせないのか成功しなかった為、一時コメントアウト。
    'Dim Web_View_Array() As WebView = New WebView() {}


    '↓UIの表示状態を判断する為のboolean変数。
    Dim article_Area_bool As Boolean = False
    Dim sign_IN_Area_bool As Boolean = False
    Dim login_Area_bool As Boolean = False
    Dim rss_Area_bool As Boolean = False
    Dim setting_Area_bool As Boolean = False
    Dim category_Area_bool As Boolean = False
    Dim id_Delete_Area_bool As Boolean = False


    '↓RSS読み込みの為の「SyndicationClient()」を宣言。
    Dim client As New SyndicationClient()


    '↓スライダーによる表示件数を代入する為の変数。
    Dim articles_User_Designation_Export_Int = 0


    '↓RSSから画像が取得できなかった場合のNo Image画像（スマホ用）。
    Dim image_Source As ImageSource = New BitmapImage(New Uri("ms-appx:///Assets/No_Image.jpg"))


    '↓RSS登録画面の「初期化」ボタンが押された場合に代入させるListデータ。
    Dim reset_RSS_lines_List_Of_String As IList(Of String)


    '↓アカウントの作成用、ログアウトなどの判定に用いるログイン後のユーザーの情報を入れる変数。
    Dim relay_ID As String = Nothing


    '↓ComboBoxをスタート時に昇順の設定をした場合、イベントハンドラが呼び出されてしまう為、回避用のbool。
    Dim start_Escape_ComboBox_Bool As Boolean = False


    '↓レスポンシブデザイン用の横幅、縦幅を代入する変数。
    Dim width_Size_Check_Double As Double = 0
    Dim height_Size_Check_Double As Double = 0

    '↓レスポンシブデザイン用の真偽判定を行う為の変数。Falseならパソコン版。Trueならスマホ版の表示。
    Dim page_Size_Check_Bool As Boolean = False

    Dim smp_Set_Bool As Boolean = False

    '↓記事（StackPanel)をArticle_Stack_Panel_Areaか、Smp_Article_Stack_Panel_Areaに子要素として設置した場合、
    '  新たに検索ボタンが押された場合、元ある記事はFor文で削除する為、どちらに設置されているかを判定する為のbool。
    '  FalseならPCページ、TrueならスマホページのエリアのStackPanelの内容を削除。
    Dim article_Remove_Page_Size_Bool As Boolean = False


    '▼ページロード時のイベント処理
    Private Sub Page_Loaded_Event(sender As Object, e As RoutedEventArgs)

        Page_Loaded_Event_Call()

    End Sub
    '▲ページロード時のイベント処理


    '▼ページロード時のイベントで呼び出される処理。
    Private Sub Page_Loaded_Event_Call()
        '↓保留メモ。UWPではVisibilityは最初の段階でXAML側で「Visibility="Visible"」にしてから、
        '  起動時に「Visibility.Collapsed」にしておかないと、事前に場所が予約されない。
        'sort_ComboBox.Visibility = Visibility.Collapsed

        Category_Button.Visibility = Visibility.Collapsed
        ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed
        Wait_Background_Black_Border.Visibility = Visibility.Collapsed
        smp_Web_View_ScrollViewer.Visibility = Visibility.Collapsed
        smp_WebView_UI_Area.Visibility = Visibility.Collapsed

        '↓スマホ版の各種画面を非表示へ。
        SMP_Visibility_Collapsed_Set()

        '↓初期の検索条件を「新着順」に起動時に設定
        sort_ComboBox.SelectedItem = Ascending_Order_ComboBoxItem
        smp_Sort_ComboBox.SelectedItem = smp_Ascending_Order_ComboBoxItem

        '↓初期の検索結果のURLのリンク先の表示をアプリ内のWebViewでの表示にする設定
        url_View_ComboBox.SelectedItem = App_WebView_ComboBoxItem
        smp_url_View_ComboBox.SelectedItem = smp_App_WebView_ComboBoxItem

        tag_1_ComboBox.SelectedItem = tag_1_OR_ComboBoxItem
        tag_2_ComboBox.SelectedItem = tag_2_OR_ComboBoxItem
        tag_3_ComboBox.SelectedItem = tag_3_OR_ComboBoxItem
        tag_4_ComboBox.SelectedItem = tag_4_OR_ComboBoxItem
        tag_5_ComboBox.SelectedItem = tag_5_OR_ComboBoxItem

        smp_tag_1_ComboBox.SelectedItem = smp_tag_1_OR_ComboBoxItem
        smp_tag_2_ComboBox.SelectedItem = smp_tag_2_OR_ComboBoxItem
        smp_tag_3_ComboBox.SelectedItem = smp_tag_3_OR_ComboBoxItem
        smp_tag_4_ComboBox.SelectedItem = smp_tag_4_OR_ComboBoxItem
        smp_tag_5_ComboBox.SelectedItem = smp_tag_5_OR_ComboBoxItem

        '↓検索ワードは起動時は全除去
        tag_1_TextBox.Text = ""
        tag_2_TextBox.Text = ""
        tag_3_TextBox.Text = ""
        tag_4_TextBox.Text = ""
        tag_5_TextBox.Text = ""


        '↓ローカルフォルダのRSSを保存しているテキストファイルの起動時用読み込み処理。
        RSS_List_Text_ListBox_Set()

        '↓UWPの資格情報ボックスの登録可能件数の取得。20件まで。
        Remaing_ID_Count_Text_Change_Sub()

        '↓ID_List.txtから登録されているＩＤの一覧をListBoxへと代入する処理を呼び出す。
        ID_List_Text_ListBox_Set()

        start_Escape_ComboBox_Bool = True

    End Sub
    '▲ページロード時のイベントで呼び出される処理。





    '▼検索ボタンが押された場合の処理
    Private Async Sub Search_Button_Click(sender As Object, e As RoutedEventArgs) Handles Search_Button.Click

        '↓非同期処理のRSSの読み込み処理（Sub）を呼び出し。
        start_RSS_Reading()

    End Sub
    '▲検索ボタンが押された場合の処理





    '▼検索ボタンが押された場合の処理で呼び出される非同期処理
    Private Async Sub start_RSS_Reading()


        Button_ALL_OFF()
        Wait_Background_Black_Border.Visibility = Visibility.Visible

        Search_Wait_ProgressRing.IsActive = True
        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)





        '↓2回目以降用に、各要素が入る動的配列の内容を全削除し、2回目の代入に備える箇所。
        rss_String_ArrayList.Clear()
        title_String_ArrayList.Clear()
        url_String_ArrayList.Clear()
        publishedDate_String_ArrayList.Clear()
        description_String_ArrayList.Clear()
        '↑2回目以降用に、各要素が入る動的配列の内容を全削除し、2回目の代入に備える箇所。


        '▼RSSが登録されたListBoxの内容を、動的配列へ代入。
        For i = 0 To RSS_ListBox.Items.Count - 1
            rss_String_ArrayList.Add(RSS_ListBox.Items(i))
        Next
        '▲RSSが登録されたListBoxの内容を、動的配列へ代入。


        '▼既に表示されている記事の削除
        If remove_Article_Count_int <> 0 Then
            For i = 0 To remove_Article_Count_int - 1
                If article_Remove_Page_Size_Bool = False Then
                    Article_Stack_Panel_Area.Children.Remove(Article_StackPanel_Array(i))
                ElseIf article_Remove_Page_Size_Bool = True Then
                    Smp_Article_Stack_Panel_Area.Children.Remove(Article_StackPanel_Array(i))
                End If
            Next i
        End If
        '▲既に表示されている記事の削除


        '▼▼UI更新
        Input_Box_Clear()
        WebView_URL_Text_Box_Clear()


        '▼ＰＣ画面かスマホ画面か判定して処理を分岐
        If page_Size_Check_Bool = False Then

            Button_Width_200_Set()
            Main_Area_GridLength_OFF()
            Area_Bool_False_Set()
            SMP_Visibility_Collapsed_Set()

            article_Area_bool = True
            PC_Main_Article_Area.Width = New GridLength(0.7, GridUnitType.Star)

            smp_Set_Bool = False

        ElseIf page_Size_Check_Bool = True Then

            article_Area_bool = True

            Main_Area_GridLength_OFF()
            Button_Width_500_Set()

            smp_Article_Area_ScrollViewer.Visibility = Visibility.Visible
            smp_Article_Stack_Panel_Text_Area.Visibility = Visibility.Visible

            smp_Set_Bool = True

        End If

        ID_Display_StackPanel.Width = 500
        '▲▲UI更新



        '↓参考サイトを参考に追記した箇所。不要ならば削除
        InitializeComponent()
        client.SetRequestHeader("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)")
        '↑参考サイトを参考に追記した箇所。不要ならば削除



        '↓スライダー指定の表示件数を取得。
        articles_User_Designation_Export_Int = Articles_Number_Set_Slider.Value
        articles_User_Designation_Export_Int = articles_User_Designation_Export_Int - 1
        '↑スライダー指定の表示件数を取得。


        '↓登録されているRSSの件数のカウント用変数。呼び出しの都度、0へ。
        Dim rss_URL_List_Count_int As Integer = 0


        '↓RSSが登録されていない場合の処理終了箇所。
        If rss_String_ArrayList.Count = 0 Then
            Show_Message("RSSが登録されていません。")
            'ここでガイダンス呼び出し
            Exit Sub
        End If



        '▼登録されているRSSの件数に応じてループ処理を行い、すべてのRSSを元に表示する処理
        For rss_URL_List_Count_int = 0 To rss_String_ArrayList.Count - 1


            '↓RSSのURLをString変数へ代入。
            Dim url_String As String = rss_String_ArrayList(rss_URL_List_Count_int)


            '↓UWPは標準でShift-jisなどに対応していない為、設定の変更の記述。
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance)


            '↓ListBoxにRSSではない文字列やURLが入力されていた場合、エラーとなるので回避確認用の変数を宣言。
            Dim get_Check_RSS_XDocument

            Try
                get_Check_RSS_XDocument = XDocument.Load(url_String)
            Catch ex As Exception
                Show_Message("※下記の登録された情報はRSSではない可能性があります。" & vbCrLf & "⇒ " & url_String)
                Exit For
            End Try


            '↓WEB上のRSSをxmlとして取得。（※（As XDocumentなどの）型は宣言せずDim形式で取得する事。エラーの原因となる為）
            Dim get_RSS_XDocument = XDocument.Load(url_String)




            '↓取得したRSSのXMLのタグの名前空間（idやclassやhrefなど）を全て取り外す。
            For Each e In get_RSS_XDocument.Descendants()
                e.Name = e.Name.LocalName
            Next




            '▼名前空間を削除したRSSから階層順に「XPathSelectElements」で
            '   If文の判定用に、 1 item 毎(1記事毎）の link と title 、その他の件数を取得する。

            '↓タイトル
            Dim check_Title_IEnumerable As IEnumerable(Of XElement) = get_RSS_XDocument.XPathSelectElements("//item/title")

            '↓リンク
            Dim check_Link_IEnumerable As IEnumerable(Of XElement) = get_RSS_XDocument.XPathSelectElements("//item/link")

            '↓日付パターン１（pubDate）
            Dim check_PubDate_IEnumerable As IEnumerable(Of XElement) = get_RSS_XDocument.XPathSelectElements("//item/pubDate")

            '↓日付パターン２（date）
            Dim check_Date_IEnumerable As IEnumerable(Of XElement) = get_RSS_XDocument.XPathSelectElements("//item/date")

            '↓記事の概略
            Dim check_Description_IEnumerable As IEnumerable(Of XElement) = get_RSS_XDocument.XPathSelectElements("//item/description")


            '▲名前空間を削除したRSSから階層順に「XPathSelectElements」で
            '   If文の判定用に、 1 item 毎(1記事毎）の link と title 、その他の件数を取得する。



            '↓タイトルをLINQで取得
            If check_Title_IEnumerable.Count = 0 Then

                '※タイトルが取得できなければFor文処理そのものを終了する。
                '  並びに他の項目の取得についても、判定はタイトルの件数を軸とした情報取得の形にする。
                Show_Message("※下記の登録された情報はRSSではない可能性があります。" & vbCrLf & "⇒ " & url_String)
                Exit For

            Else

                Dim rss_Title_Get_Query = From p In get_RSS_XDocument.Descendants("item")
                                          Select p.Element("title").Value

                For Each query_title As String In rss_Title_Get_Query

                    query_title = Regex.Replace(query_title, "<(""[^""]*""|'[^']*'|[^'"">])*>", "")

                    title_String_ArrayList.Add(query_title.ToString())
                Next

            End If


            '↓check_Title_IEnumerable.Countが0ならば処理を行わない。
            If check_Title_IEnumerable.Count <> 0 Then


                '↓urlをLINQで取得
                If check_Link_IEnumerable.Count = 0 Then

                    For i = url_String_ArrayList.Count To title_String_ArrayList.Count - 1
                        If url_String_ArrayList.Count < title_String_ArrayList.Count Then
                            url_String_ArrayList.Add("このRSSには元ページへのリンク情報が存在しないか、" & vbCrLf _
                                                        & "またはインターネットセキュリティ上の理由、" & vbCrLf _
                                                        & "または本ソフトの仕様により取得できません。")
                        End If
                    Next i

                Else


                    Dim rss_Link_Get_Query = From p In get_RSS_XDocument.Descendants("item")
                                             Select p.Element("link").Value

                    For Each query_link As String In rss_Link_Get_Query
                        If url_String_ArrayList.Count < title_String_ArrayList.Count Then

                            Try
                                url_String_ArrayList.Add(query_link)

                            Catch ex As Exception
                                url_String_ArrayList.Add("このRSSには元ページへのリンク情報が存在しないか、" & vbCrLf _
                                                            & "またはインターネットセキュリティ上の理由、" & vbCrLf _
                                                            & "または本ソフトの仕様により取得できません。")
                            End Try

                        End If
                    Next
                End If



                '↓日付をLINQで取得
                If check_PubDate_IEnumerable.Count <> 0 Or check_Date_IEnumerable.Count <> 0 Then

                    If check_PubDate_IEnumerable.Count <> 0 And check_Date_IEnumerable.Count = 0 Then

                        Dim rss_PubDate_Get_Query = From p In get_RSS_XDocument.Descendants("item")
                                                    Select p.Element("pubDate").Value

                        For Each query_date As Date In rss_PubDate_Get_Query
                            If publishedDate_String_ArrayList.Count < title_String_ArrayList.Count Then
                                Try
                                    publishedDate_String_ArrayList.Add(query_date)
                                Catch ex As Exception
                                    publishedDate_String_ArrayList.Add("この記事には日付データは存在しません。")
                                End Try
                            End If
                        Next

                    ElseIf check_PubDate_IEnumerable.Count = 0 And check_Date_IEnumerable.Count <> 0 Then

                        Dim rss_PubDate_Get_Query = From p In get_RSS_XDocument.Descendants("item")
                                                    Select p.Element("date").Value

                        For Each query_date As Date In rss_PubDate_Get_Query
                            If publishedDate_String_ArrayList.Count < title_String_ArrayList.Count Then
                                Try
                                    publishedDate_String_ArrayList.Add(query_date)
                                Catch ex As Exception
                                    publishedDate_String_ArrayList.Add("この記事には日付データは存在しません。")
                                End Try
                            End If
                        Next

                    End If

                ElseIf check_PubDate_IEnumerable.Count = 0 And check_Date_IEnumerable.Count = 0 Then

                    For i = publishedDate_String_ArrayList.Count To title_String_ArrayList.Count - 1
                        If i < title_String_ArrayList.Count Then
                            publishedDate_String_ArrayList.Add("この記事には日付データは存在しません。")
                        End If
                    Next i

                End If



                '↓記事の概略をLINQで取得
                If check_Description_IEnumerable.Count = 0 Then

                    For i = description_String_ArrayList.Count To title_String_ArrayList.Count - 1
                        If i < title_String_ArrayList.Count Then
                            description_String_ArrayList.Add("このRSSには記事の概略が存在しないか、本ソフトでは取得できません。")
                        End If
                    Next i

                Else

                    Dim rss_Description_Get_Query = From p In get_RSS_XDocument.Descendants("item")
                                                    Select p.Element("description").Value

                    For Each query_description As String In rss_Description_Get_Query
                        If description_String_ArrayList.Count < title_String_ArrayList.Count Then
                            Try
                                query_description = Regex.Replace(query_description, "<(""[^""]*""|'[^']*'|[^'"">])*>", "")

                                If (Not String.IsNullOrWhiteSpace(query_description)) Then
                                    description_String_ArrayList.Add(query_description)
                                Else
                                    description_String_ArrayList.Add("この記事にRSS上の概略情報が存在しません。")
                                End If


                            Catch ex As Exception
                                description_String_ArrayList.Add("このRSSには記事の概略が存在しないか、本ソフトでは取得できません。")
                            End Try
                        End If
                    Next
                End If


                ''↓タイトルに準じてＵＲＬ，記事、日付の項目が同じ項目取得がされているか確認用処理。完成後、削除。
                'Show_Message("タイトル数：" & title_String_ArrayList.Count)
                'Show_Message("日付数：" & publishedDate_String_ArrayList.Count)

            End If
            '↑check_Title_IEnumerable.Countが0ならば処理を行わない。



        Next rss_URL_List_Count_int
        '▲登録されているRSSの件数に応じてループ処理を行い、すべてのRSSを元に表示する処理





        '↓RSS読み取り終了後の記事件数を取得
        actually_Article_Count_int = 0
        actually_Article_Count_int = title_String_ArrayList.Count '←記事数の取得。
        '↑RSS読み取り終了後の記事件数を取得

        If page_Size_Check_Bool = False Then
            Upper_Right_Text.Text = "出力設定件数：" & articles_User_Designation_Export_Int + 1 & " / 取得記事件数：" & actually_Article_Count_int
        ElseIf page_Size_Check_Bool = True Then
            smp_Result_Text.Text = "出力設定件数：" & articles_User_Designation_Export_Int + 1 & " / 取得記事件数：" & actually_Article_Count_int
        End If

        '↓RSSの登録状況、並びにRSSではない文字列が登録されていた場合の情報確認によるエラー対策用終了処理
        If actually_Article_Count_int = 0 Or
            title_String_ArrayList.Count = 0 Then

            Show_Message("インターネットに接続されていないか、RSSが登録されていません。")
            Exit Sub

        End If
        '↑RSSの登録状況、並びにRSSではない文字列が登録されていた場合の情報確認によるエラー対策用終了処理







        '▼検索ワードに該当する記事のみを抽出する処理。
        'まずIF文で入力ボックスに何か文字が入っているかを判定し、入っているなら処理に入る。入っていなければ無視

        If (tag_1_TextBox.Text IsNot Nothing) AndAlso (tag_1_TextBox.Text.Length <> 0) _
            Or (tag_2_TextBox.Text IsNot Nothing) AndAlso (tag_2_TextBox.Text.Length <> 0) _
            Or (tag_3_TextBox.Text IsNot Nothing) AndAlso (tag_3_TextBox.Text.Length <> 0) _
            Or (tag_4_TextBox.Text IsNot Nothing) AndAlso (tag_4_TextBox.Text.Length <> 0) _
            Or (tag_5_TextBox.Text IsNot Nothing) AndAlso (tag_5_TextBox.Text.Length <> 0) Then




            '-----------------------------------------------------------------------------------------------------




            '▼NOTが含む場合の処理。(まずソートする）
            If tag_1_ComboBox.SelectedItem.Content = "含まない" _
                Or tag_2_ComboBox.SelectedItem.Content = "含まない" _
                Or tag_3_ComboBox.SelectedItem.Content = "含まない" _
                Or tag_4_ComboBox.SelectedItem.Content = "含まない" _
                Or tag_5_ComboBox.SelectedItem.Content = "含まない" Then



                '↓動的配列のリレー用の変数、ArrayListの宣言。
                Dim relay_title_String As String
                Dim relay_url_String As String
                Dim relay_publishedDate_Date As Date
                Dim relay_description_String As String


                Dim relay_title_ArrayList As New ArrayList
                Dim relay_url_ArrayList As New ArrayList
                Dim relay_publishedDate_ArrayList As New ArrayList
                Dim relay_description_ArrayList As New ArrayList



                '↓NOTで除外するワードを配列へ。
                Dim tag_Array As String() = New String() {tag_1_TextBox.Text,
                                                            tag_2_TextBox.Text,
                                                            tag_3_TextBox.Text,
                                                            tag_4_TextBox.Text,
                                                            tag_5_TextBox.Text}

                '↓ComboBoxの表示されているItemのContentを配列へ入れる。
                Dim tag_ComboBox_Content As String() = New String() {tag_1_ComboBox.SelectedItem.Content,
                                                                     tag_2_ComboBox.SelectedItem.Content,
                                                                     tag_3_ComboBox.SelectedItem.Content,
                                                                     tag_4_ComboBox.SelectedItem.Content,
                                                                     tag_5_ComboBox.SelectedItem.Content}
                '↓ループ処理のindex番号指定用。
                Dim tag_Count As Integer = 0

                '↓終了位置
                Dim article_Title_Count_End_Point As Integer = title_String_ArrayList.Count - 1

                '▼検索ワードの登録画面のTextBoxのいずれかに検索ワードが登録され、尚且つNOTで該当するものは動的配列から除外する処理。

                For tag_Count = 0 To tag_Array.Length - 1 '←配列に入れた検索ワードをFor文で全確認。

                    For i = 0 To article_Title_Count_End_Point '←記事のタイトル件数分、処理を行う。

                        '↓検索ワードが全角・半角空白であった場合には処理させない為のIf文
                        If (tag_Array(tag_Count) IsNot Nothing) AndAlso (tag_Array(tag_Count).Length <> 0) Then

                            '↓検索ワードをContainsの引数に充て、部分一致すればTrue、
                            '  そして「tag_ComboBox_Content」が"含まない"なら、真として処理を続行。
                            '  ここで行うのは title_String_ArrayList description_String_ArrayList の中身を精査し、
                            '  該当する記事の title_String_ArrayList のタイトルを空白にし、次のループ処理でスキップする対象として整理する事。
                            ' （Containsは半角・全角空白も検索ワードとして扱ってしまう為、１つ上のIF文で除外する）
                            If (title_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True _
                                Or description_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True) _
                                AndAlso (tag_ComboBox_Content(tag_Count) = "含まない") Then

                                title_String_ArrayList(i) = ""

                            End If
                        End If
                    Next i
                Next tag_Count


                '↓タイトルが空白になった要素はスルーしてリレー用の配列へ代入していく。
                For i = 0 To title_String_ArrayList.Count - 1
                    If (title_String_ArrayList(i) IsNot Nothing) AndAlso (title_String_ArrayList(i).Length <> 0) Then
                        '↓リレー処理。
                        relay_title_String = CType(title_String_ArrayList(i), String)
                        relay_url_String = CType(url_String_ArrayList(i), String)
                        relay_publishedDate_Date = CType(publishedDate_String_ArrayList(i), Date)
                        relay_description_String = CType(description_String_ArrayList(i), String)

                        relay_title_ArrayList.Add(relay_title_String)
                        relay_url_ArrayList.Add(relay_url_String)
                        relay_publishedDate_ArrayList.Add(relay_publishedDate_Date)
                        relay_description_ArrayList.Add(relay_description_String)
                    End If
                Next


                '↓一時受け入れしたリレーの件数が０件であった場合のエラー回避処理用。
                If relay_title_ArrayList.Count = 0 Then
                    Show_Message("検索ワードに登録されいている単語で該当する記事が存在しませんでした。")
                    Exit Sub
                End If


                '↓元々の使用予定の配列をクリアする。
                title_String_ArrayList.Clear()
                url_String_ArrayList.Clear()
                publishedDate_String_ArrayList.Clear()
                description_String_ArrayList.Clear()


                '↓リレーのタイトルの件数に準じてループ処理を行い、元々の使用予定の配列へ改めてリレー処理で代入。
                For i = 0 To relay_title_ArrayList.Count - 1
                    relay_title_String = CType(relay_title_ArrayList(i), String)
                    relay_url_String = CType(relay_url_ArrayList(i), String)
                    relay_publishedDate_Date = CType(relay_publishedDate_ArrayList(i), Date)
                    relay_description_String = CType(relay_description_ArrayList(i), String)

                    title_String_ArrayList.Add(relay_title_String)
                    url_String_ArrayList.Add(relay_url_String)
                    publishedDate_String_ArrayList.Add(relay_publishedDate_Date)
                    description_String_ArrayList.Add(relay_description_String)
                Next
            End If
            '▲NOTが含む場合の処理。


            '-----------------------------------------------------------------------------------------------------


            '▼ANDが含む場合の処理。
            If tag_1_ComboBox.SelectedItem.Content = "含む" _
                Or tag_2_ComboBox.SelectedItem.Content = "含む" _
                Or tag_3_ComboBox.SelectedItem.Content = "含む" _
                Or tag_4_ComboBox.SelectedItem.Content = "含む" _
                Or tag_5_ComboBox.SelectedItem.Content = "含む" Then



                '↓動的配列のリレー用の変数、ArrayListの宣言。
                Dim relay_title_String As String
                Dim relay_url_String As String
                Dim relay_publishedDate_Date As Date
                Dim relay_description_String As String


                Dim relay_title_ArrayList As New ArrayList
                Dim relay_url_ArrayList As New ArrayList
                Dim relay_publishedDate_ArrayList As New ArrayList
                Dim relay_description_ArrayList As New ArrayList



                '↓NOTで除外するワードを配列へ。
                Dim tag_Array As String() = New String() {tag_1_TextBox.Text,
                                                            tag_2_TextBox.Text,
                                                            tag_3_TextBox.Text,
                                                            tag_4_TextBox.Text,
                                                            tag_5_TextBox.Text}



                '↓ComboBoxの表示されているItemのContentを配列へ入れる。
                Dim tag_ComboBox_Content As String() = New String() {tag_1_ComboBox.SelectedItem.Content,
                                                                     tag_2_ComboBox.SelectedItem.Content,
                                                                     tag_3_ComboBox.SelectedItem.Content,
                                                                     tag_4_ComboBox.SelectedItem.Content,
                                                                     tag_5_ComboBox.SelectedItem.Content}


                '↓ループ処理のindex番号指定用。
                Dim tag_Count As Integer = 0

                '↓終了位置
                Dim article_Title_Count_End_Point As Integer = title_String_ArrayList.Count - 1


                '↓２個目の検索ワード用
                Dim second_Check_int As Integer = 0



                '▼検索ワードの登録画面のTextBoxのいずれかに検索ワードが登録され、尚且つNOTで該当するものは動的配列から除外する処理。

                For tag_Count = 0 To tag_Array.Length - 1 '←配列に入れた検索ワードをFor文で全確認。

                    '↓検索ワードが全角・半角空白であった場合には処理させない為のIf文
                    If (tag_Array(tag_Count) IsNot Nothing) AndAlso (tag_Array(tag_Count).Length <> 0) Then

                        If tag_ComboBox_Content(tag_Count) = "含む" Then

                            For i = 0 To article_Title_Count_End_Point '←リレーを含めた変動する記事のタイトル件数分、処理を行う
                                '↓検索ワードの配列を１つずつ調べ、順次該当していくものを選別する形で抽出する。
                                If title_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True _
                                            Or description_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True Then

                                    '別のやり方を考える。
                                    '↓リレー処理。
                                    relay_title_String = CType(title_String_ArrayList(i), String)
                                    relay_url_String = CType(url_String_ArrayList(i), String)
                                    relay_publishedDate_Date = CType(publishedDate_String_ArrayList(i), Date)
                                    relay_description_String = CType(description_String_ArrayList(i), String)

                                    relay_title_ArrayList.Add(relay_title_String)
                                    relay_url_ArrayList.Add(relay_url_String)
                                    relay_publishedDate_ArrayList.Add(relay_publishedDate_Date)
                                    relay_description_ArrayList.Add(relay_description_String)

                                End If

                                '↓検索ワードの配列の個数の回数毎のループの最後にのみ、リレー処理を行い該当したワードのみを抽出する。
                                If i = title_String_ArrayList.Count - 1 Then

                                    '↓元々の使用予定の配列をクリアする。
                                    title_String_ArrayList.Clear()
                                    url_String_ArrayList.Clear()
                                    publishedDate_String_ArrayList.Clear()
                                    description_String_ArrayList.Clear()


                                    For j As Integer = 0 To relay_title_ArrayList.Count - 1
                                        relay_title_String = CType(relay_title_ArrayList(j), String)
                                        relay_url_String = CType(relay_url_ArrayList(j), String)
                                        relay_publishedDate_Date = CType(relay_publishedDate_ArrayList(j), Date)
                                        relay_description_String = CType(relay_description_ArrayList(j), String)

                                        title_String_ArrayList.Add(relay_title_String)
                                        url_String_ArrayList.Add(relay_url_String)
                                        publishedDate_String_ArrayList.Add(relay_publishedDate_Date)
                                        description_String_ArrayList.Add(relay_description_String)
                                    Next


                                    '↓次のループの際には抽出した記事分のみの件数で処理を行う為、ループ処理の終了地点を変更。
                                    article_Title_Count_End_Point = title_String_ArrayList.Count - 1

                                    second_Check_int = second_Check_int + 1

                                End If
                            Next i
                        End If
                    End If

                    relay_title_ArrayList.Clear()
                    relay_url_ArrayList.Clear()
                    relay_publishedDate_ArrayList.Clear()
                    relay_description_ArrayList.Clear()

                Next tag_Count

                '↓一時受け入れしたリレーの件数が０件であった場合のエラー回避処理用。
                If title_String_ArrayList.Count = 0 Then
                    Show_Message("検索ワードに登録されいている単語で該当する記事が存在しませんでした。")
                    Exit Sub
                End If

            End If
            '▲ANDが含む場合の処理。




            '-----------------------------------------------------------------------------------------------------




            '▼Orが含む場合の処理。
            If tag_1_ComboBox.SelectedItem.Content = "または" _
                Or tag_2_ComboBox.SelectedItem.Content = "または" _
                Or tag_3_ComboBox.SelectedItem.Content = "または" _
                Or tag_4_ComboBox.SelectedItem.Content = "または" _
                Or tag_5_ComboBox.SelectedItem.Content = "または" Then


                If tag_1_ComboBox.SelectedItem.Content = "または" _
                And tag_2_ComboBox.SelectedItem.Content = "または" _
                And tag_3_ComboBox.SelectedItem.Content = "または" _
                And tag_4_ComboBox.SelectedItem.Content = "または" _
                And tag_5_ComboBox.SelectedItem.Content = "または" Then


                    Dim tag_Array As String() = New String() {tag_1_TextBox.Text, tag_2_TextBox.Text, tag_3_TextBox.Text, tag_4_TextBox.Text, tag_5_TextBox.Text}

                    Dim tag_Count As Integer = 0
                    Dim article_Check_Count As Integer = 0

                    Dim relay_title_String As String
                    Dim relay_url_String As String
                    Dim relay_publishedDate_Date As Date
                    Dim relay_description_String As String


                    Dim relay_title_ArrayList As New ArrayList
                    Dim relay_url_ArrayList As New ArrayList
                    Dim relay_publishedDate_ArrayList As New ArrayList
                    Dim relay_description_ArrayList As New ArrayList


                    Dim tag_ComboBox_Content As String() = New String() {tag_1_ComboBox.SelectedItem.Content,
                                                                     tag_2_ComboBox.SelectedItem.Content,
                                                                     tag_3_ComboBox.SelectedItem.Content,
                                                                     tag_4_ComboBox.SelectedItem.Content,
                                                                     tag_5_ComboBox.SelectedItem.Content}


                    '▼検索ワードの登録画面のTextBoxのいずれかに検索ワードが登録されている場合に行う処理。
                    For tag_Count = 0 To tag_Array.Length - 1 '←配列に入れた検索ワードをFor文で全確認。

                        If tag_ComboBox_Content(tag_Count) = "または" Then

                            For i = 0 To title_String_ArrayList.Count - 1 '←記事のタイトル件数分、処理を行う。
                                If (tag_Array(tag_Count) IsNot Nothing) AndAlso (tag_Array(tag_Count).Length <> 0) Then '←検索ワードが全角・半角空白であった場合には処理させない為のIf文

                                    Dim article_Duplicate_Skip_Bool As Boolean = True



                                    '↓検索ワードをContainsの引数に充て、記事のタイトルと概略の中で部分一致すればTrueとして処理を続行。
                                    '   （Containsは半角・全角空白も検索ワードとして扱ってしまう為、１つ上のIF文で除外する）
                                    If title_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True _
                                        Or description_String_ArrayList(i).Contains(tag_Array(tag_Count)) = True Then

                                        If relay_title_ArrayList.Count <> 0 Then
                                            For article_Check_Count = 0 To relay_title_ArrayList.Count - 1
                                                If title_String_ArrayList(i) = relay_title_ArrayList(article_Check_Count) Then
                                                    article_Duplicate_Skip_Bool = False
                                                End If
                                            Next article_Check_Count
                                        End If


                                        If article_Duplicate_Skip_Bool = True Then

                                            '↓リレー処理。
                                            relay_title_String = CType(title_String_ArrayList(i), String)
                                            relay_url_String = CType(url_String_ArrayList(i), String)
                                            relay_publishedDate_Date = CType(publishedDate_String_ArrayList(i), Date)
                                            relay_description_String = CType(description_String_ArrayList(i), String)

                                            relay_title_ArrayList.Add(relay_title_String)
                                            relay_url_ArrayList.Add(relay_url_String)
                                            relay_publishedDate_ArrayList.Add(relay_publishedDate_Date)
                                            relay_description_ArrayList.Add(relay_description_String)

                                        End If
                                    End If
                                End If
                            Next i

                        End If

                    Next tag_Count

                    '↓
                    If relay_title_ArrayList.Count = 0 Then
                        Show_Message("検索ワードに登録されいている単語で該当する記事が存在しませんでした。")
                        Exit Sub
                    End If


                    title_String_ArrayList.Clear()
                    url_String_ArrayList.Clear()
                    publishedDate_String_ArrayList.Clear()
                    description_String_ArrayList.Clear()


                    For i = 0 To relay_title_ArrayList.Count - 1

                        relay_title_String = CType(relay_title_ArrayList(i), String)
                        relay_url_String = CType(relay_url_ArrayList(i), String)
                        relay_publishedDate_Date = CType(relay_publishedDate_ArrayList(i), Date)
                        relay_description_String = CType(relay_description_ArrayList(i), String)

                        title_String_ArrayList.Add(relay_title_String)
                        url_String_ArrayList.Add(relay_url_String)
                        publishedDate_String_ArrayList.Add(relay_publishedDate_Date)
                        description_String_ArrayList.Add(relay_description_String)

                    Next i


                End If

            End If
            '▲Orが含む場合の処理。




            '↓エラー回避の為、再確認
            If title_String_ArrayList.Count = 0 Then
                Show_Message("検索ワードに登録されいている単語で該当する記事が存在しませんでした。")
                Exit Sub
            End If




            If page_Size_Check_Bool = False Then
                '↓右上に表示するテキストを検索ワード用に変更。
                Upper_Right_Text.Text = "出力設定件数：" & articles_User_Designation_Export_Int + 1 & " / 取得記事件数：" & actually_Article_Count_int _
                                & " / 検索取得記事件数：" & title_String_ArrayList.Count.ToString()
            ElseIf page_Size_Check_Bool = True Then
                smp_Result_Text.Text = "出力設定件数：" & articles_User_Designation_Export_Int + 1 & " / 取得記事件数：" & actually_Article_Count_int _
                                & vbCrLf & "検索取得記事件数：" & title_String_ArrayList.Count.ToString()
            End If

            '▲検索ワードの登録画面のTextBoxのいずれかに検索ワードが登録されている場合に行う処理。
        End If
        '▲検索ワードに該当する記事のみを抽出する処理。






        '▽メモ：もし日付のArrayListの中に、日付型ではない取得できなかった場合のCatchの文が入っている場合のテストを行う。
        '        尚且つ、入っている場合には一番最後へと移動させるアルゴリズムを考える。
        '↓
        '日付の配列に、文字列型が入っている場合の挙動処理の確認用文字列。テスト終了後コメントアウト。
        'publishedDate_String_ArrayList(2) = "この記事には日付データは存在しません"
        'publishedDate_String_ArrayList(3) = "この記事には日付データは存在しません"
        'publishedDate_String_ArrayList(5) = "この記事には日付データは存在しません"



        '▼RetrieveFeedAsyncで、取得した要素の順番を、sort_ComboBoxの子要素のitemに準じてで並び替える処理。
        '  ↓記事数が１つならば並び替えは不要としてIF文を配置。
        If title_String_ArrayList.Count > 1 Then

            Dim array_Length As Integer = title_String_ArrayList.Count - 1
            Dim last_Value_Point As Integer = title_String_ArrayList.Count - 1

            Dim relay_Date = Nothing
            Dim relay_Title As String = ""
            Dim relay_Link As String = ""
            Dim relay_Summary As String = ""


            '↓昇順
            If sort_ComboBox.SelectedIndex = 0 Then

                While array_Length <> 1

                    For i = 0 To array_Length

                        If i < array_Length - 1 Then

                            '※日付データではない文字列型が入った場合にはその都度処理。または文字列型が入るとTry文で処理？
                            relay_Date = publishedDate_String_ArrayList(i)
                            relay_Title = title_String_ArrayList(i)
                            relay_Link = url_String_ArrayList(i)
                            relay_Summary = description_String_ArrayList(i)


                            '↓iの要素参照位置に文字列が含まれている場合の入れ替え処理。
                            If TypeOf publishedDate_String_ArrayList(i) Is String Or TypeOf publishedDate_String_ArrayList(i + 1) Is String Then

                                If TypeOf publishedDate_String_ArrayList(i) Is String Then

                                    relay_Date = publishedDate_String_ArrayList(last_Value_Point)
                                    publishedDate_String_ArrayList(last_Value_Point) = publishedDate_String_ArrayList(i)
                                    publishedDate_String_ArrayList(i) = relay_Date

                                    relay_Title = title_String_ArrayList(last_Value_Point)
                                    title_String_ArrayList(last_Value_Point) = title_String_ArrayList(i)
                                    title_String_ArrayList(i) = relay_Title

                                    relay_Link = url_String_ArrayList(last_Value_Point)
                                    url_String_ArrayList(last_Value_Point) = url_String_ArrayList(i)
                                    url_String_ArrayList(i) = relay_Link

                                    relay_Summary = description_String_ArrayList(last_Value_Point)
                                    description_String_ArrayList(last_Value_Point) = description_String_ArrayList(i)
                                    description_String_ArrayList(i) = relay_Summary

                                    last_Value_Point = last_Value_Point - 1
                                    array_Length = last_Value_Point

                                End If

                                If TypeOf publishedDate_String_ArrayList(i + 1) Is String Then

                                    relay_Date = publishedDate_String_ArrayList(last_Value_Point)
                                    publishedDate_String_ArrayList(last_Value_Point) = publishedDate_String_ArrayList(i + 1)
                                    publishedDate_String_ArrayList(i + 1) = relay_Date

                                    relay_Title = title_String_ArrayList(last_Value_Point)
                                    title_String_ArrayList(last_Value_Point) = title_String_ArrayList(i + 1)
                                    title_String_ArrayList(i + 1) = relay_Title

                                    relay_Link = url_String_ArrayList(last_Value_Point)
                                    url_String_ArrayList(last_Value_Point) = url_String_ArrayList(i + 1)
                                    url_String_ArrayList(i + 1) = relay_Link

                                    relay_Summary = description_String_ArrayList(last_Value_Point)
                                    description_String_ArrayList(last_Value_Point) = description_String_ArrayList(i + 1)
                                    description_String_ArrayList(i + 1) = relay_Summary

                                    last_Value_Point = last_Value_Point - 1
                                    array_Length = last_Value_Point

                                End If


                                i = i - 1

                                '↓iの要素参照位置に文字列が含まれていない場合の入れ替え処理。
                            Else
                                If publishedDate_String_ArrayList(i) < publishedDate_String_ArrayList(i + 1) Then

                                    publishedDate_String_ArrayList(i) = publishedDate_String_ArrayList(i + 1)
                                    publishedDate_String_ArrayList(i + 1) = relay_Date

                                    title_String_ArrayList(i) = title_String_ArrayList(i + 1)
                                    title_String_ArrayList(i + 1) = relay_Title

                                    url_String_ArrayList(i) = url_String_ArrayList(i + 1)
                                    url_String_ArrayList(i + 1) = relay_Link

                                    description_String_ArrayList(i) = description_String_ArrayList(i + 1)
                                    description_String_ArrayList(i + 1) = relay_Summary

                                End If
                            End If

                        End If

                    Next i

                    array_Length = array_Length - 1
                End While

                '↓降順
            ElseIf sort_ComboBox.SelectedIndex = 1 Then


                While array_Length <> 1

                    For i = 0 To array_Length

                        If i < array_Length - 1 Then


                            '※日付データではない文字列型が入った場合にはその都度処理。または文字列型が入るとTry文で処理？
                            relay_Date = publishedDate_String_ArrayList(i)
                            relay_Title = title_String_ArrayList(i)
                            relay_Link = url_String_ArrayList(i)
                            relay_Summary = description_String_ArrayList(i)


                            '↓iの要素参照位置に文字列が含まれている場合の入れ替え処理。
                            If TypeOf publishedDate_String_ArrayList(i) Is String Or TypeOf publishedDate_String_ArrayList(i + 1) Is String Then

                                If TypeOf publishedDate_String_ArrayList(i) Is String Then

                                    relay_Date = publishedDate_String_ArrayList(last_Value_Point)
                                    publishedDate_String_ArrayList(last_Value_Point) = publishedDate_String_ArrayList(i)
                                    publishedDate_String_ArrayList(i) = relay_Date

                                    relay_Title = title_String_ArrayList(last_Value_Point)
                                    title_String_ArrayList(last_Value_Point) = title_String_ArrayList(i)
                                    title_String_ArrayList(i) = relay_Title

                                    relay_Link = url_String_ArrayList(last_Value_Point)
                                    url_String_ArrayList(last_Value_Point) = url_String_ArrayList(i)
                                    url_String_ArrayList(i) = relay_Link

                                    relay_Summary = description_String_ArrayList(last_Value_Point)
                                    description_String_ArrayList(last_Value_Point) = description_String_ArrayList(i)
                                    description_String_ArrayList(i) = relay_Summary

                                    last_Value_Point = last_Value_Point - 1
                                    array_Length = last_Value_Point

                                End If

                                If TypeOf publishedDate_String_ArrayList(i + 1) Is String Then

                                    relay_Date = publishedDate_String_ArrayList(last_Value_Point)
                                    publishedDate_String_ArrayList(last_Value_Point) = publishedDate_String_ArrayList(i + 1)
                                    publishedDate_String_ArrayList(i + 1) = relay_Date

                                    relay_Title = title_String_ArrayList(last_Value_Point)
                                    title_String_ArrayList(last_Value_Point) = title_String_ArrayList(i + 1)
                                    title_String_ArrayList(i + 1) = relay_Title

                                    relay_Link = url_String_ArrayList(last_Value_Point)
                                    url_String_ArrayList(last_Value_Point) = url_String_ArrayList(i + 1)
                                    url_String_ArrayList(i + 1) = relay_Link

                                    relay_Summary = description_String_ArrayList(last_Value_Point)
                                    description_String_ArrayList(last_Value_Point) = description_String_ArrayList(i + 1)
                                    description_String_ArrayList(i + 1) = relay_Summary

                                    last_Value_Point = last_Value_Point - 1
                                    array_Length = last_Value_Point

                                End If

                                i = i - 1

                                '↓iの要素参照位置に文字列が含まれていない場合の入れ替え処理。
                            Else

                                If publishedDate_String_ArrayList(i) > publishedDate_String_ArrayList(i + 1) Then

                                    publishedDate_String_ArrayList(i) = publishedDate_String_ArrayList(i + 1)
                                    publishedDate_String_ArrayList(i + 1) = relay_Date

                                    title_String_ArrayList(i) = title_String_ArrayList(i + 1)
                                    title_String_ArrayList(i + 1) = relay_Title

                                    url_String_ArrayList(i) = url_String_ArrayList(i + 1)
                                    url_String_ArrayList(i + 1) = relay_Link

                                    description_String_ArrayList(i) = description_String_ArrayList(i + 1)
                                    description_String_ArrayList(i + 1) = relay_Summary

                                End If
                            End If

                        End If
                    Next i

                    array_Length = array_Length - 1
                End While
            End If
        End If
        '▲RetrieveFeedAsyncで、取得した要素の順番を、sort_ComboBoxの子要素のitemに準じてで並び替える処理。







        '↓必要件数分のコントロールの配列数を変更。
        ReDim Article_StackPanel_Array(articles_User_Designation_Export_Int)
        ReDim Article_Title_TextBlock_Array(articles_User_Designation_Export_Int)
        ReDim Article_Title_Bottom_Line_Array(articles_User_Designation_Export_Int)
        ReDim Article_Date_TextBlock_Array(articles_User_Designation_Export_Int)
        ReDim url_Button_Array(articles_User_Designation_Export_Int)
        ReDim Article_Content_TextBlock_Array(articles_User_Designation_Export_Int)
        ReDim Image_Array(articles_User_Designation_Export_Int)
        '↑必要件数分のコントロールの配列数を変更。


        '↓記事削除用のカウントをここで０にし、3回目からの記事削除に対応。
        remove_Article_Count_int = 0


        '▼必要件数分の記事を表示するStackPanelを設置
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then
                Article_StackPanel_Array(i) = New StackPanel()
                Article_StackPanel_Array(i).Name = "Article_StackPanel_Array_" & i


                If page_Size_Check_Bool = False Then
                    Article_StackPanel_Array(i).Width = 900
                ElseIf page_Size_Check_Bool = True Then
                    Article_StackPanel_Array(i).Width = 500
                End If


                'Article_StackPanel_Array(i).Height = 200 
                ' ↑動作試験の時は高さを指定する事。コントロールが表示されない為。
                '　　ただし、実装時には必ずコメントアウトする事。
                '    コントロールの高さが固定され、記事の概略情報まで表示されない為。

                Article_StackPanel_Array(i).Background = New SolidColorBrush(Windows.UI.Colors.White)
                Article_StackPanel_Array(i).Margin = New Thickness(2, 20, 2, 10)

                '↓PC版(横幅の方が大きい場合）の処理。
                If page_Size_Check_Bool = False Then
                    Article_Stack_Panel_Area.Children.Add(Article_StackPanel_Array(i))

                    '↓削除する記事のエリアがどちらかを判定。こちらはＰＣエリア。
                    If i = 0 Then
                        article_Remove_Page_Size_Bool = False
                    End If

                    '↓スマホ版（縦幅の方が大きい場合）の処理。
                ElseIf page_Size_Check_Bool = True Then
                    Smp_Article_Stack_Panel_Area.Children.Add(Article_StackPanel_Array(i))

                    '↓削除する記事のエリアがどちらかを判定。こちらはスマホエリア。
                    If i = 0 Then
                        article_Remove_Page_Size_Bool = True
                    End If
                End If

            End If
        Next i
        '▲必要件数分の記事を表示するStackPanelを設置



        '↓Searchボタンが押された際、すでにある記事の表示エリア（StackPanelの数）を削除する為のカウントを代入
        remove_Article_Count_int = Article_StackPanel_Array.Count



        '▼必要件数分の記事を表示したStackPanelへ子要素としてタイトルを追加。
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then
                Article_Title_TextBlock_Array(i) = New TextBlock()
                Article_Title_TextBlock_Array(i).Name = "Article_Title_TextBlock_Array_" & i

                If page_Size_Check_Bool = False Then
                    Article_Title_TextBlock_Array(i).Width = 820
                    Article_Title_TextBlock_Array(i).FontSize = 32
                    Article_Title_TextBlock_Array(i).Margin = New Thickness(40, 33, 40, 0)
                ElseIf page_Size_Check_Bool = True Then
                    Article_Title_TextBlock_Array(i).Width = 470
                    Article_Title_TextBlock_Array(i).FontSize = 24
                    Article_Title_TextBlock_Array(i).Margin = New Thickness(15, 21, 15, 0)
                End If

                Article_Title_TextBlock_Array(i).TextWrapping = TextWrapping.Wrap
                Article_Title_TextBlock_Array(i).Text = title_String_ArrayList(i)

                '↓PC版(横幅の方が大きい場合）の処理。
                If page_Size_Check_Bool = False Then
                    Article_StackPanel_Array(i).Children.Add(Article_Title_TextBlock_Array(i))
                    '↓スマホ版（縦幅の方が大きい場合）の処理。
                ElseIf page_Size_Check_Bool = True Then
                    Article_StackPanel_Array(i).Children.Add(Article_Title_TextBlock_Array(i))
                End If

            End If
        Next i
        '▲必要件数分の記事を表示したStackPanelへ子要素としてタイトルを追加。


        '▼必要件数分の記事を表示したStackPanelへ子要素として日時情報を追加。
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then
                Article_Date_TextBlock_Array(i) = New TextBlock()

                If page_Size_Check_Bool = False Then
                    Article_Date_TextBlock_Array(i).FontSize = 25
                ElseIf page_Size_Check_Bool = True Then
                    Article_Date_TextBlock_Array(i).FontSize = 21
                End If

                Article_Date_TextBlock_Array(i).Margin = New Thickness(0, 10, 20, 0)
                Article_Date_TextBlock_Array(i).HorizontalAlignment = HorizontalAlignment.Right
                Article_Date_TextBlock_Array(i).Text = publishedDate_String_ArrayList(i).ToString()

                Article_StackPanel_Array(i).Children.Add(Article_Date_TextBlock_Array(i))

            End If
        Next i
        '▲必要件数分の記事を表示したStackPanelへ子要素として日時情報を追加。


        '▼必要件数分の記事を表示したStackPanelへ子要素としてボタンのURLを追加。
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then
                url_Button_Array(i) = New Button()
                url_Button_Array(i).Name = "url_Button_Array_" & i
                url_Button_Array(i).Content = url_String_ArrayList(i)
                url_Button_Array(i).Background = New SolidColorBrush(Windows.UI.Color.FromArgb(16, 20, 30, 40))

                If page_Size_Check_Bool = False Then
                    url_Button_Array(i).FontSize = 18
                    url_Button_Array(i).Margin = New Thickness(0, 10, 20, 0)
                ElseIf page_Size_Check_Bool = True Then
                    url_Button_Array(i).FontSize = 16
                    url_Button_Array(i).Margin = New Thickness(60, 10, 20, 0)
                End If


                '↓イベントの設定。
                If page_Size_Check_Bool = False Then
                    AddHandler url_Button_Array(i).Click, AddressOf WebView_Call
                ElseIf page_Size_Check_Bool = True Then
                    AddHandler url_Button_Array(i).Click, AddressOf smp_WebView_Call
                End If


                url_Button_Array(i).HorizontalAlignment = HorizontalAlignment.Right

                Article_StackPanel_Array(i).Children.Add(url_Button_Array(i))

            End If
        Next i
        '▲必要件数分の記事を表示したStackPanelへ子要素としてボタンのURLを追加。



        '▼必要件数分の記事を表示したStackPanelへ子要素として横線を追加。
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then
                Article_Title_Bottom_Line_Array(i) = New Line()
                Article_Title_Bottom_Line_Array(i).X1 = 10
                Article_Title_Bottom_Line_Array(i).Y1 = 10
                Article_Title_Bottom_Line_Array(i).X2 = 880
                Article_Title_Bottom_Line_Array(i).Y2 = 10
                Article_Title_Bottom_Line_Array(i).StrokeThickness = 2
                Article_Title_Bottom_Line_Array(i).Stroke = New SolidColorBrush(Windows.UI.Color.FromArgb(100, 20, 30, 40))
                Article_Title_Bottom_Line_Array(i).Margin = New Thickness(0, 13, 0, 20)

                Article_StackPanel_Array(i).Children.Add(Article_Title_Bottom_Line_Array(i))
            End If
        Next i
        '▲必要件数分の記事を表示したStackPanelへ子要素として横線を追加。







        '▼必要件数分の記事を表示したStackPanelへ子要素として記事の概略を追加。
        For i = 0 To articles_User_Designation_Export_Int
            If i < title_String_ArrayList.Count Then

                Article_Content_TextBlock_Array(i) = New TextBlock()

                If page_Size_Check_Bool = False Then
                    Article_Content_TextBlock_Array(i).Width = 820
                    Article_Content_TextBlock_Array(i).FontSize = 25
                    Article_Content_TextBlock_Array(i).Margin = New Thickness(39, 13, 39, 43)
                ElseIf page_Size_Check_Bool = True Then
                    Article_Content_TextBlock_Array(i).Width = 470
                    Article_Content_TextBlock_Array(i).FontSize = 20
                    Article_Content_TextBlock_Array(i).Margin = New Thickness(17, 13, 17, 23)
                End If

                Article_Content_TextBlock_Array(i).Text = description_String_ArrayList(i).ToString()
                Article_Content_TextBlock_Array(i).TextWrapping = TextWrapping.Wrap

                Article_StackPanel_Array(i).Children.Add(Article_Content_TextBlock_Array(i))

            End If
        Next i
        '▲必要件数分の記事を表示したStackPanelへ子要素として記事の概略を追加。


        Search_Wait_ProgressRing.IsActive = False
        Wait_Background_Black_Border.Visibility = Visibility.Collapsed

        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)

        Button_ALL_ON()

    End Sub
    '▲検索ボタンが押された場合の処理で呼び出される非同期処理







    '=============================================================================================================



    '▼▼▼主要なボタンが押された場合の処理に用いるフィールド変数・Subの記述場所。


    Dim smp_ID_Sign_IN_Area_bool As Boolean = False
    Dim pc_ID_Sign_IN_Area_bool As Boolean = False

    Dim smp_Login_Area_bool As Boolean = False
    Dim pc_Login_Area_bool As Boolean = False

    Dim smp_RSS_Area_bool As Boolean = False
    Dim pc_RSS_Area_bool As Boolean = False

    Dim smp_Setting_Area_bool As Boolean = False
    Dim pc_Setting_Area_bool As Boolean = False

    Dim smp_ID_Delete_Area_bool As Boolean = False
    Dim pc_ID_Delete_Area_bool As Boolean = False

    Dim smp_Category_Area_bool As Boolean = False
    Dim pc_Category_Area_bool As Boolean = False

    Dim button_Flag_Check_bool As Boolean = False


    Private Sub Main_Area_GridLength_OFF()
        PC_Main_Sign_IN_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_Article_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_Login_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_Category_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_RSS_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_Setting_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_ID_Delete_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_WebView_Area.Width = New GridLength(0.0, GridUnitType.Star)
    End Sub

    Private Sub Button_Width_200_Set()
        Search_Button.Width = 200
        Setting_Button.Width = 200
        RSS_Set_Button.Width = 200
        ID_Delete_Button.Width = 200
        Category_Button.Width = 200
        Login_Button.Width = 200
        ID_Sing_IN_Button.Width = 200
        Search_Button.Width = 200
    End Sub

    Private Sub Button_Width_500_Set()
        Search_Button.Width = 500
        Setting_Button.Width = 500
        RSS_Set_Button.Width = 500
        ID_Delete_Button.Width = 500
        Category_Button.Width = 500
        Login_Button.Width = 500
        ID_Sing_IN_Button.Width = 500
        Search_Button.Width = 500
    End Sub


    Private Sub Input_Box_Clear()
        Input_Login_ID.Text = ""
        Input_Login_Password_PasswordBox.Password = ""
        Input_Sign_IN_ID_TextBlock.Text = ""
        Input_Sign_IN_Password_PasswordBox.Password = ""

        smp_Input_Login_ID.Text = ""
        smp_Input_Login_Password_PasswordBox.Password = ""
        smp_Input_Sign_IN_ID_TextBox.Text = ""
        smp_Input_Sign_IN_Password_PasswordBox.Password = ""
    End Sub

    Private Sub SMP_Visibility_Collapsed_Set()

        smp_Article_Stack_Panel_Text_Area.Visibility = Visibility.Collapsed
        smp_Article_Area_ScrollViewer.Visibility = Visibility.Collapsed
        smp_ID_Sing_IN_Area.Visibility = Visibility.Collapsed
        smp_ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed
        smp_Login_Area.Visibility = Visibility.Collapsed
        smp_Setting_Area.Visibility = Visibility.Collapsed
        smp_RSS_Set_Area.Visibility = Visibility.Collapsed
        smp_ID_Delete_Area.Visibility = Visibility.Collapsed
        smp_Category_Area.Visibility = Visibility.Collapsed


        'smp_Article_Stack_Panel_Text_Area.Height = 0
        'smp_Article_Area_ScrollViewer.Height = 0
        'smp_ID_Sing_IN_Area.Height = 0
        'smp_ID_Sign_IN_Impossible_Area.Height = 0
        'smp_Login_Area.Height = 0
        'smp_Setting_Area.Height = 0
        'smp_RSS_Set_Area.Height = 0
        'smp_ID_Delete_Area.Height = 0
        'smp_Category_Area.Height = 0

    End Sub

    Private Sub Area_Bool_False_Set()
        setting_Area_bool = False
        article_Area_bool = False
        rss_Area_bool = False
        login_Area_bool = False
        category_Area_bool = False
        id_Delete_Area_bool = False
        sign_IN_Area_bool = False
    End Sub


    Private Sub Button_ALL_OFF()
        Back_AppBarButton.IsEnabled = False
        Category_Button.IsEnabled = False
        check_ID_Delete_Button.IsEnabled = False
        check_RSS_Delet_Button.IsEnabled = False
        Close_AppBarButton.IsEnabled = False
        Exit_Button.IsEnabled = False
        Forward_AppBarButton.IsEnabled = False
        Hamburger_Button.IsEnabled = False
        ID_ALL_Delete_Button.IsEnabled = False
        ID_Delete_Button.IsEnabled = False
        ID_Sing_IN_Button.IsEnabled = False
        Login_Area_Login_Button.IsEnabled = False
        Login_Button.IsEnabled = False
        Login_Password_Visualization_Button.IsEnabled = False
        Logout_Button.IsEnabled = False
        Refresh_AppBarButton.IsEnabled = False
        Reset_Button.IsEnabled = False
        Result_Close_Button.IsEnabled = False
        RSS_Add_Button.IsEnabled = False
        RSS_ALL_Delete_Button.IsEnabled = False
        RSS_Reset_Button.IsEnabled = False
        RSS_Set_Button.IsEnabled = False
        Setting_Button.IsEnabled = False
        Sign_IN_Button.IsEnabled = False
        Sing_IN_Password_Visualization_Button.IsEnabled = False
        smp_check_ID_Delete_Button.IsEnabled = False
        smp_check_RSS_Delet_Button.IsEnabled = False
        smp_ID_ALL_Delete_Button.IsEnabled = False
        smp_Login_Area_Login_Button.IsEnabled = False
        smp_Login_Password_Visualization_Button.IsEnabled = False
        smp_Logout_Button.IsEnabled = False
        smp_Result_Close_Button.IsEnabled = False
        smp_RSS_Add_Button.IsEnabled = False
        smp_RSS_ALL_Delete_Button.IsEnabled = False
        smp_RSS_Reset_Button.IsEnabled = False
        smp_Sign_IN_Button.IsEnabled = False
        smp_Sing_IN_Password_Visualization_Button.IsEnabled = False
        smp_Sort_ComboBox.IsEnabled = False
        sort_ComboBox.IsEnabled = False
        Search_Button.IsEnabled = False
    End Sub



    Private Sub Button_ALL_ON()
        Back_AppBarButton.IsEnabled = True
        Category_Button.IsEnabled = True
        check_ID_Delete_Button.IsEnabled = True
        check_RSS_Delet_Button.IsEnabled = True
        Close_AppBarButton.IsEnabled = True
        Exit_Button.IsEnabled = True
        Forward_AppBarButton.IsEnabled = True
        Hamburger_Button.IsEnabled = True
        ID_ALL_Delete_Button.IsEnabled = True
        ID_Delete_Button.IsEnabled = True
        ID_Sing_IN_Button.IsEnabled = True
        Login_Area_Login_Button.IsEnabled = True
        Login_Button.IsEnabled = True
        Login_Password_Visualization_Button.IsEnabled = True
        Logout_Button.IsEnabled = True
        Refresh_AppBarButton.IsEnabled = True
        Reset_Button.IsEnabled = True
        Result_Close_Button.IsEnabled = True
        RSS_Add_Button.IsEnabled = True
        RSS_ALL_Delete_Button.IsEnabled = True
        RSS_Reset_Button.IsEnabled = True
        RSS_Set_Button.IsEnabled = True
        Setting_Button.IsEnabled = True
        Sign_IN_Button.IsEnabled = True
        Sing_IN_Password_Visualization_Button.IsEnabled = True
        smp_check_ID_Delete_Button.IsEnabled = True
        smp_check_RSS_Delet_Button.IsEnabled = True
        smp_ID_ALL_Delete_Button.IsEnabled = True
        smp_Login_Area_Login_Button.IsEnabled = True
        smp_Login_Password_Visualization_Button.IsEnabled = True
        smp_Logout_Button.IsEnabled = True
        smp_Result_Close_Button.IsEnabled = True
        smp_RSS_Add_Button.IsEnabled = True
        smp_RSS_ALL_Delete_Button.IsEnabled = True
        smp_RSS_Reset_Button.IsEnabled = True
        smp_Sign_IN_Button.IsEnabled = True
        smp_Sing_IN_Password_Visualization_Button.IsEnabled = True
        smp_Sort_ComboBox.IsEnabled = True
        sort_ComboBox.IsEnabled = True
        Search_Button.IsEnabled = True
    End Sub




    '▼ユーザー登録のボタンが押された場合に、レイアウトを変更する処理。
    Private Sub ID_Sing_IN_Button_Click(sender As Object, e As RoutedEventArgs) Handles ID_Sing_IN_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()


        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_ID_Sign_IN_Area_bool = False
            pc_ID_Sign_IN_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_ID_Sign_IN_Area_bool = True
            pc_ID_Sign_IN_Area_bool = False
        End If


        If smp_ID_Sign_IN_Area_bool = False Then

            If sign_IN_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                sign_IN_Area_bool = True
                PC_Main_Sign_IN_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()


            ElseIf sign_IN_Area_bool = True Then

                sign_IN_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_ID_Sing_IN_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_ID_Sign_IN_Area_bool = True Then

            If sign_IN_Area_bool = False Then

                sign_IN_Area_bool = True
                smp_ID_Sing_IN_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf sign_IN_Area_bool = True Then

                sign_IN_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_ID_Sign_IN_Area_bool = False Then
                    Button_Width_500_Set()

                End If

                smp_ID_Sing_IN_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲ユーザー登録のボタンが押された場合に、レイアウトを変更する処理。





    '▼ログインのボタンが押された場合に、レイアウトを変更する処理。
    Private Sub Login_Button_Click(sender As Object, e As RoutedEventArgs) Handles Login_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()



        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_Login_Area_bool = False
            pc_Login_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_Login_Area_bool = True
            pc_Login_Area_bool = False
        End If


        If smp_Login_Area_bool = False Then

            If login_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                login_Area_bool = True
                PC_Main_Login_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf login_Area_bool = True Then

                login_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_Login_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_Login_Area_bool = True Then

            If login_Area_bool = False Then

                login_Area_bool = True
                smp_Login_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf login_Area_bool = True Then

                login_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_Login_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_Login_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲ログインのボタンが押された場合に、レイアウトを変更する処理。





    '▼RSS登録のボタンが押された場合に、レイアウトを変更する処理。
    Private Sub RSS_Set_Button_Click(sender As Object, e As RoutedEventArgs) Handles RSS_Set_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()



        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_RSS_Area_bool = False
            pc_RSS_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_RSS_Area_bool = True
            pc_RSS_Area_bool = False
        End If


        If smp_RSS_Area_bool = False Then

            If rss_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                rss_Area_bool = True
                PC_Main_RSS_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf rss_Area_bool = True Then

                rss_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_RSS_Set_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_RSS_Area_bool = True Then

            If rss_Area_bool = False Then

                rss_Area_bool = True
                smp_RSS_Set_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf rss_Area_bool = True Then

                rss_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_RSS_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_RSS_Set_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲RSS登録のボタンが押された場合に、レイアウトを変更する処理。




    '▼設定のボタンが押された場合に、レイアウトを変更する処理。
    Private Sub Setting_Button_Click(sender As Object, e As RoutedEventArgs) Handles Setting_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()


        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_Setting_Area_bool = False
            pc_Setting_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_Setting_Area_bool = True
            pc_Setting_Area_bool = False
        End If


        If smp_Setting_Area_bool = False Then

            If setting_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                setting_Area_bool = True
                PC_Main_Setting_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf setting_Area_bool = True Then

                setting_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_Setting_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_Setting_Area_bool = True Then

            If setting_Area_bool = False Then

                setting_Area_bool = True
                smp_Setting_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf setting_Area_bool = True Then

                setting_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_Setting_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_Setting_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲設定のボタンが押された場合に、レイアウトを変更する処理。




    '▼検索ワード登録のボタンが押された場合に、レイアウトを変更する処理。
    Private Sub Category_Button_Click(sender As Object, e As RoutedEventArgs) Handles Category_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()


        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_Category_Area_bool = False
            pc_Category_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_Category_Area_bool = True
            pc_Category_Area_bool = False
        End If


        If smp_Category_Area_bool = False Then

            If category_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                category_Area_bool = True
                PC_Main_Category_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_Category_Area_bool = True Then

            If category_Area_bool = False Then

                category_Area_bool = True
                smp_Category_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_Category_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲検索ワード登録のボタンが押された場合に、レイアウトを変更する処理。




    '▼ユーザー削除のボタンが押された場合に、レイアウトを変更する処理。
    Private Sub ID_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles ID_Delete_Button.Click

        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()


        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_ID_Delete_Area_bool = False
            pc_ID_Delete_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_ID_Delete_Area_bool = True
            pc_ID_Delete_Area_bool = False
        End If


        If smp_ID_Delete_Area_bool = False Then

            If id_Delete_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                id_Delete_Area_bool = True
                PC_Main_ID_Delete_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf id_Delete_Area_bool = True Then

                id_Delete_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_ID_Delete_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_ID_Delete_Area_bool = True Then

            If id_Delete_Area_bool = False Then

                id_Delete_Area_bool = True
                smp_ID_Delete_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf id_Delete_Area_bool = True Then

                id_Delete_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_ID_Delete_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_ID_Delete_Area.Visibility = Visibility.Collapsed

            End If
        End If
    End Sub
    '▲ユーザー削除のボタンが押された場合に、レイアウトを変更する処理。

    '▲▲▲主要なボタンが押された場合の処理に用いるフィールド変数・Subの記述場所。






    '▼検索結果画面の閉じるボタンが押された場合の処理。
    Private Sub Result_Close_Button_Click(sender As Object, e As RoutedEventArgs) Handles Result_Close_Button.Click

        article_Area_bool = False
        PC_Main_Article_Area.Width = New GridLength(0.0, GridUnitType.Star)

        If rss_Area_bool = True Then

            ID_Display_StackPanel.Width = 500

        ElseIf rss_Area_bool = False Then

            ID_Display_StackPanel.Width = 1460

            Search_Button.Width = 500
            Setting_Button.Width = 500
            RSS_Set_Button.Width = 500
            ID_Delete_Button.Width = 500
            Category_Button.Width = 500
            Login_Button.Width = 500
            ID_Sing_IN_Button.Width = 500
            Search_Button.Width = 500

        End If

    End Sub
    '↑検索結果画面の閉じるボタンが押された場合の処理。

    '▲UI側のボタンが押された場合に、レイアウトを変更する処理。





    '▼PC版のID登録画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。
    Private Sub Sing_IN_Password_Visualization_Button_Click(sender As Object, e As RoutedEventArgs) Handles Sing_IN_Password_Visualization_Button.Click

        If Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible Then

            Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden
            Sing_IN_Password_Visualization_Button.Content = "パスワードを表示する"

        ElseIf Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden Then

            Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible
            Sing_IN_Password_Visualization_Button.Content = "パスワードを非表示にする"

        End If
    End Sub
    '▲PC版のID登録画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。


    '▼PC版のログイン画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。
    Private Sub Login_Password_Visualization_Button_Click(sender As Object, e As RoutedEventArgs) Handles Login_Password_Visualization_Button.Click

        If Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible Then

            Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden
            Login_Password_Visualization_Button.Content = "パスワードを表示する"

        ElseIf Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden Then

            Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible
            Login_Password_Visualization_Button.Content = "パスワードを非表示にする"

        End If
    End Sub
    '▲ＰＣ版のログイン画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。




    '▼RSSが登録された場合の処理。
    Private Sub RSS_Add_Button_Click(sender As Object, e As RoutedEventArgs) Handles RSS_Add_Button.Click

        Dim input_String As String = RSS_Input_TextBox.Text

        If (RSS_Input_TextBox.Text IsNot Nothing) AndAlso (RSS_Input_TextBox.Text.Length <> 0) Then

            If Check_ListBox_Items(RSS_Input_TextBox.Text) Then
                RSS_ListBox.Items.Add(input_String)
                smp_RSS_ListBox.Items.Add(input_String)

                RSS_Input_TextBox.Text = ""
                smp_RSS_Input_TextBox.Text = ""

                RSS_List_Text_Write()

                Show_Message("入力情報を登録しました。")
            Else
                Show_Message("同じ名前の入力済み情報があります。" & vbCrLf & "入力を中止しました。")
            End If

        Else
            Show_Message("RSSが入力されていません。")
        End If

    End Sub
    '▲RSSが登録された場合の処理。



    '▼RSSが登録された際、重複がある場合には中止する処理。
    Private Function Check_ListBox_Items(strItem As String) As Boolean

        Check_ListBox_Items = True

        For i = 0 To RSS_ListBox.Items.Count - 1
            If RSS_ListBox.Items(i) = strItem Then
                Check_ListBox_Items = False
                Exit For
            End If
        Next i
    End Function
    '▲RSSが登録された際、重複がある場合には中止する処理。




    '▼PC版の選択されたRSSが削除されたボタンが押された場合の処理。
    Private Sub check_RSS_Delet_Button_Click(sender As Object, e As RoutedEventArgs) Handles check_RSS_Delet_Button.Click


        If RSS_ListBox.SelectedItems.Count = 0 Then
            Show_Message("削除したいRSSが選択されていません。")
            Exit Sub
        End If


        Dim selected_Index As Integer = RSS_ListBox.SelectedIndex


        ' 選択された項目を削除
        RSS_ListBox.Items.RemoveAt(RSS_ListBox.SelectedIndex)
        smp_RSS_ListBox.Items.RemoveAt(selected_Index)

        RSS_List_Text_Write()

        Show_Message("選択されたRSS情報を削除しました。")
    End Sub
    '▲PC版の選択されたRSSが削除されたボタンが押された場合の処理。



    '▼PC版のRSSの全削除ボタンが押された場合の処理
    Private Sub RSS_ALL_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles RSS_ALL_Delete_Button.Click

        RSS_ListBox.Items.Clear()
        smp_RSS_ListBox.Items.Clear()

        RSS_List_Text_Write()

        Show_Message("RSS情報を全て削除しました。")

    End Sub
    '▲PC版のRSSの全削除ボタンが押された場合の処理



    '▼処理毎にメッセージを表示する処理
    Public Async Sub Show_Message(relay_string As String)

        Dim message_Dialog = New Windows.UI.Popups.MessageDialog(relay_string)

        Try
            Await message_Dialog.ShowAsync()
        Catch ex As Exception

        End Try

    End Sub

    '▲処理毎にメッセージを表示する処理


    '▼処理毎にメッセージを表示する処理（選択肢あり）
    Public Async Function Show_Message_Select(relay_string As String) As Task(Of Boolean)
        Dim message_Dialog = New MessageDialog(relay_string)

        message_Dialog.Commands.Add(New UICommand("はい"))
        message_Dialog.Commands.Add(New UICommand("いいえ"))

        Dim select_Boolean As Boolean = False
        Dim res

        Do
            Try
                res = Await message_Dialog.ShowAsync()
                Exit Do
            Catch ex As Exception

            End Try
        Loop


        If res.Label = "はい" Then
            'ここにログアウト処理を実装。
            select_Boolean = True
            Return select_Boolean
        Else
            Return select_Boolean
        End If

    End Function
    '▲処理毎にメッセージを表示する処理（選択肢あり）




    '▼【初期化】ボタンが押された場合の処理（起動時のRSS情報の再表示）

    Private Async Sub RSS_Reset_Button_Click(sender As Object, e As RoutedEventArgs) Handles RSS_Reset_Button.Click


        Dim select_Boolean = Await Show_Message_Select("RSSの登録状態を起動時の状態に初期化して戻します。" & vbCrLf & "よろしいですか？")

        If select_Boolean = True Then

            RSS_Reset_Button_Click_Sub()

        End If
    End Sub




    Private Async Sub RSS_Reset_Button_Click_Sub()
        RSS_ListBox.Items.Clear()
        smp_RSS_ListBox.Items.Clear()

        '↓ListBoxへとFor文で代入処理。「Check_ListBox_Items()」を呼び出し、ListBoxへ代入されていく内容は重複を排除する。
        For Each line As String In reset_RSS_lines_List_Of_String
            If Check_ListBox_Items(line) Then
                RSS_ListBox.Items.Add(line)
                smp_RSS_ListBox.Items.Add(line)
            End If
        Next

        '↓ユーザー側でローカルフォルダの "RSS_List.txt" に、重複した内容が大量に書き込まれていた場合に重複を排除する為に呼び出す。
        RSS_List_Text_Write()

        Show_Message("RSSの登録情報の起動時の状態へ戻しました。")
    End Sub
    '▲【初期化】ボタンが押された場合の処理（起動時のRSS情報の再表示）





    '▼UWPアプリのローカルフォルダに保存されるtxtファイルへの書き込み・保存処理。
    Private Async Sub RSS_List_Text_Write()

        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「"RSS_List.txt"」を読み込ませる為の StorageFile を宣言し、『 rss_List_txt_StorageFile 』変数へ役割の代入。
        Dim rss_List_txt_StorageFile As StorageFile



        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_rss_txt_Check_1 As String = storage_Folder.Path
        Dim local_rss_txt_Check_2 As String = local_rss_txt_Check_1 & "\RSS_List.txt"





        '↓「"RSS_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_rss_txt_Check_2) Then
            rss_List_txt_StorageFile = Await storage_Folder.GetFileAsync("RSS_List.txt")
        Else
            rss_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("RSS_List.txt")
        End If


        '↓ListBoxに入っているRSSのURLを、最終的に『AppendLinesAsync』で RSS_List.txt に対して書き込みを行うのにコレクションが必要の為、
        '  コレクションへ代入する為のListをString型で宣言する。
        Dim rss_List_Of_String = New List(Of String)()


        '↓上で宣言したListの rss_List_Of_String　へ、ListBoxの内容を追加していく。
        For i = 0 To RSS_ListBox.Items.Count - 1
            rss_List_Of_String.Add(RSS_ListBox.Items(i).ToString())
        Next


        '↓追加で書くのではなく、ListBoxに入っている内容のみを、RSS_List.txtへ書き込みさせたいため、いったん「""」で全削除
        Do
            Try
                Await FileIO.WriteTextAsync(rss_List_txt_StorageFile, "")
                Exit Do
            Catch

            End Try
        Loop



        Do
            Try
                '↓コレクションへ代入。
                Dim rss_texts_IEnumerable As IEnumerable(Of String) = rss_List_Of_String

                '↓AppendLinesAsyncを用いて、コレクションの内容を、RSS_List.txtへ代入。
                Await FileIO.AppendLinesAsync(rss_List_txt_StorageFile, rss_texts_IEnumerable)
                Exit Do
            Catch

            End Try
        Loop

    End Sub
    '▲UWPアプリのローカルフォルダに保存されるtxtファイルへの書き込み・保存処理。





    '▼「Page_Loaded_Event」側で呼び出すRSSをListBoxへ設定する処理。（UWPアプリのローカルフォルダに保存されるtxtファイルへを読み込み、ListBox側へ起動時に読み込ませる処理）
    Private Async Sub RSS_List_Text_ListBox_Set()


        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「"RSS_List.txt"」を読み込ませる為の StorageFile を宣言し、『 rss_List_txt_StorageFile 』変数へ役割の代入。
        Dim rss_List_txt_StorageFile As StorageFile



        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_rss_txt_Check_1 As String = storage_Folder.Path
        Dim local_rss_txt_Check_2 As String = local_rss_txt_Check_1 & "\RSS_List.txt"


        '↓「"RSS_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_rss_txt_Check_2) Then
            rss_List_txt_StorageFile = Await storage_Folder.GetFileAsync("RSS_List.txt")
        Else
            rss_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("RSS_List.txt")
        End If


        '↓「"RSS_List.txt"」を行ごとに分解して読み込む
        Dim rss_lines_List_Of_String As IList(Of String) = Await FileIO.ReadLinesAsync(rss_List_txt_StorageFile)



        '↓ListBoxの内容をリセットで入れる処理を行う為のフィールド変数の「reset_RSS_lines_List_Of_String」へ起動時の読み込みの内容の代入。
        reset_RSS_lines_List_Of_String = Await FileIO.ReadLinesAsync(rss_List_txt_StorageFile)




        '↓ListBoxへとFor文で代入処理。「Check_ListBox_Items()」を呼び出し、ListBoxへ代入されていく内容は重複を排除する。
        For Each line As String In rss_lines_List_Of_String
            If Check_ListBox_Items(line) Then
                RSS_ListBox.Items.Add(line)
                smp_RSS_ListBox.Items.Add(line)
            End If
        Next

        '↓ユーザー側でローカルフォルダの "RSS_List.txt" に、重複した内容が大量に書き込まれていた場合に重複を排除する為に呼び出す。
        RSS_List_Text_Write()

    End Sub
    '▲「Page_Loaded_Event」側で呼び出すRSSをListBoxへ設定する処理。（UWPアプリのローカルフォルダに保存されるtxtファイルへを読み込み、ListBox側へ起動時に読み込ませる処理）










    '▼IDの登録画面の登録ボタンが押された場合の処理。

    Private Async Sub Sign_IN_Button_Click(sender As Object, e As RoutedEventArgs) Handles Sign_IN_Button.Click

        Sign_IN_Button_Click_Sub(Input_Sign_IN_ID_TextBlock.Text, Input_Sign_IN_Password_PasswordBox.Password)

    End Sub


    Private Async Sub smp_Sign_IN_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Sign_IN_Button.Click

        Sign_IN_Button_Click_Sub(smp_Input_Sign_IN_ID_TextBox.Text, smp_Input_Sign_IN_Password_PasswordBox.Password)

    End Sub



    Private Async Sub Sign_IN_Button_Click_Sub(input_Relay_ID As String, input_Relay_Password As String)

        Dim input_ID_Sign_IN
        Dim input_Password

        If (input_Relay_ID IsNot Nothing) AndAlso (input_Relay_ID.Length <> 0) Then
            input_ID_Sign_IN = input_Relay_ID
        Else
            Show_Message("ID情報が入力されていません。" & vbCrLf & "ID情報を登録する際にはID名とパスワードを登録して下さい。")
            Exit Sub
        End If



        If (input_Relay_Password IsNot Nothing) AndAlso (input_Relay_Password.Length <> 0) Then
            input_Password = input_Relay_Password
        Else
            Show_Message("パスワード情報が入力されていません。" & vbCrLf & "ID情報を登録する際にはID名とパスワードを登録して下さい。")
            Exit Sub
        End If



        If input_Relay_Password.Length < 8 Then
            Show_Message("パスワードの文字数が8文字以下です。" & vbCrLf & "8文字以上で入力して下さい。")
            Exit Sub
        End If



        Dim regex_Patern = New Regex("[a-zA-Z0-9\-]+", RegexOptions.Compiled)
        Dim pattern_Check As Boolean = regex_Patern.IsMatch(input_Password)

        If pattern_Check = False Then
            Show_Message("パスワード情報に半角数字・半角英字以外の文字が含まれています。" & vbCrLf & "半角数字・半角英字のみで入力してパスワードを登録して下さい。")
            Exit Sub
        End If



        '↓IDに重複がないか確認する為、PasswordVaultを呼びだす。
        Dim password_Vault = New Windows.Security.Credentials.PasswordVault()


        '↓作業時にはコメントアウトし、[test]のIDで動作確認を行う。（※一連の流れ自体はIDとパスワードでログインする流れに変わらない為。
        Try
            '↓IDの重複がないか確認する箇所
            Dim check_Password = password_Vault.Retrieve("RSS_Japan_News", input_ID_Sign_IN)

            If (check_Password.Password IsNot Nothing) Then
                Show_Message("入力されたIDは既に登録されています。" & vbCrLf & "別のID名で登録して下さい。" & vbCrLf & "ログインしたい場合にはログイン画面からログインして下さい。")
                Exit Sub
            End If

        Catch ex As Exception

        End Try




        '↓新規のＩＤであるならばTextBoxに入っている文字列を削除。
        input_Relay_ID = ""
        input_Relay_Password = ""



        '↓ パスワード情報の保存
        Dim credential = New Windows.Security.Credentials.PasswordCredential("RSS_Japan_News", input_ID_Sign_IN, input_Password)
        password_Vault.Add(credential)

        '↓ 登録可能なパスワード情報の件数の更新。
        Remaing_ID_Count_Text_Change_Sub()


        '↓ここでtxtファイル（名義：id_List）の作成、登録呼び出しの関数を設置。
        ID_List_Text_Write(input_ID_Sign_IN)

        Dim select_Boolean = Await Show_Message_Select("IDの作成が完了しました。" & vbCrLf & "ログイン状態へ移行しますか？")

        If select_Boolean = True Then
            Login_Sub(input_ID_Sign_IN, input_Password)
        End If

    End Sub





    '▼ログイン画面でのログインボタンが押された場合のログイン処理へ続く前の処理
    Private Sub Login_Area_Login_Button_Click(sender As Object, e As RoutedEventArgs) Handles Login_Area_Login_Button.Click

        Login_Area_Login_Button_Sub(Input_Login_ID.Text, Input_Login_Password_PasswordBox.Password)

    End Sub
    '▲ログイン画面でのログインボタンが押された場合のログイン処理へ続く前の処理



    Private Sub smp_Login_Area_Login_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Login_Area_Login_Button.Click
        Login_Area_Login_Button_Sub(smp_Input_Login_ID.Text, smp_Input_Login_Password_PasswordBox.Password)
    End Sub


    Private Sub Login_Area_Login_Button_Sub(input_Relay_ID As String, input_Relay_Password As String)
        Dim input_ID_Sign_IN
        Dim input_Password

        If (input_Relay_ID IsNot Nothing) AndAlso (input_Relay_ID.Length <> 0) Then
            input_ID_Sign_IN = input_Relay_ID
        Else
            Show_Message("ID情報が入力されていません。" & vbCrLf & "ログインする際にはIDとパスワードを入力して下さい。")
            Exit Sub
        End If


        If (input_Relay_Password IsNot Nothing) AndAlso (input_Relay_Password.Length <> 0) Then
            input_Password = input_Relay_Password
        Else
            Show_Message("パスワード情報が入力されていません。" & vbCrLf & "ログインする際にはIDとパスワードを入力して下さい。")
            Exit Sub
        End If

        Login_Sub(input_ID_Sign_IN, input_Password)
    End Sub



    '▼ID登録画面、ログイン画面でのログインボタンが押された場合の処理。
    Private Sub Login_Sub(ByVal input_ID, ByVal input_Password)

        'パスワード判定
        Dim password_Vault = New Windows.Security.Credentials.PasswordVault()
        Dim credential

        Try
            credential = password_Vault.Retrieve("RSS_Japan_News", input_ID)
        Catch ex As Exception
            Input_Login_ID.Text = ""
            Input_Login_Password_PasswordBox.Password = ""
            Show_Message("入力されたIDは存在しません。")
            Exit Sub
        End Try



        '↓ログイン画面側からログインした際に、パスワードが異なる場合に、パスワードが違っていれば中断する。
        If credential.Password <> input_Password Then
            '余裕があれば、パスワード試行回数に制限を設ける。
            Show_Message("入力されたIDのパスワードが異なります。")
            Exit Sub
        End If


        ''↓パスワードが正しいならば処理を続け、レイアウトの変更。
        ID_Sing_IN_Button.Visibility = Visibility.Collapsed
        Login_Button.Visibility = Visibility.Collapsed
        Category_Button.Visibility = Visibility.Visible



        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()

        sign_IN_Area_bool = False



        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_Category_Area_bool = False
            pc_Category_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_Category_Area_bool = True
            pc_Category_Area_bool = False
        End If


        If smp_Category_Area_bool = False Then

            If category_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                category_Area_bool = True
                PC_Main_Category_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_Category_Area_bool = True Then

            If category_Area_bool = False Then

                category_Area_bool = True
                smp_Category_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_Category_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If
        End If


        smp_ID_Sing_IN_Area.Visibility = Visibility.Collapsed
        smp_Login_Area.Visibility = Visibility.Collapsed

        smp_ID_Sign_IN_Area_bool = False
        pc_ID_Sign_IN_Area_bool = False

        smp_Login_Area_bool = False
        pc_Login_Area_bool = False


        Login_After_Set_Area.Height = New GridLength(60.0, GridUnitType.Star)

        ID_Display_TextBlock.Text = "ログイン中のID：" & input_ID

        category_Area_bool = True
        login_Area_bool = False

        '↓検索ワードの登録箇所の処理用のフィールド変数へのＩＤ名の代入。
        relay_ID = input_ID
        Tag_Login_Set_Sub()


        Input_Login_ID.Text = ""
        Input_Login_Password_PasswordBox.Password = ""

        Show_Message("ログインしました。")

        ''2回目以降の操作時、仮に属性が登録済みの場合の登録の読み込みと、
        ''1回目の操作である場合には５つの属性値の初期化を行う処理の呼び出し。

    End Sub
    '▲ID登録画面、ログイン画面でのログインボタンが押された場合の処理。



    '▼パスワード形式の登録件数の残数の表示テキストの変更用の処理。
    Private Sub Remaing_ID_Count_Text_Change_Sub()
        Dim password_Vault = New Windows.Security.Credentials.PasswordVault()
        Dim count As Integer = password_Vault.RetrieveAll.Count
        count = 20 - count
        ID_Remaining_Count_TextBlock_1.Text = count.ToString()
        ID_Remaining_Count_TextBlock_2.Text = count.ToString()

        smp_ID_Remaining_Count_TextBlock_1.Text = count.ToString()
        smp_ID_Remaining_Count_TextBlock_2.Text = count.ToString()

        '↓登録可能件数が20件までなので、20件を境にログイン画面に表示する画面を変更。
        If password_Vault.RetrieveAll.Count >= 20 Then
            ID_Sign_IN_Possible_Area.Visibility = Visibility.Collapsed
            ID_Sign_IN_Impossible_Area.Visibility = Visibility.Visible

            smp_ID_Sign_IN_Possible_Area.Visibility = Visibility.Collapsed
            smp_ID_Sign_IN_Impossible_Area.Visibility = Visibility.Visible
        ElseIf password_Vault.RetrieveAll.Count < 20 Then
            ID_Sign_IN_Possible_Area.Visibility = Visibility.Visible
            ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed

            smp_ID_Sign_IN_Possible_Area.Visibility = Visibility.Visible
            smp_ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed
        End If
        '↑登録可能件数が20件までなので、20件を境にログイン画面に表示する画面を変更。

    End Sub
    '▲パスワード形式の登録件数の残数の表示テキストの変更用の処理。





    '▼UWPアプリのローカルフォルダに保存されるtxtファイルへの書き込み・保存処理。（IDの名簿一覧用）
    Private Async Sub ID_List_Text_Write(ByVal input_ID)

        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「"ID_List.txt"」を読み込ませる為の StorageFile を宣言し、『 ID_List_txt_StorageFile 』変数へ役割の代入。
        Dim ID_List_txt_StorageFile As StorageFile


        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_txt_Check_1 As String = storage_Folder.Path
        Dim local_txt_Check_2 As String = local_txt_Check_1 & "\ID_List.txt"


        '↓「"ID_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_txt_Check_2) Then
            ID_List_txt_StorageFile = Await storage_Folder.GetFileAsync("ID_List.txt")
        Else
            ID_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("ID_List.txt")
        End If


        '↓「ID_List.txt」の内容を行で取得。
        Dim ID_lines_List_Of_String As IList(Of String) = Await FileIO.ReadLinesAsync(ID_List_txt_StorageFile)


        '↓ID_List.txt に対して書き込みを行うのにコレクションが必要の為、コレクションへ代入する為のListをString型で宣言する。
        Dim ID_List_Of_String = New List(Of String)()


        '↓ID_List.txt（ID_lines_List_Of_String）の内容を、ID_List_Of_StringへとFor文で代入処理。
        For Each line As String In ID_lines_List_Of_String
            ID_List_Of_String.Add(line)
        Next

        '↓引数のIDを追加で増やす。
        ID_List_Of_String.Add(input_ID)

        '↓1回、ID_List.txt（ID_lines_List_Of_String）を白紙にする。
        Do
            Try
                Await FileIO.WriteTextAsync(ID_List_txt_StorageFile, "")
                Exit Do
            Catch ex As Exception

            End Try
        Loop



        '↓ID_List.txt（ID_lines_List_Of_String）へ、ID_List_Of_StringをIEnumerableに経てから代入する為の変数宣言。
        '  Distinct() は、重複の削除
        Dim ID_texts_IEnumerable As IEnumerable(Of String) = ID_List_Of_String.Distinct()


        Do
            Try
                '↓AppendLinesAsyncを用いて、コレクションの内容を、ID_List.txtへ代入。
                Await FileIO.AppendLinesAsync(ID_List_txt_StorageFile, ID_texts_IEnumerable)
                Exit Do
            Catch

            End Try
        Loop


        '↓IDのListBoxへ、新規に登録した分も含めて、改めてListへ並べる処理を呼び出す。
        ID_List_Text_ListBox_Set()

    End Sub
    '▲UWPアプリのローカルフォルダに保存されるtxtファイルへの書き込み・保存処理。（IDの名簿一覧用）






    '▼PC版のカテゴリ画面（検索ワード登録画面）のログアウトボタンが押された場合の処理。
    Private Async Sub Logout_Button_Click(sender As Object, e As RoutedEventArgs) Handles Logout_Button.Click

        Dim select_Boolean

        select_Boolean = Await Show_Message_Select("ログアウトします。" & vbCrLf & "よろしいですか？")

        If select_Boolean = True Then
            Logout_Sub()
        Else

        End If
    End Sub
    '▲PC版のカテゴリ画面（検索ワード登録画面）のログアウトボタンが押された場合の処理。



    '▼ログアウト処理
    Private Sub Logout_Sub()


        If relay_ID <> Nothing Then
            Show_Message("ログアウトしました。")
        End If

        relay_ID = Nothing

        ID_Sing_IN_Button.Visibility = Visibility.Visible
        Login_Button.Visibility = Visibility.Visible
        Category_Button.Visibility = Visibility.Collapsed


        PC_Main_Category_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
        Login_After_Set_Area.Height = New GridLength(0.0, GridUnitType.Star)
        ID_Display_TextBlock.Text = ""


        WebView_URL_Text_Box_Clear()
        Main_Area_GridLength_OFF()
        Input_Box_Clear()


        If width_Size_Check_Double > height_Size_Check_Double Then
            smp_Category_Area_bool = False
            pc_Category_Area_bool = True
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            smp_Category_Area_bool = True
            pc_Category_Area_bool = False
        End If


        If smp_Category_Area_bool = False Then

            If category_Area_bool = False Then

                SMP_Visibility_Collapsed_Set()
                Area_Bool_False_Set()
                category_Area_bool = True
                PC_Main_Category_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
                ID_Display_StackPanel.Width = 500
                Button_Width_200_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460
                Button_Width_500_Set()

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If

        ElseIf smp_Category_Area_bool = True Then

            If category_Area_bool = False Then

                category_Area_bool = True
                smp_Category_Area.Visibility = Visibility.Visible
                ID_Display_StackPanel.Width = 500

                Button_Width_500_Set()

            ElseIf category_Area_bool = True Then

                category_Area_bool = False
                ID_Display_StackPanel.Width = 1460

                If pc_Category_Area_bool = False Then
                    Button_Width_500_Set()
                End If

                smp_Category_Area.Visibility = Visibility.Collapsed

            End If
        End If


        Search_Button.Width = 500
        Setting_Button.Width = 500
        RSS_Set_Button.Width = 500
        ID_Delete_Button.Width = 500
        Category_Button.Width = 500
        Login_Button.Width = 500
        ID_Sing_IN_Button.Width = 500
        Search_Button.Width = 500

        tag_1_TextBox.Text = ""
        tag_2_TextBox.Text = ""
        tag_3_TextBox.Text = ""
        tag_4_TextBox.Text = ""
        tag_5_TextBox.Text = ""

        smp_tag_1_TextBox.Text = ""
        smp_tag_2_TextBox.Text = ""
        smp_tag_3_TextBox.Text = ""
        smp_tag_4_TextBox.Text = ""
        smp_tag_5_TextBox.Text = ""

    End Sub
    '▲ログアウト処理



    '▼PC版の検索ワードのの登録用のTextBox1番目の処理。
    Private Sub tag_1_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tag_1_TextBox.TextChanged

        smp_tag_1_TextBox.Text = tag_1_TextBox.Text

        Dim tag_Input_String As String = tag_1_TextBox.Text
        Dim tag_Number_String As String = "_Tag_Number_1"

        Tag_Set_Sub(tag_Input_String, tag_Number_String)
    End Sub
    '▲PC版の検索ワードのの登録用のTextBox1番目の処理。





    '▼PC版の検索ワードのの登録用のTextBox2番目の処理。
    Private Sub tag_2_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tag_2_TextBox.TextChanged

        smp_tag_2_TextBox.Text = tag_2_TextBox.Text

        Dim tag_Input_String As String = tag_2_TextBox.Text
        Dim tag_Number_String As String = "_Tag_Number_2"

        Tag_Set_Sub(tag_Input_String, tag_Number_String)
    End Sub
    '▲PC版の検索ワードのの登録用のTextBox2番目の処理。



    '▼PC版の検索ワードのの登録用のTextBox3番目の処理。
    Private Sub tag_3_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tag_3_TextBox.TextChanged

        smp_tag_3_TextBox.Text = tag_3_TextBox.Text

        Dim tag_Input_String As String = tag_3_TextBox.Text
        Dim tag_Number_String As String = "_Tag_Number_3"

        Tag_Set_Sub(tag_Input_String, tag_Number_String)
    End Sub
    '▲PC版の検索ワードのの登録用のTextBox3番目の処理。


    '▼PC版の検索ワードのの登録用のTextBox4番目の処理。
    Private Sub tag_4_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tag_4_TextBox.TextChanged

        smp_tag_4_TextBox.Text = tag_4_TextBox.Text

        Dim tag_Input_String As String = tag_4_TextBox.Text
        Dim tag_Number_String As String = "_Tag_Number_4"

        Tag_Set_Sub(tag_Input_String, tag_Number_String)
    End Sub
    '▲PC版の検索ワードのの登録用のTextBox4番目の処理。


    '▼PC版の検索ワードのの登録用のTextBox5番目の処理。
    Private Sub tag_5_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles tag_5_TextBox.TextChanged

        smp_tag_5_TextBox.Text = tag_5_TextBox.Text

        Dim tag_Input_String As String = tag_5_TextBox.Text
        Dim tag_Number_String As String = "_Tag_Number_5"

        Tag_Set_Sub(tag_Input_String, tag_Number_String)
    End Sub
    '▲PC版の検索ワードのの登録用のTextBox5番目の処理。




    '▼PC版のTextBox1番目の右隣のComboBoxの処理
    Private Sub tag_1_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tag_1_ComboBox.SelectionChanged

        Dim selected_Index As Integer = tag_1_ComboBox.SelectedIndex
        smp_tag_1_ComboBox.SelectedIndex = selected_Index


        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = tag_1_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_1"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲PC版のTextBox1番目の右隣のComboBoxの処理


    '▼PC版のTextBox2番目の右隣のComboBoxの処理
    Private Sub tag_2_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tag_2_ComboBox.SelectionChanged

        Dim selected_Index As Integer = tag_2_ComboBox.SelectedIndex
        smp_tag_2_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = tag_2_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_2"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲PC版のTextBox2番目の右隣のComboBoxの処理



    '▼PC版のTextBox3番目の右隣のComboBoxの処理
    Private Sub tag_3_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tag_3_ComboBox.SelectionChanged

        Dim selected_Index As Integer = tag_3_ComboBox.SelectedIndex
        smp_tag_3_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = tag_3_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_3"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲PC版のTextBox3番目の右隣のComboBoxの処理



    '▼PC版のTextBox4番目の右隣のComboBoxの処理
    Private Sub tag_4_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tag_4_ComboBox.SelectionChanged

        Dim selected_Index As Integer = tag_4_ComboBox.SelectedIndex
        smp_tag_4_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = tag_4_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_4"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲PC版のTextBox4番目の右隣のComboBoxの処理



    '▼PC版のTextBox5番目の右隣のComboBoxの処理
    Private Sub tag_5_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles tag_5_ComboBox.SelectionChanged

        Dim selected_Index As Integer = tag_5_ComboBox.SelectedIndex
        smp_tag_5_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = tag_5_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_5"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲PC版のTextBox5番目の右隣のComboBoxの処理




    '▼ログイン時の検索ワードの設置処理
    Private Sub Tag_Login_Set_Sub()

        '↓ログイン中のＩＤとタグの番号を合わせた文字列を用意。
        Dim relay_Tag_Number_1 As String = relay_ID & "_Tag_Number_1"
        Dim relay_Tag_Number_2 As String = relay_ID & "_Tag_Number_2"
        Dim relay_Tag_Number_3 As String = relay_ID & "_Tag_Number_3"
        Dim relay_Tag_Number_4 As String = relay_ID & "_Tag_Number_4"
        Dim relay_Tag_Number_5 As String = relay_ID & "_Tag_Number_5"


        '↓ログイン中のIDとタグの番号に合わせたCombBoxの文字列を用意。
        Dim relay_ComboBox_Number_1 As String = relay_ID & "_ComboBox_Number_1"
        Dim relay_ComboBox_Number_2 As String = relay_ID & "_ComboBox_Number_2"
        Dim relay_ComboBox_Number_3 As String = relay_ID & "_ComboBox_Number_3"
        Dim relay_ComboBox_Number_4 As String = relay_ID & "_ComboBox_Number_4"
        Dim relay_ComboBox_Number_5 As String = relay_ID & "_ComboBox_Number_5"


        '↓データコンテナの呼び出し。
        Dim container = Windows.Storage.ApplicationData.Current.LocalSettings


        '↓タグ番号と登録された検索ワードの登録。
        Try
            Dim tag_1 As String = container.Values(relay_Tag_Number_1)
            tag_1_TextBox.Text = tag_1
            smp_tag_1_TextBox.Text = tag_1
        Catch ex As Exception

        End Try

        Try
            Dim tag_2 As String = container.Values(relay_Tag_Number_2)
            tag_2_TextBox.Text = tag_2
            smp_tag_2_TextBox.Text = tag_2
        Catch ex As Exception

        End Try

        Try
            Dim tag_3 As String = container.Values(relay_Tag_Number_3)
            tag_3_TextBox.Text = tag_3
            smp_tag_3_TextBox.Text = tag_3
        Catch ex As Exception

        End Try

        Try
            Dim tag_4 As String = container.Values(relay_Tag_Number_4)
            tag_4_TextBox.Text = tag_4
            smp_tag_4_TextBox.Text = tag_4
        Catch ex As Exception

        End Try

        Try
            Dim tag_5 As String = container.Values(relay_Tag_Number_5)
            tag_5_TextBox.Text = tag_5
            smp_tag_5_TextBox.Text = tag_5
        Catch ex As Exception

        End Try



        '↓ComboBoxの選択されている情報を設定。
        Try
            Dim comboBox_1 As String = container.Values(relay_ComboBox_Number_1)


            If comboBox_1 = "または" Then
                tag_1_ComboBox.SelectedItem = tag_1_OR_ComboBoxItem
                smp_tag_1_ComboBox.SelectedItem = smp_tag_1_OR_ComboBoxItem
            ElseIf comboBox_1 = "含む" Then
                tag_1_ComboBox.SelectedItem = tag_1_AND_ComboBoxItem
                smp_tag_1_ComboBox.SelectedItem = smp_tag_1_OR_ComboBoxItem
            ElseIf comboBox_1 = "含まない" Then
                tag_1_ComboBox.SelectedItem = tag_1_NOT_ComboBoxItem
                smp_tag_1_ComboBox.SelectedItem = smp_tag_1_NOT_ComboBoxItem
            End If
        Catch ex As Exception

        End Try

        Try
            Dim comboBox_2 As String = container.Values(relay_ComboBox_Number_2)

            If comboBox_2 = "または" Then
                tag_2_ComboBox.SelectedItem = tag_2_OR_ComboBoxItem
                smp_tag_1_ComboBox.SelectedItem = smp_tag_1_NOT_ComboBoxItem
            ElseIf comboBox_2 = "含む" Then
                tag_2_ComboBox.SelectedItem = tag_2_AND_ComboBoxItem
                smp_tag_2_ComboBox.SelectedItem = smp_tag_2_AND_ComboBoxItem
            ElseIf comboBox_2 = "含まない" Then
                tag_2_ComboBox.SelectedItem = tag_2_NOT_ComboBoxItem
                smp_tag_2_ComboBox.SelectedItem = smp_tag_2_NOT_ComboBoxItem
            End If
        Catch ex As Exception

        End Try

        Try
            Dim comboBox_3 As String = container.Values(relay_ComboBox_Number_3)

            If comboBox_3 = "または" Then
                tag_3_ComboBox.SelectedItem = tag_3_OR_ComboBoxItem
                smp_tag_3_ComboBox.SelectedItem = smp_tag_3_OR_ComboBoxItem
            ElseIf comboBox_3 = "含む" Then
                tag_3_ComboBox.SelectedItem = tag_3_AND_ComboBoxItem
                smp_tag_3_ComboBox.SelectedItem = smp_tag_3_AND_ComboBoxItem
            ElseIf comboBox_3 = "含まない" Then
                tag_3_ComboBox.SelectedItem = tag_3_NOT_ComboBoxItem
                smp_tag_3_ComboBox.SelectedItem = smp_tag_3_NOT_ComboBoxItem
            End If
        Catch ex As Exception

        End Try

        Try
            Dim comboBox_4 As String = container.Values(relay_ComboBox_Number_4)

            If comboBox_4 = "または" Then
                tag_4_ComboBox.SelectedItem = tag_4_OR_ComboBoxItem
                smp_tag_4_ComboBox.SelectedItem = smp_tag_4_OR_ComboBoxItem
            ElseIf comboBox_4 = "含む" Then
                tag_4_ComboBox.SelectedItem = tag_4_AND_ComboBoxItem
                smp_tag_4_ComboBox.SelectedItem = smp_tag_4_AND_ComboBoxItem
            ElseIf comboBox_4 = "含まない" Then
                tag_4_ComboBox.SelectedItem = tag_4_NOT_ComboBoxItem
                smp_tag_4_ComboBox.SelectedItem = smp_tag_4_NOT_ComboBoxItem
            End If
        Catch ex As Exception

        End Try

        Try
            Dim comboBox_5 As String = container.Values(relay_ComboBox_Number_5)

            If comboBox_5 = "または" Then
                tag_5_ComboBox.SelectedItem = tag_5_OR_ComboBoxItem
                smp_tag_5_ComboBox.SelectedItem = smp_tag_5_OR_ComboBoxItem
            ElseIf comboBox_5 = "含む" Then
                tag_5_ComboBox.SelectedItem = tag_5_AND_ComboBoxItem
                smp_tag_5_ComboBox.SelectedItem = smp_tag_5_AND_ComboBoxItem
            ElseIf comboBox_5 = "含まない" Then
                tag_5_ComboBox.SelectedItem = tag_5_NOT_ComboBoxItem
                smp_tag_5_ComboBox.SelectedItem = smp_tag_5_NOT_ComboBoxItem
            End If
        Catch ex As Exception

        End Try

    End Sub
    '▲ログイン時の検索ワードの設置処理



    '▼検索ワードの登録処理
    Private Sub Tag_Set_Sub(ByVal input_Tag As String, ByVal Tag_Number As String)

        '↓ログイン中のＩＤとタグの番号を合わせた文字列を用意。
        Dim relay_Tag_Number As String = relay_ID & Tag_Number

        '↓一度登録されているタグを削除。
        ApplicationData.Current.LocalSettings.DeleteContainer(relay_Tag_Number)

        '↓タグ番号と登録された検索ワードの登録。
        Dim container = Windows.Storage.ApplicationData.Current.LocalSettings
        container.Values(relay_Tag_Number) = input_Tag

    End Sub
    '▲検索ワードの登録処理



    '▼IDに紐づける検索ワードのOR,AND,NOTの設定の保存処理。
    Private Sub ID_ComboBox_Set_Sub(ByVal ComboBox_SelectedIndex As Integer, ByVal ComboBox_Number As String)

        '↓ログイン中のＩＤとタグの番号を合わせた文字列を用意。
        Dim relay_ComboBox_Number As String = relay_ID & ComboBox_Number

        '↓一度登録されているタグを削除。
        ApplicationData.Current.LocalSettings.DeleteContainer(relay_ComboBox_Number)

        '↓タグ番号と登録された検索ワードの登録。
        Dim container = Windows.Storage.ApplicationData.Current.LocalSettings
        container.Values(relay_ComboBox_Number) = ComboBox_SelectedIndex

    End Sub
    '▲IDに紐づける検索ワードのOR,AND,NOTの設定の保存処理。




    '▼IDのデリートに合わせてIDに紐づいている検索ワードを削除する。
    Private Sub ID_Delete_concurrent_Tag_Delete(ByVal relay_Delete_ID As String)

        Dim container = Windows.Storage.ApplicationData.Current.LocalSettings

        For i = 1 To 5
            container.Values.Remove(relay_Delete_ID & "_Tag_Number_" & i)
            container.Values.Remove(relay_Delete_ID & "_ComboBox_Number_" & i)
        Next

    End Sub
    '▲IDのデリートに合わせてIDに紐づいている検索ワードを削除する。



    '▼「Page_Loaded_Event」側で呼び出し、ＩＤ一覧のtxtファイルの内容を、ListBoxへ設定する処理。
    '   （UWPアプリのローカルフォルダに保存されるID_List.txtファイルへを読み込み、ListBox側へ起動時に読み込ませる処理）
    Private Async Sub ID_List_Text_ListBox_Set()


        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「ID_List.txt」を読み込ませる為の StorageFile を宣言し、『 id_List_txt_StorageFile 』変数へ役割の代入。
        Dim id_List_txt_StorageFile As StorageFile


        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_txt_Check_1 As String = storage_Folder.Path
        Dim local_txt_Check_2 As String = local_txt_Check_1 & "\ID_List.txt"


        '↓「"ID_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_txt_Check_2) Then
            id_List_txt_StorageFile = Await storage_Folder.GetFileAsync("ID_List.txt")
        Else
            id_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("ID_List.txt")
        End If


        '↓「"ID_List.txt"」を行ごとに分解して読み込む
        Dim id_lines_List_Of_String As IList(Of String) = Await FileIO.ReadLinesAsync(id_List_txt_StorageFile)


        '↓ID_Delete_ListBoxの内容を一度全削除。
        ID_Delete_ListBox.Items.Clear()
        smp_ID_Delete_ListBox.Items.Clear()


        '↓ID_Delete_ListBoxへとFor文で代入処理。
        For Each line As String In id_lines_List_Of_String
            If Check_ListBox_Items(line) Then
                ID_Delete_ListBox.Items.Add(line)
                smp_ID_Delete_ListBox.Items.Add(line)
            End If
        Next


    End Sub
    '▲「Page_Loaded_Event」側で呼び出し、ＩＤ一覧のtxtファイルの内容を、ListBoxへ設定する処理。
    '   （UWPアプリのローカルフォルダに保存されるID_List.txtファイルへを読み込み、ListBox側へ起動時に読み込ませる処理）




    '▼チェックされたＩＤのパスワード情報をRemoveで削除する処理
    Private Sub check_ID_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles check_ID_Delete_Button.Click

        If ID_Delete_ListBox.SelectedItems.Count = 0 Then
            Show_Message("削除したいRSSが選択されていません。")
            Exit Sub
        End If

        If relay_ID = ID_Delete_ListBox.SelectedItem Then
            Show_Message("ログイン中の ID です。" & vbCrLf & "ログイン中の ID は削除できません。")
            Exit Sub
        End If

        '↓passwordVault.Removeで、選択されたＩＤ名のパスワードを削除。
        Dim relay_Delete_ID_String As String = ID_Delete_ListBox.SelectedItem

        ID_Delete_concurrent_Tag_Delete(relay_Delete_ID_String)

        Dim passwordVault = New Windows.Security.Credentials.PasswordVault()
        Dim credential = passwordVault.Retrieve("RSS_Japan_News", relay_Delete_ID_String)
        passwordVault.Remove(credential)


        '↓ 登録可能なパスワード情報の件数の更新。
        Remaing_ID_Count_Text_Change_Sub()


        ' ↓リストボックスから選択された項目を削除

        Dim selected_Index As Integer = ID_Delete_ListBox.SelectedIndex

        ID_Delete_ListBox.Items.RemoveAt(ID_Delete_ListBox.SelectedIndex)
        smp_ID_Delete_ListBox.Items.RemoveAt(selected_Index)

        ID_List_Text_Line_Delete(relay_Delete_ID_String)

        Show_Message("選択されたＩＤ情報を削除しました。")

    End Sub
    '▲チェックされたＩＤのパスワード情報をRemoveで削除する処理




    '▼チェックされたＩＤのパスワード情報を、ID_List.txtから削除する処理。
    Private Async Sub ID_List_Text_Line_Delete(ByVal Delete_ID As String)


        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「ID_List.txt」を読み込ませる為の StorageFile を宣言し、『 id_List_txt_StorageFile 』変数へ役割の代入。
        Dim id_List_txt_StorageFile As StorageFile



        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_txt_Check_1 As String = storage_Folder.Path
        Dim local_txt_Check_2 As String = local_txt_Check_1 & "\ID_List.txt"


        '↓「"ID_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_txt_Check_2) Then
            id_List_txt_StorageFile = Await storage_Folder.GetFileAsync("ID_List.txt")
        Else
            id_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("ID_List.txt")
        End If


        '↓「"ID_List.txt"」を行ごとに分解して読み込む
        Dim id_lines_List_Of_String As IList(Of String) = Await FileIO.ReadLinesAsync(id_List_txt_StorageFile)



        '↓ID_List.txt に対して書き込みを行うのにコレクションが必要の為、コレクションへ代入する為のListをString型で宣言する。
        Dim replacement_ID_List_Of_String = New List(Of String)()



        '↓id_lines_List_Of_Stringの内容を、Delete_IDで指定されたＩＤ以外のＩＤを改めて入れる処理。
        For Each line As String In id_lines_List_Of_String
            If Delete_ID <> line Then
                replacement_ID_List_Of_String.Add(line)
            End If
        Next



        '↓追加で書くのではなく、ListBoxに入っている内容のみを、RSS_List.txtへ書き込みさせたいため、いったん「""」で全削除
        Do
            Try
                Await FileIO.WriteTextAsync(id_List_txt_StorageFile, "")
                Exit Do
            Catch

            End Try
        Loop


        '↓削除を終えたら、「replacement_ID_List_Of_String」の内容をtextファイルへ入れていく処理。
        Do
            Try
                '↓コレクションへ代入。
                Dim replacement_ID_texts_IEnumerable As IEnumerable(Of String) = replacement_ID_List_Of_String

                '↓AppendLinesAsyncを用いて、コレクションの内容を、RSS_List.txtへ代入。
                Await FileIO.AppendLinesAsync(id_List_txt_StorageFile, replacement_ID_texts_IEnumerable)
                Exit Do
            Catch

            End Try
        Loop

    End Sub
    '▲チェックされたＩＤのパスワード情報を、ID_List.txtから削除する処理。




    '▼PC版の全てのＩＤのパスワード情報を、ID_List.txtから削除する処理。
    Private Async Sub ID_ALL_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles ID_ALL_Delete_Button.Click

        Dim select_Boolean = Await Show_Message_Select("IDを全削除します。" _
                                                       & vbCrLf _
                                                       & "ログイン中の場合にはログイン中のID以外を削除します。" _
                                                       & vbCrLf _
                                                       & "よろしいですか？")

        If select_Boolean = False Then
            Exit Sub
        End If

        ID_ALL_Delete_Button_Click_Sub()

    End Sub




    Private Async Sub ID_ALL_Delete_Button_Click_Sub()

        '↓UWP専用のストレージフォルダーを開く。（storage_Folder変数へ代入し役割を持たせる）
        Dim storage_Folder As StorageFolder = ApplicationData.Current.LocalFolder


        '↓「ID_List.txt」を読み込ませる為の StorageFile を宣言し、『 id_List_txt_StorageFile 』変数へ役割の代入。
        Dim id_List_txt_StorageFile As StorageFile



        '↓インストール後のローカルストレージのファイルを確認。
        Dim local_txt_Check_1 As String = storage_Folder.Path
        Dim local_txt_Check_2 As String = local_txt_Check_1 & "\ID_List.txt"


        '↓「"ID_List.txt"」を読み込ませるIf文を行い、存在しなければ「CreateFileAsync」で作成。存在すれば「GetFileAsync」で取得。
        If System.IO.File.Exists(local_txt_Check_2) Then
            id_List_txt_StorageFile = Await storage_Folder.GetFileAsync("ID_List.txt")
        Else
            id_List_txt_StorageFile = Await storage_Folder.CreateFileAsync("ID_List.txt")
        End If



        '↓「"ID_List.txt"」を行ごとに分解して読み込む
        Dim id_lines_List_Of_String As IList(Of String) = Await FileIO.ReadLinesAsync(id_List_txt_StorageFile)



        '↓ID_List.txt に対して書き込みを行うのにコレクションが必要の為、コレクションへ代入する為のListをString型で宣言する。
        Dim replacement_ID_List_Of_String = New List(Of String)()



        '↓UWPローカルパスワードデータを取得。
        Dim passwordVault = New Windows.Security.Credentials.PasswordVault()



        '↓id_lines_List_Of_Stringの内容を、lineの名義で一つずつUWPローカルパスワードデータの中から削除。(ログイン中のIDは除く)
        For Each line As String In id_lines_List_Of_String
            If relay_ID <> line Then
                Try
                    ID_Delete_concurrent_Tag_Delete(line)
                    Dim credential = passwordVault.Retrieve("RSS_Japan_News", line)
                    passwordVault.Remove(credential)
                Catch ex As Exception

                End Try
            End If
        Next



        '↓ListBoxのデータを全削除。
        ID_Delete_ListBox.Items.Clear()
        smp_ID_Delete_ListBox.Items.Clear()

        '↓relay_IDに値が存在する場合にはListBoxへ追加。
        If relay_ID <> Nothing Then
            ID_Delete_ListBox.Items.Add(relay_ID)
            smp_ID_Delete_ListBox.Items.Add(relay_ID)
        End If



        '↓追加で書くのではなく、ListBoxに入っている内容のみを、RSS_List.txtへ書き込みさせたいため、いったん「""」で全削除
        Do
            Try
                Await FileIO.WriteTextAsync(id_List_txt_StorageFile, "")
                Exit Do
            Catch

            End Try
        Loop


        '↓削除を終えたら、「replacement_ID_List_Of_String」の内容をtextファイルへ入れていく処理。
        If relay_ID <> Nothing Then
            Do
                Try
                    '↓コレクションへ代入。
                    Dim replacement_ID_texts_IEnumerable As IEnumerable(Of String) = replacement_ID_List_Of_String

                    '↓AppendLinesAsyncを用いて、コレクションの内容を、id_List.txtへ代入。
                    Await FileIO.AppendLinesAsync(id_List_txt_StorageFile, replacement_ID_texts_IEnumerable)
                    Exit Do
                Catch

                End Try
            Loop
        End If

        Remaing_ID_Count_Text_Change_Sub()


        If relay_ID = Nothing Then
            Show_Message("ログイン中のIDを除く、全てのIDの削除が完了しました。")
        Else
            Show_Message("IDの全削除が完了しました。")
        End If

    End Sub
    '▲ＰＣ版の全てのＩＤのパスワード情報を、ID_List.txtから削除する処理。


    '▼PC版の設定画面のWebviewの表示設定のComboBoxをスマホ版と同期させる処理。
    Private Sub url_View_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles url_View_ComboBox.SelectionChanged
        Dim select_int As Integer = url_View_ComboBox.SelectedIndex
        smp_url_View_ComboBox.SelectedIndex = select_int
    End Sub
    '▲PC版の設定画面のWebviewの表示設定のComboBoxをスマホ版と同期させる処理。


    '▼▼WebView で取得した記事を表示する処理。
    '▼動的に配置するStackPanel内に表示するURLのButtonのイベントを設定する処理。
    Public Async Sub WebView_Call(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '↓sender(オブジェクト（ボタン））から、URLを入れたContentを取得し文字列型へ代入。
        Dim article_URL As String = sender.Content

        '↓設定画面で変更できる、ブラウザで呼び出す／アプリ内で表示、をIf文の分岐として処理を行う。
        If url_View_ComboBox.SelectedIndex = 0 Then
            '↓アプリ内でWeb画面を表示するSubの呼び出し処理。
            App_WebView_Set(article_URL)
        ElseIf url_View_ComboBox.SelectedIndex = 1 Then
            '↓外部ブラウザでリンク先を表示する処理。
            Try
                Await Launcher.LaunchUriAsync(New Uri(article_URL))
            Catch ex As Exception
                Show_Message("URLの取得に不備が生じている可能性があります。" & vbCrLf & "表記されているURLが http:// または https:// から始まるURLかご確認下さい。")
            End Try
        End If
    End Sub
    '▲動的に配置するStackPanel内に表示するURLのButtonのイベントを設定する処理。


    '▼URLのボタンに動的に追加したイベント内で呼び出す処理。
    Private Async Sub App_WebView_Set(ByVal article_URL As String)

        Button_ALL_OFF()

        Wait_Background_Black_Border.Visibility = Visibility.Visible

        Search_Wait_ProgressRing.IsActive = True
        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)



        Try
            Input_Box_Clear()
        Catch ex As Exception

        End Try

        '↓UIの変更。
        PC_Main_RSS_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_Article_Area.Width = New GridLength(0.0, GridUnitType.Star)
        PC_Main_WebView_Area.Width = New GridLength(0.7, GridUnitType.Star)


        '↓WebView画面のURLを表示するTextBoxへ、URLを設定。
        Address_TextBox.Text = article_URL

        '↓WebViewへURIを渡し、リンク先の記事をアプリ内で表示する。
        Try
            Result_URL_WebView_StackPanel.Visibility = Visibility.Visible
            Result_URL_WebView.Visibility = Visibility.Visible

            Result_URL_WebView.Navigate(New Uri(article_URL.ToString()))
        Catch ex As Exception
            Show_Message("URLの取得に不備が生じている可能性があります。" & vbCrLf & "表記されているURLが http:// または https:// から始まるURLかご確認下さい。")
        End Try
    End Sub
    '▲URLのボタンに動的に追加したイベント内で呼び出す処理。



    '▼WebViewの戻るボタン
    Private Sub Back_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles Back_AppBarButton.Click
        Try
            Result_URL_WebView.GoBack()
        Catch
            Exit Sub
        End Try
    End Sub
    '▲WebViewの戻るボタン


    '▼WebViewの進むボタン
    Private Sub Forward_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles Forward_AppBarButton.Click
        Try
            Result_URL_WebView.GoForward()
        Catch
            Exit Sub
        End Try
    End Sub
    '▲WebViewの進むボタン


    '▼WebViewの再読み込みボタン
    Private Sub Refresh_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles Refresh_AppBarButton.Click
        Result_URL_WebView.Refresh()
    End Sub
    '▲WebViewの再読み込みボタン



    '▼WebViewに表示されているページのURLを取得してAddress_TextBoxへ代入。
    Private Sub Result_URL_WebView_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles Result_URL_WebView.LoadCompleted
        Dim web_URL As String = Result_URL_WebView.Source.ToString()
        Address_TextBox.Text = web_URL
    End Sub
    '▲WebViewに表示されているページのURLを取得してAddress_TextBoxへ代入。


    '▼WebViewに表示されているページを閉じる処理。
    Private Sub Close_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles Close_AppBarButton.Click
        WebView_Close()
    End Sub

    Private Sub WebView_Close()

        Try
            Address_TextBox.Text = ""
            Result_URL_WebView.Navigate(New Uri("about:blank"))
        Catch ex As Exception

        End Try

        If rss_Area_bool = True Then
            PC_Main_RSS_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
        End If


        If rss_Area_bool = True Then
            PC_Main_RSS_Set_Area.Width = New GridLength(0.7, GridUnitType.Star)
        End If

        PC_Main_Article_Area.Width = New GridLength(0.7, GridUnitType.Star)
        PC_Main_WebView_Area.Width = New GridLength(0.0, GridUnitType.Star)

    End Sub
    '▲WebViewに表示されているページを閉じる処理。


    Private Sub WebView_URL_Text_Box_Clear()

        Try
            Result_URL_WebView.Navigate(New Uri("about:blank"))
        Catch ex As Exception

        End Try

    End Sub


    Private Async Sub WebView_NavigationCompleted()

        Search_Wait_ProgressRing.IsActive = False
        Wait_Background_Black_Border.Visibility = Visibility.Collapsed

        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)

        Button_ALL_ON()
    End Sub



    Private Async Sub WebView_Navigation_Error()

        Search_Wait_ProgressRing.IsActive = False
        Wait_Background_Black_Border.Visibility = Visibility.Collapsed

        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)

        Button_ALL_ON()

        Show_Message("WEB上の記事の取得に失敗しました。")
    End Sub

    '▲▲WebView で取得した記事を表示する処理。



    '▼▼ハンバーガーボタンのボタンイベントの箇所

    '▼ハンバーガーボタンの「画面の初期化」のボタンが押された場合の処理。
    Private Async Sub Reset_Button_Click(sender As Object, e As RoutedEventArgs) Handles Reset_Button.Click

        Dim select_Boolean

        select_Boolean = Await Show_Message_Select("画面を起動時の状態に戻します。" & vbCrLf & "並びにログアウトも行われます。" & vbCrLf & "よろしいですか？")

        If select_Boolean = True Then

            Try
                Articles_Number_Set_Slider.Value = 10
            Catch ex As Exception

            End Try


            '↓UIとBoolの状態等を初期化する為の処理。
            PC_Main_Sign_IN_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_Login_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_Article_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_RSS_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_Setting_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_Category_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_WebView_Area.Width = New GridLength(0.0, GridUnitType.Star)
            PC_Main_ID_Delete_Area.Width = New GridLength(0.0, GridUnitType.Star)



            Logout_Sub()
            WebView_URL_Text_Box_Clear()

            Search_Button.Width = 500
            Setting_Button.Width = 500
            RSS_Set_Button.Width = 500
            ID_Delete_Button.Width = 500
            Category_Button.Width = 500
            Login_Button.Width = 500
            ID_Sing_IN_Button.Width = 500
            Search_Button.Width = 500

            article_Area_bool = False
            sign_IN_Area_bool = False
            login_Area_bool = False
            rss_Area_bool = False
            setting_Area_bool = False
            category_Area_bool = False
            id_Delete_Area_bool = False

            smp_Set_Bool = False


            '▼「Page_Loaded_Event_Call()」を呼び出し処理する予定だったが、RSSの初期化に問題があった為、
            '   明確な個別の指定の方針に変更。
            Category_Button.Visibility = Visibility.Collapsed
            ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed


            '↓初期の検索結果のURLのリンク先の表示をアプリ内のWebViewでの表示にする設定
            url_View_ComboBox.SelectedItem = App_WebView_ComboBoxItem

            tag_1_ComboBox.SelectedItem = tag_1_OR_ComboBoxItem
            tag_2_ComboBox.SelectedItem = tag_2_OR_ComboBoxItem
            tag_3_ComboBox.SelectedItem = tag_3_OR_ComboBoxItem
            tag_4_ComboBox.SelectedItem = tag_4_OR_ComboBoxItem
            tag_5_ComboBox.SelectedItem = tag_5_OR_ComboBoxItem

            '↓検索ワードは起動時は全除去
            tag_1_TextBox.Text = ""
            tag_2_TextBox.Text = ""
            tag_3_TextBox.Text = ""
            tag_4_TextBox.Text = ""
            tag_5_TextBox.Text = ""


            '↓UWPの資格情報ボックスの登録可能件数の取得。20件まで。
            Remaing_ID_Count_Text_Change_Sub()

            '↓ID_List.txtから登録されているＩＤの一覧をListBoxへと代入する処理を呼び出す。
            ID_List_Text_ListBox_Set()

            '▲「Page_Loaded_Event_Call()」を呼び出し処理する予定だったが、RSSの初期化に問題があった為、
            '   明確な個別の指定の方針に変更。


            '↓RSSのリストボックスを初期化する為の処理。
            RSS_ListBox.Items.Clear()
            smp_RSS_ListBox.Items.Clear()

            '↓ListBoxへとFor文で代入処理。「Check_ListBox_Items()」を呼び出し、ListBoxへ代入されていく内容は重複を排除する。
            For Each line As String In reset_RSS_lines_List_Of_String
                If Check_ListBox_Items(line) Then
                    RSS_ListBox.Items.Add(line)
                    smp_RSS_ListBox.Items.Add(line)
                End If
            Next

            '↓ユーザー側でローカルフォルダの "RSS_List.txt" に、重複した内容が大量に書き込まれていた場合に重複を排除する為に呼び出す。
            RSS_List_Text_Write()


            PC_Main_Category_Set_Area.Width = New GridLength(0.0, GridUnitType.Star)

            smp_Login_Area.Visibility = Visibility.Collapsed
            smp_RSS_Set_Area.Visibility = Visibility.Collapsed
            smp_Setting_Area.Visibility = Visibility.Collapsed
            smp_Category_Area.Visibility = Visibility.Collapsed
            smp_ID_Delete_Area.Visibility = Visibility.Collapsed
            smp_ID_Sign_IN_Possible_Area.Visibility = Visibility.Collapsed
            smp_ID_Sign_IN_Impossible_Area.Visibility = Visibility.Collapsed
            smp_ID_Sing_IN_Area.Visibility = Visibility.Collapsed
            smp_Article_Area_ScrollViewer.Visibility = Visibility.Collapsed
            smp_Article_Stack_Panel_Text_Area.Visibility = Visibility.Collapsed

        End If
    End Sub
    '▲ハンバーガーボタンの「画面の初期化」のボタンが押された場合の処理。


    '▼ハンバーガーボタンの「アプリの終了」のボタンが押された場合の処理。
    Private Sub Exit_Button_Click(sender As Object, e As RoutedEventArgs) Handles Exit_Button.Click
        Application.Current.Exit()
    End Sub


    '▲ハンバーガーボタンの「アプリの終了」のボタンが押された場合の処理。

    '▲▲ハンバーガーボタンのボタンイベントの箇所


    '▼検索結果のソートボタンが押された場合の処理。
    Private Sub sort_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles sort_ComboBox.SelectionChanged

        Dim select_int As Integer = sort_ComboBox.SelectedIndex
        smp_Sort_ComboBox.SelectedIndex = select_int

        If start_Escape_ComboBox_Bool = True Then
            start_RSS_Reading()
        End If

    End Sub
    '▲検索結果のソートボタンが押された場合の処理。



    '▼MainPage、もといMain_Gridの画面サイズ変更時のイベント処理
    Private Sub Page_Size_Changed_Sub(sender As Object, e As SizeChangedEventArgs) Handles Main_Grid.SizeChanged

        width_Size_Check_Double = e.NewSize.Width
        height_Size_Check_Double = e.NewSize.Height

        If width_Size_Check_Double > height_Size_Check_Double Then
            page_Size_Check_Bool = False

            Search_Wait_ProgressRing.Width = 300
            Search_Wait_ProgressRing.Height = 300
        ElseIf width_Size_Check_Double < height_Size_Check_Double Then
            page_Size_Check_Bool = True

            Search_Wait_ProgressRing.Width = 150
            Search_Wait_ProgressRing.Height = 150
        End If

    End Sub


    '▲MainPage、もといMain_Gridの画面サイズ変更時のイベント処理




    '▼▼スマホ画面用追加記述
    Private Sub smp_Sort_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_Sort_ComboBox.SelectionChanged
        Dim select_int As Integer = smp_Sort_ComboBox.SelectedIndex
        sort_ComboBox.SelectedIndex = select_int
    End Sub





    '▼検索結果画面の閉じるボタンが押された場合の処理。
    Private Sub smp_Result_Close_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Result_Close_Button.Click

        article_Area_bool = False
        smp_Article_Area_ScrollViewer.Visibility = Visibility.Collapsed
        smp_Article_Stack_Panel_Text_Area.Visibility = Visibility.Collapsed

    End Sub
    '▲検索結果画面の閉じるボタンが押された場合の処理。




    '▼ＰＣ版とスマホ版の表示件数調整用のスライダーの連動処理
    Private Sub Articles_Number_Set_Slider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles Articles_Number_Set_Slider.ValueChanged
        Try
            smp_Articles_Number_Set_Slider.Value = Articles_Number_Set_Slider.Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub smp_Articles_Number_Set_Slider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs) Handles smp_Articles_Number_Set_Slider.ValueChanged
        Try
            Articles_Number_Set_Slider.Value = smp_Articles_Number_Set_Slider.Value
        Catch ex As Exception

        End Try
    End Sub
    '▲ＰＣ版とスマホ版の表示件数調整用のスライダーの連動処理




    '▼スマホ版のID登録画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。
    Private Sub smp_Sing_IN_Password_Visualization_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Sing_IN_Password_Visualization_Button.Click

        If smp_Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible Then

            smp_Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden
            smp_Sing_IN_Password_Visualization_Button.Content = "パスワードを表示する"

        ElseIf smp_Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden Then

            smp_Input_Sign_IN_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible
            smp_Sing_IN_Password_Visualization_Button.Content = "パスワードを非表示にする"

        End If
    End Sub
    '▲スマホ版のID登録画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。


    '▼スマホ版のログイン画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。
    Private Sub smp_Login_Password_Visualization_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Login_Password_Visualization_Button.Click

        If smp_Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible Then

            smp_Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden
            smp_Login_Password_Visualization_Button.Content = "パスワードを表示する"

        ElseIf smp_Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Hidden Then

            smp_Input_Login_Password_PasswordBox.PasswordRevealMode = PasswordRevealMode.Visible
            smp_Login_Password_Visualization_Button.Content = "パスワードを非表示にする"

        End If
    End Sub
    '▲スマホ版のログイン画面で、パスワードの表示、非表示をボタンが押される度に変更する処理。

    '▼スマホ版のカテゴリ画面（検索ワード登録画面）のログアウトボタンが押された場合の処理。
    Private Async Sub smp_Logout_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_Logout_Button.Click

        Dim select_Boolean

        select_Boolean = Await Show_Message_Select("ログアウトします。" & vbCrLf & "よろしいですか？")

        If select_Boolean = True Then
            Logout_Sub()
        Else

        End If
    End Sub
    '▲スマホ版のカテゴリ画面（検索ワード登録画面）のログアウトボタンが押された場合の処理。



    '▼スマホ版のRSS登録画面の初期化ボタンが押された場合の処理。
    Private Async Sub smp_RSS_Reset_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_RSS_Reset_Button.Click

        Dim select_Boolean = Await Show_Message_Select("RSSの登録状態を起動時の状態に初期化して戻します。" & vbCrLf & "よろしいですか？")

        If select_Boolean = True Then

            RSS_Reset_Button_Click_Sub()

        End If
    End Sub
    '▲スマホ版のRSS登録画面の初期化ボタンが押された場合の処理。



    '▼スマホ版のRSSが登録された場合の処理。
    Private Sub smp_RSS_Add_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_RSS_Add_Button.Click

        Dim input_String As String = smp_RSS_Input_TextBox.Text

        If (smp_RSS_Input_TextBox.Text IsNot Nothing) AndAlso (smp_RSS_Input_TextBox.Text.Length <> 0) Then

            If Check_ListBox_Items(smp_RSS_Input_TextBox.Text) Then
                RSS_ListBox.Items.Add(input_String)
                smp_RSS_ListBox.Items.Add(input_String)

                RSS_Input_TextBox.Text = ""
                smp_RSS_Input_TextBox.Text = ""

                RSS_List_Text_Write()

                Show_Message("入力情報を登録しました。")
            Else
                Show_Message("同じ名前の入力済み情報があります。" & vbCrLf & "入力を中止しました。")
            End If

        Else
            Show_Message("RSSが入力されていません。")
        End If

    End Sub
    '▲スマホ版のRSSが登録された場合の処理。


    '▼スマホ版のRSSの全削除ボタンが押された場合の処理
    Private Sub smp_RSS_ALL_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_RSS_ALL_Delete_Button.Click

        RSS_ListBox.Items.Clear()
        smp_RSS_ListBox.Items.Clear()

        RSS_List_Text_Write()

        Show_Message("RSS情報を全て削除しました。")

    End Sub
    '▲スマホ版のRSSの全削除ボタンが押された場合の処理



    '▼スマホ版の選択されたRSSが削除されたボタンが押された場合の処理。
    Private Sub smp_check_RSS_Delet_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_check_RSS_Delet_Button.Click


        If smp_RSS_ListBox.SelectedItems.Count = 0 Then
            Show_Message("削除したいRSSが選択されていません。")
            Exit Sub
        End If


        Dim selected_Index As Integer = smp_RSS_ListBox.SelectedIndex

        ' 選択された項目を削除
        RSS_ListBox.Items.RemoveAt(selected_Index)
        smp_RSS_ListBox.Items.RemoveAt(smp_RSS_ListBox.SelectedIndex)

        RSS_List_Text_Write()

        Show_Message("選択されたRSS情報を削除しました。")
    End Sub
    '▲スマホ版の選択されたRSSが削除されたボタンが押された場合の処理。


    '▼スマホ版の設定画面のWebviewの表示設定のComboBoxをＰＣ版と同期させる処理。
    Private Sub smp_url_View_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_url_View_ComboBox.SelectionChanged
        Dim select_int As Integer = smp_url_View_ComboBox.SelectedIndex
        url_View_ComboBox.SelectedIndex = select_int
    End Sub
    '▲スマホ版の設定画面のWebviewの表示設定のComboBoxをＰＣ版と同期させる処理。



    '▼スマホ版のチェックされたＩＤのパスワード情報をRemoveで削除する処理
    Private Sub smp_check_ID_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_check_ID_Delete_Button.Click

        If smp_ID_Delete_ListBox.SelectedItems.Count = 0 Then
            Show_Message("削除したいRSSが選択されていません。")
            Exit Sub
        End If

        If relay_ID = smp_ID_Delete_ListBox.SelectedItem Then
            Show_Message("ログイン中の ID です。" & vbCrLf & "ログイン中の ID は削除できません。")
            Exit Sub
        End If

        '↓passwordVault.Removeで、選択されたＩＤ名のパスワードを削除。
        Dim relay_Delete_ID_String As String = smp_ID_Delete_ListBox.SelectedItem

        ID_Delete_concurrent_Tag_Delete(relay_Delete_ID_String)

        Dim passwordVault = New Windows.Security.Credentials.PasswordVault()
        Dim credential = passwordVault.Retrieve("RSS_Japan_News", relay_Delete_ID_String)
        passwordVault.Remove(credential)


        '↓ 登録可能なパスワード情報の件数の更新。
        Remaing_ID_Count_Text_Change_Sub()


        ' ↓リストボックスから選択された項目を削除

        Dim selected_Index As Integer = smp_ID_Delete_ListBox.SelectedIndex

        ID_Delete_ListBox.Items.RemoveAt(selected_Index)
        smp_ID_Delete_ListBox.Items.RemoveAt(smp_ID_Delete_ListBox.SelectedIndex)

        ID_List_Text_Line_Delete(relay_Delete_ID_String)

        Show_Message("選択されたＩＤ情報を削除しました。")

    End Sub
    '▲スマホ版のチェックされたＩＤのパスワード情報をRemoveで削除する処理


    '▼スマホ版の全てのＩＤのパスワード情報を、ID_List.txtから削除する処理。
    Private Async Sub smp_ID_ALL_Delete_Button_Click(sender As Object, e As RoutedEventArgs) Handles smp_ID_ALL_Delete_Button.Click

        Dim select_Boolean = Await Show_Message_Select("IDを全削除します。" _
                                                       & vbCrLf _
                                                       & "ログイン中の場合にはログイン中のID以外を削除します。" _
                                                       & vbCrLf _
                                                       & "よろしいですか？")

        If select_Boolean = False Then
            Exit Sub
        End If

        ID_ALL_Delete_Button_Click_Sub()

    End Sub
    '▲スマホ版の全てのＩＤのパスワード情報を、ID_List.txtから削除する処理。





    '▼スマホ版の検索ワードのの登録用のTextBox1番目の処理。
    Private Sub smp_tag_1_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles smp_tag_1_TextBox.TextChanged

        tag_1_TextBox.Text = smp_tag_1_TextBox.Text

    End Sub
    '▲スマホ版の検索ワードのの登録用のTextBox1番目の処理。





    '▼スマホ版の検索ワードのの登録用のTextBox2番目の処理。
    Private Sub smp_tag_2_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles smp_tag_2_TextBox.TextChanged

        tag_2_TextBox.Text = smp_tag_2_TextBox.Text

    End Sub
    '▲スマホ版の検索ワードのの登録用のTextBox2番目の処理。



    '▼スマホ版の検索ワードのの登録用のTextBox3番目の処理。
    Private Sub smp_tag_3_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles smp_tag_3_TextBox.TextChanged

        tag_3_TextBox.Text = smp_tag_3_TextBox.Text

    End Sub
    '▲スマホ版の検索ワードのの登録用のTextBox3番目の処理。


    '▼スマホ版の検索ワードのの登録用のTextBox4番目の処理。
    Private Sub smp_tag_4_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles smp_tag_4_TextBox.TextChanged

        tag_4_TextBox.Text = smp_tag_4_TextBox.Text

    End Sub
    '▲スマホ版の検索ワードのの登録用のTextBox4番目の処理。


    '▼スマホ版の検索ワードのの登録用のTextBox5番目の処理。
    Private Sub smp_tag_5_TextBox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles smp_tag_5_TextBox.TextChanged

        tag_5_TextBox.Text = smp_tag_5_TextBox.Text

    End Sub
    '▲スマホ版の検索ワードのの登録用のTextBox5番目の処理。




    '▼スマホ版のTextBox1番目の右隣のComboBoxの処理
    Private Sub smp_tag_1_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_tag_1_ComboBox.SelectionChanged
        Dim selected_Index As Integer = smp_tag_1_ComboBox.SelectedIndex
        tag_1_ComboBox.SelectedIndex = selected_Index


        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = smp_tag_1_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_1"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲スマホ版のTextBox1番目の右隣のComboBoxの処理


    '▼スマホ版のTextBox2番目の右隣のComboBoxの処理
    Private Sub smp_tag_2_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_tag_2_ComboBox.SelectionChanged
        Dim selected_Index As Integer = smp_tag_2_ComboBox.SelectedIndex
        tag_2_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = smp_tag_2_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_2"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲スマホ版のTextBox2番目の右隣のComboBoxの処理



    '▼スマホ版のTextBox3番目の右隣のComboBoxの処理
    Private Sub smp_tag_3_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_tag_3_ComboBox.SelectionChanged
        Dim selected_Index As Integer = smp_tag_3_ComboBox.SelectedIndex
        tag_3_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = smp_tag_3_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_3"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲スマホ版のTextBox3番目の右隣のComboBoxの処理



    '▼スマホ版のTextBox4番目の右隣のComboBoxの処理
    Private Sub smp_tag_4_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_tag_4_ComboBox.SelectionChanged
        Dim selected_Index As Integer = smp_tag_4_ComboBox.SelectedIndex
        tag_4_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = smp_tag_4_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_4"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲スマホ版のTextBox4番目の右隣のComboBoxの処理



    '▼スマホ版のTextBox5番目の右隣のComboBoxの処理
    Private Sub smp_tag_5_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles smp_tag_5_ComboBox.SelectionChanged

        Dim selected_Index As Integer = smp_tag_5_ComboBox.SelectedIndex
        tag_5_ComboBox.SelectedIndex = selected_Index

        If relay_ID <> Nothing Then
            Dim relay_SelectedIndex As Integer = smp_tag_5_ComboBox.SelectedIndex

            Dim comboBox_Number_String As String = "_ComboBox_Number_5"

            ID_ComboBox_Set_Sub(relay_SelectedIndex, comboBox_Number_String)
        End If

    End Sub
    '▲スマホ版のTextBox5番目の右隣のComboBoxの処理



    '▼スマホ版の動的に配置するStackPanel内に表示するURLのButtonのイベントを設定する処理。
    Public Async Sub smp_WebView_Call(ByVal sender As Object, ByVal e As RoutedEventArgs)
        '↓sender(オブジェクト（ボタン））から、URLを入れたContentを取得し文字列型へ代入。
        Dim article_URL As String = sender.Content

        '↓設定画面で変更できる、ブラウザで呼び出す／アプリ内で表示、をIf文の分岐として処理を行う。
        If url_View_ComboBox.SelectedIndex = 0 Then
            '↓アプリ内でWeb画面を表示するSubの呼び出し処理。


            UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, USERAGENT, USERAGENT.Length, 0)

            smp_App_WebView_Set(article_URL)
        ElseIf url_View_ComboBox.SelectedIndex = 1 Then
            '↓外部ブラウザでリンク先を表示する処理。
            Try
                Await Launcher.LaunchUriAsync(New Uri(article_URL))
            Catch ex As Exception
                Show_Message("URLの取得に不備が生じている可能性があります。" & vbCrLf & "表記されているURLが http:// または https:// から始まるURLかご確認下さい。")
            End Try
        End If
    End Sub
    '▲スマホ版の動的に配置するStackPanel内に表示するURLのButtonのイベントを設定する処理。


    '▼スマホ版のURLのボタンに動的に追加したイベント内で呼び出す処理。
    Private Async Sub smp_App_WebView_Set(ByVal article_URL As String)

        Button_ALL_OFF()

        Wait_Background_Black_Border.Visibility = Visibility.Visible

        Search_Wait_ProgressRing.IsActive = True
        Await System.Threading.Tasks.Task.Delay(0.1 * 1000)

        Try
            Input_Box_Clear()
        Catch ex As Exception

        End Try

        '↓WebView画面のURLを表示するTextBoxへ、URLを設定。
        Address_TextBox.Text = article_URL


        '↓WebViewへURIを渡し、リンク先の記事をアプリ内で表示する。
        Try
            '↓WebView部分を非表示から表示へ。
            smp_Web_View_ScrollViewer.Visibility = Visibility.Visible
            smp_WebView_UI_Area.Visibility = Visibility.Visible
            smp_Result_URL_WebView.Navigate(New Uri(article_URL.ToString()))
        Catch ex As Exception
            Show_Message("URLの取得に不備が生じている可能性があります。" & vbCrLf & "表記されているURLが http:// または https:// から始まるURLかご確認下さい。")
        End Try
    End Sub
    '▲スマホ版のURLのボタンに動的に追加したイベント内で呼び出す処理。



    '▼スマホ版のWebviewのＵＩの戻るボタン
    Private Sub smp_Back_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles smp_Back_AppBarButton.Click
        Try
            smp_Result_URL_WebView.GoBack()
        Catch
            Exit Sub
        End Try
    End Sub
    '▲スマホ版のWebviewのＵＩの戻るボタン


    '▼スマホ版のWebviewのＵＩの進むボタン
    Private Sub smp_Forward_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles smp_Forward_AppBarButton.Click
        Try
            smp_Result_URL_WebView.GoForward()
        Catch
            Exit Sub
        End Try
    End Sub
    '▲スマホ版のWebviewのＵＩの進むボタン


    '▼スマホ版のWebviewのＵＩの再読み込みボタン
    Private Sub smp_Refresh_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles smp_Refresh_AppBarButton.Click
        smp_Result_URL_WebView.Refresh()
    End Sub
    '▲スマホ版のWebviewのＵＩの再読み込みボタン



    '▼スマホ版のWebviewのＵＩのWebViewに表示されているページのURLを取得してAddress_TextBoxへ代入。
    Private Sub smp_Result_URL_WebView_LoadCompleted(sender As Object, e As NavigationEventArgs) Handles smp_Result_URL_WebView.LoadCompleted
        Dim web_URL As String = smp_Result_URL_WebView.Source.ToString()
        smp_Address_TextBox.Text = web_URL
    End Sub
    '▲スマホ版のWebviewのＵＩのWebViewに表示されているページのURLを取得してAddress_TextBoxへ代入。


    '▼スマホ版のWebviewのＵＩのWebViewに表示されているページを閉じる処理。
    Private Sub smp_Close_AppBarButton_Click(sender As Object, e As RoutedEventArgs) Handles smp_Close_AppBarButton.Click
        smp_WebView_Close()
    End Sub

    Private Sub smp_WebView_Close()

        Try
            smp_Address_TextBox.Text = ""
            smp_Result_URL_WebView.Navigate(New Uri("about:blank"))
        Catch ex As Exception

        End Try

        smp_Web_View_ScrollViewer.Visibility = Visibility.Collapsed
        smp_WebView_UI_Area.Visibility = Visibility.Collapsed
    End Sub
    '▲スマホ版のWebviewのＵＩのWebViewに表示されているページを閉じる処理。


    '▼ユーザーエージェントの変更。一部のサイトのみスマホ表示に成功。
    <DllImport("urlmon.dll", CharSet:=CharSet.Ansi)>
    Private Shared Function UrlMkSetSessionOption(
        ByVal intOption As Integer,
        ByVal str As String,
        ByVal intLength As Integer,
        ByVal intReserved As Integer) As Integer
    End Function

    Private Const URLMON_OPTION_USERAGENT As Integer = &H10000001
    Private Const USERAGENT As String = "Test User Agent"
    '▲ユーザーエージェントの変更。一部のサイトのみスマホ表示に成功。



    '▲▲スマホ画面用追加記述
End Class