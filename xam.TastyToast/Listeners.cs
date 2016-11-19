using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Animation;

namespace xam.TastyToast
{
    public class XAnimatorUpdate : Java.Lang.Object, ValueAnimator.IAnimatorUpdateListener
    {
        public Action<ValueAnimator> AnimationUpdate { get;set;}
        public void OnAnimationUpdate(ValueAnimator animation)
        {
            AnimationUpdate?.Invoke(animation);
        }
    }
}