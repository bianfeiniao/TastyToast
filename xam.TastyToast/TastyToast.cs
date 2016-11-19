using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Graphics;
using xam.rebound.core;
using xam.rebound.android;

namespace xam.TastyToast
{
    public class TastyToast
    {
        public static int LENGTH_SHORT = 0;
        public static int LENGTH_LONG = 1;


        public static int SUCCESS = 1;
        public static int WARNING = 2;
        public static int ERROR = 3;
        public static int INFO = 4;
        public static int DEFAULT = 5;
        public static int CONFUSING = 6;

        static SuccessToastView successToastView;
        static WarningToastView warningToastView;
        static ErrorToastView errorToastView;
        static InfoToastView infoToastView;
        static DefaultToastView defaultToastView;
        static ConfusingToastView confusingToastView;

        public static void makeText(Context context, string msg, ToastLength length, int type)
        {

            Toast toast = new Toast(context);
            View layout;
            TextView text;
            switch (type)
            {
                case 1:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.success_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;
                    successToastView = (SuccessToastView)layout.FindViewById(Resource.Id.successView);
                    successToastView.startAnim();
                    text.SetBackgroundResource(Resource.Drawable.success_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;
                case 2:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.warning_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;

                    warningToastView = (WarningToastView)layout.FindViewById(Resource.Id.warningView);
                    SpringSystem springSystem = SpringSystem.create();

                    Spring spring = springSystem.createSpring();
                    spring.setCurrentValue(1.8);
                    SpringConfig config = new SpringConfig(40, 5);
                    spring.setSpringConfig(config);
                    spring.addListener(new SimpleSpringListener()
                    {
                        SpringUpdate = (_spring) =>
                        {
                            float value = (float)_spring.getCurrentValue();
                            float scale = (float)(0.9f - (value * 0.5f));

                            warningToastView.ScaleX = scale;
                            warningToastView.ScaleY = scale;
                        }
                    });

                    Thread t = new Thread(new Runnable(() =>
                    {
                        try
                        {
                            Thread.Sleep(500);
                        }
                        catch (InterruptedException e)
                        {
                        }
                        spring.setEndValue(0.4f);

                    }));
                    t.Start();
                    text.SetBackgroundResource(Resource.Drawable.warning_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;
                case 3:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.error_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;
                    errorToastView = (ErrorToastView)layout.FindViewById(Resource.Id.errorView);
                    errorToastView.startAnim();
                    text.SetBackgroundResource(Resource.Drawable.error_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;

                case 4:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.info_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;
                    infoToastView = (InfoToastView)layout.FindViewById(Resource.Id.infoView);
                    infoToastView.startAnim();
                    text.SetBackgroundResource(Resource.Drawable.info_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;
                case 5:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.default_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;
                    defaultToastView = (DefaultToastView)layout.FindViewById(Resource.Id.defaultView);
                    defaultToastView.startAnim();
                    text.SetBackgroundResource(Resource.Drawable.default_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;
                case 6:
                    layout = LayoutInflater.From(context).Inflate(Resource.Layout.confusing_toast_layout, null, false);
                    text = (TextView)layout.FindViewById(Resource.Id.toastMessage);
                    text.Text = msg;
                    confusingToastView = (ConfusingToastView)layout.FindViewById(Resource.Id.confusingView);
                    confusingToastView.startAnim();
                    text.SetBackgroundResource(Resource.Drawable.confusing_toast);
                    text.SetTextColor(Color.ParseColor("#FFFFFF"));
                    toast.View = layout;
                    break;
            }
            toast.Duration = length;
            toast.Show();
        }

    }
}