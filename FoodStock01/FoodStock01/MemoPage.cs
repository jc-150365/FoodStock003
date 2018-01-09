using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;


namespace FoodStock01
{
    public class MemoPage : ContentPage
    {
        private Entry entry;

        static readonly object Locker = new object();

        private ObservableCollection<MemoModel> ar = new ObservableCollection<MemoModel>();
        //public ObservableCollection<UserModel> ar;

        int id = 1;

        public MemoPage(string title)
        {
            //タブに表示される文字列
            Title = title;

            //アイコン
            Icon = "note32.png";

            if (MemoModel.selectUser() != null)
            {
                ar = new ObservableCollection<MemoModel>(MemoModel.selectUser());
            }
            var listView = new ListView
            {
                //ItemsSource = UserModel.selectUser(),
                //ItemTemplate = new DataTemplate(typeof(TextCell))
                ItemsSource = ar,
                //ItemTemplate = new DataTemplate(() => new MyCell(this)),

            };

            //文字入力
            var entry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            //追加
            var buttonAdd = new Button
            {
                WidthRequest = 60,
                TextColor = Color.Black,
                Text = "追加"
            };
            buttonAdd.Clicked += (s, a) =>
            {//追加ボタンの処理
                if (!String.IsNullOrEmpty(entry.Text))
                {
                    MemoModel.insertUser(id, entry.Text);

                    ar.Add(new MemoModel { Name = entry.Text });

                    id++;

                    //Application.Current.MainPage = new MainPage4();

                    entry.Text = "";
                }
            };

            listView.ItemTapped += async (s, a) =>
            {//リストタップジ
                var item = (MemoModel)a.Item;

                if (await DisplayAlert("削除してよろしいですか", item.ToString(), "OK", "キャンセル"))
                {
                    ar.RemoveAt(ar.IndexOf(item));
                    MemoModel.deleteUser(item.Id);
                }
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children =
                    {
                        new StackLayout
                        {
                            BackgroundColor = Color.Green,
                            Padding = 5,
                            Orientation = StackOrientation.Horizontal,
                            Children = {entry,buttonAdd}//Entryコントロールとボタンコントロールを配置
                        },
                        listView//その下にリストボックス
                    }

            };
        }
    }

}