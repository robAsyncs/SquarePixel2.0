using System;

namespace SquarePixel.Util;

public class ObservableExceptionHandler: IObserver<Exception>
{
    private Exception _prev;
    
    
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Exception value)
    {
        throw new NotImplementedException();
    }
}