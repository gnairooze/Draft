https://github.com/dlemstra/Magick.NET/blob/main/docs/Readme.md

/// <summary>
/// Resize the image to the specified width and height.
/// </summary>
/// <param name="image">The image to resize.</param>
/// <param name="width">The width to resize to.</param>
/// <param name="height">The height to resize to.</param>
/// <returns>The resized image.</returns>
public static Bitmap ResizeImage(Image image, int width, int height)
{
    var destRect = new Rectangle(0, 0, width, height);
    var destImage = new Bitmap(width, height);

    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

    using (var graphics = Graphics.FromImage(destImage))
    {
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        using (var wrapMode = new ImageAttributes())
        {
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel, wrapMode);
        }
    }

    return destImage;
}

public static Bitmap MakeGrayscale3(Bitmap original)
{
   //create a blank bitmap the same size as original
   Bitmap newBitmap = new Bitmap(original.Width, original.Height);

   //get a graphics object from the new image
   using(Graphics g = Graphics.FromImage(newBitmap)){

       //create the grayscale ColorMatrix
       ColorMatrix colorMatrix = new ColorMatrix(
          new float[][] 
          {
             new float[] {.3f, .3f, .3f, 0, 0},
             new float[] {.59f, .59f, .59f, 0, 0},
             new float[] {.11f, .11f, .11f, 0, 0},
             new float[] {0, 0, 0, 1, 0},
             new float[] {0, 0, 0, 0, 1}
          });

       //balck and white color matrix
	   /*float[][] FloatColorMatrix ={ 
         new float[] {1, 0, 0, 0, 0}, 
         new float[] {0, 1, 0, 0, 0}, 
         new float[] {0, 0, 1, 0, 0}, 
         new float[] {0, 0, 0, 1, 0}, 
         new float[] {0, 0, 0, 0, 1} 
       };*/
       
	   //create some image attributes
       using(ImageAttributes attributes = new ImageAttributes()){

           //set the color matrix attribute
           attributes.SetColorMatrix(colorMatrix);

           //draw the original image on the new image
           //using the grayscale color matrix
           g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                       0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
       }
   }
   return newBitmap;
}
