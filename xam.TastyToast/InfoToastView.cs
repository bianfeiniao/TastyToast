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
using Android.Views.Animations;
using Android.Animation;
using Android.Graphics;
using Android.Util;

namespace xam.TastyToast
{
    /**
 * Created by rahul on 27/7/16.
 */
    public class InfoToastView : View
    {

        RectF rectF = new RectF();
        ValueAnimator valueAnimator;
        float mAnimatedValue = 0f;
        private string TAG = "com.sdsmdg.tastytoast";
        private Paint mPaint;
        private float mWidth = 0f;
        private float mEyeWidth = 0f;
        private float mPadding = 0f;
        private float endPoint = 0f;
        private bool isEyeLeft = false;
        private bool isEyeRight = false;
        private bool isEyeMiddle = false;

        public InfoToastView(Context context) : base(context)
        {
           
        }


        public InfoToastView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
           
        }

        public InfoToastView(Context context, IAttributeSet attrs, int defStyleAttr): base(context, attrs, defStyleAttr)
        {
            
        }

        //@Override
        protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            initPaint();
            initRect();
            mWidth = MeasuredWidth;
            mPadding = dip2px(10);
            mEyeWidth = dip2px(3);
            endPoint = mPadding;
        }

        private void initPaint()
        {
            mPaint = new Paint();
            mPaint.AntiAlias = true;
            mPaint.SetStyle(Paint.Style.Stroke);
            mPaint.Color = Color.ParseColor("#337ab7");
            mPaint.StrokeWidth = dip2px(2);

        }

        private void initRect()
        {
            rectF = new RectF(mPadding, mPadding, mWidth - mPadding, mWidth - mPadding);
        }

        //@Override
        protected void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            mPaint.SetStyle(Paint.Style.Stroke);
            canvas.DrawLine(mPadding, mWidth - 3 * mPadding / 2, endPoint, mWidth - 3 * mPadding / 2, mPaint);
            mPaint.SetStyle(Paint.Style.Fill);

            if (isEyeLeft)
            {
                canvas.DrawCircle(mPadding + mEyeWidth, mWidth / 3, mEyeWidth, mPaint);
                canvas.DrawCircle(mWidth - mPadding - 2 * mEyeWidth, mWidth / 3, mEyeWidth, mPaint);
            }
            if (isEyeMiddle)
            {
                canvas.DrawCircle(mPadding + (3 * mEyeWidth / 2), mWidth / 3, mEyeWidth, mPaint);
                canvas.DrawCircle(mWidth - mPadding - (5 * mEyeWidth / 2), mWidth / 3, mEyeWidth, mPaint);
            }
            if (isEyeRight)
            {
                canvas.DrawCircle(mPadding + 2 * mEyeWidth, mWidth / 3, mEyeWidth, mPaint);
                canvas.DrawCircle(mWidth - mPadding - mEyeWidth, mWidth / 3, mEyeWidth, mPaint);
            }
        }

        public int dip2px(float dpValue)
        {
            float scale = Context.Resources.DisplayMetrics.Density;
            return (int)(dpValue * scale + 0.5f);
        }

        public void startAnim()
        {
            stopAnim();
            startViewAnim(0f, 1f, 2000);
        }

        public void stopAnim()
        {
            if (valueAnimator != null)
            {
                ClearAnimation();
                isEyeLeft = false;
                isEyeMiddle = false;
                isEyeRight = false;
                endPoint = mPadding;
                mAnimatedValue = 0f;
                valueAnimator.End();
            }
        }

        private ValueAnimator startViewAnim(float startF, float endF, long time)
        {
            valueAnimator = ValueAnimator.OfFloat(startF, endF);
            valueAnimator.SetDuration(time);
            valueAnimator.SetInterpolator(new LinearInterpolator());
            valueAnimator.AddUpdateListener(new XAnimatorUpdate() {
                AnimationUpdate = (valueAnimator) =>
          {

              mAnimatedValue = (float)valueAnimator.AnimatedValue;

        //   Log.i(TAG, "Value : " + mAnimatedValue);
        if (mAnimatedValue < 0.90)
              {
                  endPoint = ((2 * (mWidth) - (4 * mPadding)) * (mAnimatedValue / 2)) + mPadding;
              }
              else
              {
                  endPoint = mWidth - 5 * mPadding / 4;
              }

              if (mAnimatedValue < 0.16)
              {
                  isEyeRight = true;
                  isEyeLeft = false;
              }
              else if (mAnimatedValue < 0.32)
              {
                  isEyeRight = false;
                  isEyeLeft = true;
              }
              else if (mAnimatedValue < 0.48)
              {
                  isEyeRight = true;
                  isEyeLeft = false;
              }
              else if (mAnimatedValue < 0.64)
              {
                  isEyeRight = false;
                  isEyeLeft = true;
              }
              else if (mAnimatedValue < 0.80)
              {
                  isEyeRight = true;
                  isEyeLeft = false;
              }
              else if (mAnimatedValue < 0.96)
              {
                  isEyeRight = false;
                  isEyeLeft = true;
              }
              else
              {
                  isEyeLeft = false;
                  isEyeMiddle = true;
                  isEyeRight = false;
              }

              PostInvalidate();
          }
            });

            if (!valueAnimator.IsRunning) {
                valueAnimator.Start();

            }
            return valueAnimator;
        }

    }
}