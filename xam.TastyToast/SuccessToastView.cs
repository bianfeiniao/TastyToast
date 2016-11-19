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
using Android.Views.Animations;
using Android.Graphics;
using Android.Util;

namespace xam.TastyToast
{

    /**
     * Created by rahul on 22/7/16.
     */
    public class SuccessToastView :View
    {

        RectF rectF = new RectF();
    ValueAnimator valueAnimator;
    float mAnimatedValue = 0f;
    private Paint mPaint;
    private float mWidth = 0f;
    private float mEyeWidth = 0f;
    private float mPadding = 0f;
    private float endAngle = 0f;
    private bool isSmileLeft = false;
    private bool isSmileRight = false;

        public SuccessToastView(Context context) : base(context) { }
        public SuccessToastView(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public SuccessToastView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

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
        mPaint.AntiAlias=true;
        mPaint.SetStyle(Paint.Style.Stroke);
        mPaint.Color=Color.ParseColor("#5cb85c");
        mPaint.StrokeWidth=dip2px(2);
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
        canvas.DrawArc(rectF, 180, endAngle, false, mPaint);

        mPaint.SetStyle(Paint.Style.Fill);
        if (isSmileLeft)
        {
            canvas.DrawCircle(mPadding + mEyeWidth + mEyeWidth / 2, mWidth / 3, mEyeWidth, mPaint);
        }
        if (isSmileRight)
        {
            canvas.DrawCircle(mWidth - mPadding - mEyeWidth - mEyeWidth / 2, mWidth / 3, mEyeWidth, mPaint);
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
            isSmileLeft = false;
            isSmileRight = false;
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
           AnimationUpdate=(valueAnimator)=>
    {

        mAnimatedValue = (float)valueAnimator.AnimatedValue;
        if (mAnimatedValue < 0.5)
        {
            isSmileLeft = false;
            isSmileRight = false;
            endAngle = -360 * (mAnimatedValue);
        }
        else if (mAnimatedValue > 0.55 && mAnimatedValue < 0.7)
        {
            endAngle = -180;
            isSmileLeft = true;
            isSmileRight = false;
        }
        else
        {
            endAngle = -180;
            isSmileLeft = true;
            isSmileRight = true;
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