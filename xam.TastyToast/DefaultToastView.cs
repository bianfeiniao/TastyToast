using System;
using Android.Content;
using Android.Views;
using Android.Animation;
using Android.Graphics;
using Android.Util;
using Android.Views.Animations;

namespace xam.TastyToast
{
    /**
 * Created by rahul on 27/7/16.
 */
    public class DefaultToastView :View
    {

        ValueAnimator valueAnimator;
    float mAnimatedValue = 0f;
    private Paint mPaint, mSpikePaint;
    private float mWidth = 0f;
    private float mPadding = 0f;
    private float mSpikeLength;


        public DefaultToastView(Context context) : base(context) { }
        public DefaultToastView(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public DefaultToastView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

    //@Override
    protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        initPaint();
        mWidth = MeasuredWidth;
        mPadding = dip2px(5);
    }

    private void initPaint()
    {
        mPaint = new Paint();
        mPaint.AntiAlias=true;
        mPaint.SetStyle(Paint.Style.Stroke);
        mPaint.Color=Color.ParseColor("#222222");
        mPaint.StrokeWidth=dip2px(2);

        mSpikePaint = new Paint();
        mSpikePaint.AntiAlias=true;
        mSpikePaint.SetStyle(Paint.Style.Stroke);
        mSpikePaint.Color=Color.ParseColor("#222222");
        mSpikePaint.StrokeWidth=dip2px(4);

        mSpikeLength = dip2px(4);
    }

    //@Override
    protected void onDraw(Canvas canvas)
    {
        base.OnDraw(canvas);
        canvas.Save();
        canvas.DrawCircle(mWidth / 2, mWidth / 2, mWidth / 4, mPaint);

        for (int i = 0; i < 360; i += 40)
        {

            int angle = (int)(mAnimatedValue * 40 + i);
            float initialX = (float)((mWidth / 4) * Math.Cos(angle * Math.PI / 180));
            float initialY = (float)((mWidth / 4) * Math.Sin(angle * Math.PI / 180));
            float finalX = (float)((mWidth / 4 + mSpikeLength) * Math.Cos(angle * Math.PI / 180));
            float finalY = (float)((mWidth / 4 + mSpikeLength) * Math.Sin(angle * Math.PI / 180));
            canvas.DrawLine(mWidth / 2 - initialX, mWidth / 2 - initialY, mWidth / 2 - finalX,
                    mWidth / 2 - finalY, mSpikePaint);
        }
        canvas.Restore();
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

            valueAnimator.End();
            PostInvalidate();
        }
    }

    private ValueAnimator startViewAnim(float startF, float endF, long time)
    {
        valueAnimator = ValueAnimator.OfFloat(startF, endF);
        valueAnimator.SetDuration(time);
        valueAnimator.SetInterpolator(new LinearInterpolator());
        valueAnimator.RepeatCount=ValueAnimator.Infinite;
        valueAnimator.RepeatMode=ValueAnimator.Restart;
            valueAnimator.AddUpdateListener(new XAnimatorUpdate()
            {
                AnimationUpdate = (valueAnimator) =>
           {

               mAnimatedValue = (float)valueAnimator.AnimatedValue;
               PostInvalidate();
           }
            });

        if (!valueAnimator.IsRunning) {
            valueAnimator.Start();

        }
        return valueAnimator;
    }

    public int dip2px(float dpValue)
{
    float scale = Context.Resources.DisplayMetrics.Density;
    return (int)(dpValue * scale);
}
}
}