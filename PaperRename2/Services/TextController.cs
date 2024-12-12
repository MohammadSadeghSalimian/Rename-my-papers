using System.Windows.Controls;
using Humanizer;
using PaperRename2.Core;

namespace PaperRename2.Wpf.Services
{
    public interface ITextController
    {
        void MakeItShort(object textBox);
        void TittleSimplify(object textBox);
        void MakeItTitle(object textBox);
        void MakeUpper(object textBox);
        void MakeLower(object textBox);
    }
    public class TextController:ITextController
    {
        public void MakeUpper(object textBox)
        {

            if (textBox is not TextBox tb)
            {
                return;
            }
            if (string.IsNullOrEmpty(tb.Text) || string.IsNullOrEmpty(tb.SelectedText))
            {
                return;
            }
            var txt1 = tb.Text;
            var selectedTxt = tb.SelectedText;
            var st = selectedTxt.ToUpper();
            tb.Text = txt1.Replace(selectedTxt, st);
        }
        public void MakeLower(object textBox)
        {

            if (textBox is not TextBox tb)
            {
                return;
            }
            if (string.IsNullOrEmpty(tb.Text) || string.IsNullOrEmpty(tb.SelectedText))
            {
                return;
            }
            var txt1 = tb.Text;
            var selectedTxt = tb.SelectedText;
            var st = selectedTxt.ToLower();
            tb.Text = txt1.Replace(selectedTxt, st);
        }
        public void MakeItTitle(object textBox)
        {

            if (textBox is not TextBox tb)
            {
                return;
            }
            if (string.IsNullOrEmpty(tb.Text) || string.IsNullOrEmpty(tb.SelectedText))
            {
                return;
            }
            var txt1 = tb.Text;
            var selectedTxt = tb.SelectedText;
            var st = selectedTxt.Titleize();
            tb.Text = txt1.Replace(selectedTxt, st);
        }
        public void MakeItShort(object textBox)
        {
            
            if (textBox is not TextBox tb)
            {
                return;
            }
            if (string.IsNullOrEmpty(tb.Text) || string.IsNullOrEmpty(tb.SelectedText))
            {
                return;
            }
            var txt1 = tb.Text;
            var selectedTxt = tb.SelectedText;
            var st = selectedTxt.Trim().ToUpper()[..1] + ". ";
            tb.Text = txt1.Replace(selectedTxt, st);
        }

        public void TittleSimplify(object textBox)
        {
            if (textBox is not TextBox tb)
            {
                return;
            }

            if (string.IsNullOrEmpty(tb.Text) || string.IsNullOrEmpty(tb.SelectedText))
            {
                return;
            }
            var txt1 = tb.Text;
            var selectedTxt = tb.SelectedText;
            var st = selectedTxt.Titleize().SimplifyName();
            tb.Text = txt1.Replace(selectedTxt, st);
        }

    }
}
