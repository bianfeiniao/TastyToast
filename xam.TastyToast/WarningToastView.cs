using Android.Content;
using Android.Views;
using Android.Graphics;
using Android.Util;

namespace xam.TastyToast
{
    /**
 * Created by rahul on 25/7/16.
 */
    public class WarningToastView : View
    {

        RectF rectFOne = new RectF();
        RectF rectFTwo = new RectF();
        RectF rectFThree = new RectF();
        private Paint mPaint;
        private float mWidth = 0f;
        private float mHeight = 0f;
        private float mStrokeWidth = 0f;
        private float mPadding = 0f;
        private float mPaddingBottom = 0f;
        private float endAngle = 0f;

        public WarningToastView(Context context) : base(context)
        {

        }


        public WarningToastView(Context context, IAttributeSet attrs) : base(context, attrs)
        {

        }

        public WarningToastView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {

        }

        //@Override
        protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            initPaint();
            initRect();
            mHeight = MeasuredHeight;
            mWidth = MeasuredWidth;
            mPadding = convertDpToPixel(2);
            mPaddingBottom = mPadding * 2;
            mStrokeWidth = convertDpToPixel(2);
        }

        private void initPaint()
        {
            mPaint = new Paint();
            mPaint.AntiAlias = true;
            mPaint.SetStyle(Paint.Style.Stroke);
            mPaint.Color = Color.ParseColor("#f0ad4e");
            mPaint.StrokeWidth = mStrokeWidth;
        }

        private void initRect()
        {
            rectFOne = new RectF(mPadding, 0, mWidth - mPadding, mWidth - mPaddingBottom);
            rectFTwo = new RectF((float)(1.5 * mPadding), convertDpToPixel(6) + mPadding +
                    mHeight / 3, mPadding + convertDpToPixel(9), convertDpToPixel(6) + mPadding + mHeight / 2);
            rectFThree = new RectF(mPadding + convertDpToPixel(9), convertDpToPixel(3) + mPadding +
                    mHeight / 3, mPadding + convertDpToPixel(18), convertDpToPixel(3) + mPadding + mHeight / 2);
        }

        //@Override
        protected void onDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            mPaint.SetStyle(Paint.Style.Stroke);
            canvas.DrawArc(rectFOne, 170, -144, false, mPaint);
            canvas.DrawLine(mWidth - convertDpToPixel(3) - mStrokeWidth, (float)(mPadding +
                            mHeight / 6), mWidth - convertDpToPixel(3) - mStrokeWidth,
                    mHeight - convertDpToPixel(2) - mHeight / 4, mPaint);

            canvas.DrawLine(mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(8), (float)(mPadding +
                            mHeight / 8.5), mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(8),
                    (float)(mHeight - convertDpToPixel(3) - mHeight / 2.5), mPaint);

            canvas.DrawLine(mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(17), (float)(mPadding +
                            mHeight / 10), mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(17),
                    (float)(mHeight - convertDpToPixel(3) - mHeight / 2.5), mPaint);

            canvas.DrawLine(mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(26), (float)(mPadding +
                            mHeight / 10), mWidth - convertDpToPixel(3) - mStrokeWidth - convertDpToPixel(26),
                    (float)(mHeight - convertDpToPixel(2) - mHeight / 2.5), mPaint);

            canvas.DrawArc(rectFTwo, 170, 180, false, mPaint);
            canvas.DrawArc(rectFThree, 175, -150, false, mPaint);
        }

        public float convertDpToPixel(float dp)
        {
            DisplayMetrics metrics = Context.Resources.DisplayMetrics;
            float px = dp * ((float)metrics.DensityDpi / (float)160);
            return px;
        }
    }
}