using Android.Animation;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Views.Animations;

namespace xam.TastyToast
{
    /**
  * Created by Anas Altair on 8/31/2016.
  * Modified by rahul on 16/09/2016
  */
    public class ConfusingToastView :View
    {

        Bitmap eye;
        ValueAnimator valueAnimator;
    float angle = 0f;
    private Paint mPaint;
    private float mWidth = 0f;
    private float mHeight = 0f;

        public ConfusingToastView(Context context) : base(context) { }
        public ConfusingToastView(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public ConfusingToastView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

    //@Override
    protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
    {
        base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        mWidth = MeasuredWidth;
        mHeight =MeasuredHeight;
        initPaint();
        initPath();
    }

    private void initPaint()
    {
        mPaint = new Paint();
        mPaint.AntiAlias=true;
        mPaint.SetStyle(Paint.Style.Stroke);
        mPaint.Color=Color.ParseColor("#FE9D4D");
    }

    private void initPath()
    {
        Path mPath = new Path();
        RectF rectF = new RectF(mWidth / 2f - dip2px(1.5f), mHeight / 2f - dip2px(1.5f)
                , mWidth / 2f + dip2px(1.5f), mHeight / 2f + dip2px(1.5f));
        mPath.AddArc(rectF, 180f, 180f);
        rectF.Set(rectF.Left - dip2px(3), rectF.Top - dip2px(1.5f), rectF.Right, rectF.Bottom + dip2px(1.5f));
        mPath.AddArc(rectF, 0f, 180f);
        rectF.Set(rectF.Left, rectF.Top - dip2px(1.5f), rectF.Right + dip2px(3), rectF.Bottom + dip2px(1.5f));
        mPath.AddArc(rectF, 180f, 180f);
        rectF.Set(rectF.Left - dip2px(3), rectF.Top - dip2px(1.5f), rectF.Right, rectF.Bottom + dip2px(1.5f));
        mPath.AddArc(rectF, 0f, 180f);

        eye = Bitmap.CreateBitmap((int)mWidth, (int)mHeight, Bitmap.Config.Argb8888);
        Canvas c = new Canvas(eye);
        mPaint.StrokeWidth=dip2px(1.7f);
        c.DrawPath(mPath, mPaint);
    }

    //@Override
    protected void onDraw(Canvas canvas)
    {
        base.OnDraw(canvas);

        canvas.Save();
        canvas.Rotate(angle, mWidth / 4f, mHeight * 2f / 5f);
        canvas.DrawBitmap(eye, mWidth / 4f - (eye.Width / 2f), mHeight * 2f / 5f - (eye.Height / 2f), mPaint);
        canvas.Restore();
        canvas.Save();
        canvas.Rotate(angle, mWidth * 3f / 4f, mHeight * 2f / 5f);
        canvas.DrawBitmap(eye, mWidth * 3f / 4f - (eye.Width / 2f), mHeight * 2f / 5f - (eye.Height / 2f), mPaint);
        canvas.Restore();

        mPaint.StrokeWidth=dip2px(2f);
        canvas.DrawLine(mWidth / 4f, mHeight * 3f / 4f, mWidth * 3f / 4f, mHeight * 3f / 4f, mPaint);
    }

    public float dip2px(float dpValue)
    {
            
        float scale = Context.Resources.DisplayMetrics.Density;
        return dpValue * scale;
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

              angle += 4;
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
