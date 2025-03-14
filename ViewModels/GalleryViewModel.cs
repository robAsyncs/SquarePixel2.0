namespace SquarePixel.ViewModels;

public class GalleryViewModel
{
    public ImageDbViewModel ImageDbViewModel { get; }


    public GalleryViewModel(ImageDbViewModel imageDbViewModel)
    {
        ImageDbViewModel = imageDbViewModel;
    }
}