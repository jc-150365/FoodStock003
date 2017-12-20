using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodStock01
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockPage1 : ContentPage
    {
        public StockPage1(string title)
        {
            //タブに表示される文字列
            Title = title;

            InitializeComponent();
        }

        //更新ボタンが押された時
        /* private void Update_Button_Clicked(object sender, EventArgs e)
         {
             Title = "保存食品リスト";
             InitializeComponent();
         }*/

        //引っ張ったとき（更新）
        private async void OnRefreshing(object sender, EventArgs e)
        {
            // 1秒処理を待つ
            await Task.Delay(1000);

            //リフレッシュを止める
            list.IsRefreshing = false;

            Title = "保存食品リスト";

            InitializeComponent();
        }

        //デリート押された
        void OnDelete_Clicked(object sender, EventArgs args)
        {
            string no = ((CustomButtonDelete)sender).NoText;
            string name = ((CustomButtonDelete)sender).NameText;

            DisplayAlert("Delete", "主キー" + no + " " + name, "ok");

            /***試し***/
            int s_no = int.Parse(no);
            StockFoodModel.DeleteStock(s_no);

            Title = "保存食品リスト";
        
            InitializeComponent();
            /*********/

        }

        //プラスがクリックされた
        void OnPlus_Clicked(object sender, EventArgs args)
        {
            string no1 = ((CustomButton)sender).NoText;
            string name1 = ((CustomButton)sender).NameText;
            int num1 = Convert.ToInt32(((CustomButton)sender).CountText);
            string unit1 = ((CustomButton)sender).UnitText;

            int s_no1 = int.Parse(no1);//
            /***ここから試し***/
            StockFoodModel.UpdateStockPlus02(s_no1, name1, num1, unit1);

            Title = "保存食品リスト";

            InitializeComponent();
            /***ここまで試し***/
        }

        //マイナスがクリックされた
        void OnMinus_Clicked(object sender, EventArgs args)
        {
            string no2 = ((CustomButtonMinus)sender).NoText;
            string name2 = ((CustomButtonMinus)sender).NameText;
            int num2 = Convert.ToInt32(((CustomButtonMinus)sender).CountText);
            string unit2 = ((CustomButtonMinus)sender).UnitText;

            int s_no2 = int.Parse(no2);//
            /***ここから試し***/
            StockFoodModel.UpdateStockMinus(s_no2, name2, num2, unit2);

            Title = "保存食品リスト";

            InitializeComponent();
            /***ここまで試し***/
        }
    }
}