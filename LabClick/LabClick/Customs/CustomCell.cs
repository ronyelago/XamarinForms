using Xamarin.Forms;

namespace LabClick.Customs
{
    class CustomCell : ViewCell
    {
        public CustomCell()
        {
            var image = new Image();
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label left = new Label();
            Label right = new Label();

            // bindings
            left.SetBinding(Label.TextProperty, "title");
            right.SetBinding(Label.TextProperty, "subtitle");
            image.SetBinding(Image.SourceProperty, "image");
        }
    }
}
