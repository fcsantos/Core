using Android.Content;
using ProjetoMRP.Paciente.Droid.Renderers;
using ProjetoMRP.Paciente.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]

namespace ProjetoMRP.Paciente.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                //Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            }
        }
    }
}