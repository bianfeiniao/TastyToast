using Android.Content;
using Android.Views;
using Android.Animation;
using Android.Views.Animations;
using Android.Graphics;
using Android.Util;

namespace xam.TastyToast
{
    /**
  * Created by rahul on 22/7/16.
  */
    public class ErrorToastView : View
    {

        RectF rectF = new RectF();
        RectF leftEyeRectF = new RectF();
        RectF rightEyeRectF = new RectF();
        ValueAnimator valueAnimator;
        float mAnimatedValue = 0f;
        private Paint mPaint;
        private float mWidth = 0f;
        private float mEyeWidth = 0f;
        private float mPadding = 0f;
        private float endAngle = 0f;
        private bool isJustVisible = false;
        private bool isSad = false;


        public ErrorToastView(Context context) : base(context) { }
        public ErrorToastView(Context context, IAttributeSet attrs) : base(context, attrs) { }

        public ErrorToastView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

        //@Override
        protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            initPaint();
            initRect();
            mWidth = MeasuredWidth;
            mPadding = dip2px(10);
            mEyeWidth = dip2px(3);
        }

        private void initPaint()
        {
            mPaint = new Paint();
            mPaint.AntiAlias = true;
            mPaint.SetStyle(Paint.Style.Stroke);
            mPaint.Color = Color.ParseColor("#d9534f");
            mPaint.StrokeWidth = dip2px(2);
        }

        private void initRect()
        {
            rectF = new RectF(mPadding / 2, ((mWidth) / 2), mWidth - (mPadding / 2), ((3 * mWidth / 2)));
            leftEyeRectF = new RectF(mPadding + mEyeWidth - mEyeWidth, mWidth / 3 -
                    mEyeWidth, mPadding + mEyeWidth + mEyeWidth, mWidth / 3 + mEyeWidth);
            rightEyeRectF = new RectF(mWidth - mPadding - 5 * mEyeWidth / 2, mWidth / 3 -
                    mEyeWidth, mWidth - mPadding - mEyeWidth / 2, mWidth / 3 + mEyeWidth);
        }

        //@Override
        protected void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            mPaint.SetStyle(Paint.Style.Stroke);
            canvas.DrawArc(rectF, 210, endAngle, false, mPaint);

            mPaint.SetStyle(Paint.Style.Fill);
            if (isJustVisible)
            {
                canvas.DrawCircle(mPadding + mEyeWidth + mEyeWidth / 2, mWidth / 3, mEyeWidth, mPaint);
                canvas.DrawCircle(mWidth - mPadding - mEyeWidth - mEyeWidth / 2, mWidth / 3, mEyeWidth, mPaint);
            }
            if (isSad)
            {
                canvas.DrawArc(leftEyeRectF, 160, -220, false, mPaint);
                canvas.DrawArc(rightEyeRectF, 20, 220, false, mPaint);
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
                isSad = false;
                endAngle = 0f;
                isJustVisible = false;
                mAnimatedValue = 0f;
                valueAnimator.End();
            }
        }

        private ValueAnimator startViewAnim(float startF, float endF, long time)
        {
            valueAnimator = ValueAnimator.OfFloat(startF, endF);
            valueAnimator.SetDuration(time);
            valueAnimator.SetInterpolator(new LinearInterpolator());
            valueAnimator.AddUpdateListener(new XAnimatorUpdate()
            {
                //@Override
                AnimationUpdate = (valueAnimator) =>
           {

               mAnimatedValue = (float)valueAnimator.AnimatedValue;
               if (mAnimatedValue < 0.5)
               {
                   isSad = false;
                   isJustVisible = false;
                   endAngle = 240 * (mAnimatedValue);
                   isJustVisible = true;
               }
               else if (mAnimatedValue > 0.55 && mAnimatedValue < 0.7)
               {
                   endAngle = 120;
                   isSad = false;
                   isJustVisible = true;
               }
               else
               {
                   endAngle = 120;
                   isSad = true;
                   isJustVisible = false;
               }

               PostInvalidate();
           }
            });

            if (!valueAnimator.IsRunning)
            {
                valueAnimator.Start();

            }
            return valueAnimator;
        }
    }
}