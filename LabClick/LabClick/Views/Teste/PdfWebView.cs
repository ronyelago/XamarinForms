﻿using Xamarin.Forms;

namespace LabClick.Views.Teste
{
    public class PdfWebView : WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
                        returnType: typeof(string),
                        declaringType: typeof(PdfWebView),
                        defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
    }
}
